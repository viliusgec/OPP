using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Composite;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Interpreter
{
    class MoveExpression : AbstractExpression
    {
        public MoveExpression(
            string expression, StateContext stateContext,
            TextBox moveMenu, PictureBox playerPictureBox,
            Movement movement, object sender, FormsEditor editor,
            Map.MapBase map, Character player,
            MapBuilder mapBuilder, HubConnection connection,
            Room room, Label gameStateLabel, Button button5
        )
        {
            this.expression = expression;
            this.stateContext = stateContext;
            this.moveMenu = moveMenu;
            this.playerPictureBox = playerPictureBox;
            this.movement = movement;
            this.sender = sender;
            this.editor = editor;
            this.map = map;
            this.player = player;
            this.mapBuilder = mapBuilder;
            this.connection = connection;
            this.room = room;
            this.gameStateLabel = gameStateLabel;
            this.button5 = button5;
        }

        public override void Execute()
        {
            if (stateContext.GetState().GetType().Name != "StartState")
                return;

            connection = SingletonConnection.GetInstance().GetConnection();

            int[] temp;
            Point prevLoc = playerPictureBox.Location;

            var convertedExpression = ConvertExpressionToKey(expression);

            temp = movement.SendBoxCoordinates(sender, editor, map, player, mapBuilder, null, convertedExpression);
            if (temp[0] == 0 && temp[1] == 0)
                return;

            //Strategy
            playerPictureBox.Location = new Point(temp[0], temp[1]);
            movement.FlipImage(playerPictureBox, prevLoc, false);


            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            //Cia irgi idet ta tikrinima
            if (convertedExpression == "W" || convertedExpression == "Q" || convertedExpression == "E")
                Thread.Sleep(25);

            movement.fall_down(temp, editor, map, player);
            check_if_win();
        }

        public static string ConvertExpressionToKey(string expression)
        {

            return expression.ToLower() switch
            {
                "moveleft" => "A",
                "moveright" => "D",
                "jump" => "W",
                "jumpupleft" => "Q",
                "jumpupright" => "E",
                "digdown" => "ShiftKey",
                "digleft" => "J",
                "digright" => "K",
                _ => "",
            };
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            var testukas = room.GetName();
            var cqq = connection;
            await connection.InvokeAsync("SendCoordinates",
                x.ToString(), y.ToString(), room.GetName());
        }

        public void check_if_win()
        { 
            if (playerPictureBox.Location.Y >= playerPictureBox.Height * 15)
            {
               // State
                stateContext.TransitionTo(new EndState());
                gameStateLabel.Text = stateContext.ShowText();
                gameStateLabel.Text += "\r\nYou won!";
                button5.Enabled = false;
                button5.Hide();
                stateContext.SendState(room.GetName());
            }
        }
    }
}


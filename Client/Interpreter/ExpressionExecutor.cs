using Client.Composite;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Client.Interpreter
{
    internal class ExpressionExecutor
    {
        private readonly string expression;
        private readonly StateContext stateContext;
        private readonly TextBox moveMenu;
        private readonly PictureBox playerPictureBox;
        private readonly Movement movement;
        private readonly object sender;
        private readonly FormsEditor editor;
        private readonly Map.MapBase map;
        private readonly Character player;
        private readonly MapBuilder mapBuilder;
        private readonly HubConnection connection;
        private readonly Room room;
        private readonly Label gameStateLabel;
        private readonly Button button5;

        public ExpressionExecutor(
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
            this.room = room;
            this.gameStateLabel = gameStateLabel;
            this.button5 = button5;
            ExecuteExpressions();
        }

        public void ExecuteExpressions()
        {
            string[] expressions = SplitIntoExpressionArray();
            List<AbstractExpression> expressionsList = MakeExpressionsIntoObjects(expressions);
            foreach (AbstractExpression ex in expressionsList)
            {
                ex.Execute();
                Thread.Sleep(80);
                //Cia uzdet ta timeri, kad suktusi
            }
        }

        private string[] SplitIntoExpressionArray()
        {
            char[] separators = new[] { ' ', ',', ';', '.', '-' };
            string[] expressions = expression.Split(separators);
            return expressions;
        }

        private List<AbstractExpression> MakeExpressionsIntoObjects(string[] expressions)
        {
            List<AbstractExpression> expressionsList = new();
            foreach (string s in expressions)
            {
                if (s.ToLower().Contains("moveleft") || s.ToLower().Contains("moveright") ||
                    s.ToLower().Contains("jump") || s.ToLower().Contains("jumpupleft") || s.ToLower().Contains("jumpupright") ||
                    s.ToLower().Contains("digdown") || s.ToLower().Contains("digleft") || s.ToLower().Contains("digright"))
                {

                    MoveExpression temp = new(s, stateContext, moveMenu, playerPictureBox, movement, sender, editor, map, player, mapBuilder, connection, room, gameStateLabel, button5);
                    expressionsList.Add(temp);
                }
            }
            return expressionsList;
        }
    }
}


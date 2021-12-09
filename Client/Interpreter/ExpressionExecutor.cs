using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Client.Composite;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Interpreter
{
    class ExpressionExecutor
    {
        private string expression;
        private StateContext stateContext;
        private TextBox moveMenu;
        private PictureBox playerPictureBox;
        private Movement movement;
        private object sender;
        private FormsEditor editor;
        private Map.MapBase map;
        private Character player;
        private MapBuilder mapBuilder;
        private HubConnection connection;
        private Room room;
        private Label gameStateLabel;
        private Button button5;

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
            List<AbstractExpression> expressionsList = makeExpressionsIntoObjects(expressions);
            foreach (var ex in expressionsList)
            {
                ex.Execute();
                Thread.Sleep(80);
                //Cia uzdet ta timeri, kad suktusi
            }
        }

        private string[] SplitIntoExpressionArray()
        {
            char[] separators = new[] { ' ', ',', ';', '.', '-' };
            string[] expressions = this.expression.Split(separators);
            return expressions;
        }

        private List<AbstractExpression> makeExpressionsIntoObjects(string[] expressions)
        {
            List<AbstractExpression> expressionsList = new List<AbstractExpression>();
            foreach (string s in expressions)
            {
                if (s.ToLower().Contains("moveleft") || s.ToLower().Contains("moveright") ||
                    s.ToLower().Contains("jump") || s.ToLower().Contains("jumpupleft") || s.ToLower().Contains("jumpupright") ||
                    s.ToLower().Contains("digdown") || s.ToLower().Contains("digleft") || s.ToLower().Contains("digright"))
                {

                    MoveExpression temp = new MoveExpression(s, stateContext, moveMenu, playerPictureBox, movement, sender, editor, map, player, mapBuilder, connection, room, gameStateLabel, button5);
                    expressionsList.Add(temp);
                }
            }
            return expressionsList;
        }
    }
}


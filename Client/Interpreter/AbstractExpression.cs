using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Composite;
using Client.Decorator;
using Client.Map;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Interpreter
{
    abstract class AbstractExpression
    {
        protected string expression;
        protected StateContext stateContext;
        protected TextBox moveMenu;
        protected PictureBox playerPictureBox;
        protected Movement movement;
        protected object sender;
        protected FormsEditor editor;
        protected Map.MapBase map;
        protected Character player;
        protected MapBuilder mapBuilder;
        protected HubConnection connection;
        protected Room room;

        public abstract void Execute();
    }
}

using Client.Composite;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace Client.Interpreter
{
    internal abstract class AbstractExpression
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
        protected Label gameStateLabel;
        protected Button button5;
        public abstract void Execute();
    }
}

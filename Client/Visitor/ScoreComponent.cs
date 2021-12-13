using System.Windows.Forms;

namespace Client.Visitor
{
    public class ScoreComponent : IComponent
    {
        private Label label;
        private int currency;
        private int item_currency;
        public void Accept(IVisitor visitor, Label label, int currency, int item_currency)
        {
            this.label = label;
            this.currency = currency;
            this.item_currency = item_currency;
            visitor.VisitConcreteComponentA(this);
        }
        public void BuyWithScore()
        {
            currency -= item_currency;
            label.Text = "Score: " + currency;
        }
    }
}

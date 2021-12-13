using System.Windows.Forms;

namespace Client.Visitor
{
    public class MoneyComponent : IComponent
    {
        private Label label;
        private int currency;
        private int item_currency;
        public void Accept(IVisitor visitor, Label label, int currency, int item_currency)
        {
            this.label = label;
            this.currency = currency;
            this.item_currency = item_currency;
            visitor.VisitConcreteComponentB(this);
        }
        public void BuyWithMoney()
        {
            currency -= item_currency;
            label.Text = "Score: " + currency;
        }
    }
}

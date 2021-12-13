namespace Client.Visitor
{
    public interface IVisitor
    {
        public void VisitConcreteComponentA(ScoreComponent element);
        public void VisitConcreteComponentB(MoneyComponent element);
    }
}

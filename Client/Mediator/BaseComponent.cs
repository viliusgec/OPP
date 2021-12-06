namespace Client.Mediator
{
    public class BaseComponent
    {
        protected Mediator mediator;

        public BaseComponent(Mediator mediator = null)
        {
            this.mediator = mediator;
        }
        public void SetMediator(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }
}

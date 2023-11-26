namespace Model
{
    public enum ActionInvokeOptions
    {
        Whenever,
        After
    }

    public class InvokeAction : AbstractAction
    {
        public string toInvoke;
        public ActionInvokeOptions options;
        public AbstractAction toCast;

        public InvokeAction(AbstractMercenary owner, ActionInvokeOptions options, AbstractAction toCast) : base(owner)
        {
            this.toCast = toCast;
            this.options = options;
            toInvoke = options.ToString() + toCast.GetType().Name;
            toInvoke = toInvoke.Replace("Action", "");
        }

        public override void Use()
        {
            CardManager.InvokeCards(toInvoke, toCast);

            switch (options)
            {
                case ActionInvokeOptions.Whenever:
                    toCast.Whenever();
                    break;
                case ActionInvokeOptions.After:
                    toCast.After();
                    break;
            }
        }
    }
}

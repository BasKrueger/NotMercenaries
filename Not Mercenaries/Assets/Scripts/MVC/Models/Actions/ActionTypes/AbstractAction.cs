namespace Model
{
    public abstract class AbstractAction
    {
        public AbstractMercenary owner;

        public AbstractAction(AbstractMercenary owner)
        {
            this.owner = owner;
        }

        public virtual void Whenever() { }
        public abstract void Use();
        public virtual void After() { }
    }
}
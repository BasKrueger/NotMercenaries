namespace Model
{
    internal class AddBuffAction : AbstractAction
    {
        public AbstractBuff toAdd;
        public AbstractMercenary target;

        public AddBuffAction(AbstractMercenary owner, AbstractBuff toAdd, AbstractMercenary target) : base(owner)
        {
            this.toAdd = toAdd;
            this.target = target;
        }

        public override void Use()
        {
            target.AddBuff(toAdd);
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercBuffed);
        }
    }
}

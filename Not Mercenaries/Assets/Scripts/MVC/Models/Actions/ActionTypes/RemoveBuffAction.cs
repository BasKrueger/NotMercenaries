namespace Model
{
    public class RemoveBuffAction : AbstractAction
    {
        public AbstractMercenary target;
        public AbstractBuff toRemove;

        public RemoveBuffAction(AbstractMercenary owner, AbstractBuff toRemove, AbstractMercenary target) : base(owner)
        {
            this.target = target;
            this.toRemove = toRemove;
        }

        public override void Use()
        {
            target?.RemoveBuff(toRemove);
            Game.UpdateGameState(DTO.GameStateCause.MercBuffRemoved);
        }
    }
}

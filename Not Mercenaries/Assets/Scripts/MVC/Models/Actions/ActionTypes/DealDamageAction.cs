namespace Model
{
    public class DealDamageAction : AbstractAction
    {
        public int damage;
        public AbstractMercenary target;

        public DealDamageAction(AbstractMercenary owner, AbstractMercenary target, int damage) : base(owner)
        {
            this.damage = damage;
            this.target = target;
        }

        public override void Use()
        {
            target.TakeDamage(this);
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercDamaged);
        }
    }
}

using DTO;

namespace Model
{
    public class HealDamageAction : AbstractAction
    {
        public AbstractMercenary target;
        public int healing;

        public HealDamageAction(AbstractMercenary owner, AbstractMercenary target, int amount) : base(owner)
        {
            this.target = target;
            this.healing = amount;
        }

        public override void Use()
        {
            target?.HealDamage(this);
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.MercHealed);
        }
    }
}

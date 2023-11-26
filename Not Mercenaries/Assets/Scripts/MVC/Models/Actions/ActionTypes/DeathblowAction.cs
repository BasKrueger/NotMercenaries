using DTO;

namespace Model
{
    public class DeathblowAction : AbstractAction
    {
        private AbstractAbility target;
        public DeathblowAction(AbstractMercenary owner, AbstractAbility target) : base(owner)
        {
            this.target = target;
        }

        public override void Use()
        {
            target.Deathblow(this);
        }
    }
}

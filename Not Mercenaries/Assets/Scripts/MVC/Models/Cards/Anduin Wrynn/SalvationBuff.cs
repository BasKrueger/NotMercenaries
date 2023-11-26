namespace Model
{
    public class SalvationBuff : AbstractBuff
    {
        public SalvationBuff(int id) : base
            (
                id: id,

                name: "Salvation",
                description: "takes <m1> less damage this turn",
                magicNumber1: 10
            )
        { }

        public override void WheneverDealDamage(DealDamageAction cause)
        {
            if(cause.target == base.owner)
            {
                cause.damage -= 10;
            }
            base.WheneverDealDamage(cause);
        }

        public override void TurnEnd(TurnEndAction cause)
        {
            ActionManager.AddNext(new RemoveBuffAction(cause.owner, this, base.owner));
            base.TurnEnd(cause);
        }
    }
}

namespace Model
{
    public class XuenAttackBuff : AbstractBuff
    {
        public XuenAttackBuff(int id) : base
            (
                id: id,

                name: "Xuens Might",
                description: "has <m1> more attack. Lasts <m2> turns",
                magicNumber1: 1,
                magicNumber2: 2
            )
        { }

        protected override void Update()
        {
            base.owner.attack.Buff(magicNumber1);
            base.Update();
        }

        public override void TurnEnd(TurnEndAction cause)
        {
            magicNumber2 -= 1;
            if (magicNumber2 <= 0)
            {
                ActionManager.AddNext(new RemoveBuffAction(cause.owner, this, this.owner));
            }
            base.TurnEnd(cause);
        }
    }
}

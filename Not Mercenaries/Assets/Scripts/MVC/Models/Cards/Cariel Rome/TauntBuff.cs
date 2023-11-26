namespace Model
{
    public class TauntBuff : AbstractBuff
    {
        public TauntBuff(int id) : base
            (
                id: id,

                name: "Taunt",
                description: "has Taunt for <m1> turns",
                magicNumber1: 3
            )
        {
        }

        public override void TurnEnd(TurnEndAction cause)
        {
            magicNumber1 -= 1;
            if (magicNumber1 <= 0)
            {
                ActionManager.AddNext(new RemoveBuffAction(this.owner, this, this.owner));
            }
        }

        protected override void Update()
        {
            base.Update();
            owner.isTaunting = true;
        }
    }
}

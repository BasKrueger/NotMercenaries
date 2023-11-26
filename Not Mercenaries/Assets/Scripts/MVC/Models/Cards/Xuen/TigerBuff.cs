namespace Model
{
    public class TigerBuff : AbstractBuff
    {
        public TigerBuff(int id) : base
            (
                id: id,

                name: "Xuens Lightning",
                description: "has <m1> more attack.",
                magicNumber1: 5
            )
        { }

        protected override void Update()
        {
            base.owner.attack.Buff(magicNumber1);
            base.Update();
        }
    }
}

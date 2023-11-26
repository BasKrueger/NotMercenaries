namespace Model
{
    public class SealOfLightBuff : AbstractBuff
    {
        public SealOfLightBuff(int id) : base
            (
                id: id,

                name: "Seal of Light",
                description: "+<m1> attack.",
                magicNumber1: 6
            )
        { }

        protected override void Update()
        {
            base.Update();
            owner.attack.Buff(magicNumber1);
        }
    }
}

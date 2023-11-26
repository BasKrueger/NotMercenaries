namespace Model
{
    public class LightBuff : AbstractBuff
    {
        public LightBuff(int id) : base
            (
                id: id,

                name: "Light Buff",
                description: "has +<m1>/+<m2>",
                magicNumber1: 6,
                magicNumber2: 6
            )
        {
        }

        protected override void Update()
        {
            base.Update();
            owner.health.Buff(magicNumber1);
            owner.attack.Buff(magicNumber2);
        }
    }
}

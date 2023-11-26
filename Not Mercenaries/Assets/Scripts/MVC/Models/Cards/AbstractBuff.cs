namespace Model
{
    public class AbstractBuff : AbstractCard
    {
        public AbstractMercenary owner
        {
            get
            {
                return Game.FindOwnerOf(this);
            }
        }


        public AbstractBuff(int id, string name, string description, int damage = 0, int healing = 0, int magicNumber1 = 0, int magicNumber2 = 0, int magicNumber3 = 0, int magicNumber4 = 0, int magicNumber5 = 0) : 
            base(id, 0, name, description, damage, healing, magicNumber1, magicNumber2, magicNumber3, magicNumber4, magicNumber5)
        {
        }

        public sealed override void NormalUpdate()
        {
            base.NormalUpdate();
        }

        #region Create DTO
        public DTO.BuffState GetState()
        {
            return new DTO.BuffState()
            {
                Description = GetFormatedDescription()
            };
        }
        #endregion
    }
}

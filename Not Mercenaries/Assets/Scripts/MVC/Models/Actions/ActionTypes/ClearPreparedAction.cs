using DTO;

namespace Model
{
    public class ClearPreparedAction : AbstractAction
    {
        public ClearPreparedAction(AbstractMercenary owner) : base(owner)
        {
        }

        public override void Use()
        {
            owner.ClearPreparedPlay();
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.MercPreparedAction);
        }
    }
}

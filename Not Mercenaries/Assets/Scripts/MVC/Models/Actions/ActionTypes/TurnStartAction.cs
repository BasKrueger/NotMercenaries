using DTO;

namespace Model
{
    public class TurnStartAction : AbstractAction
    {
        public TurnStartAction(AbstractMercenary owner) : base(owner)
        {
        }

        public override void Use()
        {
            CardManager.InvokeCards("TurnStart", this);
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.TurnStarted);
        }
    }
}

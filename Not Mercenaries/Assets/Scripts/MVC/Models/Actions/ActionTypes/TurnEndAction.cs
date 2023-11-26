using DTO;

namespace Model
{
    public class TurnEndAction : AbstractAction
    {
        public TurnEndAction(AbstractMercenary owner) : base(owner)
        {
        }

        public override void Use()
        {
            CardManager.InvokeCards("TurnEnd", this);
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.TurnEnded);
        }
    }
}

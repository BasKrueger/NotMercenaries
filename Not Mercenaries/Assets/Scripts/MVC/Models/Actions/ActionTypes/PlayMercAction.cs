namespace Model
{
    public class PlayMercAction : AbstractAction
    {
        public AbstractMercenary toPlay;
        public Player player;

        public PlayMercAction(AbstractMercenary owner, Player player, AbstractMercenary toPlay) : base(owner)
        {
            this.player = player;
            this.toPlay = toPlay;
        }

        public override void Whenever()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercPlayStart);
        }

        public override void Use()
        {
            toPlay.OnPlay(this);
            ActionManager.AddNext(new SummonMercAction(owner, player, toPlay));
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercPlayEnd);
        }
    }
}

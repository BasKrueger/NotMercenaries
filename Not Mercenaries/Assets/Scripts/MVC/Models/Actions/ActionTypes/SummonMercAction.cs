namespace Model
{
    public class SummonMercAction : AbstractAction
    {
        public AbstractMercenary toPlay;
        public Player player;

        public SummonMercAction(AbstractMercenary owner, Player player, AbstractMercenary mercToPlay) : base(owner)
        {
            this.player = player;
            this.toPlay = mercToPlay;
        }

        public override void Whenever()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercSummonStart);
        }

        public override void Use()
        {
            player.SummonMercenary(this);
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.MercSummonEnd);
        }

        public SummonMercAction(PlayMercAction original) : base(original.owner)
        {
            this.toPlay = original.toPlay;
        }
    }
}
namespace Model
{
    public class PlayAbilityAction : AbstractAction
    {
        public AbstractAbility cardToPlay { get; private set; }
        public AbstractMercenary target { get; private set; }

        public PlayAbilityAction(AbstractMercenary owner, AbstractAbility cardToPlay, AbstractMercenary target) : base(owner)
        {
            this.target = target;
            this.cardToPlay = cardToPlay;
        }

        public override void Whenever()
        {
            Game.UpdateGameState(DTO.GameStateCause.CardPlaying, cardToPlay);
        }

        public override void Use()
        {
            if (target != null && target.boardPosition == BoardPosition.GraveYard)
            {
                target = Game.GetAlternativeTarget(target);
                if (target == null) return;
            }

            cardToPlay.OnPlay(this);
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.CardPlayed, cardToPlay);
        }

        #region Generate DTO
        public DTO.PreparedPlayState GetState()
        {
            return new DTO.PreparedPlayState()
            {
                target = target?.GetState(false),
                card = cardToPlay?.GetState(),
            };
        }
        #endregion
    }
}

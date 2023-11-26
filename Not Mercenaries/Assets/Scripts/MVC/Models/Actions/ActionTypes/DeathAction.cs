using DTO;

namespace Model
{
    public enum Deathkind
    {
        natural,
        forced
    }

    public class DeathAction : AbstractAction
    {
        private AbstractMercenary target;
        private Deathkind kind;

        public DeathAction(AbstractMercenary owner, AbstractMercenary target, Deathkind kind = Deathkind.forced) : base(owner)
        {
            this.target = target;
        }

        public override void Use()
        {
            ActionManager.CancelAllPlaysFrom(target);
            var deathPlay = ActionManager.history.GetLast<PlayAbilityAction>(Searchmode.turns, 1);

            ActionManager.AddNext(new DeathblowAction(deathPlay.owner, deathPlay.cardToPlay));

            if (kind == Deathkind.natural)
            {
                if (target.health > 0) return;
            }

            target.Die(this);
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.MercDied);
        }
    }
}

namespace DTO
{
    public class GameState
    {
        public GameStateCause cause;
        public Model.GamePhase currentPhase;

        public PlayerState[] players;
        public AbilityState lastAbility;

        public int winningPlayer = -1;

        public MercenaryState GetMercState(int id)
        {
            foreach(var player in players)
            {
                var merc = player.GetMercState(id);
                if (merc != null) return merc;
            }

            return null;
        }

        public AbilityState GetAbilityState(int id)
        {
            foreach (var player in players)
            {
                var ability = player.GetAbilityState(id);
                if (ability != null) return ability;
            }

            return null;
        }
    }

    public enum GameStateCause
    {
        MercPlayEnd,
        TurnEnded,
        MercSummonEnd,
        GameStarted,
        CardPlayed,
        TurnStarted,
        CombatEnded,
        MercPreparedAction,
        MercUpdated,
        CombatStarted,
        MercDamaged,
        CardPlaying,
        MercDied,
        DeathBlowTriggered,
        MercHealed,
        MercAttackEnd,
        MercAttackStart,
        MercPlayStart,
        MercSummonStart,
        MercBuffed,
        MercBuffRemoved,
        GameOver
    }
}

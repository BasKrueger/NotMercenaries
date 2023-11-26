using System.Collections.Generic;

namespace Model
{
    internal class CombatAction : AbstractAction
    {
        private List<PlayAbilityAction> sortedPlays;

        public CombatAction(Player A, Player B) : base(null)
        {
            sortedPlays = new List<PlayAbilityAction>();

            var allPlays = new List<PlayAbilityAction>();
            allPlays.AddRange(A.GetPreparedPlays());
            allPlays.AddRange(B.GetPreparedPlays());

            while (allPlays.Count > 0)
            {
                var toRemove = new List<PlayAbilityAction>();
                PlayAbilityAction fastestAction = null;
                foreach (var play in allPlays)
                {
                    if (play.cardToPlay == null)
                    {
                        toRemove.Add(play);
                        continue;
                    }

                    if (fastestAction == null)
                    {
                        fastestAction = play;
                        continue;
                    }

                    if(play.cardToPlay.cost < fastestAction.cardToPlay.cost)
                    {
                        fastestAction = play;
                    }
                }

                sortedPlays.Add(fastestAction);
                allPlays.Remove(fastestAction);
                foreach(var action in toRemove) { allPlays.Remove(action); }
            }

            sortedPlays.Reverse();
        }

        public override void Whenever()
        {
            Game.UpdateGameState(DTO.GameStateCause.CombatStarted);
            ActionManager.AddNext(new TurnStartAction(owner));
        }

        public override void Use()
        {
            foreach(var action in sortedPlays)
            {
                ActionManager.AddNext(action);
                ActionManager.AddNext(new ClearPreparedAction(action.owner));
            }
        }

        public override void After()
        {
            Game.UpdateGameState(DTO.GameStateCause.CombatEnded);
            ActionManager.AddNext(new TurnEndAction(owner));
        }
    }
}

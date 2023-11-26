using System.Collections.Generic;

namespace Model
{
    public enum Searchmode
    {
        actions,
        turns
    }

    public class ActionHistory
    {
        public List<AbstractAction> actionHistory { get; private set; } = new List<AbstractAction>();  
        public static ActionHistory operator +(ActionHistory a, AbstractAction b)
        {
            if(a.actionHistory.Contains(b)) return a;

            a.actionHistory.Add(b);
            return a;
        }

        public bool Contains<T>(Searchmode mode, int range = int.MaxValue)
        {
            return Search<T>(mode, range).Count > 0;
        }

        public T GetLast<T>(Searchmode mode, int range = int.MaxValue)
        {
            var result = Search<T>(mode, range);
            if(result.Count > 0)
            {
                return result[0];
            }
            return default(T);
        }

        public List<T> Search<T>(Searchmode mode, int range = int.MaxValue)
        {
            var result = new List<T>();

            if(mode == Searchmode.actions)
            {
                for (int i = 0; i < actionHistory.Count && i < range; i++)
                {
                    object action = actionHistory[actionHistory.Count - 1 - i];
                    if (action is T)
                    {
                        result.Add((T)action);
                    }
                }
            }
            else if(mode == Searchmode.turns)
            {
                int turnCounter = 0;
                for (int i = 0; i < actionHistory.Count && turnCounter < range; i++)
                {
                    object action = actionHistory[actionHistory.Count - 1 - i];
                    if (action is T)
                    {
                        result.Add((T)action);
                    }
                    if(action is TurnEndAction)
                    {
                        turnCounter++;
                    }
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Model
{
    public static class ActionManager
    {
        public static ActionHistory history = new ActionHistory();
        private static List<AbstractAction> queuedActions = new List<AbstractAction>();

        public static void PlayQueuedActions()
        {
            while (queuedActions.Count > 0)
            {
                var action = queuedActions[0];
                Use(action);

                queuedActions.Remove(action);
            }
        }

        public static void AddNext(AbstractAction action)
        {
            var whenever = new InvokeAction(action.owner, ActionInvokeOptions.Whenever, action);
            var after = new InvokeAction(action.owner, ActionInvokeOptions.After, action);

            queuedActions.Insert(0, after);
            queuedActions.Insert(0, action);
            queuedActions.Insert(0, whenever);
        }

        public static void AddLast(AbstractAction action)
        {
            var whenever = new InvokeAction(action.owner, ActionInvokeOptions.Whenever, action);
            var after = new InvokeAction(action.owner, ActionInvokeOptions.After, action);

            queuedActions.Add(whenever);
            queuedActions.Add(action);
            queuedActions.Add(after);
        }

        public static void UseNow(AbstractAction action)
        {
            var whenever = new InvokeAction(action.owner, ActionInvokeOptions.Whenever, action);
            var after = new InvokeAction(action.owner, ActionInvokeOptions.After, action);

            Use(whenever);
            Use(action);
            Use(after);
        }

        public static void CancelNextActionOfType(AbstractAction action)
        {
            for (int i = 0; i < queuedActions.Count; i++)
            {
                AbstractAction inqueue = queuedActions[i];
                if (inqueue.GetType() == action.GetType())
                {
                    queuedActions.Remove(inqueue);
                    return;
                }
            }
        }

        public static void CancelAllPlaysFrom(AbstractMercenary target)
        {
            List<AbstractAction> toRemove = new List<AbstractAction>();
            foreach (var action in queuedActions)
            {
                if(action is PlayAbilityAction && action.owner == target)
                {
                    toRemove.Add(action);
                }

                if(action is InvokeAction)
                {
                    if(((InvokeAction)action).toCast is PlayAbilityAction)
                    {
                        if(((PlayAbilityAction)((InvokeAction)action).toCast).owner == target)
                        {
                            toRemove.Add(action);
                        }
                    }
                }
            }

            foreach(var action in toRemove)
            {
                queuedActions.Remove(action);
            }
        }

        public static void CancelAllActions() => queuedActions = new List<AbstractAction>();

        private static void Use(AbstractAction action)
        {
            action.Use();
            history += action;
            CardManager.UpdateMercs();
        }

        public static void Reset()
        {
            history = new ActionHistory();
            queuedActions = new List<AbstractAction>();
        }
    }
}

using System.Collections.Generic;

namespace Model
{
    internal class MultiAction : AbstractAction
    {
        private List<AbstractAction> actions;
        public MultiAction(AbstractMercenary owner, List<AbstractAction> actions) : base(owner)
        {
            this.actions = new List<AbstractAction>();
            foreach(var action in actions)
            {
                this.actions.Add(action);
            }
        }

        public override void Use()
        {
            foreach(var action in actions)
            {
                ActionManager.UseNow(action);
            }
        }
    }
}

using DTO;
using System.Collections.Generic;

namespace Model
{
    public class AttackAction : AbstractAction
    {
        public AbstractMercenary target;

        public AttackAction(AbstractMercenary owner, AbstractMercenary target) : base(owner)
        {
            this.target = target;
        }

        public override void Whenever()
        {
            Game.UpdateGameState(GameStateCause.MercAttackStart);
        }

        public override void Use()
        {
            var enemyTaunts = new List<AbstractMercenary>();
            foreach(var merc in owner.player.enemy.inPlay)
            {
                if(merc.isTaunting && merc != target)
                {
                    enemyTaunts.Add(merc);
                }
            }

            if(enemyTaunts.Count > 0)
            {
                this.target = enemyTaunts[UnityEngine.Random.Range(0, enemyTaunts.Count)];
            }


            List<AbstractAction> actions = new List<AbstractAction>();

            actions.Add(new DealDamageAction(this.owner, this.target, this.owner.attack));
            actions.Add(new DealDamageAction(this.target, this.owner, this.target.attack));

            ActionManager.AddNext(new MultiAction(this.owner, actions));
        }

        public override void After()
        {
            Game.UpdateGameState(GameStateCause.MercAttackEnd);
        }
    }
}

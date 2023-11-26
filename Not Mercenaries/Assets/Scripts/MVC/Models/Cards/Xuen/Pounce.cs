using System.Collections.Generic;

namespace Model
{
    public class Pounce : AbstractAbility
    {
        public Pounce(int id) : base
            (
                id: id,

                name: "Pounce",
                description: "Steal <m1> attack from allies for 2 turns and attack an enemy.",
                speed: 4,
                targets: abilityTargets.single | abilityTargets.enemy,
                magicNumber1: 5
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach(var merc in cause.owner.player.inPlay)
            {
                if (merc == cause.owner) continue;

                var difference = new Stat(cause.target.attack) - new Stat(cause.owner.attack);
                for (int i = 0;i < 5 && i < difference;i++)
                {
                    actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Xuens Fear"), merc));
                    actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Xuens Might"), cause.owner));
                }
            }

            actions.Add(new AttackAction(cause.owner, cause.target));

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
            base.OnPlay(cause);
        }
    }
}

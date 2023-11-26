using System;
using System.Collections.Generic;

namespace Model
{
    public class EqualizingStrike : AbstractAbility
    {
        public EqualizingStrike(int id) : base
            (
                id: id,

                name: "Equalizing Strike",
                description: "Choose an enemy. Set this mercs attack to <m1> higher than it for 2 turns and attack it.",
                speed: 5,
                targets: abilityTargets.single | abilityTargets.enemy,
                magicNumber1: 5
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            var difference = new Stat(cause.target.attack) - new Stat(cause.owner.attack);
            difference += magicNumber1;

            if(difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Xuens Might"), cause.owner));
                }
            }
            else if(difference < 0)
            {
                for (int i = 0; i < MathF.Abs(difference); i++)
                {
                    actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Xuens Fear"), cause.owner));
                }
            }
           

            actions.Add(new AttackAction(cause.owner, cause.target));

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
            base.OnPlay(cause);
        }
    }
}

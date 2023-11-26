using System.Collections.Generic;

namespace Model
{
    public class Salvation : AbstractAbility
    {
        public Salvation(int id) : base
            (
                id: id,

                name: "Holy Word: Salvation",
                description: "Your allies take <m1> less damage this turn.",
                speed: 3,
                targets: abilityTargets.all | abilityTargets.ally,
                school: SpellSchool.holy,
                magicNumber1: 10
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach(var merc in cause.owner.player.inPlay)
            {
                actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Salvation"), merc));
            }

            ActionManager.AddNext(new MultiAction(cause.owner, actions));

            base.OnPlay(cause);
        }
    }
}

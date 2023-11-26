using System.Collections.Generic;

namespace Model
{
    public class SealOfLight : AbstractAbility
    {
        public SealOfLight(int id) : base
            (
                id: id,

                name : "Seal of Light",
                description: "Restore <h> health to a friendly merc and give it +<m1> attack.",
                speed: 4,
                targets: abilityTargets.single | abilityTargets.ally,

                healing: 15,
                magicNumber1: 6
            )
        { }


        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            actions.Add(new HealDamageAction(cause.owner, cause.target, healing));
            actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Seal of Light"), cause.target));

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
        }
    }
}

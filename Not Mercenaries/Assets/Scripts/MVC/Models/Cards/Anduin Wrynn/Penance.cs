using System.Collections.Generic;

namespace Model
{
    public class Penance : AbstractAbility
    {
        public Penance(int id) : base
            (
                id: id,

                name: "Penance",
                description: "Deal <d> damage to an enemy and restore <h> health to a random ally.",
                speed: 4,
                targets: abilityTargets.single | abilityTargets.enemy,
                school: SpellSchool.holy,
                damage: 12,
                healing: 16
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();
            actions.Add(new DealDamageAction(cause.owner, cause.target, base.damage));

            var target = Game.GetRandomValidTarget(abilityTargets.ally | abilityTargets.single | abilityTargets.damaged, cause.owner);
            actions.Add(new HealDamageAction(cause.owner, target, base.healing));

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
            base.OnPlay(cause);
        }
    }
}

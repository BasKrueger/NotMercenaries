using System.Collections.Generic;

namespace Model
{
    public class FlameStrike : AbstractAbility
    {
        public FlameStrike(int id) : base
            (
                id: id,

                name: "Flamestrike",
                description: "Deal <d> damage to all enemies.",
                speed: 7,
                targets: abilityTargets.all | abilityTargets.enemy,
                school: SpellSchool.fire,
                damage: 14
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();
            foreach(var target in cause.owner.player.enemy.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, target, base.damage));
            }
            ActionManager.AddNext(new MultiAction(owner, actions));
            base.OnPlay(cause);
        }
    }
}

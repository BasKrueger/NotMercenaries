using System.Collections.Generic;

namespace Model
{
    public class VindicatorsFury : AbstractAbility
    {
        public VindicatorsFury(int id) : base
            (
                id: id,

                name: "Vindicator's Fury",
                description: "Attack an enemy. Deathblow: deal <d> damage to all enemies",
                speed: 5,
                targets: abilityTargets.single | abilityTargets.enemy,
                school: SpellSchool.holy,
                damage: 15
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            ActionManager.AddNext(new AttackAction(cause.owner, cause.target));
            base.OnPlay(cause);
        }

        public override void Deathblow(DeathblowAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach (var merc in cause.owner.player.enemy.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, merc, base.damage));
            }

            ActionManager.AddNext(new MultiAction(cause.owner, actions));

            base.Deathblow(cause);
        }
    }
}

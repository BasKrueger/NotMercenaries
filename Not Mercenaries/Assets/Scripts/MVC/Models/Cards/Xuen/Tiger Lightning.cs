using System.Collections.Generic;

namespace Model
{
    public class TigerLightning : AbstractAbility
    {
        public TigerLightning(int id) : base
            (
                id: id,

                name: "Tiger Lightning",
                description: "Deal this mercs attack damage to a random enemy. Deathblow: Gain +<m1> attack and repeat this.",
                speed: 6,
                targets: abilityTargets.all | abilityTargets.enemy,
                school: SpellSchool.nature,
                magicNumber1: 5
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            ActionManager.AddNext(new DealDamageAction(cause.owner, Game.GetRandomValidTarget(base.targets, cause.owner), cause.owner.attack));

            base.OnPlay(cause);
        }

        public override void Deathblow(DeathblowAction cause)
        {
            var actions = new List<AbstractAction>();

            actions.Add(new AddBuffAction(cause.owner, CardManager.CreateBuff("Xuens Lightning"), cause.owner));
            actions.Add(new PlayAbilityAction(cause.owner, this, Game.GetRandomValidTarget(this.targets, cause.owner)));

            base.Deathblow(cause);
        }
    }
}

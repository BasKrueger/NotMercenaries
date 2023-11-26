using System.Collections.Generic;

namespace Model
{
    public class WrathOfTheLightbound : AbstractAbility
    {
        public WrathOfTheLightbound(int id) : base
            (
                id: id,

                name : "Wrath of the Lightbound",
                description: "deal <d> damage to all enemies. Then deal <m1> damage to all characters",
                speed: 8,
                targets: abilityTargets.all | abilityTargets.enemy | abilityTargets.ally,
                school: SpellSchool.holy,

                damage: 20,
                magicNumber1: 5
            )
        { }


        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach(var merc in cause.owner.player.enemy.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, merc, base.damage));
            }

            foreach (var merc in cause.owner.player.enemy.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, merc, base.magicNumber1));
            }
            foreach (var merc in cause.owner.player.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, merc, base.magicNumber1));
            }

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
        }
    }
}

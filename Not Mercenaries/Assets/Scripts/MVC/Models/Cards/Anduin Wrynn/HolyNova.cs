using System.Collections.Generic;

namespace Model
{
    public class HolyNova : AbstractAbility
    {
        public HolyNova(int id) : base
            (
                id: id,

                name: "Holy Nova",
                description: "Deal <d> damage to all enemies and restore <h> to all allies",
                speed: 5,
                targets: abilityTargets.all | abilityTargets.enemy | abilityTargets.ally,
                school: SpellSchool.holy,
                damage: 8,
                healing: 18
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach(var merc in cause.owner.player.enemy.inPlay)
            {
                actions.Add(new DealDamageAction(cause.owner, merc, base.damage));
            }

            foreach (var merc in cause.owner.player.inPlay)
            {
                actions.Add(new HealDamageAction(cause.owner, merc, base.healing));
            }

            ActionManager.AddNext(new MultiAction(cause.owner, actions));
            base.OnPlay(cause);
        }
    }
}

using System.Linq;

namespace Model
{
    public class FireballStorm : AbstractAbility
    {
        public FireballStorm(int id) : base
            (
                id: id,

                name: "Fireballstorm",
                description: "Deal <d> damage to a random enemy. Repeat for each fire ability you've cast this turn.",
                speed: 7,
                targets: abilityTargets.all | abilityTargets.enemy,
                school: SpellSchool.fire,
                damage: 14
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            for(int i =0;i < GetComboCount(SpellSchool.fire) + 1;i++)
            {
                var target = Game.GetRandomValidTarget(abilityTargets.single | abilityTargets.enemy, owner);
                ActionManager.AddNext(new DealDamageAction(cause.owner, target, base.damage));
            }
            base.OnPlay(cause);
        }
    }
}

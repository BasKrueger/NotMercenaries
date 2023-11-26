using System.Linq;

namespace Model
{
    public class DragonQueensGambit : AbstractAbility
    {
        public DragonQueensGambit(int id) : base
            (
                id: id,

                name: "Dragonqueen's Gambit",
                description: "Deal damage to an enemy equal to its attack.",
                speed: 2,
                targets: abilityTargets.single | abilityTargets.enemy,
                school: SpellSchool.fire
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            ActionManager.AddNext(new DealDamageAction(cause.owner, cause.target, cause.target.attack));
            base.OnPlay(cause);
        }
    }
}

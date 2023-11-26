namespace Model
{
    public class Fireball : AbstractAbility
    {
        public Fireball(int id) : base
            (
                id: id,

                name: "Fireball",
                description: "Deal <d> damage.",
                speed: 4,
                targets: abilityTargets.single | abilityTargets.enemy | abilityTargets.ally,
                school: SpellSchool.fire,
                damage: 16
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            ActionManager.AddNext(new DealDamageAction(cause.owner, cause.target, base.damage));
            base.OnPlay(cause);
        }
    }
}

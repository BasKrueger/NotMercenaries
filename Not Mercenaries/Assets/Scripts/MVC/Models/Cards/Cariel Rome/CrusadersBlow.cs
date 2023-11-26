namespace Model
{
    public class CrusadersBlow : AbstractAbility
    {
        public CrusadersBlow(int id) : base
            (
                id: id,

                name: "Crusaders Blow",
                description: "Attack an enemy. Deathblow: Restore <h> health to this merc.",
                speed: 6,
                targets: abilityTargets.single | abilityTargets.enemy,
                school: SpellSchool.holy,
                healing: 60
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
            ActionManager.AddNext(new HealDamageAction(cause.owner, cause.owner, base.healing));
            base.Deathblow(cause);
        }
    }
}

namespace Model
{
    public class DragonBreath : AbstractAbility
    {
        public DragonBreath(int id) : base
            (
                id: id,

                name: "Dragon's Breath",
                description: "Deal <d> damage to an enemy or restore <h> to an ally",
                speed: 6,
                targets: abilityTargets.single | abilityTargets.enemy | abilityTargets.ally,
                school: SpellSchool.fire,
                damage: 14,
                healing: 20
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            if(cause.target.player == cause.owner.player)
            {
                ActionManager.AddNext(new HealDamageAction(cause.owner, cause.target, base.healing));
            }
            else
            {
                ActionManager.AddNext(new DealDamageAction(cause.owner, cause.target, base.damage));
            }

            base.OnPlay(cause);
        }
    }
}

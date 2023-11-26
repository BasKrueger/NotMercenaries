using System.Collections.Generic;

namespace Model
{
    public class FlameBuffet : AbstractAbility
    {
        public FlameBuffet(int id) : base
            (
                id: id,

                name: "Flame Buffet",
                description: "Deal <d> damage to a random enemy or restore <h> health to a random target. Repeat <m1> more times.",
                speed: 6,
                targets: abilityTargets.all | abilityTargets.enemy | abilityTargets.ally,
                school: SpellSchool.fire,

                damage: 5,
                healing: 10,
                magicNumber1: 5
            )
        {
        }

        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            for(int i = 0;i < magicNumber1 + 1;i++)
            {
                if(UnityEngine.Random.Range(0f,100f) > 50f)
                {
                    actions.Add(new DealDamageAction(cause.owner, cause.owner.player.enemy.inPlay.Random(), base.damage));
                }
                else
                {
                    actions.Add(new HealDamageAction(cause.owner, cause.owner.player.inPlay.Random(), base.healing));
                }
            }

            ActionManager.AddNext(new MultiAction(owner, actions));
            base.OnPlay(cause);
        }
    }
}

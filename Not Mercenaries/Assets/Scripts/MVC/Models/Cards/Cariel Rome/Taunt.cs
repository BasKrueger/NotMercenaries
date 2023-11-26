using System.Collections.Generic;

namespace Model
{
    public class Taunt : AbstractAbility
    {
        public Taunt(int id) : base
           (
               id: id,

               name: "Taunt",
               description: "Restore <h> health to this merc and gain taunt for 3 turns.",
               speed: 1,
               targets: abilityTargets.all,
               school: SpellSchool.holy,
               healing: 12
           )
        {
        }


        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();
            actions.Add(new HealDamageAction(this.owner, this.owner, 1));
            actions.Add(new AddBuffAction(this.owner, CardManager.CreateBuff("Taunt"), this.owner));

            ActionManager.AddNext(new MultiAction(this.owner, actions));
            base.OnPlay(cause);
        }
    }
}

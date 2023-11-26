using System.Collections.Generic;

namespace Model
{
    public class RadientLight : AbstractAbility
    {
        public RadientLight(int id) : base
           (
               id: id,

               name: "Radient Light",
               description: "Give all allies +<m1>/+<m2>.",
               speed: 2,
               targets: abilityTargets.all,
               school: SpellSchool.holy,

               magicNumber1: 6,
               magicNumber2: 6
           )
        {
        }


        public override void OnPlay(PlayAbilityAction cause)
        {
            var actions = new List<AbstractAction>();

            foreach(var merc in cause.owner.player.inPlay)
            {
                actions.Add(new AddBuffAction(this.owner, CardManager.CreateBuff("Light Buff"), merc));
            }

            ActionManager.AddNext(new MultiAction(this.owner, actions));
            base.OnPlay(cause);
        }
    }
}

namespace Model
{
    public class CarielRome : AbstractMercenary
    {
        public CarielRome(int id) : base
             (
             id: id,

             name: "Cariel Rome",
             description: "Holy Crusader",
             cost: 0,
             health: 78,
             attack: 12
             )
        {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Crusaders Blow"),
                CardManager.CreateAbility(this, "Taunt"),
                CardManager.CreateAbility(this, "Seal of Light")
            };
        }
    }
}

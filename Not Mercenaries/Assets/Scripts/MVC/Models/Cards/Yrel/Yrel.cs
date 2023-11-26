namespace Model
{
    public class Yrel : AbstractMercenary
    {
        public Yrel(int id) : base
             (
             id: id,

             name: "Yrel",
             description: "There is no power stronger than hope. ",
             cost: 0,
             health: 88,
             attack: 12
             )
        {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Vindicator's Fury"),
                CardManager.CreateAbility(this, "Radient Light"),
                CardManager.CreateAbility(this, "Wrath of the Lightbound")
            };
        }
    }
}

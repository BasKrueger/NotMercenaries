namespace Model
{
    public class AnduinWrynn : AbstractMercenary
    {
        public AnduinWrynn(int id) : base
            (
            id : id,

            name: "Anduin Wrynn",
            description: "For the light!",
            cost: 0,
            health: 72,
            attack: 9
            )
            {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Penance"),
                CardManager.CreateAbility(this, "Holy Nova"),
                CardManager.CreateAbility(this, "Holy Word: Salvation")
            };
        }
    }
}

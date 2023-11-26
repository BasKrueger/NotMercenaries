namespace Model
{
    public class Xuen : AbstractMercenary
    {
        public Xuen(int id) : base
            (
            id : id,

            name: "Xuen",
            description: "Shall we test your might?",
            cost: 0,
            health: 76,
            attack: 9
            )
            {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Pounce"),
                CardManager.CreateAbility(this, "Equalizing Strike"),
                CardManager.CreateAbility(this, "Tiger Lightning"),
            };
        }
    }
}

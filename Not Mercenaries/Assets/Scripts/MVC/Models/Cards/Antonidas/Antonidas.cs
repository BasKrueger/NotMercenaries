namespace Model
{
    public class Antonidas : AbstractMercenary
    {
        public Antonidas(int id) : base
            (
            id : id,

            name: "Antonidas",
            description: "Master of Fire Magic",
            cost: 0,
            health: 76,
            attack: 7
            )
            {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Fireball"),
                CardManager.CreateAbility(this, "Flamestrike"),
                CardManager.CreateAbility(this, "Fireballstorm")
            };
        }
    }
}

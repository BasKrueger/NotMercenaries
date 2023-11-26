namespace Model
{
    public class Alextrasza : AbstractMercenary
    {
        public Alextrasza(int id) : base
            (
            id : id,

            name: "Alextrasza",
            description: "I bring live and hope",
            cost: 0,
            health: 81,
            attack: 8
            )
            {
            base.abilities = new System.Collections.Generic.List<AbstractAbility>()
            {
                CardManager.CreateAbility(this, "Dragon's Breath"),
                CardManager.CreateAbility(this, "Flame Buffet"),
                CardManager.CreateAbility(this, "Dragonqueen's Gambit")
            };
        }
    }
}

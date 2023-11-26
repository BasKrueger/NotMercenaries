namespace DTO
{
    public class AbilityState :IID
    {
        protected int id = -1;
        public int Id { get => id; set { id = value; } }

        public StatState cost;
        public string name;
        public string description;
    }
}

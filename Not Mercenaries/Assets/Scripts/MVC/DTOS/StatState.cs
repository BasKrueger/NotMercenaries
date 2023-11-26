namespace DTO
{
    public class StatState
    {
        public int value;
        public int maxValue;
        public int baseValue;

        public static implicit operator int(StatState d) => d.value;
        public override string ToString() => value.ToString();
    }
}

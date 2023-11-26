namespace Model
{
    public class Stat
    {
        private int buffedValue = 0;
        private int _value = 0;
        public int value
        {
            get
            {
                return _value + buffedValue;
            }
            set
            {
                if(maxValue == -1)
                {
                    maxValue = value;
                }
                if(baseValue == -1)
                {
                    baseValue = value;
                }

                if (value > maxValue) value = maxValue;

                _value = value;
            }
        }
        public int maxValue = -1;
        public int baseValue = -1;

        public Stat() { }
        public Stat(int value)
        {
            this.value = value;
        }
        public Stat(Stat other)
        {
            this.value = other.value;
            this.buffedValue = other.buffedValue;
            this.baseValue = other.baseValue;
        }

        public void Normalize() => buffedValue = 0;
        public void Buff(int amount) => buffedValue += amount;
        public void SetBuff(int amount) => buffedValue = amount;

        public static implicit operator int(Stat d) => d.value;
        public static implicit operator string(Stat d) => d.value.ToString();
        public override string ToString() => value.ToString();

        public static Stat operator -(Stat a, int b)
        {
            a.value = a._value - b;
            return a;
        }

        public static Stat operator +(Stat a, int b)
        {
            a.value = a._value + b;
            return a;
        }

        public DTO.StatState GetState()
        {
            return new DTO.StatState()
            {
                value = this.value,
                maxValue = this.maxValue,
                baseValue = this.baseValue
            };
        }
    }
}

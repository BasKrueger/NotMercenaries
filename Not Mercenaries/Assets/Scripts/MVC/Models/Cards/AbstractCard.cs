namespace Model
{
    public abstract class AbstractCard
    {
        public Stat cost { get; protected set; } = new Stat();
        public string name { get; protected set; }
        protected string description;

        protected Stat damage;
        protected Stat healing;

        protected (Stat stat, SpellSchool school) SpellmagicNumber1;
        protected (Stat stat, SpellSchool school) SpellmagicNumber2;
        protected (Stat stat, SpellSchool school) SpellmagicNumber3;
        protected (Stat stat, SpellSchool school) SpellmagicNumber4;
        protected (Stat stat, SpellSchool school) SpellmagicNumber5;

        protected Stat magicNumber1;
        protected Stat magicNumber2;
        protected Stat magicNumber3;
        protected Stat magicNumber4;
        protected Stat magicNumber5;

        public int id { get; private set; } = -1;

        public AbstractCard(int id, int cost, string name, string description, int damage, int healing,
           int magicNumber1, int magicNumber2, int magicNumber3, int magicNumber4, int magicNumber5)
        {
            this.id = id;

            this.cost = new Stat(cost);
            this.name = name;
            this.description = description;

            this.damage = new Stat(damage);
            this.healing = new Stat(healing);
            this.magicNumber1 = new Stat(magicNumber1);
            this.magicNumber2 = new Stat(magicNumber2);
            this.magicNumber3 = new Stat(magicNumber3);
            this.magicNumber4 = new Stat(magicNumber4);
            this.magicNumber5 = new Stat(magicNumber5);
        }

        protected string GetFormatedDescription()
        {
            var result = description;

            result = result.Replace("<d>", damage.ToString());
            result = result.Replace("<h>", healing.ToString());
            result = result.Replace("<m1>", magicNumber1.ToString());
            result = result.Replace("<m2>", magicNumber2.ToString());
            result = result.Replace("<m3>", magicNumber3.ToString());
            result = result.Replace("<m4>", magicNumber4.ToString());
            result = result.Replace("<m5>", magicNumber5.ToString());

            return result;
        }

        #region Action calls
        public virtual void WheneverPlayMerc(PlayMercAction cause) { }
        public virtual void AfterPlayMerc(PlayMercAction cause) { }

        public virtual void WheneverSummonMerc(SummonMercAction cause) { }
        public virtual void AfterSummonMerc(SummonMercAction cause) { }

        public virtual void WheneverDealDamage(DealDamageAction cause) { }
        public virtual void AfterDealDamage(DealDamageAction cause) { }

        public virtual void WheneverPlayAbility(PlayAbilityAction cause) { }
        public virtual void AfterPlayAbility(PlayAbilityAction cause) { }

        public virtual void TurnEnd(TurnEndAction cause) { }
        public virtual void TurnStart(TurnStartAction cause) { }

        public virtual void WheneverDeath(DeathAction cause) { }
        public virtual void AfterDeath(DeathAction cause) { }

        public virtual void WheneverDeathblow(DeathblowAction cause) { }
        public virtual void Deathblow(DeathblowAction cause) { }
        public virtual void AfterDeathblow(DeathblowAction cause) { }

        public virtual void WheneverAttack(AttackAction cause) { }
        public virtual void AfterAttack(AttackAction cause) { }

        public virtual void WheneverHealDamage(HealDamageAction cause) { }
        public virtual void AfterHealDamage(HealDamageAction cause) { }
        #endregion

        public virtual void NormalUpdate()
        {
            Normalize();
            Update();
        }

        protected virtual void Normalize()
        {
            cost.Normalize();
            healing.Normalize();
            damage.Normalize();

            magicNumber1.Normalize();
            magicNumber2.Normalize();
            magicNumber3.Normalize();
            magicNumber4.Normalize();
            magicNumber5.Normalize();
        }

        protected virtual void Update()
        {

        }

    }
}
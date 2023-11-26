using System;

namespace Model
{
    [Flags]
    public enum abilityTargets
    {
        targetless = all | single | enemy | ally,
        all =       1 << 0,
        single =    1 << 1,
        enemy =     1 << 2,
        ally =      1 << 3,
        damaged = 1 << 4,
    }

    [Flags]
    public enum SpellSchool
    {
        none,
        all = arcane | fire | frost | fel | holy | shadow | nature,
        arcane = 1 << 0,
        fire = 1 << 1,
        frost = 1 << 2,
        fel = 1 << 3,
        holy = 1 << 4,
        shadow = 1 << 5,
        nature = 1 << 6
    }

    public abstract class AbstractAbility : AbstractCard
    {
        public abilityTargets targets { get; private set; }
        public SpellSchool school { get; private set; }

        public AbstractMercenary owner
        {
            get
            {
                return Game.FindOwnerOf(this);
            }
        }

        public AbstractAbility(int id, int speed, string name, string description, abilityTargets targets, SpellSchool school = SpellSchool.none, int damage = 0, int healing = 0, int spellDamage = 0, 
            int magicNumber1 = 0, int magicNumber2 = 0, int magicNumber3 = 0, int magicNumber4 = 0, int magicNumber5 = 0) 
            : base(id, speed, name, description, damage, healing, magicNumber1, magicNumber2, magicNumber3, magicNumber4, magicNumber5)
        {
            this.targets = targets;
            this.school = school;
        }

        public bool IsValidTarget(AbstractMercenary target)
        {
            if ((targets & abilityTargets.all) != 0) return true;

            if (target == null) return false;

            if ((targets & abilityTargets.single) != 0)
            {
                if ((targets & abilityTargets.enemy) != 0 && Game.FindPlayerOf(this) != Game.FindPlayerOf(target)) return true;
                if ((targets & abilityTargets.ally) != 0 && Game.FindPlayerOf(this) == Game.FindPlayerOf(target)) return true;
            }

            return false;
        }

        protected bool Combo(SpellSchool school = SpellSchool.none) => GetComboCount(school) > 0;

        protected int GetComboCount(SpellSchool school = SpellSchool.none)
        {
            int comboCounter = 0;

            var pastPlays = ActionManager.history.Search<PlayAbilityAction>(Searchmode.turns, 1);

            foreach(var play in pastPlays)
            {
                if(play.owner.player != owner.player) continue;
                if((play.cardToPlay.school & school) == 0) continue;

                comboCounter++;
            }

            return comboCounter;
        }


        public sealed override void NormalUpdate()
        {
            base.NormalUpdate();
        }

        public override void Deathblow(DeathblowAction cause)
        {
            owner?.Deathblow(cause);
            base.Deathblow(cause);
        }

        #region Action calls
        public virtual void OnPlay(PlayAbilityAction cause) { }
        #endregion

        #region Create DTO
        public DTO.AbilityState GetState()
        {
            return new DTO.AbilityState()
            {
                Id = id,
                cost = cost.GetState(),
                name = name,
                description = GetFormatedDescription()
            };
        }
        #endregion
    }
}
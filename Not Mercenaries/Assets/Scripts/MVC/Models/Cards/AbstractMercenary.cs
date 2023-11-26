using DTO;
using System;
using System.Collections.Generic;

namespace Model
{
    public enum BoardPosition
    {
        Bank,
        InPlay,
        GraveYard
    }

    public abstract class AbstractMercenary : AbstractCard
    {
        public event Action died;

        public BoardPosition boardPosition { get; private set; } = BoardPosition.Bank;
        private BoardPosition nextBoardPosition = BoardPosition.Bank;

        public Stat health { get; private set; } = new Stat();
        public Stat attack { get; private set; } = new Stat();
        public bool isTaunting = false;

        public List<AbstractAbility> abilities { get; protected set; } = new List<AbstractAbility>();
        public List<AbstractBuff> buffs { get; protected set; } = new List<AbstractBuff>();
        public PlayAbilityAction preparedPlay { get; private set; }

        public Player player
        {
            get
            {
                return Game.FindPlayerOf(this);
            }
        }

        public AbstractMercenary(int id, int cost, string name, string description, int health, int attack, int damage = 0, int healing = 0,
            int number = 0, int number2 = 0, int number3 = 0, int number4 = 0, int number5 = 0
            )
            : base(id, cost, name, description, damage, healing, number, number2, number3, number4, number5)
        {
            this.health.value = health;
            this.attack.value = attack;
        }

        public void PreparePlay(Player player, AbstractAbility cardToPlay, AbstractMercenary target)
        {
            preparedPlay = new PlayAbilityAction(this, cardToPlay, target);
            Game.UpdateGameState(DTO.GameStateCause.MercPreparedAction);
        }

        public void ClearPreparedPlay()
        {
            preparedPlay = null;
            Game.UpdateGameState(DTO.GameStateCause.MercPreparedAction);
        }

        public void InvokeBuffs(string methodName, params object[] args)
        {
            foreach (var buff in buffs)
            {
                try
                {
                    buff.GetType().GetMethod(methodName).Invoke(buff, args);
                }
                catch (Exception e) { }
            }
        }

        public void InvokeAbilities(string methodName, params object[] args)
        {
            foreach (var ability in abilities)
            {
                try
                {
                    ability.GetType().GetMethod(methodName).Invoke(ability, args);
                }
                catch (Exception e) { }
            }
        }

        public bool CanPlayAbility(AbstractAbility toPlay, AbstractMercenary target)
        {
            return abilities.Contains(toPlay) && toPlay.IsValidTarget(target);
        }

        #region Action calls
        public virtual void OnPlay(PlayMercAction cause)
        {

        }

        public virtual void OnSummon(SummonMercAction cause)
        {
            nextBoardPosition = BoardPosition.InPlay;
        }

        public virtual void TakeDamage(DealDamageAction cause)
        {
            health -= cause.damage;
            if (health <= 0)
            {
                ActionManager.AddNext(new DeathAction(cause.owner, this, Deathkind.natural));
            }
        }

        public virtual void HealDamage(HealDamageAction cause)
        {
            health += cause.healing;
        }

        public virtual void Die(DeathAction cause)
        {
            nextBoardPosition = BoardPosition.GraveYard;
            Normalize();
            buffs.Clear();
            died?.Invoke();
        }

        public virtual void RemoveBuff(AbstractBuff toRemove)
        {
            buffs.Remove(toRemove);
            NormalUpdate();
        }

        public virtual void AddBuff(AbstractBuff toAdd)
        {
            if (buffs.Contains(toAdd)) return;
            buffs.Add(toAdd);
            NormalUpdate();
        }
        #endregion

        public sealed override void NormalUpdate()
        {
            base.NormalUpdate();
            foreach (var ability in abilities)
            {
                ability.NormalUpdate();
            }
            foreach (var buff in buffs)
            {
                buff.NormalUpdate();
            }
        }

        protected override void Normalize()
        {
            health.Normalize();
            attack.Normalize();
            isTaunting = false;
        }

        protected override void Update()
        {
            boardPosition = nextBoardPosition;
        }

        #region Create DTO
        public DTO.MercenaryState GetState(bool includePreparedPlay = true)
        {
            var state = new DTO.MercenaryState();
            state.Id = id;
            state.mercName = base.name;
            state.description = base.GetFormatedDescription();
            state.position = boardPosition;
            state.health = health.GetState();
            state.attack = attack.GetState();
            state.isTaunting = isTaunting;

            if (includePreparedPlay)
            {
                state.preparedPlay = preparedPlay?.GetState();
            }

            List<DTO.AbilityState> abilityStates = new List<DTO.AbilityState>();
            foreach(var ability in abilities)
            {
                abilityStates.Add(ability.GetState());
            }

            List<DTO.BuffState> buffStates = new List<DTO.BuffState>();
            foreach(var buff in buffs)
            {
                buffStates.Add(buff.GetState());
            }


            state.abilities = abilityStates;
            state.buffs = buffStates;
            return state;
        }
        #endregion
    }
}
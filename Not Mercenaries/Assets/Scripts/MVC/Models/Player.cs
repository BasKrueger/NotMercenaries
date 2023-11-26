using System.Collections.Generic;

namespace Model
{
    public class Player
    {
        public List<AbstractMercenary> bank = new List<AbstractMercenary>();
        public List<AbstractMercenary> inPlay = new List<AbstractMercenary>();
        public List<AbstractMercenary> graveYard = new List<AbstractMercenary>();

        public Player enemy
        {
            get
            {
                return Game.FindEnemyOf(this);
            }
        }

        public Player()
        {
            bank.Add(CardManager.CreateMercenary("Antonidas"));
            bank.Add(CardManager.CreateMercenary("Alextrasza"));
            bank.Add(CardManager.CreateMercenary("Cariel Rome"));
            bank.Add(CardManager.CreateMercenary("Anduin Wrynn"));
            bank.Add(CardManager.CreateMercenary("Xuen"));
            bank.Add(CardManager.CreateMercenary("Yrel"));
        }

        public void SummonMercenary(SummonMercAction cause)
        {
            if (!bank.Contains(cause.toPlay)) return;

            if (inPlay.Count < 6)
            {
                bank.Remove(cause.toPlay);
                inPlay.Add(cause.toPlay);

                cause.toPlay.died += () =>
                {
                    bank.Remove(cause.toPlay);
                    inPlay.Remove(cause.toPlay);
                    graveYard.Add(cause.toPlay);

                    if(inPlay.Count == 0 && bank.Count == 0)
                    {
                        Game.Loose(this);
                    }
                };

                cause.toPlay.OnSummon(cause);
            }
        }

        public void PreparePlay(AbstractAbility toPlay, AbstractMercenary target)
        {
            foreach (var merc in inPlay)
            {
                if (merc != null && merc.CanPlayAbility(toPlay, target))
                {
                    merc.PreparePlay(this, toPlay, target);
                }
            }
        }

        public void ClearPreparedPlays()
        {
            foreach (var merc in inPlay) merc.ClearPreparedPlay();
        }

        public bool CanPlayAbility(AbstractAbility abilityToPlay, AbstractMercenary target)
        {
            foreach(var merc in inPlay)
            {
                if (merc != null && merc.CanPlayAbility(abilityToPlay, target)) return true;
            }
            return false;
        }

        public List<PlayAbilityAction> GetPreparedPlays()
        {
            var actions = new List<PlayAbilityAction>();
            foreach(var merc in inPlay)
            {
                if (merc.preparedPlay == null) continue;
                actions.Add(merc.preparedPlay);
            }
            return actions;
        }

        public AbstractMercenary FindOwnerOf(AbstractAbility ability)
        {
            var total = new List<AbstractMercenary>(bank);
            total.AddRange(inPlay);
            foreach (var merc in total)
            {
                if (merc.abilities.Contains(ability)) return merc;
            }
            return null;
        }

        public AbstractMercenary FindOwnerOf(AbstractBuff buff)
        {
            var total = new List<AbstractMercenary>(bank);
            total.AddRange(inPlay);
            foreach (var merc in total)
            {
                if (merc.buffs.Contains(buff)) return merc;
            }
            return null;
        }

        public bool CanPlayMerc(AbstractMercenary mercToPlay) => bank.Contains(mercToPlay) && inPlay.Count < 6;

        public bool ContainsAbility(AbstractAbility ability) => FindOwnerOf(ability) != null;

        public bool ContainsMerc(AbstractMercenary merc) => bank.Contains(merc) || inPlay.Contains(merc);

        #region Create DTO
        public DTO.PlayerState GetState()
        {
            var bankStates = new List<DTO.MercenaryState>();
            var playStates = new List<DTO.MercenaryState>();
            var graveYardStates = new List<DTO.MercenaryState>();

            foreach (var merc in bank)
            {
                bankStates.Add(merc.GetState());
            }
            foreach (var merc in inPlay)
            {
                if (merc == null) continue;
                playStates.Add(merc.GetState());
            }
            foreach(var merc in graveYard)
            {
                graveYardStates.Add(merc.GetState());
            }

            return new DTO.PlayerState()
            {
                Bank = bankStates,
                InPlay = playStates,
                GraveYard = graveYardStates
            };
        }
        #endregion
    }
}

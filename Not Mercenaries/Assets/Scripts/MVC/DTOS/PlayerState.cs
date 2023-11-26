using System.Collections.Generic;

namespace DTO
{
    public class PlayerState
    {
        public List<MercenaryState> Bank;
        public List<MercenaryState> InPlay;
        public List<MercenaryState> GraveYard;

        public MercenaryState GetMercState(int id)
        {
            var merc = Bank.GetById(id);
            if (merc != null) return merc;

            merc = InPlay.GetById(id);
            if(merc != null) return merc;

            merc = GraveYard.GetById(id);
            if(merc != null) return merc;

            return null;
        }

        public AbilityState GetAbilityState(int id)
        {
            foreach(var merc in Bank)
            {
                var ability = merc.abilities.GetById(id);
                if(ability != null) return ability;
            }

            foreach (var merc in InPlay)
            {
                var ability = merc.abilities.GetById(id);
                if (ability != null) return ability;
            }

            foreach (var merc in InPlay)
            {
                var ability = merc.abilities.GetById(id);
                if (ability != null) return ability;
            }

            return null;
        }
    }
}
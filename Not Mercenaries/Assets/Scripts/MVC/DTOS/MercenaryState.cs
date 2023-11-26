using System.Collections.Generic;
using UnityEngine;

namespace DTO
{
    public class MercenaryState : IID
    {
        protected int id = -1;
        public int Id { get => id; set { id = value; } }

        public Model.BoardPosition position;
        public string mercName;
        public string description;

        public StatState health;
        public StatState attack;

        public List<AbilityState> abilities;
        public List<BuffState> buffs;

        public PreparedPlayState preparedPlay;
        public bool isTaunting;
    }
}
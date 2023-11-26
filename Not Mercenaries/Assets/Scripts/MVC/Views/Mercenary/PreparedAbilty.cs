using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public class PreparedAbilty : MonoBehaviour, IGameView, IID
    {
        [SerializeField]
        private TextMeshProUGUI name;

        protected int id = -1;
        public int Id { get => id; set { id = value; } }
        [HideInInspector]
        public int playerIndex;

        private void Awake()
        {
            Game.RequestStateUpdate(this);
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            if (this == null) yield break;

            var merc = state.GetMercState(id);
            if (merc != null && merc.preparedPlay != null)
            {
                gameObject.SetActive(true);
                name.text = merc.preparedPlay.card.name;
                yield break;
            }

            gameObject.SetActive(false);
        }
    }
}

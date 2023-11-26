using System.Collections;
using TMPro;
using UnityEngine;

namespace View
{
    public class MercCard : AbstractCard, IGameView
    {
        [SerializeField]
        private TextMeshProUGUI name;
        [SerializeField]
        private TextMeshProUGUI desription;
        [SerializeField]
        private TextMeshProUGUI health;
        [SerializeField]
        private TextMeshProUGUI attack;

        private void Start()
        {
            Game.RequestStateUpdate(this);
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            var mercState = state.GetMercState(id);
            if (mercState == null) yield break;

            name.text = mercState.mercName;
            desription.text = mercState.description;
            health.text = mercState.health.ToString();
            attack.text = mercState.attack.ToString();

            yield break;
        }
    }
}

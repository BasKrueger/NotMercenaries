using System.Collections;
using TMPro;
using UnityEngine;

namespace View
{
    public class AbilityCard : AbstractCard, IGameView, IID
    {
        [SerializeField]
        private TextMeshProUGUI name;
        [SerializeField]
        private TextMeshProUGUI description;

        private void Start()
        {
            Game.RequestStateUpdate(this);
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            var abilityState = state.GetAbilityState(id);

            if (abilityState == null) yield break;

            name.text = abilityState.name;
            description.text = abilityState.description;
            yield break;
        }
    }
}

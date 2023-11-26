using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EndTurnButtonText : MonoBehaviour, IGameView
    {
        private TextMeshProUGUI text;
        
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            if (state.cause != DTO.GameStateCause.TurnStarted) yield break;

            switch (state.currentPhase)
            {
                case Model.GamePhase.Deploy:
                    text.text = "Finish";
                    break;
                case Model.GamePhase.Prepare:
                    text.text = "End Turn";
                    break;
                case Model.GamePhase.Battle:
                    text.text = "Combat";
                    break;
            }
        }
    }
}


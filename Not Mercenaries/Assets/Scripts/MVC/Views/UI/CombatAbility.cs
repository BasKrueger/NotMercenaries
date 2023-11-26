using DTO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatAbility : MonoBehaviour, IGameView
{
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI name;

    public IEnumerator OnGameStateChanged(GameState state)
    {
        if(state.cause == GameStateCause.GameStarted)
        {
            this.gameObject.SetActive(false);
            yield break;
        }

        if (state.currentPhase != Model.GamePhase.Battle) yield break;

        if(state.cause == GameStateCause.CardPlaying && state.lastAbility != null)
        {
            gameObject.SetActive(true);
            description.text = state.lastAbility.name;
            name.text = state.lastAbility.description;
            yield return new WaitForSeconds(0.75f);
        }

        if (state.cause == GameStateCause.CardPlayed)
        {
            yield return new WaitForSeconds(0.75f);
            gameObject.SetActive(false);

        }
    }
}

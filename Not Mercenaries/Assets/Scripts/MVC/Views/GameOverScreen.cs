using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour, IGameView
{
    [SerializeField]
    private TextMeshProUGUI winningText;

    public IEnumerator OnGameStateChanged(DTO.GameState state)
    {
        gameObject.SetActive(state.winningPlayer != -1);
        winningText.text = state.winningPlayer == 0 ? "You Won" : "You Lost";
        yield break;
    }
}

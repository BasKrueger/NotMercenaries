using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class EndTurnButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(TurnEndClicked);
        }

        private void TurnEndClicked()
        {
            Game.model.EndTurn();
        }
    }
}


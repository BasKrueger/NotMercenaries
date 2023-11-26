using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class Board : MonoBehaviour, IDropable
    {
        public void OnDropped(IDragable otherObject)
        {
            var card = otherObject.gameObject.GetComponent<View.AbstractCard>();
            if (card == null) return;
            Game.model.PlayCard(card.Id);
        }
    }
}

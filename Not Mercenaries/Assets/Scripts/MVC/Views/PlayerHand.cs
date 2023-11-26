using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Model;
using DTO;

namespace View
{
    public class PlayerHand : MonoBehaviour, IGameView
    {
        const float cardWidth = 2f;
        [SerializeField]
        private AbstractCard cardTemplate;
        private List<AbstractCard> cards = new List<AbstractCard>();

        [SerializeField]
        private int playerIndex;

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            if (state.currentPhase == Model.GamePhase.Deploy)
            {
                UpdateCardAmount(state.players[playerIndex]);
                UpdateCardPositions(state.players[playerIndex]);
                yield break;
            }

            StartCoroutine(EmptyHand());
            IEnumerator EmptyHand()
            {
                while (cards.Count > 0)
                {
                    RemoveCard(cards[0]);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private void UpdateCardAmount(DTO.PlayerState state)
        {
            foreach(var mercState in state.Bank)
            {
                if (!cards.ContainsID(mercState.Id))
                {
                    SpawnCard(mercState);
                }
            }

            var toRemove = new List<AbstractCard>();

            for(int i = 0;i < cards.Count;i++)
            {
                var merc = cards[i];
                if (!state.Bank.ContainsID(merc.Id))
                {
                    toRemove.Add(merc);
                }
            }

            foreach(var card in toRemove)
            {
                RemoveCard(card);
            }
        }

        private void UpdateCardPositions(DTO.PlayerState state)
        {
            for (int i = 0; i < state.Bank.Count; i++)
            {
                if (cards[i] == null) continue;

                Vector3 targetPosition = transform.position;
                targetPosition.x = i * cardWidth - ((cards.Count - 1) * (cardWidth / 2));

                cards[i].targetPosition = targetPosition;
            }
        }

        private void SpawnCard(DTO.MercenaryState state)
        {
            var card = Instantiate(cardTemplate);

            card.transform.SetParent(transform);
            card.Id = state.Id;

            cards.Add(card);
        }

        private void RemoveCard(View.AbstractCard card)
        {
            cards.Remove(card);
            if(card != null) Destroy(card.gameObject);
        }
    }

}
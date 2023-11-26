using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DTO;

namespace View
{
    public class MercHand : MonoBehaviour, IGameView, IID
    {
        const float cardWidth = 2f;

        [SerializeField]
        private AbilityCard cardTemplate;
        private List<AbstractCard> cards = new List<AbstractCard>();

        private Mercenary merc;

        protected int id = -1;
        public int Id { get => id; set { id = value; } }

        public void SetUp(Mercenary merc)
        {
            this.merc = merc;
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            var mercState = state.GetMercState(id);
            if (mercState == null) yield break;

            UpdateCardAmount(mercState.abilities);
            UpdateCardPositions(mercState.abilities);
        }

        private void UpdateCardAmount(List<DTO.AbilityState> abilityStates)
        {
            foreach(var abilityState in abilityStates)
            {
                if (!cards.ContainsID(abilityState.Id))
                {
                    SpawnCard(abilityState);
                }
            }

            var toRemove = new List<AbstractCard>();

            foreach(var ability in cards)
            {
                if (!abilityStates.ContainsID(ability.Id))
                {
                    toRemove.Add(ability);
                }
            }

            foreach(var ability in toRemove)
            {
                RemoveCard(ability);
            }
        }

        private void UpdateCardPositions(List<DTO.AbilityState> abilityStates)
        {
            for (int i = 0; i < abilityStates.Count; i++)
            {
                if (this == null) return;
                Vector3 targetPosition = transform.position;
                targetPosition.x = i * cardWidth - ((cards.Count - 1) * (cardWidth / 2));

                cards[i].targetPosition = targetPosition;
            }
        }

        private void SpawnCard(DTO.AbilityState state)
        {
            var card = Instantiate(cardTemplate);

            card.transform.SetParent(transform);
            card.transform.position = merc.transform.position;
            card.Id = state.Id;

            cards.Add(card);
        }

        private void RemoveCard(AbstractCard card)
        {
            cards.Remove(card);
            if(card != null) Destroy(card.gameObject);
        }

        private void OnEnable()
        {
            foreach(var card in cards) { card.transform.position = merc.transform.position; }
        }
    }

}
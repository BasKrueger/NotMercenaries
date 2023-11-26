using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTO;

namespace View
{
    public class Board : MonoBehaviour, IGameView
    {
        [SerializeField]
        private int playerIndex;

        const float cardWidth = 2.5f;

        [SerializeField]
        private GameObject MercenaryTemplate;
        private List<Mercenary> characters = new List<Mercenary>();

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            if (
                state.currentPhase != Model.GamePhase.Deploy &&
                state.cause == DTO.GameStateCause.MercPlayEnd ||
                state.currentPhase != Model.GamePhase.Deploy &&
                state.cause == DTO.GameStateCause.MercDied
                ) 
            {
                yield return new WaitForSeconds(0.2f);
            } 

            UpdateCharacterAmount(state.players[playerIndex]);
            UpdateCharacterPositions(state.players[playerIndex]);
        }

        private void UpdateCharacterAmount(DTO.PlayerState state)
        {
            foreach (var mercState in state.InPlay)
            {
                if (!characters.ContainsID(mercState.Id))
                {
                    SpawnCharacter(mercState);
                }
            }

            var toRemove = new List<Mercenary>();
            foreach(var merc in characters)
            {
                if (!state.InPlay.ContainsID(merc.Id))
                {
                    toRemove.Add(merc);
                }
            }

            foreach(var merc in toRemove)
            {
                RemoveCharacter(merc);
            }
        }

        private void UpdateCharacterPositions(DTO.PlayerState state)
        {
            for (int i = 0; i < state.InPlay.Count && i < characters.Count; i++)
            {
                Vector3 targetPosition = transform.position;
                targetPosition.x = i * cardWidth - ((characters.Count - 1) * (cardWidth / 2));

                characters[i].transform.position = targetPosition;
            }
        }

        private void SpawnCharacter(DTO.MercenaryState state)
        {
            var merc = Instantiate(MercenaryTemplate).GetComponent<View.Mercenary>();

            merc.transform.SetParent(transform);
            merc.Id = state.Id;
            merc.GetComponent<Controller.Mercenary>()?.SetUp(state.Id);
            merc.SetUp(playerIndex);
            merc.selected += OnCharacterSelected;

            characters.Add(merc);
        }

        private void RemoveCharacter(Mercenary merc)
        {
            characters.Remove(merc);
            merc.selected -= OnCharacterSelected;
            Destroy(merc.gameObject);
        }

        private void OnCharacterSelected(Mercenary selected)
        {
            foreach(var c in characters)
            {
                if (c != selected) c.Select(false);
            }
        }
    }
}
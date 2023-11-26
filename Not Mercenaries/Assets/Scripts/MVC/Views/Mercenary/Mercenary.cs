using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace View
{
    public class Mercenary : MonoBehaviour, IGameView, IID
    {
        public event Action<Mercenary> selected;

        private MercHand hand;
        private MercStats stats;

        private bool isSelected = false;
        private bool prepare = false;

        protected int id = -1;
        public int Id 
        { 
            get => id;
            set 
            { 
                foreach(var id in GetComponentsInChildren<IID>(true))
                {
                    if (id == this) continue;
                    id.Id = value;
                }
                id = value; 
            } 
        }

        private void Awake()
        {
            hand = GetComponentInChildren<MercHand>();
            stats = GetComponentInChildren<MercStats>();
        }

        public void SetUp(int playerIndex)
        {
            hand?.SetUp(this);
            stats.SetUp();
            Select(false);

            Game.RequestStateUpdate(this);
        }

        public void Select(bool state)
        {
            if (state)
            {
                selected?.Invoke(this);
            }
            hand?.gameObject.SetActive(state);
            isSelected = state;
        }

        private void OnMouseDown()
        {
            if (!prepare) return;

            if (!isSelected) Game.model.UnPlayCard(id);
            Select(!isSelected);
        }

        public IEnumerator OnGameStateChanged(DTO.GameState state)
        {
            if (state.cause == DTO.GameStateCause.TurnStarted)
            {
                prepare = state.currentPhase == Model.GamePhase.Prepare;
                if (!prepare) Select(false);
            }
            if(state.cause == DTO.GameStateCause.MercPreparedAction)
            {
                if (state.GetMercState(id).preparedPlay != null)
                {
                    Select(false);
                }
            }
            yield break;
        }
    }
}

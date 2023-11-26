using DTO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public class MercStats : MonoBehaviour, IGameView, IID
    {
        [SerializeField]
        private TextMeshProUGUI health;
        [SerializeField]
        private TextMeshProUGUI attack;
        [SerializeField]
        private Animator healthAnimator;

        protected int id = -1;
        public int Id { get => id; set { id = value; } }

        public void SetUp()
        {
            Game.RequestStateUpdate(this);
        }

        public IEnumerator OnGameStateChanged(GameState state)
        {
            var newAttack = state.GetMercState(id)?.attack;
            var oldAttack = int.Parse(attack.text);

            var newHealth = state.GetMercState(id)?.health;
            var oldHealth = int.Parse(health.text);

            if(state.cause == GameStateCause.MercDamaged)
            {
                if (newHealth < oldHealth)
                {
                    healthAnimator.Play("TakeDamage");
                }
            }

            if(oldAttack != 0 || oldHealth != 0)
            {
                if (newHealth != oldHealth || newAttack != oldAttack)
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }

            health.text = newHealth?.ToString();
            attack.text = newAttack?.ToString();
        }
    }

}

using UnityEngine;

namespace Controller
{
    class Mercenary : MonoBehaviour, IDropable
    {
        private int id;

        public void SetUp(int id)
        {
            this.id = id;
        }

        public void OnDropped(IDragable otherObject)
        {
            var card = otherObject.gameObject.GetComponent<View.AbilityCard>();
            if (card != null)
            {
                Game.model.PlayCard(card.Id, this.id);
            }
        }
    }
}

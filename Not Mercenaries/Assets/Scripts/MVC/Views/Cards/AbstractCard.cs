using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public abstract class AbstractCard : MonoBehaviour, Controller.IDragable, IID
    {
        const float minDistanceToTarget = 0.025f;
        const float rotationForce = 5f;

        [NonSerialized]
        public Vector3 targetPosition = new Vector3();
        [SerializeField] 
        private float moveSpeed = 30;
        private bool gettingDragged = false;

        protected int id = -1;
        public int Id { get => id; set { id = value; } }

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotationForce * Time.deltaTime);

            if (gettingDragged) return;

            if(Vector3.Distance(transform.position, targetPosition) > minDistanceToTarget)
            {
                Vector3 direction = targetPosition - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }

        public void DragStart(Vector3 position) => gettingDragged = true;
        public void DragEnd(Vector3 position) => gettingDragged = false;

        public void Dragging(Vector3 position, Vector3 delta)
        {
            transform.position = position;
            transform.Rotate(new Vector3(-delta.z, 0, delta.x) * 10);
        }

    }
}

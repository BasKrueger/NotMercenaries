using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public interface IDropable
    {
        void OnDropped(IDragable otherObject);
    }
    public interface IDragable
    {
        GameObject gameObject { get; }
        void DragStart(Vector3 position);
        void Dragging(Vector3 position, Vector3 delta);
        void DragEnd(Vector3 position);
    }

    public class DragAndDropHandler : MonoBehaviour
    {
        private static DragAndDropHandler instance;
        private IDragable draggingObject;

        private int defaultLayer;
        private Vector3 lastDragPosition = new Vector3();

        private void Awake()
        {
            if (instance != null) Destroy(this);
            instance = this;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0)) OnDragStart();
            if (Input.GetMouseButton(0)) OnDrag();
            if (Input.GetMouseButtonUp(0)) OnDragEnd();

            lastDragPosition = GetDragPosition();
        }

        private void OnDragStart()
        {
            draggingObject = GetDragTarget();
            if (draggingObject == null) return;

            defaultLayer = draggingObject.gameObject.layer;
            draggingObject.gameObject.layer = LayerMask.NameToLayer("Selected");

            draggingObject.DragStart(GetDragPosition());
        }

        private void OnDrag()
        {
            if (draggingObject == null) return;
            draggingObject.Dragging(GetDragPosition(), lastDragPosition - GetDragPosition());
        }

        private void OnDragEnd()
        {
            if (draggingObject == null) return;

            draggingObject.DragEnd(GetDragPosition());
            GetDropTarget()?.OnDropped(draggingObject);

            draggingObject.gameObject.layer = defaultLayer;
            draggingObject = null;
        }

        #region support functions
        private Vector3 GetDragPosition()
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int mask = ~LayerMask.GetMask("Selected");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                return hit.point;
            }

            return new Vector3();
        }

        private IDropable GetDropTarget()
        {
            if (GetDragPosition() == new Vector3())
            {
                return null;
            }

            RaycastHit hit = new RaycastHit();
            int mask = ~LayerMask.GetMask("Selected");
            Vector3 direction = GetDragPosition() - Camera.main.transform.position;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, Mathf.Infinity, mask))
            {
                if (hit.transform.GetComponent<IDropable>() != null)
                {
                    return hit.transform.GetComponent<IDropable>();
                }
            }

            return null;
        }

        private IDragable GetDragTarget()
        {
            if (GetDragPosition() == new Vector3())
            {
                return null;
            }

            RaycastHit hit = new RaycastHit();
            int mask = ~LayerMask.GetMask("Selected");
            Vector3 direction = GetDragPosition() - Camera.main.transform.position;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, Mathf.Infinity, mask))
            {
                return hit.transform.GetComponent<IDragable>();
            }

            return null;
        }
        #endregion
    }
}

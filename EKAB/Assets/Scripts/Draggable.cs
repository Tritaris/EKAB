using UnityEngine;
using UnityEngine.EventSystems;

namespace UNO {
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public Transform parentToReturnTo = null;

        public void OnBeginDrag(PointerEventData eventData) {
            //Debug.Log("OnBeginDrag " + eventData.position);
            this.transform.position = eventData.position;

            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData) {
            //Debug.Log("OnDrag " + eventData.position);
            this.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) {
            //Debug.Log("OnEndDrag");
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = parentToReturnTo.position;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}

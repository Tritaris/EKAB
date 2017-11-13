using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UNO {
    public class DropZone : MonoBehaviour, IDropHandler {
        CardManager cm;

        void Awake() {
            cm = FindObjectOfType<CardManager>();
        }

        void IDropHandler.OnDrop(PointerEventData eventData) {
            

            if (d != null) {
                if (cm.cardToGameObject.ContainsKey(eventData.pointerDrag)) {
                    
                }
                else {
                    throw new Exception("cardToGameObject doesn't contain " + eventData.pointerDrag.name);
                }
            }
        }        
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UNO {
    public class DropZone : MonoBehaviour, IDropHandler {
        CardManager cm;
        GameManager gameManager;        

        void Awake() {
            cm = FindObjectOfType<CardManager>();
            gameManager = FindObjectOfType<GameManager>();            
        }

        void IDropHandler.OnDrop(PointerEventData eventData) {
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            Card c = cm.gameObjectToCard[eventData.pointerDrag];
            

            if (d != null) {
                if (cm.gameObjectToCard.ContainsKey(eventData.pointerDrag)) {
                    if (gameObject.name == gameManager.CurrentPlayerName() + "Panel") {
                        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
                        d.parentToReturnTo = this.transform;
                        d.parentToReturnTo.position = this.transform.position;

                        foreach (var card in cm.Draw(1)) {
                            gameManager.CurrentPlayerList().Add(card);
                        }
                        //cm.Draw(gameManager.CurrentPlayerList(), 1);

                        gameManager.TogglePlayDeckLock(gameManager.CurrentPlayerList());                        
                    }
                }                
            }
        }        
    }
}

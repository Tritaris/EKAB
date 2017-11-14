using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UNO {
    public class DiscardPile : MonoBehaviour, IDropHandler {
        CardManager cm;
        GameManager gm;

        void Awake() {
            cm = FindObjectOfType<CardManager>();
            gm = FindObjectOfType<GameManager>();
        }

        void IDropHandler.OnDrop(PointerEventData eventData) {
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            Card c = cm.gameObjectToCard[eventData.pointerDrag];
            Card topCard = cm.discardedCards[cm.discardedCards.Count - 1];

            if (d != null) {
                if (cm.gameObjectToCard.ContainsKey(eventData.pointerDrag)) {
                    //Determine if card can be placed on discard pile
                    if (c.Color == topCard.Color || c.Color == Card.CardColor.Wild || c.Color == cm.wildCardColor || c.Action == topCard.Action && c.Action != Card.CardAction.None || c.Points == topCard.Points && c.Points < 20) {
                        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
                        d.parentToReturnTo = this.transform;
                        d.parentToReturnTo.position = this.transform.position;                        
                        
                        gm.ProcessCard(c);
                        
                        if(cm.wildCardColor != Card.CardColor.Wild) {
                            cm.wildCardColor = Card.CardColor.Wild;
                        }
                    }
                }
                else {
                    throw new Exception("cardToGameObject doesn't contain " + eventData.pointerDrag.name);
                }
            }
        }        
    }
}

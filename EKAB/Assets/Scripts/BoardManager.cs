using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UNO {
    public class BoardManager : MonoBehaviour {
        public List<Card> playerCards;
        public List<Card> computerCards;
        public List<Card> discardedCards;
        public List<Card> playDeck;

        CardManager cm;        
        
        void Start() {
            cm = GetComponent<CardManager>();

            cm.BuildPlayDeck();
        }
        
        void Update() {
            
        }
    }
}

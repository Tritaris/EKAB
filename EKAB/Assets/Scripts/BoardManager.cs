using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UNO {
    public class BoardManager : MonoBehaviour {
        public List<Card> playerCards;
        public List<Card> computerCards;
        public List<Card> discardedCards;
        public List<Card> playDeck;

        private const int CardsPerHand = 7;

        private CardManager cm;        
        
        void Start() {
            cm = GetComponent<CardManager>();

            cm.BuildPlayDeck();

            Deal();            
        }
        
        void Update() {
            
        }

        void Deal() {
            //Add cards to hands
            Draw(playerCards, CardsPerHand);
            Draw(computerCards, CardsPerHand);            
        }

        void Draw(List<Card> hand, int numberOfCards) {
            for (int i = 0; i < numberOfCards; i++) {
                hand.Add(playDeck.ElementAt<Card>(0));
                playDeck.RemoveAt(0);
            }            
        }

        void Discard(List<Card> hand, int numberOfCards) {
            for (int i = 0; i < numberOfCards; i++) {
                discardedCards.Add(hand.ElementAt<Card>(0));
                hand.RemoveAt(0);
            }
        }
    }
}

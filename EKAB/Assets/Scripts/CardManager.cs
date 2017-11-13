using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UNO {
    public class CardManager : MonoBehaviour {
        public List<Card> masterDeck; //Main list of all the cards in game. non-randomized list of cards.

        public List<Card> playDeck;     //The main deck from which card are draw from. Randomized list of cards.
        public List<Card> discardedCards;      //Contains cards currently out of play.  

        public List<Card> playerCards;
        public int playerScore;
        public List<Card> computerCards;
        public int computerScore;

        public Dictionary<GameObject, Card> cardToGameObject;

        private const int CardsPerHand = 7;

        private void Awake() {
            cardToGameObject = new Dictionary<GameObject, Card>();
        }

        void Start() {           
            GenerateDeck();

            BuildPlayDeck();

            Deal();
        }
        
        void Update() {
            
        }

        void GenerateDeck() {
            masterDeck = new List<Card>();            

            for (int i = 0; i < 2; i++) {
                foreach (Card.CardColor color in Enum.GetValues(typeof(Card.CardColor))) {
                    //Skip Wild Color
                    if (color == Card.CardColor.Wild) {
                        continue;
                    }

                    //Generate 0 Point Cards
                    if (i == 0) {
                        masterDeck.Add(new Card(color, Card.CardAction.None, 0));
                    }

                    //Generate Points Cards
                    for (int j = 1; j <= 9; j++) {                    
                        masterDeck.Add(new Card(color, Card.CardAction.None, j));                    
                    }

                    //Generate Action Cards                    
                    masterDeck.Add(new Card(color, Card.CardAction.DrawTwo, 20));
                    masterDeck.Add(new Card(color, Card.CardAction.Reverse, 20));
                    masterDeck.Add(new Card(color, Card.CardAction.Skip, 20));                    
                }
            }

            //Generate Wild Cards
            for (int k = 0; k < 4; k++) {
                masterDeck.Add(new Card(Card.CardColor.Wild, Card.CardAction.Wild, 50));
                masterDeck.Add(new Card(Card.CardColor.Wild, Card.CardAction.WildDraw, 50));
            }            
        }        

        public void RandomizeCardList(List<Card> deck) {
            deck.Shuffle(); 
        }

        public void BuildPlayDeck() {
            playDeck = new List<Card>(masterDeck);
            RandomizeCardList(playDeck);
        }

        void Deal() {
            //Add cards to hands
            Draw(playerCards, CardsPerHand);
            Draw(computerCards, CardsPerHand);
            Draw(discardedCards, 1);
        }

        public void Draw(List<Card> hand, int numberOfCards) {
            for (int i = 0; i < numberOfCards; i++) {
                hand.Add(playDeck.ElementAt<Card>(0));
                playDeck.RemoveAt(0);
            }
        }

        public void Discard(Card card) {
            List<Card> tempList;
            if (playerCards.Contains(card)) {
                playerScore += card.Points;
                tempList = playerCards;
            }
            else if (computerCards.Contains(card)) {
                computerScore += card.Points;
                tempList = computerCards;
            }
            else {
                tempList = discardedCards;
            }

            int cardIndex = tempList.IndexOf(card);
            
            discardedCards.Add(tempList.ElementAt<Card>(cardIndex));
            tempList.RemoveAt(cardIndex);
            
        }
    }

    public static class IListExtensions {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}

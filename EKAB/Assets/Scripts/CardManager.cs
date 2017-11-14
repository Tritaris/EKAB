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
        public List<Card> computerCards;

        public Card.CardColor wildCardColor;

        public Dictionary<GameObject, Card> gameObjectToCard;

        private const int CardsPerHand = 7;
        //GameManager gameManager;
        GraphicManager graphicManager;

        private void Awake() {
            //gameManager = GetComponent<GameManager>();            
            gameObjectToCard = new Dictionary<GameObject, Card>();
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

        public bool CheckIfNeedCard(List<Card> cardList) {
            Card topCard = discardedCards[discardedCards.Count - 1];
            bool[] needCardArray = new bool[cardList.Count];

            for (int i = 0; i < cardList.Count; i++) {
                if (cardList[i].Color == topCard.Color || cardList[i].Color == wildCardColor || cardList[i].Action == topCard.Action && cardList[i].Action != Card.CardAction.None || cardList[i].Points == topCard.Points && cardList[i].Points < 20) {
                    //Debug.Log("A card can be played");
                    needCardArray[i] = false;
                }
                else {
                    //Debug.Log("Unlock Draw Pile");
                    needCardArray[i] = true;
                }
            }

            bool needCard = !needCardArray.Contains(false);
            Debug.Log("Lock playDeck = " + !needCard);
            return needCard;
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
            playerCards =  new List<Card>(Draw(CardsPerHand));
            computerCards = new List<Card>(Draw(CardsPerHand));
            discardedCards = new List<Card>(Draw(1));
        }

        public List<Card> Draw(int numberOfCards) {
            List<Card> cardsDrawn = new List<Card>();

            for (int i = 0; i < numberOfCards; i++) {
                //hand.Add(playDeck.ElementAt<Card>(playDeck.Count - 1));
                cardsDrawn.Add(playDeck.ElementAt<Card>(playDeck.Count - 1));
                playDeck.RemoveAt(playDeck.Count - 1);                
            }

            return cardsDrawn;
        }

        public void Discard(Card card) {
            List<Card> tempList = GetCardList(card);            

            int cardIndex = tempList.IndexOf(card);
            
            discardedCards.Add(tempList.ElementAt<Card>(cardIndex));
            tempList.RemoveAt(cardIndex);            
        }

        public void SetWildCardColor(Card.CardColor color) {
            wildCardColor = color;
        }

        public void SetWildCardColorRed() {
            wildCardColor = Card.CardColor.Red;
            Debug.Log("wildCardColor = " + wildCardColor);                       
        }

        public void SetWildCardColorBlue() {
            wildCardColor = Card.CardColor.Blue;
            Debug.Log("wildCardColor = " + wildCardColor);           
        }

        public void SetWildCardColorGreen() {
            wildCardColor = Card.CardColor.Green;
            Debug.Log("wildCardColor = " + wildCardColor);
        }

        public void SetWildCardColorYellow() {
            wildCardColor = Card.CardColor.Yellow;
            Debug.Log("wildCardColor = " + wildCardColor);            
        }

        public List<Card> GetCardList(Card card) {
            if (playerCards.Contains(card)) {
                return playerCards;
            }
            else if (computerCards.Contains(card)) {                
                return computerCards;
            }
            else if(playDeck.Contains(card)) {
                return playDeck;
            }
            else {
                return discardedCards;
            }
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

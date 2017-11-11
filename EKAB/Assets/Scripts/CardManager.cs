using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UNO {
    public class CardManager : MonoBehaviour {
        public Sprite[] textures { get; private set; }
        public string[] names { get; private set; }        
        
        private List<Card> cardList;

        BoardManager bm;

        private void Awake() {
            LoadSprites();
        }

        void Start() {
            bm = FindObjectOfType<BoardManager>();
            
            GenerateDeck();            
            RandomizePlayDeck();
        }
        
        void Update() {
            
        }

        void GenerateDeck() {
            cardList = new List<Card>();

            //Generate 0 Point Cards
            foreach (Card.CardColor color in Enum.GetValues(typeof(Card.CardColor))) {
                if(color == Card.CardColor.Wild) {
                    Debug.Log("NO Wild 0");
                    continue;
                }
                cardList.Add(new Card(color, Card.CardAction.None, 0));
            }

            for (int i = 0; i < 2; i++) {
                //Generate Points Cards
                for (int j = 1; j <= 9; j++) {
                    foreach (Card.CardColor color in Enum.GetValues(typeof(Card.CardColor))) {
                        if (color == Card.CardColor.Wild) {
                            Debug.Log("NO Wild " + j);
                            continue;
                        }
                        cardList.Add(new Card(color, Card.CardAction.None, j));
                    }
                }

                //Generate Action Cards
                foreach (Card.CardColor color in Enum.GetValues(typeof(Card.CardColor))) {
                    if (color == Card.CardColor.Wild) {
                        Debug.Log("NO Wild - Action");
                        continue;
                    }
                    cardList.Add(new Card(color, Card.CardAction.DrawTwo, 0));
                    cardList.Add(new Card(color, Card.CardAction.Reverse, 0));
                    cardList.Add(new Card(color, Card.CardAction.Skip, 0));
                }
            }

            //Generate Wild Cards
            for (int k = 0; k < 4; k++) {
                cardList.Add(new Card(Card.CardColor.Wild, Card.CardAction.Wild, 0));
                cardList.Add(new Card(Card.CardColor.Wild, Card.CardAction.WildDraw, 0));
            }

            bm.playDeck = cardList;
        }        

        void RandomizePlayDeck() {            
            bm.playDeck.Shuffle(); 
        }

        void RebuildPlayDeck() {
            bm.playDeck = new List<Card>(cardList);
            RandomizePlayDeck();
        }

        void LoadSprites() {
            //Loads sprites from resouce folder and assigns them to an array by name
            textures = Resources.LoadAll<Sprite>("Textures/Cards/");
            names = new string[textures.Length];

            for (int i = 0; i < names.Length; i++) {
                names[i] = textures[i].name;
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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UNO {
    public class CardManager : MonoBehaviour {
        public Sprite[] Textures { get; private set; }
        public string[] Names { get; private set; }        
        
        public List<Card> cardList;

        private BoardManager bm;

        private void Awake() {
            LoadSprites();
        }

        void Start() {
            bm = FindObjectOfType<BoardManager>();
            
            GenerateDeck();

            BuildPlayDeck();
        }
        
        void Update() {
            
        }

        void GenerateDeck() {
            cardList = new List<Card>();            

            for (int i = 0; i < 2; i++) {
                foreach (Card.CardColor color in Enum.GetValues(typeof(Card.CardColor))) {
                    //Skip Wild Color
                    if (color == Card.CardColor.Wild) {
                        continue;
                    }

                    //Generate 0 Point Cards
                    if (i == 0) {
                        cardList.Add(new Card(color, Card.CardAction.None, 0));
                    }

                    //Generate Points Cards
                    for (int j = 1; j <= 9; j++) {                    
                        cardList.Add(new Card(color, Card.CardAction.None, j));                    
                    }

                    //Generate Action Cards                    
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
        }        

        public void RandomizePlayDeck() {            
            bm.playDeck.Shuffle(); 
        }

        public void BuildPlayDeck() {
            bm.playDeck = new List<Card>(cardList);
            RandomizePlayDeck();
        }

        void LoadSprites() {
            //Loads sprites from resouce folder and assigns them to an array by name
            Textures = Resources.LoadAll<Sprite>("Textures/Cards/");
            Names = new string[Textures.Length];

            for (int i = 0; i < Names.Length; i++) {
                Names[i] = Textures[i].name;
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

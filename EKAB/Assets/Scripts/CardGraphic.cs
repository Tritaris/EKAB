using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UNO {
    public class CardGraphic : MonoBehaviour {
        CardManager cm;
        BoardManager bm;      

        public Dictionary<Card, GameObject> cardToGameObject;        
        
        public Canvas canvas;

        private int spacingX = 50;
        private int spacingY = 75;
        private int cardWidth = 100;
        private int cardHeight = 145;        

        private void Awake() {
            cardToGameObject = new Dictionary<Card, GameObject>();           
        }

        void Start() {           

            cm = FindObjectOfType<CardManager>();
            bm = FindObjectOfType<BoardManager>();

            //Set Image to starting value            
            ShowPlayerCards();
        }

        void Update() {
            
        }

        Sprite LoadCardSprite(Card card) {
            //Builds sprite name string based on the card values.
            string spriteName = "";

            switch (card.Color) {
                case Card.CardColor.Red:
                    spriteName = "Red_";
                    break;
                case Card.CardColor.Blue:
                    spriteName = "Blue_";
                    break;
                case Card.CardColor.Green:
                    spriteName = "Green_";
                    break;
                case Card.CardColor.Yellow:
                    spriteName = "Yellow_";
                    break;
                case Card.CardColor.Wild:
                    spriteName = "Wild_";
                    break;
            }

            switch (card.Action) {
                case Card.CardAction.DrawTwo:
                    spriteName += "DrawTwo";
                    break;
                case Card.CardAction.Reverse:
                    spriteName += "Reverse";
                    break;
                case Card.CardAction.Skip:
                    spriteName += "Skip";
                    break;
                case Card.CardAction.Wild:
                    spriteName += "Wild";
                    break;
                case Card.CardAction.WildDraw:
                    spriteName += "DrawFour";
                    break;
                case Card.CardAction.None:
                    spriteName += card.Points;
                    break;
            }
            
            //Returns Card image using sprite name string
            return cm.Textures[Array.IndexOf(cm.Names, spriteName)]; 
        }

        void ShowPlayerCards() {
            Debug.Log("ShowPlayerCards");
            int x = 1;
            foreach (var card in bm.playerCards) {
                GameObject card_go = new GameObject();
                RectTransform card_rect = card_go.AddComponent<RectTransform>();
                card_rect.sizeDelta = new Vector2(cardWidth, cardHeight);

                // Add our tile/GO pair to the dictionary.
                cardToGameObject.Add(card, card_go);                               

                card_go.name = "Card " + x;
                card_go.transform.position = new Vector2(x * spacingX, spacingY);
                card_go.transform.SetParent(canvas.transform, true);

                Image card_image = card_go.AddComponent<Image>();
                card_image.sprite = LoadCardSprite(card);
                card_image.preserveAspect = true;

                x++;
            }
        }
    }
}

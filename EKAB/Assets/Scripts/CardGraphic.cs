using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UNO {
    public class CardGraphic : MonoBehaviour {
        CardManager cm;
        public Image _image;
        public Card card;

        //Varibles for TESTING
        Card.CardColor prevColor;
        Card.CardAction prevAction;
        int prevPoints;

        void Start() {
            _image = GetComponent<Image>();
            cm = FindObjectOfType<CardManager>();

            //Set Image to starting value
            LoadCardSprite();
        }

        void Update() {
            //TESTING :: Update Image if card values are changed. 
            if (prevAction != card.Action || prevColor != card.Color || prevPoints != card.Points) {
                LoadCardSprite();
            }
        }

        void LoadCardSprite() {
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

            //Sets Card image using sprite name string
            _image.sprite = cm.Textures[Array.IndexOf(cm.Names, spriteName)];

            //TESTING :: Used to update image when card values are updated.
            prevAction = card.Action;
            prevColor = card.Color;
            prevPoints = card.Points;
        }
    }
}

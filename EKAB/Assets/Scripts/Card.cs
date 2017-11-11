using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UNO {
    public class Card : MonoBehaviour {

        public enum CardColor {
            Red,
            Green,
            Blue,
            Yellow,
            Wild
        }

        public enum CardAction {
            Wild,
            WildDraw,
            Reverse,
            Skip,
            DrawTwo,
            None
        }

        CardManager cm;

        public CardColor Color;
        public CardAction Action;
        [Range(0,9)]
        public int Points;

        CardColor prevColor;
        CardAction prevAction;
        int prevPoints;

        public Sprite _sprite;
        public Sprite newSprite;

        private SpriteRenderer sr;

        public Card(CardColor color, CardAction action, int points) {
            Color = color;
            Action = action;
            Points = points;
        }

        // Use this for initialization
        void Start() {
            sr = GetComponentInChildren<SpriteRenderer>();
            cm = FindObjectOfType<CardManager>();
            LoadCardSprite();
        }

        // Update is called once per frame
        void Update() {
            //if(_sprite != newSprite && newSprite != null) {
            //    _sprite = cm.textures[Array.IndexOf(cm.names, newSprite.name)];
            //    sr.sprite = _sprite;
            //}

            if(prevAction != Action || prevColor != Color || prevPoints != Points) {
                LoadCardSprite();                
            }
        }

        void LoadCardSprite() {
            string spriteName = "";

            switch (Color) {
                case CardColor.Red:
                    spriteName = "Red_";
                    break;
                case CardColor.Blue:
                    spriteName = "Blue_";
                    break;
                case CardColor.Green:
                    spriteName = "Green_";
                    break;
                case CardColor.Yellow:
                    spriteName = "Yellow_";
                    break;
                case CardColor.Wild:
                    spriteName = "Wild_";
                    break;
            }

            switch (Action) {
                case CardAction.DrawTwo:
                    spriteName += "DrawTwo";
                    break;
                case CardAction.Reverse:
                    spriteName += "Reverse";
                    break;
                case CardAction.Skip:
                    spriteName += "Skip";
                    break;
                case CardAction.Wild:
                    spriteName += "Wild";
                    break;
                case CardAction.WildDraw:
                    spriteName += "DrawFour";
                    break;
                case CardAction.None:
                    spriteName += Points;
                    break;
            }

            _sprite = cm.textures[Array.IndexOf(cm.names, spriteName)];
            sr.sprite = _sprite;

            Debug.Log(spriteName);

            //Used to update on the fly. Won't be needed in the game
            prevAction = Action;
            prevColor = Color;
            prevPoints = Points;
        }
    }
}

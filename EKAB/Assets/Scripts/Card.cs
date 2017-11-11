using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UNO {
    [Serializable]
    public class Card {
        public enum CardColor {
            Red,
            Green,
            Blue,
            Yellow,
            Wild
        }

        public enum CardAction {
            None,
            DrawTwo,
            Reverse,
            Skip,            
            Wild,
            WildDraw
        }        

        public CardColor Color;
        public CardAction Action;
        [Range(0,9)]
        public int Points;               

        public Card(CardColor color, CardAction action, int points) {
            Color = color;
            Action = action;
            Points = points;
        }        
    }
}

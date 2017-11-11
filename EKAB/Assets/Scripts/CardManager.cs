using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UNO {
    public class CardManager : MonoBehaviour {
        BoardManager bm;

        public Sprite[] textures;
        public string[] names;
        
        public Card[] cards;

        // Use this for initialization
        void Start() {
            bm = FindObjectOfType<BoardManager>();
            LoadSprites();
            GenerateDeck();
        }

        // Update is called once per frame
        void Update() {
            
        }

        void GenerateDeck() {
            cards = new Card[108];


        }

        void LoadSprites() {
            textures = Resources.LoadAll<Sprite>("Textures/Cards/");
            names = new string[textures.Length];

            for (int i = 0; i < names.Length; i++) {
                names[i] = textures[i].name;
            }
        }
    }
}

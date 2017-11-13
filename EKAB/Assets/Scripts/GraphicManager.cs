using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UNO {
    public class GraphicManager : MonoBehaviour {        
        public Sprite[] Textures { get; private set; }
        public string[] Names { get; private set; }        
        
        public Canvas canvas;
        public GameObject playerPanel;
        public GameObject computerPanel;
        public GameObject discardPanel;
        

        private CardManager cm;

        //private int spacingX = 50;
        //private int spacingY = 75;
        private int cardWidth = 100;
        private int cardHeight = 145;        

        private void Awake() {            
            cm = FindObjectOfType<CardManager>();            
        }

        void Start() {
            LoadCardSprites();            
                                    
            ShowCards(cm.playerCards,playerPanel);
            ShowCards(cm.computerCards, computerPanel);
            ShowCards(cm.discardedCards, discardPanel);
        }

        void Update() {
            
        }

        void LoadCardSprites() {
            //Loads sprites from resource folder and assigns them to an array by name
            Textures = Resources.LoadAll<Sprite>("Textures/Cards/");
            Names = new string[Textures.Length];

            for (int i = 0; i < Names.Length; i++) {
                Names[i] = Textures[i].name;
            }
        }

        Sprite SetCardImage(Card card) {
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

            card.Name = spriteName;

            //Returns Card image using sprite name string
            return Textures[Array.IndexOf(Names, spriteName)];
        }        

        void ShowCards(List<Card> cardList, GameObject panel) {
            Debug.Log("Show " + panel.name);            
            foreach (var card in cardList) {
                GameObject card_go = new GameObject();
                card_go.AddComponent<Draggable>();

                RectTransform card_rect = card_go.AddComponent<RectTransform>();
                card_rect.sizeDelta = new Vector2(cardWidth, cardHeight);
                

                CanvasGroup cg = card_go.AddComponent<CanvasGroup>();
                cg.blocksRaycasts = true;

                LayoutElement le = card_go.AddComponent<LayoutElement>();
                le.preferredWidth = cardWidth;
                le.preferredHeight = cardHeight;
                le.flexibleWidth = 0;
                le.flexibleHeight = 0;

                // Add our GO/Card pair to the dictionary.
                cm.cardToGameObject.Add(card_go, card);
                
                //card_go.transform.position = new Vector2(0, 0);
                card_go.transform.SetParent(panel.transform, true);
                card_rect.localPosition = new Vector2(0, 0);

                Image card_image = card_go.AddComponent<Image>();
                card_image.sprite = SetCardImage(card);
                card_image.preserveAspect = true;

                card_go.name = card.Name;                
            }
        }
    }
}

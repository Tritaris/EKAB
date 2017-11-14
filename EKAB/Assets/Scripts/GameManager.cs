using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UNO {
    public class GameManager : MonoBehaviour {
        public Text playerScoreText;
        public Text computerScoreText;

        private int numPlayers = 2;

        private int _computerScore;
        private int _playerScore;

        public int turnCount = 0;
        public int currentTurn = 0;

        private bool _showColorPanel = false;

        CardManager cm;
        GraphicManager gm;

        public int PlayerScore {
            get {
                return _playerScore;
            }
            set {
                _playerScore = value;
                playerScoreText.text = "Player: " + PlayerScore;
            }
        }

        public int ComputerScore {
            get {
                return _computerScore;
            }
            set {
                _computerScore = value;
                computerScoreText.text = "Computer: " + ComputerScore;
            }
        }

        public bool ShowColorPanel {
            get { return _showColorPanel; }
            set { _showColorPanel = value; }
        }

        void Awake() {
            cm = GetComponent<CardManager>();
            gm = GetComponent<GraphicManager>();
        }

        void Start() {
                       
        }

        void Update() {
            if (turnCount == currentTurn && ShowColorPanel == false) {
                if (turnCount % numPlayers == 0) {
                    Debug.Log("Player 1's Turn");
                    gm.CardLock(cm.playerCards, false);
                    gm.CardLock(cm.computerCards, true);

                    TogglePlayDeckLock(CurrentPlayerList());
                }
                else if (turnCount % numPlayers == 1) {
                    Debug.Log("Computer's Turn");
                    gm.CardLock(cm.playerCards, true);
                    gm.CardLock(cm.computerCards, false);

                    TogglePlayDeckLock(CurrentPlayerList());
                }
                turnCount++;                
            }
        }

        //Locks and unlocks the playDeck based on if the currentPlayer needs a card.
        public void TogglePlayDeckLock(List<Card> cardList) {
            if (cm.CheckIfNeedCard(cardList)) {
                Debug.Log(CurrentPlayerName() + " needs card");
                gm.CardLock(cm.playDeck, false);
            }
            else {
                Debug.Log(CurrentPlayerName() + " doesn't needs card");
                gm.CardLock(cm.playDeck, true);
            }
        }

        public void ProcessCard(Card card) {
            AddPoints(card);
            
            switch (card.Action) {
                case Card.CardAction.DrawTwo:
                    Debug.Log("DrawTwo");
                    //cm.Draw(NextPlayerList(), 2);
                    List<Card> temp = cm.Draw(2);
                    gm.MoveCardGraphicsToPanel(temp, gm.NextPlayerPanel());
                    foreach (var c in temp) {
                        NextPlayerList().Add(c);
                    }
                    turnCount++;
                    break;
                case Card.CardAction.None:
                    Debug.Log("None");
                    break;
                case Card.CardAction.Reverse:
                    Debug.Log("Reverse");
                    turnCount++;
                    break;
                case Card.CardAction.Skip:
                    Debug.Log("Skip");
                    turnCount++;
                    break;
                case Card.CardAction.Wild:
                    Debug.Log("Wild");
                    gm.ShowColorPanel();
                    ShowColorPanel = true;                    
                    break;
                case Card.CardAction.WildDraw:
                    Debug.Log("WildDraw");
                    gm.ShowColorPanel();
                    //cm.Draw(NextPlayerList(), 4);
                    List<Card> temp1 = cm.Draw(4);
                    gm.MoveCardGraphicsToPanel(temp1, gm.NextPlayerPanel());
                    foreach (var c in temp1) {
                        NextPlayerList().Add(c);
                    }
                    turnCount++;
                    ShowColorPanel = true;                    
                    break;
            }

            cm.Discard(card);

            currentTurn = turnCount;
        }

        public List<Card> NextPlayerList() {
            if (currentTurn % numPlayers == 0) {
                return cm.computerCards;
            }
            else {
                return cm.playerCards;
            }
        }

        public List<Card> CurrentPlayerList() {
            if (currentTurn % numPlayers == 0) {
                return cm.playerCards;
            }
            else {
                return cm.computerCards;                
            }
        }

        public string CurrentPlayerName() {
            if (currentTurn % numPlayers == 0) {
                return "Player";
            }
            else {
                return "Computer";
            }
        }

        public string NextPlayerName() {
            if (currentTurn % numPlayers == 0) {
                return "Computer";
            }
            else {
                return "Player";
            }
        }

        public void AddPoints(Card card) {
            if(cm.playerCards.Contains(card))
                PlayerScore += card.Points;
            else if(cm.computerCards.Contains(card))
                ComputerScore += card.Points;            
        }
    }
}

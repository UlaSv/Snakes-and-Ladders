using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Ladder
{
    internal class Player
    {
        private int playerPosition;
        private int playerNumber;
        private bool winner =  false;
        private int dice;
        public Player(int number)
        {
            playerNumber = number;
            playerPosition = 0;
        }
        public void UpdatedPosition(int position)
        {
            playerPosition = position;
        }
        public int PlayerNumber(int playerNumber)
        {
            return playerNumber;
        }
        public int Position()
        {
            return playerPosition;
        }
        public void GetDiceNumber(int dice)
        {
            this.dice=dice;
        }
        public void Move(LinkedList<int> blocks, Dictionary<int, int> snakes, Dictionary<int, int> ladders, TextBox text)
        {
            if (playerPosition + dice < blocks.Count)
            {
                playerPosition = blocks.ElementAt(dice + playerPosition);
                if (playerPosition == blocks.Count - 1)
                {
                    winner = true;
                }
                if (snakes.ContainsKey(playerPosition))
                {
                    int snakeTrap = blocks.ElementAt(snakes[playerPosition]);
                    text.Text += $"SNAKE ALERT!!! Go from {playerPosition} ===> to {snakeTrap}" + Environment.NewLine;
                    playerPosition = snakeTrap;
                }
                if (ladders.ContainsKey(playerPosition))
                {
                    int ladderLuck = blocks.ElementAt(ladders[playerPosition]);
                    text.Text += $"LADDER ALERT!!! Go from {playerPosition} ===> to {ladderLuck}" + Environment.NewLine;
                    playerPosition = ladderLuck;
                }
                if (playerPosition > blocks.Count - 1)
                {
                    text.Text += "Rolled too much. Stay where you are";
                }
            }
        }
        public bool Winner()
        {
            return winner;
        }

    }
}

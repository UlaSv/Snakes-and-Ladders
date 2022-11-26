using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Ladder
{
    public partial class Game : Form
    {
        private int nr_players;
        private int diceNumber;
        private int count;
        string answer;
        private LinkedList<int> tiles = new LinkedList<int>();
        private List<Player> playerList = new List<Player>();
        private Dictionary<int, int> snakesDicctionary = new Dictionary<int, int>();
        private Dictionary<int, int> ladderDicctionary = new Dictionary<int, int>();
        public Game(int number)
        {
            InitializeComponent();
            Size = new Size(500, 418);
            textBox4.Enabled = false;
            nr_players = number;
            diceNumber = 0;
            count = 0;
            ladderDicctionary = new Dictionary<int, int>
            {
                {2, 38}, {7, 14}, {8, 31}, {16, 26}, {21, 42}
            };

            snakesDicctionary = new Dictionary<int, int>
            {
                {15, 5}, {20, 10}, {45, 24}, {27, 18}, {49, 9}
            };
            for (int i = 0; i < nr_players; i++)
            {
                Player player = new Player(i);
                playerList.Add(player);
            }
        }

        public void ShowSnadder()
        {
            foreach (KeyValuePair<int, int> item in ladderDicctionary)
            {
                textBox1.Text += "A Ladder goes from: " + item.Key + "  to: " + item.Value + Environment.NewLine;
            }

            textBox1.Text += "-------------------------------------" + Environment.NewLine;
            foreach (KeyValuePair<int, int> item in snakesDicctionary)
            {
                textBox1.Text += "A Snake goes from: " + item.Key + "  to: " + item.Value + Environment.NewLine;
            }
            textBox1.Text += "-------------------------------------" + Environment.NewLine;
        }
        public void MakeTiles()
        {
            string fileName = @"C:\Users\email\Desktop\tiles.csv";
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    tiles.AddLast(Int32.Parse(values[0]));
                }
            }
        }
        public void RollDice()
        {
            Random rnd = new Random();
            diceNumber = rnd.Next(1, 7);
            textBox2.Text = diceNumber.ToString();
        }

        public void Start(object sender, EventArgs e)
        {
            ShowSnadder();
            MakeTiles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RollDice();
            Player currentPlayer = playerList[count];
            currentPlayer.GetDiceNumber(diceNumber);
            int number = currentPlayer.PlayerNumber(count) + 1;
            textBox3.Text += $"Player{number} rolled a {diceNumber}" + Environment.NewLine;
            int previousPosition = currentPlayer.Position();
            currentPlayer.Move(tiles, snakesDicctionary, ladderDicctionary, textBox3);
            textBox3.Text += $"{number} moved from cell [{previousPosition}] ====> to cell [{currentPlayer.Position()}]" + Environment.NewLine;

            if (currentPlayer.Winner())
            {
                textBox4.Text += $"Player {number} won" + Environment.NewLine;
                button1.Enabled = false;
                textBox4.Enabled = true;
            }

            textBox3.Text += ("--------------------------------") + Environment.NewLine;
            count = (count + 1) % nr_players;
            textBox3.Text += $"It's player{count + 1} turn" + Environment.NewLine;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Size = new Size(871, 418);
            button1.Enabled = false;
            textBox7.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Size = new Size(500, 418);

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            answer = textBox6.Text;
            if (answer == "1")
            {
                //add block to the end
                tiles.AddLast(tiles.Count + 1);
                textBox6.Text = $"Block successfully added. The total of blocks is {tiles.Count}" + Environment.NewLine;
                textBox6.Text += "Press Restart to continue";
            }
            if (answer == "2")
            {
                //add snake
                if (snakesDicctionary.Count * 2 / tiles.Count < 0.5)
                {
                    textBox6.Text = "Enter in the textbox below the starting position and the ending position of the SNAKE";
                    answer = "2";
                    textBox7.Enabled = true;
                    button4.Enabled = true;
                }
                else
                    MessageBox.Show("too many snakes");
            }
            if (answer == "3")
            {
                if (ladderDicctionary.Count * 2 / tiles.Count < 0.5)
                {
                    textBox6.Text = "Enter in the textbox below the starting position and the ending position of the LADDER";
                    answer = "3";
                    textBox7.Enabled = true;
                    button4.Enabled = true;
                }
                else
                    MessageBox.Show("too many ladders");
            }
            if (answer == "4")
            {
                //delete a block from a position
                textBox6.Text = "Enter in the textbox below the position of the tile you want to remove";
                answer = "4";
                textBox7.Enabled = true;
                button4.Enabled = true;

            }
            if (answer == "5")
            {
                //delete all blocks
                tiles.Clear();
                MessageBox.Show("All tiles were deleted. Game over");
                Meniu back = new Meniu();
                back.Show();
                this.Close();
            }
        }
        private void AddSnake(int begin, int stop)
        {
            if (begin == stop || begin < stop)
                MessageBox.Show("Wrong positions");
            else if (ladderDicctionary.ContainsKey(begin) || ladderDicctionary.ContainsKey(stop) || ladderDicctionary.ContainsValue(begin) || ladderDicctionary.ContainsValue(stop))
                MessageBox.Show("Position taken. Another ladder is there already");
            else if (snakesDicctionary.ContainsKey(begin) || snakesDicctionary.ContainsKey(stop) || snakesDicctionary.ContainsValue(begin) || snakesDicctionary.ContainsValue(stop))
                MessageBox.Show("Position taken. A snake is already there");
            else
            {
                snakesDicctionary.Add(begin, stop);
                textBox1.Clear();
                ShowSnadder();
            }
        }
        private void AddLadder(int begin, int stop)
        {

            if (begin == stop || begin > stop)
                MessageBox.Show("Wrong positions");
            else if (ladderDicctionary.ContainsKey(begin) || ladderDicctionary.ContainsKey(stop) || ladderDicctionary.ContainsValue(begin) || ladderDicctionary.ContainsValue(stop))
                MessageBox.Show("Position taken. Another ladder is there already");
            else if (snakesDicctionary.ContainsKey(begin) || snakesDicctionary.ContainsKey(stop) || snakesDicctionary.ContainsValue(begin) || snakesDicctionary.ContainsValue(stop))
                MessageBox.Show("Position taken. A snake is already there");
            else
            {
                ladderDicctionary.Add(begin, stop);
                textBox1.Clear();
                ShowSnadder();
            }

        }
        private void RemoveTile(int pos)
        {
            if (pos < 0 && pos > tiles.Count)
                MessageBox.Show("Position out of range");
            else if (ladderDicctionary.ContainsKey(pos) || ladderDicctionary.ContainsValue(pos))
            {
                tiles.Remove(pos);
                ladderDicctionary.Remove(pos);
                MessageBox.Show("Ups. You deleted a ladder as well");
                textBox1.Clear();
                ShowSnadder();
            }
            else if (snakesDicctionary.ContainsKey(pos) || snakesDicctionary.ContainsValue(pos))
            {
                tiles.Remove(pos);
                snakesDicctionary.Remove(pos);
                MessageBox.Show("Ups. You deleted a snake as well");
                textBox1.Clear();
                ShowSnadder();
            }
            else
            {
                tiles.Remove(pos);
                MessageBox.Show("Tile successfully deleted with no complications");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string text = textBox7.Text;
            if (answer == "2" || answer == "3")
            {
                string[] array = text.Split(' ');
                int start = Int32.Parse(array[0]);
                int end = Int32.Parse(array[1]);
                if (answer == "2")
                    AddSnake(start, end);
                if (answer == "3")
                    AddLadder(start, end);
            }
            else if (answer == "4")
            {
                int position = Int32.Parse(text);
                RemoveTile(position);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Enabled = false;
            textBox6.Clear();
            button4.Enabled = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Meniu back = new Meniu();
            back.Show();
            this.Close();
        }
    }
}

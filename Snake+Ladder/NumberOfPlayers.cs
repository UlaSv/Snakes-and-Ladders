using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Ladder
{
    public partial class NumberOfPlayers : Form
    {
        private int players=0;
        public NumberOfPlayers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            players = 1;
            Game game = new Game(players);
            game.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            players = 2;
            Game game = new Game(players);
            game.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            players = 3;
            Game game = new Game(players);
            game.Show();
            this.Hide();
        }
    }
}

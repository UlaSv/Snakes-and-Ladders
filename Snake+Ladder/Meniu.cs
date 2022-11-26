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
    public partial class Meniu : Form
    {
        public Meniu()
        {
            InitializeComponent();
        }

        private void LoadGame(object sender, EventArgs e)
        {
            NumberOfPlayers number = new NumberOfPlayers();
            number.Show();

        }
    }
}

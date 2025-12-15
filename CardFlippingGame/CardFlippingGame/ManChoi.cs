using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardFlippingGame
{
    public partial class ManChoi : Form
    {
        public ManChoi()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            GameLogic gameLogic = new GameLogic();
            GameManage gameManage=new GameManage();
            gameManage.Chinh();
            GameUI UI = new GameUI(gameManage,gameLogic);
            this.Resize += UI.f_resize;
            UI.BackGroundImage(this,"Default");
            UI.ThemeSelectionButton(this);
        }

    }
}

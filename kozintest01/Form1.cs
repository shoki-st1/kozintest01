using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kozintest01
{
    public partial class Form1 : Form
    {
        //いるもの
        //ボタン
        private Button AddButton,DeleteButton;
        //ラベル
        private Label TimeDis;

        //現在時刻
        static DateTime date = DateTime.Now;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

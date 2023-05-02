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
        //定義の宣言
        //追加ボタン,削除
        private Button AddButton,DeleteButton;
        //テキストボックス名前、優先度、期限
        private TextBox name,rank,finish;

        //時間表示ラベル
        private Label TimeDis;
        //タイマー
        Timer DateTimer;

        //表(一覧表示)
        private DataGridView ScheduleDis;

        //現在時刻
        static DateTime date = DateTime.Now;



        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 500;
            this.Height = 600;

            //formサイズの指定
            this.MaximumSize = new Size(600,700);
            this.MinimumSize = new Size(400, 500);



            //設定
            //追加ボタン
            this.AddButton = new Button();
            //オブジェクト名
            this.AddButton.Name = "Add";
            this.AddButton.Text = "追加";
            this.AddButton.Font = new Font("UTF-8",10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;
            //位置
            this.AddButton.Location = ();


            //削除ボタン
            this.DeleteButton = new Button();
            //オブジェクト名
            this.DeleteButton.Name = "Delete";
            this.DeleteButton.Text = "削除";
            this.AddButton.Font = new Font("UTF-8", 10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;
            this.DeleteButton.Location = ();



        }
    }
}

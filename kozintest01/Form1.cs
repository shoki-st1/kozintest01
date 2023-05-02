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
            //form指定
            this.Width = 500;
            this.Height = 600;

            //form拡大縮小の指定
            this.MaximumSize = new Size(600,700);
            this.MinimumSize = new Size(400, 500);


            //設定
        //追加ボタン
            AddButton = new System.Windows.Forms.Button();
            AddButton.FlatStyle = FlatStyle.Flat;
            AddButton.FlatAppearance.BorderSize = 0;

            //マウスが通る、押す
            AddButton.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            AddButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //オブジェクト名
            this.AddButton.Name = "Add";
            this.AddButton.Text = "追加";
            this.AddButton.Font = new Font("UTF-8",10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;

            //位置
            this.AddButton.Location = new Point(100,200);
            //ボタン大きさ
            this.AddButton.Size = new System.Drawing.Size(50,30);

            //イベント
            this.AddButton.Click += new EventHandler(this.AddButton_Click);
            this.AddButton.Parent = this;
            this.AddButton.BringToFront();
            this.ResumeLayout(false);


        //削除ボタン
            DeleteButton = new System.Windows.Forms.Button();
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.FlatAppearance.BorderSize = 0;

            DeleteButton.FlatAppearance.MouseOverBackColor= Color.Gray;
            DeleteButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            this.DeleteButton.Name = "Delete";
            this.DeleteButton.Text = "削除";
            this.DeleteButton.Font = new Font("UTF-8", 10);
            this.DeleteButton.TextAlign = ContentAlignment.TopCenter;
            //位置
            this.DeleteButton.Location = new Point(300,400);
            //ボタン大きさ
            this.DeleteButton.Size = new System.Drawing.Size(50,30);

            this.DeleteButton.Click += new EventHandler(this.DeleteButton_Click);
            this.DeleteButton.Parent = this;
            this.DeleteButton.BringToFront();
            this.ResumeLayout(false);

        }

        //追加ボタン
        private void AddButton_Click(object sender,EventArgs e)
        {

        }

        //削除ボタン
        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }

    }
}

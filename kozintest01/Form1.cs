using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace kozintest01
{
    public partial class Form1 : Form
    {


        //定義の宣言
        //予定、追加ボタン,削除
        private Button AddButton,DeleteButton;


        //テキストボックス名前、優先度、期限
        private TextBox name,rank,finish;

        //現在時間表示ラベル
        private Label TimeDis;

        //時間ボタン
        //日付進める
        private Button AddtimeButton;
        //日付戻る
        private Button DeltimeButton;


        //タイマー宣言
        Timer DateTimer;
        

        //表(一覧表示)
        private DataGridView ScheduleDis;

        //現在時刻関数
        static DateTime date = DateTime.Now;


        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            AutoScaleMode = AutoScaleMode.None;


            //form指定
            this.Width = 450;
            this.Height = 500;

            //form拡大縮小の指定
            this.MaximumSize = new Size(450,500);
            this.MinimumSize = new Size(450, 500);


            //設定
            //ボタン
            //追加ボタン
            AddButton = new System.Windows.Forms.Button();
            AddButton.FlatStyle = FlatStyle.Flat;
            AddButton.FlatAppearance.BorderSize = 0;

            //マウスが通る、押す色
            AddButton.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            AddButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //ボタン名前
            this.AddButton.Name = "Add";
            //表示テキスト設定
            this.AddButton.Text = "追加";
            this.AddButton.Font = new Font("UTF-8",10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;

            //位置
            this.AddButton.Location = new Point(350,150);
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

            //マウス押す、通る色
            DeleteButton.FlatAppearance.MouseOverBackColor= Color.Gray;
            DeleteButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //ボタン名前
            this.DeleteButton.Name = "Delete";
            //表示テキスト設定
            this.DeleteButton.Text = "削除";
            this.DeleteButton.Font = new Font("UTF-8", 10);
            this.DeleteButton.TextAlign = ContentAlignment.TopCenter;

            //位置
            this.DeleteButton.Location = new Point(350,450);
            //ボタン大きさ
            this.DeleteButton.Size = new System.Drawing.Size(50,30);

            //イベント
            this.DeleteButton.Click += new EventHandler(this.DeleteButton_Click);
            this.DeleteButton.Parent = this;
            this.DeleteButton.BringToFront();
            this.ResumeLayout(false);



            //テキストボックス定義
            //名前入力テキストボックス



            //表示



            //時間ラベル
            /*
           //時間表示ラベル
            private Label TimeDis;
            //日付進める
            private Button AddtimeButton;
            //日付戻る
            private Button DeltimeButton;
             */


            //時間進めるボタン
            AddtimeButton = new System.Windows.Forms.Button();
            AddtimeButton.FlatStyle = FlatStyle.Flat;
            AddtimeButton.FlatAppearance.BorderSize= 0;

            AddtimeButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            AddtimeButton.FlatAppearance.MouseDownBackColor= Color.WhiteSmoke;

            this.AddtimeButton.Name = "Addtime";

            this.AddtimeButton.Text = "＞";
            this.AddtimeButton.Font = new Font("UTF-8", 10);
            this.AddtimeButton.TextAlign = ContentAlignment.MiddleCenter;

            //位置
            this.AddtimeButton.Location = new Point(380, 0);
            //ボタン大きさ
            this.AddtimeButton.Size = new System.Drawing.Size(70, 70);

            //イベント
            this.AddtimeButton.Click += new EventHandler(this.AddtimeButton_Click);
            this.AddtimeButton.Parent = this;
            this.AddtimeButton.BringToFront();
            this.ResumeLayout(false);


            //時間戻す
            DeltimeButton = new System.Windows.Forms.Button();
            DeltimeButton.FlatStyle = FlatStyle.Flat;
            DeltimeButton.FlatAppearance.BorderSize = 0;

            DeltimeButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            DeltimeButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            this.DeltimeButton.Name = "Deltime";

            this.DeltimeButton.Text = "＜";
            this.DeltimeButton.Font = new Font("UTF-8", 10);
            this.DeltimeButton.TextAlign = ContentAlignment.MiddleCenter;
            //位置
            this.DeltimeButton.Location = new Point(0, 0);
            //ボタン大きさ
            this.DeltimeButton.Size = new System.Drawing.Size(70, 70);

            //イベント
            this.DeltimeButton.Click += new EventHandler(this.DeltimeButton_Click);
            this.DeltimeButton.Parent = this;
            this.DeltimeButton.BringToFront();
            this.ResumeLayout(false);

        }

        //予定追加ボタン
        private void AddButton_Click(object sender,EventArgs e)
        {

        }

        //予定削除ボタン
        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        //時間進める
        private void AddtimeButton_Click(object sender, EventArgs e)
        {

        }


        //時間戻す
        private void DeltimeButton_Click(object sender, EventArgs e)
        {

        }



    }
}

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


        /*テキストボックス名前、期限
            コンボボックス優先度
            期限選択
        一列で表示
        */
        TextBox name;
        ComboBox rank ;
        DateTimePicker finish;

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

            //form指定
            this.Width = 450;
            this.Height = 500;

            //form拡大縮小の指定
            this.MaximumSize = new Size(450,500);
            this.MinimumSize = new Size(450, 500);


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



            //入力するものの定義
            /*private TextBox name;
                private ComboBox rank;
                DateTimePicker finish;
            */

            //名前入力テキストボックス
            name = new TextBox();

            name.Font = new Font("UTF-8", 10);
            //size
            name.Size = new Size(120, 100);
            //位置
            name.Location = new Point(30,100);

            //form追加
            this.Controls.Add(name);



            //優先度
            //優先度選択のコンボボックス
            rank = new ComboBox();

            rank.Font = new Font("UTF-8", 10);
            //サイズ
            rank.Size = new Size(40, 100);
            //位置(名前の右上座標+10)
            rank.Location = new Point(name.Location.X + name.Width + 10,100);
            

            //選択
            rank.Items.Add("1");
            rank.Items.Add("2");
            rank.Items.Add("3");
            rank.Items.Add("4");
            rank.Items.Add("5");

            this.Controls.Add(rank);

            //選択されたアイテムの要素数
            //int selectedIndex = name.selectedIndex;



            //期限
            //期限選択
            finish = new DateTimePicker();

            finish.Font = new Font("UTF-8", 10);
            //サイズ
            finish.Size = new Size(120,100);
            //位置(優先度の右上座標+10)
            finish.Location = new Point(rank.Location.X + rank.Width + 10,100);

            //formに追加
            this.Controls.Add(finish);



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
            
            AddtimeButton.FlatAppearance.BorderColor = Color.Black;
            

            AddtimeButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            AddtimeButton.FlatAppearance.MouseDownBackColor= Color.WhiteSmoke;

            this.AddtimeButton.Name = "Addtime";

            this.AddtimeButton.Text = "＞";
            this.AddtimeButton.Font = new Font("UTF-8", 10);
            this.AddtimeButton.TextAlign = ContentAlignment.MiddleCenter;


            //ボタン大きさ
            this.AddtimeButton.Size = new System.Drawing.Size(70, 60);
            //位置(formサイズからボタンの分を引いた値)
            this.AddtimeButton.Location = new Point(this.ClientSize.Width - AddtimeButton.Width, 0);
            

            //イベント
            this.AddtimeButton.Click += new EventHandler(this.AddtimeButton_Click);
            this.AddtimeButton.Parent = this;
            this.AddtimeButton.BringToFront();
            this.ResumeLayout(false);
            this.Controls.Add(this.AddtimeButton);


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

            //ボタン大きさ
            this.DeltimeButton.Size = new System.Drawing.Size(70, 60);
            //位置
            this.DeltimeButton.Location = new Point(0,0);


            //イベント
            this.DeltimeButton.Click += new EventHandler(this.DeltimeButton_Click);
            this.DeltimeButton.Parent = this;
            this.DeltimeButton.BringToFront();
            this.ResumeLayout(false);
            this.Controls.Add(DeltimeButton);




            //時間表示ラベル
            //作成
            TimeDis = new System.Windows.Forms.Label();

            //現在の時間を表示させる
            TimeDis.Text = "時間";
            //フォント
            TimeDis.Font = new Font("UTF-8", 10);

            //枠に色をつける
            TimeDis.BorderStyle = BorderStyle.FixedSingle;
            //色
            TimeDis.ForeColor = Color.Black;

            //ラベルの大きさ(formサイズ-(戻すボタン+進むボタン))
            TimeDis.Size = new System.Drawing.Size(this.ClientSize.Width - (DeltimeButton.Width + AddtimeButton.Width),60);
            //位置(DeltimeButtonの横幅を足した位置)
            TimeDis.Location = new Point(0 + DeltimeButton.Width,0);

            //formに追加
            this.Controls.Add(TimeDis);




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
        public void AddtimeButton_Click(object sender, EventArgs e)
        {

        }


        //時間戻す
        public void DeltimeButton_Click(object sender, EventArgs e)
        {

        }



    }
}

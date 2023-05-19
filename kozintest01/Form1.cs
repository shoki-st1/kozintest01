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



        //現在時刻関数
        static DateTime date = DateTime.Now;

        //現在時間表示ラベル
        private Label TimeDis;


        //リンク集に飛ぶための宣言
        private Button LinkButton;

        //???
        private Button DeltimeButton;


        //タイマー宣言
        Timer DateTimer;


        //定義
        private Panel DataPanel;
        //表(一覧表示)
        private DataGridView ScheduleDis;
        //データベース
        //private DataTable dataTable;
        //追加
        //DataTable adddataTable = new DataTable();

        //---------------------------------------------------------------------------------------

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
            this.MinimumSize = new Size(450,500);


            //---------------------------------------------------------------------------------------------
            //ボタン
            //追加ボタン
            AddButton = new System.Windows.Forms.Button();
            AddButton.FlatStyle = FlatStyle.Flat;
            AddButton.FlatAppearance.BorderSize = 0;


            //バックカラー
            AddButton.BackColor = Color.OrangeRed;
            //マウスが通る、押す色
            AddButton.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            AddButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //ボタン名前
            this.AddButton.Name = "Add";
            //表示テキスト設定
            this.AddButton.Text = "追加";
            this.AddButton.Font = new Font("UTF-8",10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;

            //位置(formからボタン分と少し引く)
            this.AddButton.Location = new Point(this.ClientSize.Width - AddButton.Width -10,150);
            //ボタン大きさ
            this.AddButton.Size = new System.Drawing.Size(50,30);

            //イベント
            this.AddButton.Click += new EventHandler(this.AddButton_Click);
            this.AddButton.Parent = this;
            this.AddButton.BringToFront();
            this.ResumeLayout(false);



            //-------------------------------------------------------------------------------------------------------
            //削除ボタン
            DeleteButton = new System.Windows.Forms.Button();
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.FlatAppearance.BorderSize = 0;

            //バックカラー
            DeleteButton.BackColor = Color.LightBlue;
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
            this.DeleteButton.Location = new Point(this.ClientSize.Width - DeleteButton.Width -10,400);
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

            //-------------------------------------------------------------------------------------------------------
            //名前入力テキストボックス
            name = new TextBox();
            //フォント
            name.Font = new Font("UTF-8", 10); 
            //サイズ
            name.Size = new Size(170, 100);
            //位置
            name.Location = new Point(30,100);

            //form追加
            this.Controls.Add(name);


            //-------------------------------------------------------------------------------------------------------
            //優先度
            //優先度選択のコンボボックス
            rank = new ComboBox();
            //フォント
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


            //------------------------------------------------------------------------------------------------------
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


            //リンクボタン
            LinkButton = new System.Windows.Forms.Button();
            LinkButton.FlatStyle = FlatStyle.Flat;
            LinkButton.FlatAppearance.BorderSize= 0;
            LinkButton.FlatAppearance.BorderColor = Color.Black;

            //ボタンの色
            LinkButton.BackColor = Color.Silver;
            LinkButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            LinkButton.FlatAppearance.MouseDownBackColor= Color.WhiteSmoke;

            this.LinkButton.Name = "Link";

            this.LinkButton.Text = "🔎";
            this.LinkButton.Font = new Font("UTF-8", 10);
            this.LinkButton.TextAlign = ContentAlignment.MiddleCenter;


            //ボタン大きさ
            this.LinkButton.Size = new System.Drawing.Size(70, 60);
            //位置(formサイズからボタンの分を引いた値)
            this.LinkButton.Location = new Point(this.ClientSize.Width - LinkButton.Width, 0);
            

            //イベント
            this.LinkButton.Click += new EventHandler(this.LinkButton_Click);
            this.LinkButton.Parent = this;
            this.LinkButton.BringToFront();
            this.ResumeLayout(false);
            this.Controls.Add(this.LinkButton);



            //------------------------------------------------------------------------------------------------------
            //時間戻す
            DeltimeButton = new System.Windows.Forms.Button();
            DeltimeButton.FlatStyle = FlatStyle.Flat;
            DeltimeButton.FlatAppearance.BorderSize = 0;

            //ボタンの色
            DeltimeButton.BackColor = Color.Silver;
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



            //-----------------------------------------------------------------------------------------------
            //時間表示ラベル
            //作成
            TimeDis = new System.Windows.Forms.Label();

            //現在の時間を表示させる
            TimeDis.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //フォント
            TimeDis.Font = new Font("UTF-8", 10);

            //枠に色をつける
            TimeDis.BackColor = Color.SeaGreen;
            //色
            TimeDis.ForeColor = Color.Black;
            //文字の位置
            TimeDis.TextAlign = ContentAlignment.MiddleCenter;

            //ラベルの大きさ(formサイズ-(戻すボタン+進むボタン))
            TimeDis.Size = new System.Drawing.Size(this.ClientSize.Width - (DeltimeButton.Width + LinkButton.Width),60);
            //位置(DeltimeButtonの横幅を足した位置)
            TimeDis.Location = new Point(0 + DeltimeButton.Width,0);

            //formに追加
            this.Controls.Add(TimeDis);


            //-------------------------------------------------------------------------------------------------------

            //パネル
            //表を表示させて乗せるためのパネル、表のみでは位置を決めれないため
            Panel DataPanel = new Panel();
            //位置とサイズ
            DataPanel.Location = new Point(20,150);
            DataPanel.Size = new Size(310,300);
            //デバック用
            DataPanel.BackColor = Color.Red;
            //formに追加
            this.Controls.Add(DataPanel);

            DataGridView ScheduleDis = new DataGridView();
            ScheduleDis.Dock = DockStyle.Fill;
            DataPanel.Controls.Add(ScheduleDis);

            // データテーブルの定義
            DataTable dataTable = new DataTable();
            // テーブルの列を定義
            dataTable.Columns.Add("やること", typeof(string));
            dataTable.Columns.Add("優先度", typeof(int));
            dataTable.Columns.Add("期限", typeof(DateTime));

            ScheduleDis.DataSource = dataTable;

            // 初期データを追加
            //dataTable.Rows.Add("test", 25, DateTime.Now);

            

            //カラムの自動生成設定trueで自動
            //ScheduleDis.AutoGenerateColumns = true;

            /*
            //優先度のみ右寄せ
            ScheduleDis.Rows[0].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            //反映後じゃないとデータがない
            //列の大きさ変更、カラムの要素を代入
            DataGridViewColumn namecolumn = ScheduleDis.Columns[0];
            DataGridViewColumn ranckcolumn = ScheduleDis.Columns[1];
            DataGridViewColumn finishcolumn = ScheduleDis.Columns[2];

            //列の幅指定
            namecolumn.Width = 130;
            ranckcolumn.Width = 50;
            finishcolumn.Width = 110;
            */





        }

        //予定追加ボタン
        private void AddButton_Click(object sender,EventArgs e)
        {
            // パネルからデータグリッドビューを取得
            //DataGridView ScheduleDis = DataPanel.Controls.OfType<DataGridView>().FirstOrDefault();
            

            // データテーブルを取得
            DataTable dataTable = (DataTable)ScheduleDis.DataSource;

            // 新しいデータを追加
            dataTable.Rows.Add("new task", 10, DateTime.Now.AddDays(1));

            // データグリッドビューを更新
            //ScheduleDis.Refresh();


            //パネルのコントロールを取得
            //Control.ControlCollection controls = DataPanel.Controls;

            //DataGridView ScheduleDis = null;
            /*
            DataTable adddataTable = GetDataTable(dataTable); // データテーブルを取得する例
            //ScheduleDis.DataSource = dataTable; // データテーブルをDataGridViewにバインドする
    
            // テキストあれば処理
            if (string.IsNullOrEmpty(name.Text))
            {
                // 入力内容が空でない場合の処理
                //データの入力
                adddataTable.Rows.Add(name.Text, 0);

                //表に反映
                ScheduleDis.DataSource = adddataTable;
            }
            else
            {
                // 入力内容が空の場合の処理
                MessageBox.Show("やることを入力してください");
            }

            */

        }

        //予定削除ボタン
        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }


        //リンク集に飛ばす
        public void LinkButton_Click(object sender, EventArgs e)
        {
            //urlの指定
            string url = "https://www.google.com/?hl=ja";
            //urlを開く
            System.Diagnostics.Process.Start(url);
        }


        //???
        //時間戻す
        public void DeltimeButton_Click(object sender, EventArgs e)
        {

        }



    }
}

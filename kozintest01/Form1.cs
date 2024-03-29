﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace kozintest01
{
    public partial class Form1 : Form
    {
        //定義の宣言
        //予定、追加ボタン,削除
        private Button AddButton, DeleteButton;


        /*テキストボックス名前、期限
            コンボボックス優先度
            期限選択
        一列で表示
        */
        private TextBox nameTextbox;
        private ComboBox rank;
        private DateTimePicker finish;

        //入力名ラベル
        private Label nameDis, rankDis, finiDis;

        //現在時刻関数
        static DateTime date = DateTime.Now;

        //現在時間表示ラベル
        private Label TimeDis;


        //リンク集に飛ぶためのボタン宣言
        private Button LinkButton;

        //新しくformを開く
        private Button NewFormButton;

        //タイマー宣言
        //現在時刻のタイマー
        Timer nowTimer;

        //定義
        private Panel DataPanel;
        //表(一覧表示)
        private DataGridView ScheduleDis;
        //データベース
        private DataTable dataTable;

        //---------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            //追加
            //パネル
            InitializeDataPanel();
            //テーブル
            InitializeDataTable();
            //表
            InitializeDataGridView();

        }


        //DataPanelの設定関数
        private void InitializeDataPanel()
        {
            //パネル宣言
            DataPanel = new Panel();
            //位置
            DataPanel.Location = new Point(20, 150);
            //大きさ
            DataPanel.Size = new Size(310, 300);
            //テスト用背景色
            DataPanel.BackColor = Color.Red;
            //formに乗せる
            this.Controls.Add(DataPanel);
        }

        //Tableの設定関数
        private void InitializeDataTable()
        {
            //宣言
            dataTable = new DataTable();
            //カラムの入力
            dataTable.Columns.Add("やること", typeof(string));
            dataTable.Columns.Add("優先度", typeof(int));
            dataTable.Columns.Add("期限", typeof(DateTime));
        }

        //表の設定関数
        private void InitializeDataGridView()
        {
            //宣言
            ScheduleDis = new DataGridView();
            //大きさ
            ScheduleDis.Dock = DockStyle.Fill;
            //Tableを表に入れる
            ScheduleDis.DataSource = dataTable;
            //パネルの上に乗せる
            DataPanel.Controls.Add(ScheduleDis);

        }

        //フォルダ読み込み
        public void ReadFolder()
        {
            //フォルダパス
            string folderPath = "./TaskReadFolder";

            //フォルダの判定
            if (Directory.Exists(folderPath))
            {
                //フォルダがある
                //MessageBox.Show("フォルダがある");
            }
            else
            {
                // フォルダが存在しない場合の処理
                //フォルダを作る
                //MessageBox.Show("フォルダを作る");
                Directory.CreateDirectory(folderPath);
            }
            //ファイル読み込み
            ReadFile();
        }

        //ファイル読み込み
        public void ReadFile()
        {
            //パス
            string filePath = "./TaskReadFolder/Taskun.csv";
            //ファイルが存在する場合ファイルがあるか
            if (!File.Exists(filePath))
            {
                //MessageBox.Show("ファイルを作る");
                // ファイルが存在しない場合、新規に作成します
                using (StreamWriter sw = File.CreateText(filePath))
                {
                }
            }
            //ある場合読み込み
            else
            {
                //ファイルから読み込む(文字コードUTF-8)
                using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("UTF-8")))
                {
                    //ファイルの最後まで繰り返し
                    while (!reader.EndOfStream)
                    {
                        //1行ずつ取得
                        string dataLine = reader.ReadLine();
                        //エスケープ処理済みを取得
                        string[] values = ParseCsvLine(dataLine);

                        // 新しい行を作成し、値を追加する
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < values.Length; i++)
                        {
                            //文字列型にする
                            string value = values[i];
                            
                            row[i] = value;
                        }
                        //表に追加
                        dataTable.Rows.Add(row);
                    }
                }
            }
        }

        // CSVの1行を解析してデータを取得する
        private string[] ParseCsvLine(string line)
        {
            //CSVファイルの1行に含まれるデータを格納するためのリスト
            List<string> values = new List<string>();
            //データを一時的に格納するための文字列バッファ
            StringBuilder currentValue = new StringBuilder();
            //フラグ
            bool inQuotes = false;

            //lineの終わりまで繰り返す
            foreach (char c in line)
            {
                if (c == '"')
                {
                    // ダブルクオートの開始または終了
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    // 区切り文字（カンマ）で分割
                    values.Add(currentValue.ToString());
                    //初期化
                    currentValue.Clear();
                }
                else
                {
                    // 文字を追加
                    currentValue.Append(c);
                }
            }

            // 最後のデータを追加
            values.Add(currentValue.ToString());

            //文字の配列として返す
            return values.ToArray();
        }

        //ファイルへの書き込み
        public void WriteFile()
        {
            //ファイルに書き込み(文字コードUTF-8)
            using (StreamWriter writer = new StreamWriter("./TaskReadFolder/Taskun.csv", false, Encoding.UTF8))
            {
                //表の最後まで
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //1行のデータを取得
                    DataRow row = dataTable.Rows[i];
                    List<string> escapedValues = new List<string>();

                    //1行のデータを配列として終わりまで繰り返す
                    foreach (var item in row.ItemArray)
                    {
                        //文字列として取り出す
                        string value = item.ToString();
                        string escapedValue = value.Replace("\"", "\"\""); // ダブルクォーテーションをエスケープ
                        //カンマ、改行の判定
                        if (escapedValue.Contains(",") || escapedValue.Contains("\n"))
                        {
                            escapedValue = "\"" + escapedValue + "\""; // カンマや改行が含まれる場合は値をダブルクォーテーションで囲む
                        }
                        //リストに追加
                        escapedValues.Add(escapedValue);
                    }

                    string dataLine = string.Join(",", escapedValues);
                    //ファイルに書き込み
                    writer.WriteLine(dataLine);
                }
            }
            //MessageBox.Show("書き込み処理");
        }

        //追加ボタンの設定
        public void Addbottun()
        {
            //宣言
            AddButton = new System.Windows.Forms.Button();
            AddButton.FlatStyle = FlatStyle.Flat;
            AddButton.FlatAppearance.BorderSize = 0;

            //バックカラー
            AddButton.BackColor = Color.OrangeRed;
            //マウスが通る、押す色
            AddButton.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            AddButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //表示テキスト設定
            this.AddButton.Text = "追加";
            this.AddButton.Font = new Font("UTF-8", 10);
            this.AddButton.TextAlign = ContentAlignment.TopCenter;

            //位置(formからボタン分と少し引く)
            this.AddButton.Location = new Point(this.ClientSize.Width - AddButton.Width - 10, 150);
            //ボタン大きさ
            this.AddButton.Size = new System.Drawing.Size(50, 30);

            //イベント
            this.AddButton.Click += new EventHandler(this.AddButton_Click);
            this.AddButton.Parent = this;
            this.AddButton.BringToFront();
            this.ResumeLayout(false);
        }

        //削除ボタンの設定
        public void Deletebutton()
        {
            //宣言
            DeleteButton = new System.Windows.Forms.Button();
            DeleteButton.FlatStyle = FlatStyle.Flat;
            DeleteButton.FlatAppearance.BorderSize = 0;

            //バックカラー
            DeleteButton.BackColor = Color.LightBlue;
            //マウス押す、通る色
            DeleteButton.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            DeleteButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //表示テキスト設定
            this.DeleteButton.Text = "削除";
            this.DeleteButton.Font = new Font("UTF-8", 10);
            this.DeleteButton.TextAlign = ContentAlignment.TopCenter;

            //位置
            this.DeleteButton.Location = new Point(this.ClientSize.Width - DeleteButton.Width - 10, 400);
            //ボタン大きさ
            this.DeleteButton.Size = new System.Drawing.Size(50, 30);

            //イベント
            this.DeleteButton.Click += new EventHandler(this.DeleteButton_Click);
            this.DeleteButton.Parent = this;
            this.DeleteButton.BringToFront();
            this.ResumeLayout(false);
        }

        //やること名前テキストの設定
        public void SetNametext()
        {
            //名前入力テキストボックス
            nameTextbox = new TextBox();
            //フォント
            nameTextbox.Font = new Font("UTF-8", 10);
            //サイズ
            nameTextbox.Size = new Size(170, 100);
            //位置
            nameTextbox.Location = new Point(30, 100);

            //初期text
            nameTextbox.Text = "";

            //form追加
            this.Controls.Add(nameTextbox);

            // テキストボックスの TextChanged イベントハンドラの設定
            nameTextbox.TextChanged += nameTextbox_TextChanged;

        }

        // テキストボックスの TextChanged イベントハンドラ
        private void nameTextbox_TextChanged(object sender, EventArgs e)
        {
            // テキストボックスのテキストが変更された際に実行される処理
            // ここでテキストの反映や処理を行います
            TextBox textBox = (TextBox)sender; // イベント発生元のテキストボックスを取得
            string newText = textBox.Text; // 変更後のテキストを取得

        }

        //"やること"のラベル
        public void SetNameLabel()
        {
            //表示ラベル
            //作成
            nameDis = new System.Windows.Forms.Label();

            //表示
            nameDis.Text = "やること";
            //フォント
            nameDis.Font = new Font("UTF-8", 10);

            //文字色
            nameDis.ForeColor = Color.Black;
            //文字の位置
            nameDis.TextAlign = ContentAlignment.MiddleCenter;

            //位置("やること"表示位置)
            nameDis.Location = new Point(nameTextbox.Location.X - 10, 75);

            //formに追加
            this.Controls.Add(nameDis);
        }


        //優先度コンボボックス設定
        public void SetRank()
        {
            //優先度選択のコンボボックス
            rank = new ComboBox();
            //フォント
            rank.Font = new Font("UTF-8", 10);

            //サイズ
            rank.Size = new Size(40, 100);
            //位置(名前の右上座標+10)
            rank.Location = new Point(nameTextbox.Location.X + nameTextbox.Width + 10, 100);

            //優先度選択
            rank.Items.Add("1");
            rank.Items.Add("2");
            rank.Items.Add("3");
            rank.Items.Add("4");
            rank.Items.Add("5");

            //formに乗せる
            this.Controls.Add(rank);

        }

        //"優先度"ラベル
        public void SetRankLabel()
        {
            //表示ラベル
            //作成
            rankDis = new System.Windows.Forms.Label();

            //表示
            rankDis.Text = "優先度";
            //フォント
            rankDis.Font = new Font("UTF-8", 10);

            //色
            rankDis.ForeColor = Color.Black;
            //文字の位置
            rankDis.TextAlign = ContentAlignment.MiddleCenter;

            //位置("優先度"表示位置)
            rankDis.Location = new Point(rank.Location.X - 30, 75);

            //formに追加
            this.Controls.Add(rankDis);
        }

        //期限日設定
        public void SetFinish()
        {
            //期限選択
            finish = new DateTimePicker();
            //フォント
            finish.Font = new Font("UTF-8", 10);

            //サイズ
            finish.Size = new Size(120, 100);
            //位置(優先度の右上座標+10)
            finish.Location = new Point(rank.Location.X + rank.Width + 10, 100);

            //formに追加
            this.Controls.Add(finish);
        }

        //"期限日"ラベル
        public void SetFinishLabel()
        {
            //表示ラベル
            //作成
            finiDis = new System.Windows.Forms.Label();

            //表示
            finiDis.Text = "期限日";
            //フォント
            finiDis.Font = new Font("UTF-8", 10);

            //色
            finiDis.ForeColor = Color.Black;
            //文字の位置
            finiDis.TextAlign = ContentAlignment.MiddleCenter;

            //位置(やること表示位置)
            finiDis.Location = new Point(finish.Location.X - 5, 75);

            //formに追加
            this.Controls.Add(finiDis);
        }

        //リンクボタンの設定
        public void SetLinkButton()
        {
            //リンクボタン
            LinkButton = new System.Windows.Forms.Button();
            LinkButton.FlatStyle = FlatStyle.Flat;
            LinkButton.FlatAppearance.BorderSize = 0;
            LinkButton.FlatAppearance.BorderColor = Color.Black;

            //ボタンの色
            LinkButton.BackColor = Color.LightSkyBlue;
            LinkButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            LinkButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //テキスト設定
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
            //formに乗せる
            this.Controls.Add(this.LinkButton);
        }

        //アラームformを開く作るボタンの設定
        public void SetNewFormButton()
        {
            //ボタンの定義
            NewFormButton = new System.Windows.Forms.Button();
            NewFormButton.FlatStyle = FlatStyle.Flat;
            NewFormButton.FlatAppearance.BorderSize = 0;

            //ボタンの色
            NewFormButton.BackColor = Color.OrangeRed;
            NewFormButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            NewFormButton.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;

            //フォント右寄せ
            this.NewFormButton.Name = "Formbutton";
            this.NewFormButton.Text = "⌚";
            this.NewFormButton.Font = new Font("UTF-8", 10);
            this.NewFormButton.TextAlign = ContentAlignment.MiddleCenter;

            //ボタン大きさ
            this.NewFormButton.Size = new System.Drawing.Size(70, 60);
            //位置
            this.NewFormButton.Location = new Point(0, 0);

            //イベント
            this.NewFormButton.Click += new EventHandler(this.NewFormButton_Click);
            this.NewFormButton.Parent = this;
            this.NewFormButton.BringToFront();
            this.ResumeLayout(false);
            this.Controls.Add(NewFormButton);
        }

        //現在時刻タイマー
        public void SetnowTimer()
        {
            // タイマーの初期化
            nowTimer = new Timer();
            nowTimer.Interval = 1000; // 1秒ごとに更新
            nowTimer.Tick += Timer_Tick;
            //タイマーのスタート
            nowTimer.Start();
        }

        //現在時刻表示
        private void Timer_Tick(object sender, EventArgs e)
        {
            // タイマーのイベントハンドラでラベルのテキストを更新
            TimeDis.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        //現在時刻表示ラベル設定
        public void SetTimeDis()
        {
            //時間表示ラベル
            //作成
            TimeDis = new System.Windows.Forms.Label();

            //フォント
            TimeDis.Font = new Font("UTF-8", 10);

            //枠に色をつける
            TimeDis.BackColor = Color.SeaGreen;
            //色
            TimeDis.ForeColor = Color.Black;
            //文字の位置
            TimeDis.TextAlign = ContentAlignment.MiddleCenter;

            //ラベルの大きさ(formサイズ-(戻すボタン+進むボタン))
            TimeDis.Size = new System.Drawing.Size(this.ClientSize.Width - (NewFormButton.Width + LinkButton.Width), 60);
            //位置(DeltimeButtonの横幅を足した位置)
            TimeDis.Location = new Point(0 + NewFormButton.Width, 0);

            //formに追加
            this.Controls.Add(TimeDis);

            //タイマーの設定と表示
            SetnowTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Taskun";
            //フォーム背景色指定
            this.BackColor = Color.Aquamarine;

            //form指定
            this.Width = 450;
            this.Height = 500;

            //form拡大縮小の指定
            this.MaximumSize = new Size(450, 500);
            this.MinimumSize = new Size(450, 500);

            //---------------------------------------------------------------------------------------------
            //追加ボタン
            Addbottun();
            //削除ボタン
            Deletebutton();

            //---------------------------------------------------------------------------------------------
            //やること名前
            SetNametext();
            SetNameLabel();
            //---------------------------------------------------------------------------------------------
            //優先度
            SetRank();
            SetRankLabel();
            //---------------------------------------------------------------------------------------------
            //期限
            SetFinish();
            SetFinishLabel();
            //---------------------------------------------------------------------------------------------
            //リンクボタン
            SetLinkButton();
            //---------------------------------------------------------------------------------------------
            //アラームボタン
            SetNewFormButton();
            //---------------------------------------------------------------------------------------------
            //現在時刻表示
            SetTimeDis();
            //反映後じゃないとデータがない
            //列の大きさ変更、カラムの要素を代入
            DataGridViewColumn namecolumn = ScheduleDis.Columns[0];
            DataGridViewColumn ranckcolumn = ScheduleDis.Columns[1];
            DataGridViewColumn finishcolumn = ScheduleDis.Columns[2];

            //列の右寄せ
            ranckcolumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //列の幅指定
            namecolumn.Width = 130;
            ranckcolumn.Width = 50;
            finishcolumn.Width = 110;
            //ファイルからの読み込み
            ReadFolder();
        }

        
        //予定追加ボタン動作関数
        private void AddButton_Click(object sender,EventArgs e)
        {
            //文字列に代入
            string temp = nameTextbox.Text;
            int selectedIndex = rank.SelectedIndex; // 選択された項目のインデックス
            DateTime selectedDate = finish.Value;

            //やることを入力していたなら(textboxがnullになっている)
            if (!string.IsNullOrEmpty(temp))
            {
                // 表に追加する処理(やること、優先度、期限)
                dataTable.Rows.Add(temp, (selectedIndex + 1), selectedDate);

                //追加
                //ファイル書き込み
                WriteFile();
            }
            else
            {
                MessageBox.Show("やることを入力してください");
            }

            //フォーカスをテキストボックスに変更
            nameTextbox.Focus();

        }

        //予定削除ボタン
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // DataGridViewで選択された行を取得
            if (ScheduleDis.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ScheduleDis.SelectedRows[0];

                //選択された行を削除
                //削除で行を詰める
                ScheduleDis.Rows.Remove(selectedRow);

                //削除
                //ファイル書き込み
                WriteFile();
            }
            else
            {
                //選択されずにボタンを押されたとき
                MessageBox.Show("削除する行を選択してください。");
            }
            //フォーカスをテキストボックスに変更
            nameTextbox.Focus();
        }


        //リンク集に飛ばす
        public void LinkButton_Click(object sender, EventArgs e)
        {
            //urlの指定
            string url = "https://www.google.com/?hl=ja";
            //urlを開く
            System.Diagnostics.Process.Start(url);
        }

        //formを作るボタン
        public void NewFormButton_Click(object sender, EventArgs e)
        {
            //ボタンを無効にすることでformの無限生成を止める
            NewFormButton.Enabled = false;

            //新しいform
            AddForm childForm = new AddForm();
            childForm.FormClosed += ChildForm_FormClosed; // 子フォームの閉じられたイベントにハンドラを追加
            childForm.Show();
        }

        //formが閉じられた時の処理
        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ボタンを有効にする
            NewFormButton.Enabled = true;
        }
    }

    //新たなform
    public class AddForm : Form
    {
        //タイマー
        private Timer AratTimer;
        //ボタン
        private Button timeButton;

        //入力テキストボックス
        private TextBox timetextBox;

        //formの設定
        public void SetForm()
        {
            this.Text = "アラーム(秒)";
            //フォーム背景色指定
            this.BackColor = Color.Orange;

            //form指定
            this.Width = 150;
            this.Height = 150;

            // フォームの最大化ボタンと最小化ボタンを非表示にする
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        //テキストボックスの設定
        public void SetTimeText()
        {
            //宣言
            timetextBox = new TextBox();
            //サイズ横縦
            timetextBox.Width = 100;
            timetextBox.Height = 50;
            timetextBox.Font = new Font("UTF-8",10);
            //文字の右寄せ
            timetextBox.TextAlign = HorizontalAlignment.Right;

            //位置
            timetextBox.Location = new Point(25,50);

            //キーボードが押された時の判定(数字のみ可)
            timetextBox.KeyPress += NumericTextBox_KeyPress; // KeyPressイベントハンドラを追加
            //form
            Controls.Add(timetextBox);
        }

        //テキストボックス数値のみ入力の関数
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 入力されたキーが数値でない場合はイベントをキャンセル
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b') // 数字でないかつバックスペースでない場合
            {
                e.Handled = true;
            }
        }

        //タイマーボタンの設定
        //開始
        public void TimeButton()
        {
            //宣言
            timeButton = new Button();
            //テキスト
            timeButton.Text = "Start Timer";
            //イベント
            timeButton.Click += TimeButton_Click;

            //位置
            timeButton.Location = new Point(50, 80);
            //form
            Controls.Add(timeButton);
        }

        //ボタンを押すとタイマーの制御
        private void TimeButton_Click(object sender, EventArgs e)
        {
            //入力されていないか0以下なら
            if (string.IsNullOrEmpty(timetextBox.Text) || int.Parse(timetextBox.Text) <= 0)
            {
                MessageBox.Show("1以上の数値を入力してください");
            }
            else
            {
                //フラグの判定
                if (AratTimer.Enabled == false)
                {
                    AratTimer.Enabled = true; // タイマーを開始
                    //テキストの変更
                    timeButton.Text = "Stop Timer";
                }
                else
                {
                    AratTimer.Enabled = false; // タイマーを停止
                                               //テキストの変更
                    timeButton.Text = "Start Timer";
                }

            }
            
        }

        //タイマーの設定
        public void AratSetTimer()
        {
            //宣言
            AratTimer = new Timer();
            AratTimer.Interval = 1000; // タイマーの間隔を1秒に設定
            //タイマーの無効
            AratTimer.Enabled = false;
            AratTimer.Tick += AratTimer_Tick; // タイマーのTickイベントハンドラを追加
        }

        //タイマーの動作(1秒間)
        private void AratTimer_Tick(object sender, EventArgs e)
        {
            // タイマーのTickイベントが発生したときに実行される処理
            if(int.Parse(timetextBox.Text) <= 0)
            {
                //時間切れ
                AratTimer.Enabled = false;
                // 警告音を再生
                SystemSounds.Exclamation.Play();
                //通知
                MessageBox.Show("終わり");
                timeButton.Text = "Start Timer";
                //フォーカスを持っていく
                timetextBox.Focus();

            }
            else
            {
                //1ずつ減らす処理
                int temp;
                //intに変換
                temp = int.Parse(timetextBox.Text);
                temp--;
                //文字に直す
                timetextBox.Text = temp.ToString();
            }

        }

        //表示主に関数で制御
        public AddForm()
        {
            //formの設定呼び出し
            SetForm();
            //タイマーの設定呼び出し
            AratSetTimer();
            //タイマーボタンの設定呼び出し
            TimeButton();
            //テキストボックスの呼び出し
            SetTimeText();

            //テキストボックスにフォーカス
            timetextBox.Focus();
        }


    }

}

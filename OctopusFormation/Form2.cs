namespace OctopusFormation
{
    public partial class Form2 : Form
    {
        /// <summary>タコ情報</summary>
        private Octopus[] member;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form2()
        {
            InitializeComponent();

            member = new Octopus[20];
            for (int i = 0; i < member.Length; i++)
            {
                member[i] = new Octopus(new Point(400, 300));
            }

            this.FormBorderStyle = FormBorderStyle.None; // タイトルバーや縁をなくす
            this.BackColor = Color.White; // フォームの背景色を白にする
            this.TransparencyKey = Color.White; // フォームの透過色として白を指定する
            this.WindowState = FormWindowState.Maximized; // フォームを全画面にする
        }

        /// <summary>
        /// フォーム描画イベント
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (member != null)
            {
                for (int i = 0; i < member.Length; i++)
                {
                    e.Graphics.DrawImage(imageList1.Images[0], member[i].P);
                    //e.Graphics.DrawString(i.ToString(), new Font("MS ゴシック", 8), Brushes.AliceBlue, member[i].P.X + 4, member[i].P.Y - 10);
                }
            }
        }

        /// <summary>
        /// 移動座標増分データ
        /// </summary>
        private readonly Dictionary<Keys, Point> Velocity = new Dictionary<Keys, Point>()
        {
            { Keys.Right, new Point(32, 0) },
            { Keys.Left, new Point(-32, 0) },
            { Keys.Up, new Point(0, -32) },
            { Keys.Down, new Point(0, 32) },
        };

        /// <summary>
        /// タコを移動させる
        /// </summary>
        /// <param name="p">現在座標</param>
        /// <param name="v">座標増分</param>
        /// <returns>移動後の座標</returns>
        private Point MoveObject(Point p, Point v)
        {
            int x = p.X + v.X;
            int y = p.Y + v.Y;
            if (x < 0) x = 0;
            if (x > this.ClientSize.Width - 32) x = this.ClientSize.Width - 32;
            if (y < 0) y = 0;
            if (y > this.ClientSize.Height - 32) y = this.ClientSize.Height - 32;
            return new Point(x, y);
        }

        /// <summary>
        /// フォームキー押下イベント
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Velocity.ContainsKey(e.KeyCode))
            {
                Point v = Velocity[e.KeyCode];
                for (int i = member.Length - 1; i > 0; i--)
                {
                    member[i].V = member[i - 1].V;
                    member[i].P = MoveObject(member[i].P, member[i].V);
                }
                member[0].V = v;
                member[0].P = MoveObject(member[0].P, member[0].V);

                Refresh();
            }
        }

        /// <summary>マウスポインタの位置保持用</summary>
        private Point currentMouseP = new Point(0, 0);

        /// <summary>
        /// タイマーTICK時処理
        /// </summary>
        /// <param name="sender">イベント発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // マウスポインタの位置を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            int y = System.Windows.Forms.Cursor.Position.Y;

            // マウスポインタが移動していたら処理
            if (currentMouseP.X != x || currentMouseP.Y != y)
            {
                currentMouseP = new Point(x, y);
                var v = new Point((x - member[0].P.X) / 5, (y - member[0].P.Y) / 5);
                for (int i = member.Length - 1; i > 0; i--)
                {
                    member[i].V = member[i - 1].V;
                    member[i].P = MoveObject(member[i].P, member[i].V);
                }
                member[0].V = v;
                member[0].P = MoveObject(member[0].P, member[0].V);
                Refresh();
            }
        }
    }
}
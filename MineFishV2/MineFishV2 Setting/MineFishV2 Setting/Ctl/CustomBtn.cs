using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MineFishV2_Setting.Ctl {
    public partial class UserBtn : UserControl {
        private string text = "test";

        [Category("User")]
        public string LabelText {
            get { return text; }
            set {
                text = value;
                textLabel.Text = text;
            }
        }

        [Category("User")]
        public Color Color1 { get; set; } = Color.FromArgb(248, 88, 162);

        [Category("User")]
        public Color Color2 { get; set; } = Color.FromArgb(254, 88, 92);

        [Browsable(true)]
        [Category("User")]
        public event EventHandler OnClick;

        public UserBtn() {
            InitializeComponent();
        }

        private void UserBtn_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            Brush brush = new LinearGradientBrush(rectangle, Color1, Color2, 65f);
            graphics.FillRectangle(brush, rectangle);
        }

        private void UserBtn_Resize(object sender, EventArgs e) {
            Utils.smoothBorder(this, Height);
            Refresh();
        }

        private void textLabel_Click(object sender, EventArgs e) {
            OnClick?.Invoke(this, e);
        }
    }
}

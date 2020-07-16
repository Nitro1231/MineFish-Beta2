using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MineFishV2_Setting.Ctl {
    public partial class Link : UserControl {
        Image img = null;

        [Category("User")]
        public string URL { get; set; } = "";

        [Category("User")]
        public Image image {
            get {
                return img;
            }
            set {
                img = value;
                iconBox.Image = image;
            }
        }

        [Category("User")]
        public Color Color1 { get; set; } = Color.FromArgb(248, 88, 162);

        [Category("User")]
        public Color Color2 { get; set; } = Color.FromArgb(254, 88, 92);

        public Link() {
            InitializeComponent();
            Utils.smoothBorder(this, Height);
            iconBox.Size = new Size(Width - 10, Height - 10);
            iconBox.Image = image;
        }

        private void iconBox_Resize(object sender, EventArgs e) {
            iconBox.Size = new Size(Width - 10, Height - 10);
            Utils.smoothBorder(this, Height);
        }

        private void iconBox_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(URL);
        }

        private void Link_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            Brush brush = new LinearGradientBrush(rectangle, Color1, Color2, 65f);
            graphics.FillRectangle(brush, rectangle);
        }
    }
}

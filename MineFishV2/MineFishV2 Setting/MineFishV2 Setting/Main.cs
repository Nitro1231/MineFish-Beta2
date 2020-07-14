using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MineFishV2_Setting {
    public partial class Main : Form {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        string programPath;
        string imgPath;

        public Main() {
            InitializeComponent();
            mainPanel.Height = Height - topPanel.Height + 20;
            centerPanel.Left = (Width - centerPanel.Width) / 2;
            snsPanel.Location = new Point((Width - snsPanel.Width) / 2, mainPanel.Height - snsPanel.Height - 20);
            verLabel.Location = new Point(mainPanel.Width - verLabel.Width - 5, mainPanel.Height - verLabel.Height);
            Utils.smoothBorder(this, 20);
            Utils.smoothBorder(mainPanel, 20);

            programPath = Directory.GetCurrentDirectory();
            imgPath = programPath + "\\img";

            if (Directory.Exists(imgPath)) {
                foreach (string filePath in Directory.GetFiles(imgPath))
                    imageBox.Items.Add(Path.GetFileName(filePath));
            }

            if (File.Exists(Process.GetCurrentProcess() + "\\config.ini")) {
                var Myini = new IniFile("config.ini");
                imageBox.Text = Myini.Read("image");
                startX.Text = Myini.Read("starting_x");
                startY.Text = Myini.Read("starting_Y");
                sizeX.Text = Myini.Read("size_x");
                sizeY.Text = Myini.Read("size_Y");
            }
        }

        private void autoDetect_OnClick(object sender, EventArgs e) {
            Process processes = Process.GetProcessesByName("javaw").FirstOrDefault();
            if (processes != null) {
                RECT r = new RECT();
                GetWindowRect(processes.MainWindowHandle, ref r);

                if (imageBox.SelectedText == "" && imageBox.Items.Count != 0) {
                    imageBox.SelectedIndex = 0;
                }

                startX.Text = ((double)(r.Right - r.Left) * 2 / 3).ToString();
                startY.Text = ((double)(r.Bottom - r.Top) * 1 / 4).ToString();
                sizeX.Text = ((double)(r.Right - r.Left) * 1 / 3).ToString();
                sizeY.Text = ((double)(r.Bottom - r.Top) * 3 / 4).ToString();


                Bitmap bitmap = new Bitmap((int)((double)(r.Right - r.Left) * 1 / 3), (int)((double)(r.Bottom - r.Top) * 3 / 4));
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(r.Left + (int)((double)(r.Right - r.Left) * 2 / 3), r.Top + (int)((double)(r.Bottom - r.Top) * 1 / 4), 0, 0, bitmap.Size);

                ImagePreview imgP = new ImagePreview(bitmap);
                imgP.Show();
                //pictureBox1.Image = bitmap;
            }
        }

        private void saveBtn_OnClick(object sender, EventArgs e) {
            var Myini = new IniFile("config.ini");
            Myini.Write("image", imageBox.SelectedText);
            Myini.Write("starting_x", startX.Text);
            Myini.Write("starting_y", startY.Text);
            Myini.Write("size_x", sizeX.Text);
            Myini.Write("size_y", sizeY.Text);
        }

        private void topPanel_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            Rectangle rectangle = new Rectangle(0, 0, topPanel.Width, topPanel.Height);
            Brush brush = new LinearGradientBrush(rectangle, Color.FromArgb(40, 242, 156), Color.FromArgb(12, 184, 224), 65f);
            graphics.FillRectangle(brush, rectangle);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e) {
            Utils.mouseMove(Handle);
        }
    }
}

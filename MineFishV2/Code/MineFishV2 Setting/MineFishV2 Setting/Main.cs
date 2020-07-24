using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;

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
        Process mineFishV2;
        Process minecraft;

        public Main() {
            InitializeComponent();

            mainPanel.Height = Height - topPanel.Height + 20;
            centerPanel.Left = (Width - centerPanel.Width) / 2;
            snsPanel.Location = new Point((Width - snsPanel.Width) / 2, mainPanel.Height - snsPanel.Height - 20);
            verLabel.Location = new Point(mainPanel.Width - verLabel.Width - 5, mainPanel.Height - verLabel.Height);
            
            minBox.Image = IconChar.CompressAlt.ToBitmap(50, Color.White);
            closeBox.Image = IconChar.Times.ToBitmap(50, Color.White);

            mailLink.image = IconChar.At.ToBitmap(50, Color.White);
            blogLink.image = IconChar.PaperPlane.ToBitmap(50, Color.White);
            gitLink.image = IconChar.Github.ToBitmap(50, Color.White);
            ytLink.image = IconChar.Youtube.ToBitmap(50, Color.White);
            tLink.image = IconChar.Twitter.ToBitmap(50, Color.White);
            dcLink.image = IconChar.Discord.ToBitmap(50, Color.White);

            Utils.smoothBorder(this, 20);
            Utils.smoothBorder(mainPanel, 20);

            programPath = Directory.GetCurrentDirectory();
            imgPath = programPath + @"\Core\img";

            getImage();

            if (File.Exists(programPath + @"\Core\config.ini")) {
                var Myini = new IniFile(@".\Core\config.ini");
                imageBox.Text = Myini.Read("image");
                startX.Text = Myini.Read("starting_x");
                startY.Text = Myini.Read("starting_Y");
                sizeX.Text = Myini.Read("size_x");
                sizeY.Text = Myini.Read("size_Y");
            }
        }

        private void getImage() {
            string preselected = imageBox.Text;

            imageBox.Items.Clear();
            if (Directory.Exists(imgPath))
                foreach (string filePath in Directory.GetFiles(imgPath))
                    imageBox.Items.Add(Path.GetFileName(filePath));

            if (preselected.Equals(""))
                if (imageBox.Items.Count != 0)
                    imageBox.SelectedIndex = 0;
            else
                imageBox.Text = preselected;
        }

        private void autoDetect_OnClick(object sender, EventArgs e) {
            Process processes = Process.GetProcessesByName("javaw").FirstOrDefault();
            if (processes != null) {
                getImage();

                RECT r = new RECT();
                GetWindowRect(processes.MainWindowHandle, ref r);

                startX.Text = (r.Left + (int)((double)(r.Right - r.Left) * 2 / 3)).ToString();
                startY.Text = (r.Top + (int)((double)(r.Bottom - r.Top) * 1 / 4)).ToString();
                sizeX.Text = ((int)((double)(r.Right - r.Left) * 1 / 3)+20).ToString();
                sizeY.Text = ((int)((double)(r.Bottom - r.Top) * 3 / 4)+20).ToString();

                Bitmap bitmap = new Bitmap((int)((double)(r.Right - r.Left) * 1 / 3) + 20, (int)((double)(r.Bottom - r.Top) * 3 / 4) + 20);
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(r.Left + (int)((double)(r.Right - r.Left) * 2 / 3), r.Top + (int)((double)(r.Bottom - r.Top) * 1 / 4), 0, 0, bitmap.Size);

                ImagePreview imgP = new ImagePreview(bitmap);
                imgP.Show();
            } else {
                MessageBox.Show("Cannot detect the Minecraft process. Play Minecraft first and then try again.", "Cannot find process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveBtn_OnClick(object sender, EventArgs e) {
            try {
                var Myini = new IniFile(@".\Core\config.ini");
                Myini.Write("image", imageBox.SelectedText);
                Myini.Write("starting_x", startX.Text);
                Myini.Write("starting_y", startY.Text);
                Myini.Write("size_x", sizeX.Text);
                Myini.Write("size_y", sizeY.Text);
                MessageBox.Show("Saved successfully!", "Config Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception err) {
                MessageBox.Show("An error occurred while saving 'config.ini' file. If this error keep occurred, please contact to the developer with following information:\n" + err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startBtn_OnClick(object sender, EventArgs e) {
            minecraft = Process.GetProcessesByName("javaw").FirstOrDefault();
            if (minecraft != null) {
                try {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = Path.GetFullPath(programPath + @"\Core\Core.exe");
                    start.WorkingDirectory = Path.GetFullPath(programPath + @"\Core");

                    mineFishV2 = Process.Start(start);
                    Hide();
                    checkMinTimer.Enabled = true;
                } catch (Exception err) {
                    MessageBox.Show("An error occurred while starting 'core.exe' file. If this error keep occurred, please contact to the developer with following information:\n" + err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Cannot detect the Minecraft process. Play Minecraft first and then try again.", "Cannot find process", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkMinTimer_Tick(object sender, EventArgs e) {
            minecraft = Process.GetProcessesByName("javaw").FirstOrDefault();
            if (minecraft == null) {
                try {
                    mineFishV2.Kill();
                } catch { }
                checkMinTimer.Enabled = false;
                getImage();
                Show();
            } else if (!isRunning(mineFishV2)) {
                checkMinTimer.Enabled = false;
                getImage();
                Show();
            }
        }

        private bool isRunning(Process process) {
            try { Process.GetProcessById(process.Id); }
            catch (Exception) { return false; }
            return true;
        }

        private void minBox_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        private void closeBox_Click(object sender, EventArgs e) {
            Close();
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

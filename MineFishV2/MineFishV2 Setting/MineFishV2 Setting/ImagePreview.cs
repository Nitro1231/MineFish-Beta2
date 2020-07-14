using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineFishV2_Setting {
    public partial class ImagePreview : Form {
        public ImagePreview(Image img) {
            InitializeComponent();
            Size = img.Size;
            pictureBox1.Image = img;
        }
    }
}

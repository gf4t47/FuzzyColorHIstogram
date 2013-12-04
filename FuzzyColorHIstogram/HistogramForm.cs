using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCH
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
        }

        private string openImg()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "img files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }

            return null;
        }

        private void showImg(PictureBox p, Bitmap img)
        {
            p.SizeMode = PictureBoxSizeMode.Zoom;
            p.Image = img;
        }

        private Bitmap CCH(string filename)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);
            ColorHistogram ch = new ColorHistogram();
            Image<Bgr, Byte> ret = ch.saveRGB(img, 4);

            return ret.ToBitmap();
        }

        private Bitmap FuzzyCH(string filename)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);
            FuzzyColorHistogram fch = new FuzzyColorHistogram();
            Image<Bgr, Byte> ret = fch.GenerateRGBHistImage(fch.calcFCH(img));

            return ret.ToBitmap();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filename = openImg();
            if(File.Exists(filename))
            {
                Bitmap src = new Bitmap(filename);
                showImg((PictureBox)sender, src);

                Bitmap cch = CCH(filename);
                showImg(pictureBoxCCH1, cch);

                Bitmap fch = FuzzyCH(filename);
                showImg(pictureBoxFCH1, fch);
            }
            

                
     

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string filename = openImg();

            if (File.Exists(filename))
            {
                Bitmap src = new Bitmap(filename);
                showImg((PictureBox)sender, src);

                Bitmap cch = CCH(filename);
                showImg(pictureBoxCCH2, cch);

                Bitmap fch = FuzzyCH(filename);
                showImg(pictureBoxFCH2, fch);
            }
        }
    }
}

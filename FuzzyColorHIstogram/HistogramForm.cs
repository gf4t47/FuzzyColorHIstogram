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

        private Bitmap CCH(Bitmap src)
        {
            if(src == null)
            {
                return null;
            }
            
            Image<Bgr, Byte> img = new Image<Bgr, byte>(src);
            ColorHistogram ch = new ColorHistogram();
            Image<Bgr, Byte> ret = ch.saveRGB(img, 4);

            return ret.ToBitmap();
        }

        private Bitmap FuzzyCH(string filename, int n_, int n, double m, double e)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(filename);
            FuzzyColorHistogram fch = new FuzzyColorHistogram(n_, n, m, e);
            Image<Bgr, Byte> ret = fch.GenerateRGBHistImage(fch.calcFCH(img));

            return ret.ToBitmap();
        }

        private Bitmap FuzzyCH(Bitmap src, int n_, int n, double m, double e)
        {
            if (src == null)
            {
                return null;
            }

            Image<Bgr, Byte> img = new Image<Bgr, byte>(src);
            FuzzyColorHistogram fch = new FuzzyColorHistogram(n_, n, m, e);
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
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string filename = openImg();

            if (File.Exists(filename))
            {
                Bitmap src = new Bitmap(filename);
                showImg((PictureBox)sender, src);
            }
        }

        private void CalcCCH1_Click(object sender, EventArgs e)
        {
            Bitmap cch = CCH((Bitmap)pictureBoxSrc1.Image);
            showImg(pictureBoxCCH1, cch);
        }

        private void CalcFCH1_Click(object sender, EventArgs e)
        {
            int iN_1 = Convert.ToInt32(n_1.Text);
            int iN1 = Convert.ToInt32(n1.Text);
            double dM1 = Convert.ToDouble(m1.Text);
            double dE1 = Convert.ToDouble(e1.Text);

            Bitmap fch = FuzzyCH((Bitmap)pictureBoxSrc1.Image, iN_1, iN1, dM1, dE1);
            showImg(pictureBoxFCH1, fch);
        }

        private void CalcCCH2_Click(object sender, EventArgs e)
        {
            Bitmap cch = CCH((Bitmap)pictureBoxSrc2.Image);
            showImg(pictureBoxCCH2, cch);
        }

        private void CalcFCH2_Click(object sender, EventArgs e)
        {
            int iN_2 = Convert.ToInt32(n_2.Text);
            int iN2 = Convert.ToInt32(n2.Text);
            double dM2 = Convert.ToDouble(m2.Text);
            double dE2 = Convert.ToDouble(e2.Text);

            Bitmap fch = FuzzyCH((Bitmap)pictureBoxSrc2.Image, iN_2, iN2, dM2, dE2);
            showImg(pictureBoxFCH2, fch);
        }
    }
}

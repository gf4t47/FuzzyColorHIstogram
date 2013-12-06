using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using FuzzyColorHIstogram.FCH;
using FuzzyColorHIstogram.Properties;

namespace FuzzyColorHIstogram
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
        }

        private static string OpenImg()
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = Resources.HistogramForm_openImg_img_files____jpg____jpg_All_files__________,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }

            return null;
        }

        private Bitmap CCH(Bitmap src)
        {
            if(src == null)
            {
                return null;
            }
            
            var img = new Image<Bgr, byte>(src);
            var ch = new ColorHistogram();
            var ret = ch.SaveRGB(img, 4);

            return ret.ToBitmap();
        }

        private Bitmap FuzzyCH(Bitmap src, int n_, int n, double m, double e)
        {
            if (src == null)
            {
                return null;
            }

            var img = new Image<Bgr, byte>(src);
            var fch = new FuzzyColorHistogram(n_, n, m, e);
            var ret = fch.GenerateRGBHistImage(fch.CalcFCH(img));

            return ret.ToBitmap();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filename = OpenImg();
            if (!File.Exists(filename)) return;
            var src = new Bitmap(filename);
            showImg((PictureBox)sender, src);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var filename = OpenImg();

            if (!File.Exists(filename)) return;
            var src = new Bitmap(filename);
            showImg((PictureBox)sender, src);
        }

        private void CalcCCH1_Click(object sender, EventArgs e)
        {
            var cch = CCH((Bitmap)pictureBoxSrc1.Image);
            showImg(pictureBoxCCH1, cch);
        }

        private void CalcFCH1_Click(object sender, EventArgs e)
        {
            var iN_1 = Convert.ToInt32(n_1.Text);
            var iN1 = Convert.ToInt32(n1.Text);
            var dM1 = Convert.ToDouble(m1.Text);
            var dE1 = Convert.ToDouble(e1.Text);

            var fch = FuzzyCH((Bitmap)pictureBoxSrc1.Image, iN_1, iN1, dM1, dE1);
            showImg(pictureBoxFCH1, fch);
        }

        private void CalcCCH2_Click(object sender, EventArgs e)
        {
            var cch = CCH((Bitmap)pictureBoxSrc2.Image);
            showImg(pictureBoxCCH2, cch);
        }

        private void CalcFCH2_Click(object sender, EventArgs e)
        {
            var iN_2 = Convert.ToInt32(n_2.Text);
            var iN2 = Convert.ToInt32(n2.Text);
            var dM2 = Convert.ToDouble(m2.Text);
            var dE2 = Convert.ToDouble(e2.Text);

            var fch = FuzzyCH((Bitmap)pictureBoxSrc2.Image, iN_2, iN2, dM2, dE2);
            showImg(pictureBoxFCH2, fch);
        }
    }
}

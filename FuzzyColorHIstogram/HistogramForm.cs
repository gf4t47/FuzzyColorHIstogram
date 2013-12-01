using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyColorHIstogram
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
        }

        public void show(string filename)
        {
            Image<Bgr, Byte> imageSource = new Image<Bgr, byte>(filename);

            HistogramViewer hv = new HistogramViewer();
            hv.Text = "RGB直方图";
            hv.ShowInTaskbar = false;
            hv.HistogramCtrl.GenerateHistograms(imageSource, 256);
            //hv.WindowState = FormWindowState.Maximized;
            hv.Show();
            imageSource.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create images for "Hatch Styles" ImageComboBoxEdit            
            CreateHatchStyleItems();


            // create images for "Custom Colors" ImageComboBoxEdit
            CreateCustomColorsItems();

            // in-place editing
            CreatedGridControlSource();

        }

        private void CreatedGridControlSource()
        {
            BindingList<Test> source = new BindingList<Test>();
            source.Add(new Test() { Style = HatchStyle.DarkDownwardDiagonal, CustomColor = Color.Red });
            source.Add(new Test() { Style = HatchStyle.DarkVertical, CustomColor = Color.Yellow });
            source.Add(new Test() { Style = HatchStyle.Percent10, CustomColor = Color.Brown });
            source.Add(new Test() { Style = HatchStyle.Trellis, CustomColor = Color.White });

            gridControl1.DataSource = source;
            gridControl1.ForceInitialize();

            RepositoryItemImageComboBox reHS = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as RepositoryItemImageComboBox;
            reHS.Assign(imageComboBoxEdit1.Properties);
            gridView1.Columns["Style"].ColumnEdit = reHS;


            RepositoryItemImageComboBox reColors = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as RepositoryItemImageComboBox;
            reColors.Assign(imageComboBoxEdit2.Properties);
            gridView1.Columns["CustomColor"].ColumnEdit = reColors;
        }

        private void CreateCustomColorsItems()
        {
            List<Color> customColors = new List<Color>();
            customColors.AddRange(new Color[] { Color.Red, Color.Black, Color.Yellow, Color.Green, Color.Blue, Color.Brown, Color.White });
            foreach (Color color in customColors)
            {
                imageComboBoxEdit2.Properties.Items.Add(new ImageComboBoxItem(color.ToString(), color));
            }

            ImageList imagesColors = new ImageList();
            imageComboBoxEdit2.Properties.SmallImages = imagesColors;

            foreach (ImageComboBoxItem item in imageComboBoxEdit2.Properties.Items)
            {
                int iWidth = 16;
                int iHeight = 16;
                Bitmap bmp = new Bitmap(iWidth, iHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, iWidth, iHeight);
                    g.FillRectangle(new SolidBrush((Color)item.Value), 1, 1, iWidth - 2, iHeight - 2);

                }
                imagesColors.Images.Add(bmp);
                item.ImageIndex = imagesColors.Images.Count - 1;
            }
        }

        private void CreateHatchStyleItems()
        {
            imageComboBoxEdit1.Properties.Items.AddEnum(typeof(System.Drawing.Drawing2D.HatchStyle));

            ImageList imagesHS = new ImageList();
            imageComboBoxEdit1.Properties.SmallImages = imagesHS;

            foreach (ImageComboBoxItem item in imageComboBoxEdit1.Properties.Items)
            {
                int iWidth = 16;
                int iHeight = 16;
                Bitmap bmp = new Bitmap(iWidth, iHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, iWidth, iHeight);
                    HatchBrush b = new HatchBrush((HatchStyle)item.Value, Color.Black, Color.White);
                    g.FillRectangle(b, 0, 0, iWidth, iHeight);

                }
                imagesHS.Images.Add(bmp);
                item.ImageIndex = imagesHS.Images.Count - 1;
            }
        }
    }

    public class Test
    {
        public HatchStyle Style { get; set; }
        public Color CustomColor { get; set; }
    }
}

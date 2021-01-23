using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Bunifu.UI.WinForm;

namespace prog_joyeria
{
    partial class Form1 : IDisposable
    {


        private int SelectedImageIndex = 0;
        private List<byte[]> LoadedImages { get; set; }

        byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public Image ConvertBytesToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                return Image.FromStream(ms);
            }
        }
        //public void Update(byte[] image)
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString))
        //    {
        //        if (cn.State == ConnectionState.Closed)
        //        {
        //            cn.Open();
    //                using (SqlCommand cmd = new SqlCommand($"update Stock set Joya="'{image}'",cn))
    //                {
    //    cmd.CommandType = CommandType.Text;
    //    cmd.Parameters.AddWithValue("@Joyas", image);
    //    cmd.ExecuteNonQuery();
    //}

    //        }
    //    }
    //}


    private void LoadData()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    using (DataTable dt=new DataTable("Pictures"))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("select * from Stock",cn);
                        adapter.Fill(dt);
                        stock.DataSource = dt;
                    }
                }
            }          
            var imageColumn = (DataGridViewImageColumn)stock.Columns["joyaDataGridViewImageColumn1"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            stock.RowTemplate.Height = 100;
        }
        public Image FastLoad(string path)
        {
            if (File.Exists(path))
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                    return Image.FromStream(ms);
            }
            else
            {
                MessageBox.Show($"No se encuentra la imagen ({path}) en la carpeta joya");
                return Image.FromFile(mainPath + "\\Joyas\\imagen_preview.jpg");
            }
           
        }

        private void LoadImagesFromFolder(string[] paths)
        {

            LoadedImages = new List<byte[]>();
            
            foreach (string path in paths)

            {
                using (var tempImage = Image.FromFile(path))
                {
                    LoadedImages.Add(ConvertImageToBytes(tempImage));
                    
                }

                //Image tempImage =FastLoad(path);
                
                //LoadedImages.Add(tempImage);
                

            }

        }


        //private void buscarListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    if (buscarListView.SelectedIndices.Count > 0)
        //    {
        //        var selectedIndex = buscarListView.SelectedIndices[0];
        //        Image selectedImg = LoadedImages[selectedIndex];
        //        buscarImg.Image = selectedImg;
        //        SelectedImageIndex = selectedIndex;
        //    }
        //}

        //private void button_navigation(object sender, EventArgs e)
        //{
        //    var clickedButton = sender as Button;
        //    if (clickedButton.Text.Equals("Previous"))
        //    {
        //        if (SelectedImageIndex > 0)
        //        {
        //            SelectedImageIndex -= 1;
        //            Image selectedImg = LoadedImages[SelectedImageIndex];
        //            buscarImg.Image = selectedImg;
        //            SelectTheClickedItem(buscarListView, SelectedImageIndex);
        //        }

        //    }
        //    else
        //    {
        //        if (SelectedImageIndex < (LoadedImages.Count - 1))
        //        {
        //            SelectedImageIndex += 1;
        //            Image selectedImg = LoadedImages[SelectedImageIndex];
        //            buscarImg.Image = selectedImg;
        //            SelectTheClickedItem(buscarListView, SelectedImageIndex);
        //        }
        //    }
        //}

        private void SelectTheClickedItem(ListView list, int index)
        {
            for (int item = 0; item < list.Items.Count; item++)
            {
                if (item == index)
                {
                    list.Items[item].Selected = true;
                }
                else
                {
                    list.Items[item].Selected = false;
                }
            }

        }

        //private void seleccionarCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
           
        //    if (folderBrowser.ShowDialog() == DialogResult.OK)
        //    {
        //        // selected directory
        //        var selectedDirectory = folderBrowser.SelectedPath;
        //        // images paths from selected directory
        //        var imagePaths = Directory.GetFiles(selectedDirectory);
        //        // loading images from images paths
        //        LoadImagesFromFolder(imagePaths);
        //        //foreach (byte[] a in LoadedImages)
        //        //{
        //        //    Insert(a);
        //        //}

        //        // initializing images list
        //        ImageList images = new ImageList();
        //        images.ImageSize = new Size(100, 100);


        //        //foreach (var image in LoadedImages)
        //        //{
        //        //    images.Images.Add(image);
        //        //}
                
        //        // setting our listview with the imagelist
        //        buscarListView.LargeImageList = images;
                
        //        for (int itemIndex = 0; itemIndex <= LoadedImages.Count; itemIndex++)
        //        {
        //            string name=Path.GetFileNameWithoutExtension(imagePaths[itemIndex]);
        //            buscarListView.Items.Add(new ListViewItem($"{name}", itemIndex));
        //        }
        //    }
        //}
    }
}

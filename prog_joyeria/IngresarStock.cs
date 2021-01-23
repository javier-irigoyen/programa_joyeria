using Bunifu.UI.WinForms;
using Guna.UI.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;


namespace prog_joyeria
{
    partial class Form1 : Form
    {
        private void tabTxtIngresarCodigo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt= consulta("select Código from Stock where Código='" + tabTxtIngresarCodigo.Text+"'");
            if (dt.Rows.Count != 0)
            {
                IngresarAdvertenciaCod.Visible = true;
            }
            else
            {
                IngresarAdvertenciaCod.Visible = false;
            }
        }
        //programa que devuelve los valores de los checkbox de un contenedor
        private string ScanBunifuCheckBox(GunaGroupBox GunaGb)
        {
            //valor de material

            List<string> resultado = new List<string>();

            // recorremos todos los controles
            foreach (Control control in GunaGb.Controls)
            {
                // si algún control es un CheckBox
                if (control is BunifuCheckBox)
                {
                    // entonces revisamos su valor
                    BunifuCheckBox checkbox = control as BunifuCheckBox;
                    // si está seleccionado, entonces lo 
                    // agregamos a una lista temporal
                    if (checkbox.Checked)
                        resultado.Add(checkbox.BindingControl.Text);
                }
            }
            // finalmente, concatenamos la lista de resultados temporales
            // y lo asignamos a nuestra caja de texto
            return string.Join(", ", resultado.ToArray());
        }

        //programa que desmarca los checkbox de un contenedor
        private void uncheckBunifuCheckBox(GunaGroupBox GunaGb)
        {
            foreach (Control control in GunaGb.Controls)
            {
                // si algún control es un CheckBox
                if (control is BunifuCheckBox)
                {
                    // entonces revisamos su valor
                    BunifuCheckBox checkbox = control as BunifuCheckBox;

                    if (checkbox.Checked)
                        checkbox.Checked = false;
                }
            }
        }



        //coloca los valores del material marcados en tiempo real
        private void tabCbIngresarOroAm_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            txtMaterial.Text = ScanBunifuCheckBox(tabIngresarContenedorMaterial);
        }


        public bool ThumbnailCallback()
        {
            return true;
        }

        //evento click para ingresar los datos de la joya en la base de datos
        private void tabBtnIngresar_Click(object sender, EventArgs e)
        {
            if (tabTxtIngresarCodigo.Text != "" & tabCbIngresarTipo.Text != null &
                 txtMaterial.Text != "" & tabTxtIngresarDescripcion.Text !="" &
                 tabTxtIngresarPrecio.Text != "" & ! IngresarAdvertenciaCod.Visible)
            {


                DateTime today = DateTime.Today;
                string tienda;
                string moneda;
                string fecha = today.ToString("dd/MM/yyyy");

                //valor de tienda
                if (tabRbIngresarTienda1.Checked == true)
                {
                    tienda = "Import";
                }
                else
                {
                    tienda = "Corporación";
                }

                //valor de moneda
                if (tabRbIngresarDolar.Checked == true)
                {
                    moneda = "USD";
                }
                else
                {
                    moneda = "S/.";
                }

                ////valor de imagen Joya
                //Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                //Image a = tabIngresarPic.BackgroundImage.GetThumbnailImage(85, 85, callback, new IntPtr());
                
                //// Stream usado como buffer
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //// Se guarda la imagen en el buffer
                //a.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //byte[] imagen = ms.GetBuffer();

                //guardar imagen en la carpeta Joyas
                if (tabIngresarPic.Tag != null)
                {
                    string fileName = tabTxtIngresarCodigo.Text + ".jpg";
                    string sourcePath = tabIngresarPic.Tag.ToString();
                    string targetPath = mainPath + "Joyas";

                    string sourceFile = sourcePath;
                    string destFile = System.IO.Path.Combine(targetPath, fileName);

                    System.IO.Directory.CreateDirectory(targetPath);
                    System.IO.File.Copy(sourceFile, destFile, true);
                    tabIngresar.Tag = null;
                }

                //ingreso al sql Stock
                string agregar = "insert into Stock ([Código],[Vitrina],[Tipo],[Material]," +
                    "[Descripción],[Tamaño],[Color],[Claridad],[Peso],[Moneda],[Precio Lista],[Tienda]," +
                    "[Comprobante],[n],[Fecha],[Control]) values ('" + tabTxtIngresarCodigo.Text +
                    "','" + tabCbIngresarVitrina.Text +
                    "','" + tabCbIngresarTipo.Text +
                    "','" + txtMaterial.Text +
                    "','" + tabTxtIngresarDescripcion.Text +
                    "','" + tabTxtIngresarTamano.Text +
                    "','" + tabTxtIngresarColor.Text +
                    "','" + tabTxtIngresarClaridad.Text +
                    "','" + tabTxtIngresarPeso.Text +
                    "','" + moneda +
                    "','" + tabTxtIngresarPrecio.Text +
                    "','" + tienda +
                    "','" + tabCbIngresarComprobante.Text +
                    "','" + tabTxtIngresarN.Text +
                    "','" + fecha +
                    "','" +  "si"+
                     "')";

                conectar();
                SqlCommand command = new SqlCommand(agregar, conexionSQL);
                
                command.ExecuteNonQuery();
                desconectar();

                //ingreso al sql StockBckUp
                string agregarBackUp = "insert into StockBackUp ([Código],[Vitrina],[Tipo],[Material]," +
                    "[Descripción],[Tamaño],[Color],[Claridad],[Peso],[Moneda],[Precio Lista],[Tienda]," +
                    "[Comprobante],[n],[Fecha],[Control]) values ('" + tabTxtIngresarCodigo.Text +
                    "','" + tabCbIngresarVitrina.Text +
                    "','" + tabCbIngresarTipo.Text +
                    "','" + txtMaterial.Text +
                    "','" + tabTxtIngresarDescripcion.Text +
                    "','" + tabTxtIngresarTamano.Text +
                    "','" + tabTxtIngresarColor.Text +
                    "','" + tabTxtIngresarClaridad.Text +
                    "','" + tabTxtIngresarPeso.Text +
                    "','" + moneda +
                    "','" + tabTxtIngresarPrecio.Text +
                    "','" + tienda +
                    "','" + tabCbIngresarComprobante.Text +
                    "','" + tabTxtIngresarN.Text +
                    "','" + fecha +
                    "','" + "si" +
                    "')";

                conectar();
                SqlCommand commandBackUp = new SqlCommand(agregarBackUp, conexionSQL);

                commandBackUp.ExecuteNonQuery();
                desconectar();

                MessageBox.Show("Se ingreso Joya al Stock Correctamente.","Confirmación");
                

                tabTxtIngresarCodigo.Text = "";
                tabCbIngresarVitrina.Text = null;
                tabCbIngresarTipo.Text = null;
                txtMaterial.Text = "";
                tabTxtIngresarDescripcion.Text = "";
                tabTxtIngresarTamano.Text = "";
                tabTxtIngresarColor.Text = "";
                tabTxtIngresarClaridad.Text = "";
                tabTxtIngresarPeso.Text = "";
                moneda = "";
                tabTxtIngresarPrecio.Text = "";
                tienda = "";
                tabCbIngresarComprobante.Text = null;
                tabTxtIngresarN.Text = "";
                fecha = "";

                uncheckBunifuCheckBox(tabIngresarContenedorMaterial);


                //this.leer_datos("select * from Stock", ref resultados, "stock");
                //this.mifiltro = ((DataTable)resultados.Tables["stock"]).DefaultView;
                resultados.Dispose();
                

                //this.leer_datos("select * from Stock", ref resultados, "stock");
                
                

            }
            else if(IngresarAdvertenciaCod.Visible)
            {
                MessageBox.Show("No se ingresó. Ya existe el Código","ERROR");
            }
            else 
            {
                MessageBox.Show("No se ingresó. Complete los mínimos datos necesarios", "ERROR");
            }





        }

        //evento validar imagen correcta
        bool IsValidImage(string filename)
        {
            try
            {
                using (Image newImage = Image.FromFile(filename))
                { }
            }
            catch (OutOfMemoryException ex )
            {
                //The file does not have a valid image format.
                //-or- GDI+ does not support the pixel format of the file

                return false;
            }
            return true;
        }


        //evento arrastrar imagen al picturebox 
        private void tabIngresarPic_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabIngresarPic_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                if (IsValidImage(pic))
                {
                    tabIngresarPic.Tag = pic;
                    Image img = Image.FromFile(pic);
                    tabIngresarPic.BackgroundImage = img;
                }
                


            }
        }


        private void tabBtnRegistrosStockActualizar_Click(object sender, EventArgs e)
        {
            this.mifiltro = consulta().DefaultView;
            stock.DataSource = mifiltro;
        }

    }
}

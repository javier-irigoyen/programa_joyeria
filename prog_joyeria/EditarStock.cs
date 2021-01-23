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


        private void tabTxtEditarStockCod_TextChanged(object sender, EventArgs e)
        {
            string codigo = tabTxtEditarStockCod.Text;

            DataTable dt2 = consulta($"SELECT * FROM Stock WHERE Código='{codigo}'");

            if (dt2.Rows.Count != 0)
            {
                EditarAdvertenciaCod.Visible = false;

                //VALOR DE TIENDA
                if (dt2.Rows[0]["Tienda"].ToString() == "Import")
                {
                    tabRbEditarTienda1.Checked = true;
                    tabRbEditarTienda2.Checked = false;

                }
                else if ((dt2.Rows[0]["Tienda"].ToString() == "Corporación"))
                {
                    tabRbEditarTienda2.Checked = true;
                    tabRbEditarTienda1.Checked = false;

                }
                else
                {
                    tabRbEditarTienda1.Checked = false;
                    tabRbEditarTienda2.Checked = false;
                }
                //VALOR DE MONEDA
                if (dt2.Rows[0]["Moneda"].ToString() == "USD")
                {
                    tabRbEditarDolar.Checked = true;
                    tabRbEditarSoles.Checked = false;
                }
                else if ((dt2.Rows[0]["Moneda"].ToString() == "S/."))
                {
                    tabRbEditarSoles.Checked = true;
                    tabRbEditarDolar.Checked = false;
                }
                else
                {
                    tabRbEditarDolar.Checked = false;
                    tabRbEditarSoles.Checked = false;
                }

                //VALOR DE MATERIAL


                tabTxtEditarPeso.Text = dt2.Rows[0]["Peso"].ToString();
                tabCbEditarVitrina.Text = dt2.Rows[0]["Vitrina"].ToString();
                tabTxtEditarDescripcion.Text = dt2.Rows[0]["Descripción"].ToString();
                tabTxtEditarPrecio.Text = dt2.Rows[0]["Precio Lista"].ToString();
                tabTxtEditarColor.Text = dt2.Rows[0]["Color"].ToString();
                tabTxtEditarClaridad.Text = dt2.Rows[0]["Claridad"].ToString();
                tabTxtEditarTamano.Text = dt2.Rows[0]["Tamaño"].ToString();
                tabCbEditarTipo.Text = dt2.Rows[0]["Tipo"].ToString();
                tabLbEditarMaterial.Text = dt2.Rows[0]["Material"].ToString();

                tabTxtEditarCorte.Text = dt2.Rows[0]["Tienda"].ToString();

                tabEditarPic.BackgroundImage = FastLoad(mainPath + "\\Joyas\\" + codigo + ".jpg");
            }
            else
            {
                EditarAdvertenciaCod.Visible = true;

                tabRbEditarTienda1.Checked = false;
                tabRbEditarTienda2.Checked = false;

                tabRbEditarDolar.Checked = false;
                tabRbEditarSoles.Checked = false;

                uncheckBunifuCheckBox(tabEditarContenedorMaterial);

                tabTxtEditarPeso.Text = "";
                tabCbEditarVitrina.Text = null;
                tabTxtEditarDescripcion.Text = "";
                tabTxtEditarPrecio.Text = "";
                tabTxtEditarColor.Text = "";
                tabTxtEditarClaridad.Text = "";
                tabTxtEditarTamano.Text = "";
                tabCbEditarTipo.Text = null;
                tabLbEditarMaterial.Text = "";

                tabTxtEditarCorte.Text = "";

                tabEditarPic.BackgroundImage = FastLoad(mainPath + "\\Joyas\\imagen_preview.jpg");
            }
        }
        private void tabCbEditarOroAm_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            tabLbEditarMaterial.Text = ScanBunifuCheckBox(tabEditarContenedorMaterial);
        }

        //evento click para EDITAR Stock
        private void tabBtnStockEditar_Click(object sender, EventArgs e)
        {
            if (tabTxtEditarStockCod.Text != "" & tabCbEditarTipo.Text != null &
                   tabLbEditarMaterial.Text != "" & tabTxtEditarDescripcion.Text != "" &
                   tabTxtEditarPrecio.Text != "" & ! EditarAdvertenciaCod.Visible)
            {


                string tienda;
                string moneda;

                //valor de tienda
                if (tabRbEditarTienda1.Checked == true)
                {
                    tienda = "Import";
                }
                else
                {
                    tienda = "Corporación";
                }

                //valor de moneda
                if (tabRbEditarDolar.Checked == true)
                {
                    moneda = "USD";
                }
                else
                {
                    moneda = "S/.";
                }



                
                string codigo = tabTxtEditarStockCod.Text;
                string vitrina = tabCbEditarVitrina.Text;
                string tipo = tabCbEditarTipo.Text;
                string material = tabLbEditarMaterial.Text;
                string descripcion = tabTxtEditarDescripcion.Text;
                string tamano = tabTxtEditarTamano.Text;
                string color = tabTxtEditarColor.Text;
                string claridad = tabTxtEditarClaridad.Text;
                string peso = tabTxtEditarPeso.Text;

                string precio = tabTxtEditarPrecio.Text;

                string comprobante = tabCbEditarComprobante.Text;
                string n = tabTxtEditarN.Text;


                ////valor de imagen Joya
                //Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                //Image a = tabEditarPic.BackgroundImage.GetThumbnailImage(85, 85, callback, new IntPtr());              
                //// Stream usado como buffer
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //// Se guarda la imagen en el buffer
                //a.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //byte[] imagen = ms.GetBuffer();

                //guardar imagen en la carpeta Joyas
                if (tabEditarPic.Tag != null)
                {
                    string fileName = codigo + ".jpg";
                    string sourcePath = tabEditarPic.Tag.ToString();
                    string targetPath = mainPath + "Joyas";

                    string sourceFile = sourcePath;
                    string destFile = System.IO.Path.Combine(targetPath, fileName);

                    System.IO.Directory.CreateDirectory(targetPath);
                    System.IO.File.Copy(sourceFile, destFile, true);
                    tabEditarPic.Tag = null;
                }

                //EDITAR en el SQL Stock
                string query = $"update Stock set Vitrina='{vitrina}', Tipo='{tipo}', Material='{material}', Descripción='{descripcion}'," +
                    $" Tamaño='{tamano}', Color='{color}', Claridad='{claridad}', Peso='{peso}', Moneda='{moneda}',[Precio Lista]='{precio}'," +
                    $" Tienda='{tienda}', Comprobante='{comprobante}', n='{n}' where Código='{codigo}'";
                conectar();
                SqlCommand command = new SqlCommand(query, conexionSQL);
               
                command.ExecuteNonQuery();
                desconectar();

                //EDITAR en el SQL StockBackUp
                string queryBackUp = $"update StockBackUp set Vitrina='{vitrina}', Tipo='{tipo}', Material='{material}', Descripción='{descripcion}'," +
                    $" Tamaño='{tamano}', Color='{color}', Claridad='{claridad}', Peso='{peso}', Moneda='{moneda}',[Precio Lista]='{precio}'," +
                    $" Tienda='{tienda}', Comprobante='{comprobante}', n='{n}' where Código='{codigo}'";
                conectar();
                SqlCommand commandBackUp = new SqlCommand(queryBackUp, conexionSQL);

                commandBackUp.ExecuteNonQuery();
                desconectar();

                MessageBox.Show("Se editó Joya del Stock", "Confirmación");

                tabTxtEditarStockCod.Text = "";
                tabCbEditarVitrina.Text = null;
                tabCbEditarTipo.Text = null;
                tabLbEditarMaterial.Text = "";
                tabTxtEditarDescripcion.Text = "";
                tabTxtEditarTamano.Text = "";
                tabTxtEditarColor.Text = "";
                tabTxtEditarClaridad.Text = "";
                tabTxtEditarPeso.Text = "";
                moneda = "";
                tabTxtEditarPrecio.Text = "";
                tienda = "";
                tabCbEditarComprobante.Text = null;
                tabTxtEditarN.Text = "";
                

                uncheckBunifuCheckBox(tabEditarContenedorMaterial);

                resultados.Dispose();
            }
            else
            {

                MessageBox.Show("No se pudo editar Joya del Stock, Complete los datos mínimos de la Joya", "ERROR");

            }
        }

            //evento click para ELIMINAR Stock
            private void tabBtnStockEliminar_Click(object sender, EventArgs e)
            {
                int flag;
                string codigo = tabTxtEditarStockCod.Text;
                string query = $"delete from Stock where Código='{codigo}'";
                conectar();
                SqlCommand command = new SqlCommand(query, conexionSQL);
                flag = command.ExecuteNonQuery();
                desconectar();
                if (flag == 1)
                {
                    MessageBox.Show("Se eliminó Joya del Stock", "Confirmación");
                
                
                
                }
                else { MessageBox.Show("No se pudo eliminar Joya del Stock", "ERROR"); }
            }

            //evento arrastar imagen
            private void tabEditarPic_DragEnter(object sender, DragEventArgs e)
            {
                e.Effect = DragDropEffects.Copy;
            }
           
            private void tabEditarPic_DragDrop(object sender, DragEventArgs e)
            {
            string[] pathPic = (string[])e.Data.GetData(DataFormats.FileDrop) ;
            
            
            foreach (string pic in pathPic)
                {
                    if (IsValidImage(pic))
                    {
                        //guarda el path en el tag del objeto 
                        tabEditarPic.Tag = pic;

                        Image img = Image.FromFile(pic);
                            
                        tabEditarPic.BackgroundImage = img;
                        
                    }

                }
            }
        
    }
}

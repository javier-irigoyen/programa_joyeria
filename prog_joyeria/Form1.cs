using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using System.Configuration;
using Guna.UI.WinForms;
namespace prog_joyeria
{
    partial class Form1 : Form, IDisposable
    {

        string mainPath = AppDomain.CurrentDomain.BaseDirectory;
        static string conexion = "Data Source=LAPTOP_JAVIER;Initial Catalog=Sharvel;Integrated Security=True";
        SqlConnection conexionSQL = new SqlConnection(conexion);

        DataSet resultadosVentas = new DataSet();
        DataView mifiltroVentas;

        DataSet resultadosVentaPagos = new DataSet();
        DataView mifiltroVentaPagos;

        private void conectar()
        {
            conexionSQL.Open();
        }
        private void desconectar()
        {
            conexionSQL.Close();
        }

        private DataTable consulta(string query = "select * from Stock ")
        {

            conectar();
            SqlCommand command = new SqlCommand(query, conexionSQL);
            SqlDataAdapter data = new SqlDataAdapter(command);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            data.Dispose();
            desconectar();
            return tabla;


        }
        //SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString);




        public Form1()
        {
            InitializeComponent();
            CustomizeDesing();

        }

        private void tabBuscarCTextBox_TextChanged(object sender, EventArgs e)
        {

            string codigo = tabBuscarCTextBox.Text;

            SqlConnection con = new SqlConnection("Data Source=LAPTOP_JAVIER;Initial Catalog=Sharvel;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Stock WHERE Código='{codigo}'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
          
            if (dt.Rows.Count != 0)
            {
                tabBuscarLbTienda.Text = dt.Rows[0]["Tienda"].ToString();
                tabBuscarLbPeso.Text = dt.Rows[0]["Peso"].ToString();
                tabBuscarLbVitrina.Text = dt.Rows[0]["Vitrina"].ToString();
                tabBuscarLbDescripcion.Text = dt.Rows[0]["Descripción"].ToString();
                tabBuscarLbPrecio.Text = dt.Rows[0]["Precio Lista"].ToString();
                tabBuscarLbColor.Text = dt.Rows[0]["Color"].ToString();
                tabBuscarLbClaridad.Text = dt.Rows[0]["Claridad"].ToString();
                tabBuscarLbTamano.Text = dt.Rows[0]["Tamaño"].ToString();
                tabBuscarLbTipo.Text = dt.Rows[0]["Tipo"].ToString();
                tabBuscarLbMaterial.Text = dt.Rows[0]["Material"].ToString();
                tabBuscarLbMoneda.Text = dt.Rows[0]["Moneda"].ToString();
                tabBuscarLbCorte.Text = dt.Rows[0]["Tienda"].ToString();

                tabBuscarCPicBox.Image = FastLoad(mainPath + "\\Joyas\\" + codigo + ".jpg");
            }
            else
            {
                tabBuscarLbTienda.Text = "";
                tabBuscarLbPeso.Text = "";
                tabBuscarLbVitrina.Text = "";
                tabBuscarLbDescripcion.Text = "";
                tabBuscarLbPrecio.Text = "";
                tabBuscarLbColor.Text = "";
                tabBuscarLbClaridad.Text = "";
                tabBuscarLbTamano.Text = "";
                tabBuscarLbTipo.Text = "";
                tabBuscarLbMaterial.Text = "";
                tabBuscarLbMoneda.Text = "";
                tabBuscarLbCorte.Text = "";

                tabBuscarCPicBox.Image = FastLoad(mainPath + "\\Joyas\\imagen_preview.jpg");
            }

        }

        private void tabBuscarBtnActualizar_Click(object sender, EventArgs e)
        {
            Cotizacion();
        }

        private void tabCotizacioncbMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cotizacion();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'ventas._Ventas' Puede moverla o quitarla según sea necesario.
            this.ventasTableAdapter.Fill(this.ventas._Ventas);
            // TODO: esta línea de código carga datos en la tabla 'sharvelDataSet.Stock' Puede moverla o quitarla según sea necesario.
            this.stockTableAdapter.Fill(this.sharvelDataSet.Stock);

            // TODO: esta línea de código carga datos en la tabla 'sharvelDataSet2.TablaPagosVentaMaterial' Puede moverla o quitarla según sea necesario.
            this.tablaPagosVentaMaterialTableAdapter.Fill(this.sharvelDataSet2.TablaPagosVentaMaterial);
            // TODO: esta línea de código carga datos en la tabla 'tablaVentaMaterial._TablaVentaMaterial' Puede moverla o quitarla según sea necesario.
            this.tablaVentaMaterialTableAdapter.Fill(this.tablaVentaMaterial._TablaVentaMaterial);

            // TODO: esta línea de código carga datos en la tabla 'sharvelDataSet1.TablaVentaTemporal' Puede moverla o quitarla según sea necesario.
            this.tablaVentaTemporalTableAdapter.Fill(this.sharvelDataSet1.TablaVentaTemporal);
            // TODO: esta línea de código carga datos en la tabla 'sharvelDataSetCuentaTemporal.TablaCuentaTemporal' Puede moverla o quitarla según sea necesario.
            this.tablaCuentaTemporalTableAdapter.Fill(this.sharvelDataSetCuentaTemporal.TablaCuentaTemporal);
            // TODO: esta línea de código carga datos en la tabla 'sharvelDataSetVentaPagos.TablaVentaPagos' Puede moverla o quitarla según sea necesario.
            this.tablaVentaPagosTableAdapter.Fill(this.sharvelDataSetVentaPagos.TablaVentaPagos);









           

            tabComHora.Format = DateTimePickerFormat.Time;
            tabComHora.ShowUpDown = true;

            tabComFecha.CalendarForeColor = Color.DarkGoldenrod;


            this.leer_datos("select * from Stock", ref resultados, "stock");
            this.mifiltro = ((DataTable)resultados.Tables["stock"]).DefaultView;
            this.stock.DataSource = mifiltro;

            this.leer_datos("select * from Ventas", ref resultadosVentas, "Ventas");
            this.mifiltroVentas = ((DataTable)resultadosVentas.Tables["Ventas"]).DefaultView;
            this.tabDgRegVentas.DataSource = mifiltroVentas;

            this.leer_datos("select * from TablaVentaPagos", ref resultadosVentaPagos, "TablaVentaPagos");
            this.mifiltroVentaPagos = ((DataTable)resultadosVentaPagos.Tables["TablaVentaPagos"]).DefaultView;
            this.tabRegDgVentaPagos.DataSource = mifiltroVentaPagos;


            btnBuscar.ForeColor = Color.White;

            //var imageColumn = (DataGridViewImageColumn)stock.Columns["joyaDataGridViewImageColumn1"];
            //imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            stock.RowTemplate.Height = 100;

            tabVenderCliDgJoyasPorVender.RowTemplate.Height = 50;

            tabVenderCliDgJoyasPorVender.DefaultCellStyle.Font = new Font("Century Gothic", 12);
            tabVenderCliDgJoyasPorVender.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 12);
            tabVenderCliDgJoyasPorVender.RowsDefaultCellStyle.Font = new Font("Century Gothic", 12);


            tabVenderCliDgCuentaPorVender.RowTemplate.Height = 50;
            tabVenderCliDgCuentaPorVender.DefaultCellStyle.Font = new Font("Century Gothic", 12);



            tabVenderCliDgCuentaPorVender.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 12);

            try
            {

                //LoadData();
                Cotizacion();
                tabVenderCliCambioDolar.Text = scrapDolarVentaPrl.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR ");
                tabVenderCliCambioDolar.Text = "3.6";
            }
        }





        private void btnActualizarFotos_Click(object sender, EventArgs e)
        {
            List<String> codigo = new List<String>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString))
            {
                cn.Open();
                string query1 = "select Código from Stock";
                using (SqlCommand command = new SqlCommand(query1, cn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            codigo.Add(reader.GetString(0));
                        }
                    }


                }
                cn.Close();

                foreach (string i in codigo)
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();

                        SqlCommand command2 = new SqlCommand("UPDATE  Stock SET Joya = (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\\Users\\javier\\Desktop\\Inventario mejorado\\joyas\\" + i + ".jpg', SINGLE_BLOB) AS Joya) WHERE Código ='" + i + "'", cn);
                        //command2.Parameters.AddWithValue("@codigo", i);
                        command2.ExecuteNonQuery();
                        cn.Close();

                    }
                }
            }
        }



        private void DragDropImage(GunaElipsePanel panelpic)
        {
            var filename = "filename.jpg";
            var path = Path.Combine(Path.GetTempPath(), filename);
            panelpic.BackgroundImage.Save(path);
            var paths = new[] { path };
            panelpic.DoDragDrop(new DataObject(DataFormats.FileDrop, paths), DragDropEffects.Copy);
        }




        //evento click enviar fotos al sql
        private void btnFotosSql_Click(object sender, EventArgs e)
        {

            List<String> codigoList = new List<String>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString))
            {
                cn.Open();
                string query1 = "select Código from Stock";
                using (SqlCommand commando = new SqlCommand(query1, cn))
                {
                    using (SqlDataReader reader = commando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            codigoList.Add(reader.GetString(0));
                        }
                    }


                }
                cn.Close();
            }

            foreach (string codigo in codigoList)
            {
                Image img = Image.FromFile(mainPath + "\\Joyas\\" + codigo + ".jpg");
                //valor de imagen Joya
                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image a = img.GetThumbnailImage(150, 150, callback, new IntPtr());

                // Stream usado como buffer
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                // Se guarda la imagen en el buffer
                a.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imagen = ms.GetBuffer();

                string query = $"update Stock set Joya= @imagen where Código='{codigo}'";
                conectar();
                SqlCommand command = new SqlCommand(query, conexionSQL);
                command.Parameters.AddWithValue("@imagen", imagen);
                command.ExecuteNonQuery();
                desconectar();
                img.Dispose();
                a.Dispose();
                ms.Dispose();
                command.Dispose();
            }


        }

        private void ControlStockDgTabla_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
                
                ContextMenuStrip TablaControlStockMenu = new ContextMenuStrip();


                int positionXYmouseRow = ControlStockDgTabla.HitTest(e.X, e.Y).RowIndex;
                ControlStockDgTabla.ClearSelection();

                // Select the found DataGridViewRow
                ControlStockDgTabla.Rows[positionXYmouseRow].Selected = true;
                if (positionXYmouseRow >= 0)
                {
                    TablaControlStockMenu.Items.Add("Se encuentra la Joya?: Si").Name = "Se encuentra la Joya?: Si";
                    TablaControlStockMenu.Items.Add("Se encuentra la Joya?: No").Name = "Se encuentra la Joya?: No";
                    TablaControlStockMenu.Font = new Font("Century Gothic", 14);
                    TablaControlStockMenu.BackColor = Color.FromArgb(11, 17, 38);
                    TablaControlStockMenu.ForeColor = Color.DarkGoldenrod;


                }
                TablaControlStockMenu.Show(ControlStockDgTabla, new Point(e.X, e.Y));
                
                TablaControlStockMenu.ItemClicked += new ToolStripItemClickedEventHandler(TablaControlStockMenu_ItemClicked);
                ControlStockDgTabla.Tag= positionXYmouseRow;
            }
            


        }
        void TablaControlStockMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ControlStockDgTabla.ClearSelection();

            // Select the found DataGridViewRow
            ControlStockDgTabla.Rows[Convert.ToInt32( ControlStockDgTabla.Tag)].Selected = true;
            string yes = "Si";
            string no = "No";
            string codigo = ControlStockDgTabla.Rows[Convert.ToInt32(ControlStockDgTabla.Tag)].Cells[0].Value.ToString();
            switch (e.ClickedItem.Name.ToString())
            {
                case "Se encuentra la Joya?: Si":




                    string queryYes = $"update Stock set [Control]='{yes}' where Código='{codigo}'";

                    conectar();
                    SqlCommand commandYes = new SqlCommand(queryYes, conexionSQL);
                    commandYes.ExecuteNonQuery();
                    desconectar();

                    DataTable dtYes = consulta($"select * from Stock");
                    ControlStockDgTabla.DataSource = dtYes;
                    ControlStockDgTabla.ClearSelection();
                    ControlStockDgTabla.Rows[Convert.ToInt32(ControlStockDgTabla.Tag)].Selected = true;

                    break;

                case "Se encuentra la Joya?: No":

                    string queryNo = $"update Stock set [Control]='{no}' where Código='{codigo}'";

                    conectar();
                    SqlCommand commandNo = new SqlCommand(queryNo, conexionSQL);
                    commandNo.ExecuteNonQuery();
                    desconectar();

                    DataTable dtNo = consulta($"select * from Stock");
                    ControlStockDgTabla.DataSource = dtNo;
                    ControlStockDgTabla.ClearSelection();
                    ControlStockDgTabla.Rows[Convert.ToInt32(ControlStockDgTabla.Tag)].Selected = true;

                    break;



            }

        }

        private void tabRegVentasFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            string salida_datos = "";
            string[] palabras_busqueda = this.tabRegVentasFiltro.Text.Split(' ');

            foreach (string palabra in palabras_busqueda)
            {
                if (salida_datos.Length == 0)
                {
                    salida_datos = "(Comprobante like '%" + palabra +
                        "%' or N like '%" + palabra +
                        "%' or Fecha like '%" + palabra +
                        "%'or Vendedor like '%" + palabra +
                        "%'or Código like '%" + palabra +
                        "%'or [Descripción Total] like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or [Precio Venta] like '%" + palabra +
                        
                        "%'or IGV like '%" + palabra +
                        "%'or [Valor Venta] like '%" + palabra +
                        
                        "%'or [Nombre Cliente]  like '%" + palabra + "%')";
                }
                else
                {
                    salida_datos += " and (Comprobante like '%" + palabra +
                        "%' or N like '%" + palabra +
                        "%' or Fecha like '%" + palabra +
                        "%'or Vendedor like '%" + palabra +
                        "%'or Código like '%" + palabra +
                        "%'or [Descripción Total] like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or [Precio Venta] like '%" + palabra +
                        
                        "%'or IGV like '%" + palabra +
                        "%'or [Valor Venta] like '%" + palabra +
                        
                        "%'or [Nombre Cliente] like '%" + palabra + "%')";
                }

            }
            mifiltroVentas.RowFilter = salida_datos;
        }

        private void tabRegDgVentaPagos_KeyUp(object sender, KeyEventArgs e)
        {
            string salida_datos = "";
            string[] palabras_busqueda = this.tabVentaPagosFiltro.Text.Split(' ');

            foreach (string palabra in palabras_busqueda)
            {
                if (salida_datos.Length == 0)
                {
                    salida_datos = "(Comprobante like '%" + palabra +
                        "%' or N like '%" + palabra +
                        "%' or Fecha like '%" + palabra +
                        "%'or [Modo Pago] like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or Cuenta like '%" + palabra +
                        "%'or Peso like '%" + palabra +
                        

                        "%'or Material  like '%" + palabra + "%')";
                }
                else
                {
                    salida_datos += " and (Comprobante like '%" + palabra +
                        "%' or N like '%" + palabra +
                        "%' or Fecha like '%" + palabra +
                        "%'or [Modo Pago] like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or Cuenta like '%" + palabra +
                        "%'or Peso like '%" + palabra +
                        

                        "%'or Material like '%" + palabra + "%')";
                }

            }
            mifiltroVentaPagos.RowFilter = salida_datos;

        }

        





        //private void ControlStockDgTabla_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right && e.RowIndex > -1)
        //    {

        //        foreach (DataGridViewRow dr in ControlStockDgTabla.SelectedRows)

        //        {

        //            dr.Selected = false;

        //        }

        //        // Para seleccionar

        //        ControlStockDgTabla.Rows[e.RowIndex].Selected = true;
        //    }













































































































































        //private void tabIngresarPic_MouseMove(object sender, MouseEventArgs e)
        //{

        //    if (e.Button == MouseButtons.Left & tabIngresarPic.BackgroundImage !=null)
        //        this.DoDragDrop(new DataObject(DataFormats.FileDrop, new[] { @"Image Path" }), DragDropEffects.All);
        //        //this.DragDropImage(tabIngresarPic);
        //}



        //string query2 = "UPDATE  Stock SET Joya = (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\\Users\\javier\\Desktop\\Inventario mejorado\\joyas\\@codigo.jpg', SINGLE_BLOB) AS Joya) WHERE Código =@codigo";
        //foreach (string i in codigo)
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["prog_joyeria.Properties.Settings.SharvelConnectionString"].ConnectionString))
        //    {
        //        if (cn.State == ConnectionState.Closed)
        //        {
        //            cn.Open();

        //            SqlCommand command2 = new SqlCommand("UPDATE  Stock SET Joya = (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\\Users\\javier\\Desktop\\Inventario mejorado\\joyas\\"+i+".jpg', SINGLE_BLOB) AS Joya) WHERE Código ='"+i+"'", cn);
        //            //command2.Parameters.AddWithValue("@codigo", i);
        //            command2.ExecuteNonQuery();
        //            cn.Close();

        //        }
        //    }
        //}
        //}

    }

       
    }




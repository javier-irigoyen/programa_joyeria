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
    partial class Form1 : Form
    {
        //muestra toda la descripcion del Stock
        private void tabVenderCliTxtCodigo_TextChanged(object sender, EventArgs e)
        {
            string codigo = tabVenderCliTxtCodigo.Text;

            DataTable dt = consulta($"SELECT * FROM Stock WHERE Código='{codigo}'");
            
            if (dt.Rows.Count != 0)
            {
                tabVenderCliTienda.Text = dt.Rows[0]["Tienda"].ToString();
                tabVenderCliPeso.Text = dt.Rows[0]["Peso"].ToString();
                tabVenderCliVitrina.Text = dt.Rows[0]["Vitrina"].ToString();
                tabVenderCliDescripcion.Text = dt.Rows[0]["Descripción"].ToString();
                tabVenderCliPrecio.Text = dt.Rows[0]["Precio Lista"].ToString();
                tabVenderCliColor.Text = dt.Rows[0]["Color"].ToString();
                tabVenderCliClaridad.Text = dt.Rows[0]["Claridad"].ToString();
                tabVenderCliTamano.Text = dt.Rows[0]["Tamaño"].ToString();
                tabVenderCliTipo.Text = dt.Rows[0]["Tipo"].ToString();
                tabVenderCliMaterial.Text = dt.Rows[0]["Material"].ToString();
                tabVenderCliMoneda.Text = dt.Rows[0]["Moneda"].ToString();
                tabVenderCliCorte.Text = dt.Rows[0]["Tienda"].ToString();
                //marca la moneda
                if (tabVenderCliMoneda.Text =="USD")
                {
                    tabVenderCliRbVentaMonedaUSD.Checked = true;
                    tabVenderCliRbVentaMonedaSoles.Checked = false;
                }
                else if(tabVenderCliMoneda.Text == "S/.")
                {
                    tabVenderCliRbVentaMonedaSoles.Checked = true;
                    tabVenderCliRbVentaMonedaUSD.Checked = false;
                }
                tabVenderCliPrecioVenta.Text = dt.Rows[0]["Precio Lista"].ToString();

                tabVenderCliPb.Image = FastLoad(mainPath + "\\Joyas\\" + codigo + ".jpg");
            }
            else
            {
                tabVenderCliTienda.Text = "";
                tabVenderCliPeso.Text = "";
                tabVenderCliVitrina.Text = "";
                tabVenderCliDescripcion.Text = "";
                tabVenderCliPrecio.Text = "";
                tabVenderCliColor.Text = "";
                tabVenderCliClaridad.Text = "";
                tabVenderCliTamano.Text = "";
                tabVenderCliTipo.Text = "";
                tabVenderCliMaterial.Text = "";
                tabVenderCliMoneda.Text = "";
                tabVenderCliCorte.Text = "";



                tabVenderCliPb.Image = FastLoad(mainPath + "\\Joyas\\imagen_preview.jpg");
            }
        }


        //boton que agrega joya a la TablaVentaTemporal
        private void tabVCliBtnAgregarTbVentaTemporal_Click(object sender, EventArgs e)
        {
            string codigo = tabVenderCliTxtCodigo.Text;
            if (tabVenderCliCbComprobante.Text=="" | tabVenderCliTxtN.Text=="" | codigo=="" | tabVenderCliPrecioVenta.Text=="")
            {
                MessageBox.Show("No se ha completado los datos necesarios");
                return;
            }

            string filtro = $"select Código from TablaVentaTemporal where Código='{codigo}'";
            DataTable dtTablaVentaTemporal =consulta(filtro);
            if (dtTablaVentaTemporal.Rows.Count != 0)
            {
                return;
            }

            string igvText;
            if (tabVenderCliCbComprobante.Text != "Pedido")
            {
                double igv = Convert.ToDouble(tabVenderCliPrecioVenta.Text) * 0.18;
                igvText = igv.ToString();
            }
            else
            {
                igvText = "0";
            }

            string moneda;
            if (tabVenderCliRbVentaMonedaUSD.Checked==true)
            {
                moneda ="USD";
            }
            else
            {
                moneda = "S/.";
            }


            


            //ingreso al sql
            string agregar = "insert into TablaVentaTemporal ([Comprobante],[N],[Código],[Descripción Total],[Precio Lista]," +
                "[Moneda],[Precio Venta],[IGV]) values ('" + tabVenderCliCbComprobante.Text +
                "','" + tabVenderCliTxtN.Text +
                "','" + tabVenderCliTxtCodigo.Text + 
                
                "','" + tabVenderCliTipo.Text +" "+ tabVenderCliMaterial.Text + " " + tabVenderCliDescripcion.Text + " " + 
                tabVenderCliCorte.Text + " " +tabVenderCliTamano.Text + " " + tabVenderCliColor.Text + " " + tabVenderCliClaridad.Text +

                "','" + tabVenderCliMoneda.Text + " " + tabVenderCliPrecio.Text +
                "','" + moneda +
                "','" + tabVenderCliPrecioVenta.Text +
                "','" + igvText+
                
                "')";

            conectar();
            SqlCommand command = new SqlCommand(agregar, conexionSQL);           
            command.ExecuteNonQuery();
            desconectar();

            string Comprobante = tabVenderCliCbComprobante.Text;
            string N = tabVenderCliTxtN.Text;
            DataTable dt = consulta($"select * from TablaVentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgJoyasPorVender.DataSource = dt;

            tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender,
                "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);





        }

        //evento click derecho que muestra boton eliminar de la TablaVentaTemporal
        private void tabVenderCliDgJoyasPorVender_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip TablaVentaTemporalMenu = new ContextMenuStrip();
                int positionXYmouseRow = tabVenderCliDgCuentaPorVender.HitTest(e.X, e.Y).RowIndex;
                if (positionXYmouseRow >= 0)
                {
                    TablaVentaTemporalMenu.Items.Add("Eliminar").Name = "Eliminar";

                }
                TablaVentaTemporalMenu.Show(tabVenderCliDgCuentaPorVender, new Point(e.X, e.Y));
                TablaVentaTemporalMenu.ItemClicked += new ToolStripItemClickedEventHandler(TablaVentaTemporalMenu_ItemClicked);
            }
        }

        //evento click eliminar de la TablaVentaTemporal
        void TablaVentaTemporalMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "Eliminar":

                    string codigo = GetValorCelda(tabVenderCliDgJoyasPorVender, 2);
                    string query = $"delete from TablaVentaTemporal where Código='{codigo}'";

                    conectar();
                    SqlCommand command = new SqlCommand(query, conexionSQL);
                    command.ExecuteNonQuery();
                    desconectar();

                    string Comprobante = tabVenderCliCbComprobante.Text;
                    string N = tabVenderCliTxtN.Text;
                    DataTable dt = consulta($"select * from TablaVentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
                    tabVenderCliDgJoyasPorVender.DataSource = dt;

                    tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender,
                        "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);
                    break;
            }
        }

        //programa que devuelve valor de la celda de la fila seleccionada
        public static string GetValorCelda(DataGridView dgv, int num)
        {
            string valor = "";

            valor = dgv.Rows[dgv.CurrentRow.Index].Cells[num].Value.ToString();

            return valor;
        }

        //Filtro para mostrar de la TablaVentaTemporal y en la TablaCuentaTemporal por Comprobante y N
        private void tabVenderCliCbComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Comprobante = tabVenderCliCbComprobante.Text;
            string N = tabVenderCliTxtN.Text;
            DataTable dt = consulta($"select * from TablaVentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgJoyasPorVender.DataSource = dt;

            tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender,
                "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);



            DataTable dt2 = consulta($"select * from TablaCuentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgCuentaPorVender.DataSource = dt2;

            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text);

        }

        private void tabVenderCliTxtN_TextChanged(object sender, EventArgs e)
        {
            string Comprobante = tabVenderCliCbComprobante.Text;
            string N = tabVenderCliTxtN.Text;
            DataTable dt = consulta($"select * from TablaVentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgJoyasPorVender.DataSource = dt;

            tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender,
                "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);



            DataTable dt2 = consulta($"select * from TablaCuentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgCuentaPorVender.DataSource = dt2;

            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text);
        }


        //boton que agrega a la TablaCuentaTemporal 
        private void tabVCliBtnAgregarTbCuentaTemporal_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string fecha = today.ToString("dd/MM/yyyy");
            string moneda;
            //valor de moneda
            if (tabVenderCliRbMonedaUSD.Checked == true)
            {
                moneda = "USD";
            }
            else
            {
                moneda = "S/.";
            }

            string codigo = tabVenderCliTxtCodigo.Text;
            if (tabVenderCliCbComprobante.Text == "" | tabVenderCliTxtN.Text == "" | tabVenderCliModoPago.Text == "" | tabVenderCliMontoCuenta.Text == "" )
            {
                MessageBox.Show("No se ha completado los datos necesarios | tabVenderCliCbVendedor!=null");
                return;
            }


            //ingreso al sql
            string agregar = "insert into TablaCuentaTemporal ([Comprobante],[N],[Fecha],[Modo Pago],[Moneda]," +
            "[Cuenta],[Peso],[Material]) values ('" + tabVenderCliCbComprobante.Text +
            "','" + tabVenderCliTxtN.Text +
            "','" + fecha +

            "','" + tabVenderCliModoPago.Text +
            "','" + moneda +
            "','" + tabVenderCliMontoCuenta.Text +
            "','" + tabVenderCliComprarPeso.Text +
            "','" + tabVenderCliMaterialCompra.Text +

            "')";

            conectar();
            SqlCommand command = new SqlCommand(agregar, conexionSQL);
            command.ExecuteNonQuery();
            desconectar();

            string Comprobante = tabVenderCliCbComprobante.Text;
            string N = tabVenderCliTxtN.Text;
            DataTable dt = consulta($"select * from TablaCuentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgCuentaPorVender.DataSource = dt;

            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text);
        }


        //evento click derecho que muestra boton eliminar de la TablaCuentaTemporal
        private void tabVenderCliDgCuentaPorVender_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip TablaCuentaTemporalMenu = new ContextMenuStrip();
                int positionXYmouseRow = tabVenderCliDgCuentaPorVender.HitTest(e.X, e.Y).RowIndex;
                if (positionXYmouseRow >= 0)
                {
                    TablaCuentaTemporalMenu.Items.Add("Eliminar").Name = "Eliminar";

                }
                TablaCuentaTemporalMenu.Show(tabVenderCliDgCuentaPorVender, new Point(e.X, e.Y));
                TablaCuentaTemporalMenu.ItemClicked += new ToolStripItemClickedEventHandler(TablaCuentaTemporalMenu_ItemClicked);
            }
           
        }

        //evento click eliminar de la TablaCuentaTemporal
        void TablaCuentaTemporalMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "Eliminar":
                    string nPago = GetValorCelda(tabVenderCliDgCuentaPorVender, 8);
            string query = $"delete from TablaCuentaTemporal where [N° Pago]='{nPago}'";

            conectar();
            SqlCommand command = new SqlCommand(query, conexionSQL);
            command.ExecuteNonQuery();
            desconectar();

            string Comprobante = tabVenderCliCbComprobante.Text;
            string N = tabVenderCliTxtN.Text;
            DataTable dt = consulta($"select * from TablaCuentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
            tabVenderCliDgCuentaPorVender.DataSource = dt;

            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text); 

                    break;
            }

                
        }




        // evento click para vender
        private void tabVenderCliBtnVender_Click(object sender, EventArgs e)
        {
            if (tabVenderCliCbComprobante.Text == "" | tabVenderCliTxtN.Text == "" | tabVenderCliCbVendedor != null)
            {
                MessageBox.Show("No se ha completado los datos necesarios ");
                return;
            }

            if (tabVenderCliCambioDolar.Text != "0" & tabVenderCliCambioDolar.Text != "" & tabVenderCliCambioDolar.Text != null )
            {
                DateTime today = DateTime.Today;
                string fecha = today.ToString("dd/MM/yyyy");

                double precioCuenta = Convert.ToDouble(tabVenderPrecioCuentaTotal.Text);
                double precioVenta = Convert.ToDouble(tabVenderPrecioVentaTotal.Text);


                if (tabVenderCliMonedaPrecioTot.Text == "USD")
                {

                }
                else if (tabVenderCliMonedaPrecioTot.Text == "S/.")
                {
                    precioVenta = precioVenta / Convert.ToDouble(tabVenderCliCambioDolar.Text);
                }

                if (tabVenderCliMonedaMontoPag.Text == "USD")
                {

                }
                else if (tabVenderCliMonedaMontoPag.Text == "S/.")
                {
                    precioCuenta = precioCuenta / Convert.ToDouble(tabVenderCliCambioDolar.Text);
                }


                if (precioCuenta >= precioVenta)
                {

                    //agrega las Joyas de la TablaVentaTemporal a la tabla Ventas
                    foreach (DataGridViewRow row in tabVenderCliDgJoyasPorVender.Rows)
                    {


                        string codigo = row.Cells[2].Value.ToString();
                        string descripcionTotal = row.Cells[3].Value.ToString();
                        string moneda = row.Cells[5].Value.ToString();
                        string precioVentaJoya = row.Cells[6].Value.ToString();
                        string igvText = row.Cells[7].Value.ToString();

                        double valorVenta = Convert.ToDouble(precioVentaJoya) - Convert.ToDouble(igvText);
                        string valorVentaText = valorVenta.ToString();


                        string agregarVenta = "insert into Ventas ([Comprobante],[N],[Fecha],[Vendedor],[Código]," +
               "[Descripción Total],[Moneda],[Precio Venta],[IGV],[Valor Venta],[Nombre Cliente]) values" +
               " ('" + tabVenderCliCbComprobante.Text +
               "','" + tabVenderCliTxtN.Text +
               "','" + fecha +
               "','" + tabVenderCliCbVendedor.Text +
               "','" + codigo +
               "','" + descripcionTotal +

               "','" + moneda +
               "','" + precioVentaJoya +
               "','" + igvText +
               "','" + valorVentaText +
               "','" + tabCliNombre.Text +


               "')";

                        conectar();
                        SqlCommand commandAgregarVenta = new SqlCommand(agregarVenta, conexionSQL);
                        commandAgregarVenta.ExecuteNonQuery();
                        desconectar();

                        //borrar Joyas de la tabla Stock
                        string borrarTablaStock = $"delete from Stock where Código='{codigo}'";
                        conectar();
                        SqlCommand commandBorrarTablaStock = new SqlCommand(borrarTablaStock, conexionSQL);
                        commandBorrarTablaStock.ExecuteNonQuery();
                        desconectar();

                    }

                    //borrar Joyas de la tabla VentaTemporal
                    string borrarTablaVentaTemporal = $"delete from TablaVentaTemporal where Comprobante='{tabVenderCliCbComprobante.Text}'" +
                        $" and N={tabVenderCliTxtN.Text}";
                    conectar();
                    SqlCommand commandBorrarTablaVentaTemporal = new SqlCommand(borrarTablaVentaTemporal, conexionSQL);
                    commandBorrarTablaVentaTemporal.ExecuteNonQuery();
                    desconectar();

                    //agrega de TablaCuentaTemporal a TablaVentaPagos
                    foreach (DataGridViewRow row in tabVenderCliDgCuentaPorVender.Rows)
                    {


                        string FechaCuenta = row.Cells[2].Value.ToString();
                        string modoPago = row.Cells[3].Value.ToString();
                        string moneda = row.Cells[4].Value.ToString();
                        string cuenta = row.Cells[5].Value.ToString();
                        string peso = row.Cells[6].Value.ToString();
                        string material = row.Cells[7].Value.ToString();




                        string agregarTablaVentaPagos = "insert into TablaVentaPagos ([Comprobante],[N],[Fecha],[Modo Pago],[Moneda]," +
               "[Cuenta],[Peso],[Material]) values" +
               " ('" + tabVenderCliCbComprobante.Text +
               "','" + tabVenderCliTxtN.Text +
               "','" + FechaCuenta +
               "','" + modoPago +
               "','" + moneda +
               "','" + cuenta +
               "','" + peso +
               "','" + material +

               "')";

                        conectar();
                        SqlCommand commandAgregarVenta = new SqlCommand(agregarTablaVentaPagos, conexionSQL);
                        commandAgregarVenta.ExecuteNonQuery();
                        desconectar();

                    }


                    //borrar Joyas de la tabla CuentaTemporal
                    string borrarTablaCuentaTemporal = $"delete from TablaCuentaTemporal where Comprobante='{tabVenderCliCbComprobante.Text}'" +
                        $" and N={tabVenderCliTxtN.Text}";
                    conectar();
                    SqlCommand commandBorrarTablaCuentaTemporal = new SqlCommand(borrarTablaCuentaTemporal, conexionSQL);
                    commandBorrarTablaCuentaTemporal.ExecuteNonQuery();
                    desconectar();







                    string Comprobante = tabVenderCliCbComprobante.Text;
                    string N = tabVenderCliTxtN.Text;
                    DataTable dtCuentaTemp = consulta($"select * from TablaCuentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
                    tabVenderCliDgCuentaPorVender.DataSource = dtCuentaTemp;

                    DataTable dtVentaTemp = consulta($"select * from TablaVentaTemporal where Comprobante='{Comprobante}' and N='{N}'");
                    tabVenderCliDgJoyasPorVender.DataSource = dtVentaTemp;

                    DataTable dtVenta = consulta("select * from Ventas");
                    tabDgRegVentas.DataSource = dtVenta;

                    DataTable dtStock = consulta("select * from Stock");
                    stock.DataSource = dtStock;

                    DataTable dtVentasPagos = consulta("select * from TablaVentaPagos");
                    tabRegDgVentaPagos.DataSource = dtVentasPagos;

                    MessageBox.Show("Se registró Venta con éxito");


                }
                else
                {
                    MessageBox.Show("El cliente No ha pagado el monto total");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No se ha ingresado el tipo de cambio correcto");
                return;
            }
           

        }


        //cambiar de soles a dolares para Monto a Cuenta 
        private void tabVenderCliMonedaMontoPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text);
            
        }

        //cambiar moneda de Precio Venta Total
        private void tabVenderCliMonedaPrecioTot_SelectedIndexChanged(object sender, EventArgs e)
        {

            tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender, 
                "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);
            
        }

        //programa para cambiar de suma de dolar a soles
        private string SumaDolarSoles(string moneda,DataGridView TablaSuma,string montoCol,string monedaCol,string tipoCambio)
        {
            if (tabVenderCliCambioDolar.Text != "" & tabVenderCliCambioDolar.Text != null)
            {
                if (moneda == "USD")
                {
                    double sumaVentaTotalUSD = 0;

                    foreach (DataGridViewRow row in TablaSuma.Rows)
                    {
                        if (row.Cells[montoCol].Value != null & row.Cells[monedaCol].Value.ToString() == "USD")
                        {
                            sumaVentaTotalUSD += Convert.ToDouble(row.Cells[montoCol].Value.ToString());
                        }

                        else if (row.Cells[monedaCol].Value.ToString() == "S/.")
                        {
                            sumaVentaTotalUSD += Convert.ToDouble(row.Cells[montoCol].Value.ToString()) / Convert.ToDouble(tipoCambio);
                        }
                    }

                    sumaVentaTotalUSD = Math.Round(sumaVentaTotalUSD, 1);
                    return sumaVentaTotalUSD.ToString();
                }
                else if (moneda == "S/.")
                {
                    double sumaVentaTotalSoles = 0;

                    foreach (DataGridViewRow row in TablaSuma.Rows)
                    {
                        if (row.Cells[montoCol].Value != null & row.Cells[monedaCol].Value.ToString() == "S/.")
                        {
                            sumaVentaTotalSoles += Convert.ToDouble(row.Cells[montoCol].Value.ToString());
                        }

                        else if (row.Cells[monedaCol].Value.ToString() == "USD")
                        {
                            sumaVentaTotalSoles += Convert.ToDouble(row.Cells[montoCol].Value.ToString()) * Convert.ToDouble(tipoCambio);
                        }
                    }

                    sumaVentaTotalSoles = Math.Round(sumaVentaTotalSoles, 1);
                    return sumaVentaTotalSoles.ToString();
                }
                else
                {
                    MessageBox.Show("moneda incorrecta");
                    return "NA";

                }
            }
            else
            {
                return "NA";
            }
                
        }

        //evento cambiar tipo de cambio
        private void tabVenderCliCambioDolar_TextChanged(object sender, EventArgs e)
        {

            tabVenderPrecioCuentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaMontoPag.Text, tabVenderCliDgCuentaPorVender,
                                 "dataGridViewTextBoxColumn25", "dataGridViewTextBoxColumn24", tabVenderCliCambioDolar.Text);

            tabVenderPrecioVentaTotal.Text = SumaDolarSoles(tabVenderCliMonedaPrecioTot.Text, tabVenderCliDgJoyasPorVender,
                     "dataGridViewTextBoxColumn35", "dataGridViewTextBoxColumn34", tabVenderCliCambioDolar.Text);



        }

    }
}

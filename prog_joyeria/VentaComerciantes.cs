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
        //evento agregar venta material a la TablaVentaMaterial
        private void tabComBtnVentaMat_Click(object sender, EventArgs e)
        {

            if (tabComMaterial.Text == "" | tabComPeso.Text == "" | tabComPrecioPorGramo.Text == "")
            {
                MessageBox.Show("No se ha completado los datos necesarios");
                return;
            }

            string comerciante = tabComNombre.Text;



            DateTime today = DateTime.Today;
            string fecha = today.ToString("dd/MM/yyyy");

            string moneda;
            if (tabComVentaUSD.Checked == true)
            {
                moneda = "USD";
            }
            else if (tabComVentaSoles.Checked == true)
            {
                moneda = "S/.";
            }
            else
            {
                MessageBox.Show("moneda incorrecta");
                return;
            }

            Double precioVenta = Convert.ToDouble(tabComPrecioPorGramo.Text) * Convert.ToDouble(tabComPeso.Text);
            string precioVentaText = Math.Round(precioVenta).ToString();


            //ingreso al sql
            string agregar = "insert into TablaVentaMaterial ([Comerciante],[Fecha],[Material],[Peso],[Moneda]," +
                "[Precio Gramo],[Precio Venta]) values ('" + comerciante +
                "','" + fecha +
                "','" + tabComMaterial.Text +

                "','" + tabComPeso.Text +

                "','" + moneda +
                "','" + tabComPrecioPorGramo.Text +
                "','" + precioVentaText +


                "')";

            conectar();
            SqlCommand command = new SqlCommand(agregar, conexionSQL);
            command.ExecuteNonQuery();
            desconectar();


            DataTable dt = consulta($"select * from TablaVentaMaterial where Comerciante='{comerciante}' order by [N°Venta] DESC");
            tabComDgTablaVenderMat.DataSource = dt;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
              "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
              "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);




        }

        //programa que calcula la deuda sumando las ventas y restando los pagos
        private string deudaSumaVentaRestaPago(string moneda, DataGridView TablaVentas, string ColMonedaVentas, string ColMontoVentas,
                                               DataGridView TablaPagos, string ColMonedaPago, string ColMontoPago, string tipoCambio)
        {
            if (tabVenderCliCambioDolar.Text != "" & tabVenderCliCambioDolar.Text != null)
            {
                if (moneda == "USD")
                {
                    double sumaVentaTotalUSD = 0;
                    double sumaPagoTotalUSD = 0;

                    foreach (DataGridViewRow row in TablaVentas.Rows)
                    {
                        if (row.Cells[ColMontoVentas].Value != null & row.Cells[ColMonedaVentas].Value.ToString() == "USD")
                        {
                            sumaVentaTotalUSD += Convert.ToDouble(row.Cells[ColMontoVentas].Value.ToString());
                        }

                        else if (row.Cells[ColMonedaVentas].Value.ToString() == "S/.")
                        {
                            sumaVentaTotalUSD += Convert.ToDouble(row.Cells[ColMontoVentas].Value.ToString()) / Convert.ToDouble(tipoCambio);
                        }
                    }

                    foreach (DataGridViewRow row in TablaPagos.Rows)
                    {
                        if (row.Cells[ColMontoPago].Value != null & row.Cells[ColMonedaPago].Value.ToString() == "USD")
                        {
                            sumaPagoTotalUSD += Convert.ToDouble(row.Cells[ColMontoPago].Value.ToString());
                        }

                        else if (row.Cells[ColMonedaPago].Value.ToString() == "S/.")
                        {
                            sumaPagoTotalUSD += Convert.ToDouble(row.Cells[ColMontoPago].Value.ToString()) / Convert.ToDouble(tipoCambio);
                        }
                    }
                    double deudaTotalUSD = Math.Round(sumaVentaTotalUSD - sumaPagoTotalUSD, 1);



                    return deudaTotalUSD.ToString();
                }
                else if (moneda == "S/.")
                {
                    double sumaVentaTotalSoles = 0;
                    double sumaPagoTotalSoles = 0;

                    foreach (DataGridViewRow row in TablaVentas.Rows)
                    {
                        if (row.Cells[ColMontoVentas].Value != null & row.Cells[ColMonedaVentas].Value.ToString() == "S/.")
                        {
                            sumaVentaTotalSoles += Convert.ToDouble(row.Cells[ColMontoVentas].Value.ToString());
                        }

                        else if (row.Cells[ColMonedaVentas].Value.ToString() == "USD")
                        {
                            sumaVentaTotalSoles += Convert.ToDouble(row.Cells[ColMontoVentas].Value.ToString()) * Convert.ToDouble(tipoCambio);
                        }
                    }

                    foreach (DataGridViewRow row in TablaPagos.Rows)
                    {
                        if (row.Cells[ColMontoPago].Value != null & row.Cells[ColMonedaPago].Value.ToString() == "S/.")
                        {
                            sumaPagoTotalSoles += Convert.ToDouble(row.Cells[ColMontoPago].Value.ToString());
                        }

                        else if (row.Cells[ColMonedaPago].Value.ToString() == "USD")
                        {
                            sumaPagoTotalSoles += Convert.ToDouble(row.Cells[ColMontoPago].Value.ToString()) * Convert.ToDouble(tipoCambio);
                        }
                    }


                    double deudaTotalSoles = Math.Round(sumaVentaTotalSoles - sumaPagoTotalSoles, 1);
                    return deudaTotalSoles.ToString();
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

        private void tabComBtnPagos_Click(object sender, EventArgs e)
        {
            if (tabComCbModoPago.Text == "" | tabComMontoCuenta.Text == "" | tabComFecha.Text == "")
            {
                MessageBox.Show("No se ha completado los datos necesarios");
                return;
            }

            string comerciante = tabComNombre.Text;



            //DateTime today = DateTime.Today;
            //string fecha = today.ToString("dd/MM/yyyy");

            string moneda;
            if (tabComPagoUSD.Checked == true)
            {
                moneda = "USD";
            }
            else if (TabComPagoSoles.Checked == true)
            {
                moneda = "S/.";
            }
            else
            {
                MessageBox.Show("moneda incorrecta");
                return;
            }





            //ingreso al sql
            string agregar = "insert into TablaPagosVentaMaterial ([Comerciante],[Fecha],[Modo Pago],[Moneda]," +
                "[Monto Cuenta]) values ('" + comerciante +
                "','" + tabComFecha.Text +
                "','" + tabComCbModoPago.Text +

                "','" + moneda +
                "','" + tabComMontoCuenta.Text +

                "')";

            conectar();
            SqlCommand command = new SqlCommand(agregar, conexionSQL);
            command.ExecuteNonQuery();
            desconectar();


            DataTable dt = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{comerciante}' order by [N°Pago] DESC");
            tabComDgTablaPagosVentaMat.DataSource = dt;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);

        }

        private void tabComCbMonedaDeuda_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
              "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
              "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        //evento click derecho en la TablaVentaMaterial
        private void tabComDgTablaVenderMat_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip TablaVentaMaterialMenu = new ContextMenuStrip();

                
                int positionXYmouseRow = tabComDgTablaVenderMat.HitTest(e.X, e.Y).RowIndex;
                if (positionXYmouseRow >= 0)
                {
                    TablaVentaMaterialMenu.Items.Add("Eliminar").Name = "Eliminar";
                    TablaVentaMaterialMenu.Font = new Font("Century Gothic", 14);
                    TablaVentaMaterialMenu.BackColor = Color.FromArgb(11, 17, 38);
                    TablaVentaMaterialMenu.ForeColor = Color.DarkGoldenrod;
                    

                }
                TablaVentaMaterialMenu.Show(tabComDgTablaVenderMat, new Point(e.X, e.Y));
                TablaVentaMaterialMenu.ItemClicked += new ToolStripItemClickedEventHandler(TablaVentaMaterialMenu_ItemClicked);
            }
        }

        void TablaVentaMaterialMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            string comerciante = tabComNombre.Text;
            switch (e.ClickedItem.Name.ToString())
            {
                case "Eliminar":
                    DialogResult dialogResult = MessageBox.Show("Está seguro que desea eliminar esta Venta de Material?", "Confirmación", 
                        MessageBoxButtons.YesNo);
                    
                    if (dialogResult == DialogResult.Yes)
                    {
                        string NVenta = GetValorCelda(tabComDgTablaVenderMat, 7);
                        string query = $"delete from TablaVentaMaterial where [N°Venta]='{NVenta}'";

                        conectar();
                        SqlCommand command = new SqlCommand(query, conexionSQL);
                        command.ExecuteNonQuery();
                        desconectar();

                        DataTable dt = consulta($"select * from TablaVentaMaterial where Comerciante='{comerciante}' order by [N°Venta] DESC");
                        tabComDgTablaVenderMat.DataSource = dt;

                        tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
                          "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
                          "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
                        
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;

                    }
                    break;




            }

        }

        //evento click derecho en la Tabla PagosVentaMaterial
        private void tabComDgTablaPagosVentaMat_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip TablaPagosMaterialMenu = new ContextMenuStrip();
                int positionXYmouseRow = tabComDgTablaPagosVentaMat.HitTest(e.X, e.Y).RowIndex;
                if (positionXYmouseRow >= 0)
                {
                    TablaPagosMaterialMenu.Items.Add("Eliminar").Name = "Eliminar";
                    TablaPagosMaterialMenu.Font = new Font("Century Gothic", 14);
                    TablaPagosMaterialMenu.BackColor = Color.FromArgb(11, 17, 38);
                    TablaPagosMaterialMenu.ForeColor = Color.DarkGoldenrod;

                }
                TablaPagosMaterialMenu.Show(tabComDgTablaPagosVentaMat, new Point(e.X, e.Y));
                TablaPagosMaterialMenu.ItemClicked += new ToolStripItemClickedEventHandler(TablaPagosMaterialMenu_ItemClicked);
            }
        }

        void TablaPagosMaterialMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string comerciante = tabComNombre.Text;
            switch (e.ClickedItem.Name.ToString())
            {
                case "Eliminar":
                    DialogResult dialogResult = MessageBox.Show("Está seguro que desea eliminar esta Venta de Material?", "Confirmación",
                        MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string NPago = GetValorCelda(tabComDgTablaPagosVentaMat, 5);
                        string query = $"delete from TablaPagosVentaMaterial where [N°Pago]='{NPago}'";

                        conectar();
                        SqlCommand command = new SqlCommand(query, conexionSQL);
                        command.ExecuteNonQuery();
                        desconectar();

                        DataTable dt = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{comerciante}' order by [N°Pago] DESC");
                        tabComDgTablaPagosVentaMat.DataSource = dt;

                        tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
                 "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
                 "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;

                    }

                   break;
            }

        }
    }
}

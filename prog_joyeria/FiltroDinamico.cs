using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;


namespace prog_joyeria
{
    partial class Form1 : Form,IDisposable
    {
        DataSet resultados = new DataSet();
        DataView mifiltro;
        

        public void leer_datos(string query, ref DataSet dtsPrincipal, string tabla)
        {
            try
            {                            
                SqlCommand cmd = new SqlCommand(query, conexionSQL);
                conectar();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtsPrincipal, tabla);
                da.Dispose();
                desconectar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void tabRegistroTextBoxFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            string salida_datos = "";
            string[] palabras_busqueda = this.tabRegistroTextBoxFiltro.Text.Split(' ');

            foreach (string palabra in palabras_busqueda)
            {
                if (salida_datos.Length == 0)
                {
                    salida_datos = "(Código like '%" + palabra +
                        "%' or Tipo like '%" + palabra +
                        "%' or Descripción like '%" + palabra +
                        "%'or Material like '%" + palabra +
                        "%'or Vitrina like '%" + palabra +
                        "%'or Tamaño like '%" + palabra +
                        "%'or Color like '%" + palabra +
                        "%'or Claridad like '%" + palabra +
                        "%'or Peso like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or [Precio Lista] like '%" + palabra +
                        "%'or Tienda like '%" + palabra +
                        "%'or Comprobante like '%" + palabra +
                        "%'or n like '%" + palabra +
                        "%'or Fecha like '%" + palabra + "%')";
                }
                else
                {
                    salida_datos += " and (Código like '%" + palabra +
                        "%' or Tipo like '%" + palabra +
                        "%' or Descripción like '%" + palabra +
                        "%'or Material like '%" + palabra +
                        "%'or Vitrina like '%" + palabra +
                        "%'or Tamaño like '%" + palabra +
                        "%'or Color like '%" + palabra +
                        "%'or Claridad like '%" + palabra +
                        "%'or Peso like '%" + palabra +
                        "%'or Moneda like '%" + palabra +
                        "%'or [Precio Lista] like '%" + palabra +
                        "%'or Tienda like '%" + palabra +
                        "%'or Comprobante like '%" + palabra +
                        "%'or n like '%" + palabra +
                        "%'or Fecha like '%" + palabra + "%')";
                }

            }
            this.mifiltro.RowFilter = salida_datos;
        }
        private void stock_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                string codigo = stock.Rows[e.RowIndex].Cells[0].Value.ToString();
                tabRegistrosPic.Image = FastLoad(mainPath + "\\Joyas\\" + codigo + ".jpg");

            }


        }
    }
}

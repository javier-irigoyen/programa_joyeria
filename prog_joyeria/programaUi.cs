using System;
using System.Data;
using System.Windows.Forms;

namespace prog_joyeria
{
    partial class Form1 : Form
    {
        private void CustomizeDesing()
        {
            pnlBuscar.Visible = false;
            pnlStock.Visible = false;
            pnlRegistros.Visible = false;
            pnlVender.Visible = false;
            pnlEnviar.Visible = false;
            pnlGraficos.Visible = false;
           
            pnlPagos.Visible = false;
            pnlTrabajos.Visible = false;
            
        }

        private void hideSubmenu()
        {
            if (pnlBuscar.Visible == true)
                pnlBuscar.Visible = false;
            if (pnlStock.Visible == true)
                pnlStock.Visible = false;
            if (pnlRegistros.Visible == true)
                pnlRegistros.Visible = false;
            if (pnlVender.Visible == true)
                pnlVender.Visible = false;

            if (pnlEnviar.Visible == true)
                pnlEnviar.Visible = false;

            if (pnlGraficos.Visible == true)
                pnlGraficos.Visible = false;

            

            if (pnlPagos.Visible == true)
                pnlPagos.Visible = false;

            if (pnlTrabajos.Visible == true)
                pnlTrabajos.Visible = false;

        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubmenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void logo_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCotizacion";
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlBuscar);
            tabMain.PageName = "tabBuscar";

        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlStock);
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            
            tabMain.PageName = "tabIngresar";


        }

        private void btnStockEditar_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabEditar";
        }


        private void btnVender_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlVender);

        }

        private void btnDiamante_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabDiamante";
        }

        

        private void btnVolver_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabIngresar";
        }
        private void btnVenderCliente_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabVenderCliente";
        }
        private void btnRegistros_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlRegistros);
           
        }
        private void btnRegistrosStock_Click(object sender, EventArgs e)
        {
            //this.mifiltro = consulta().DefaultView;
            
            tabMain.PageName = "tabRegistrosStock";

        }
        private void btnRegistrosVentas_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabRegistrosVentas";

        }

        private void btnRegVentasPagos_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabRegVentasPagos";
        }

        private void btnSockControl_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "ControlStock";
        }


        private void btnEnviar_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlEnviar);
        }

        private void btnTrabajos_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlTrabajos);
        }

        

        private void btnGraficos_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlGraficos);
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlPagos);
        }


        private void btnBuscarDescripcion_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabBuscarFiltro";
        }

        private void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabBuscarCodigo";
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "Comerciantes";
        }

        //ui que muestra submenu al darle click btnMaterial
        private void ComBtnPalomino_Click(object sender, EventArgs e)
        {

            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnPalomino.Text;
            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void tabComBtnVolver_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "Comerciantes";
            tabComNombre.Text = "";
        }

        private void ComBtnHenry_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnHenry.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void ComBtnKelly_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnKelly.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void ComBtnBlanca_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnBlanca.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void ComBtnDelia_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnDelia.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void ComBtnMario_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnMario.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }

        private void ComBtnRicardo_Click(object sender, EventArgs e)
        {
            tabMain.PageName = "tabCom";
            tabComNombre.Text = ComBtnRicardo.Text;

            DataTable dtPagosVentaMat = consulta($"select * from TablaPagosVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaPagosVentaMat.DataSource = dtPagosVentaMat;

            DataTable dtVentaMat = consulta($"select * from TablaVentaMaterial where Comerciante='{tabComNombre.Text}'");
            tabComDgTablaVenderMat.DataSource = dtVentaMat;

            tabComDeuda.Text = deudaSumaVentaRestaPago(tabComCbMonedaDeuda.Text, tabComDgTablaVenderMat,
             "dataGridViewTextBoxColumn40", "dataGridViewTextBoxColumn41", tabComDgTablaPagosVentaMat,
             "dataGridViewTextBoxColumn44", "montoCuentaDataGridViewTextBoxColumn", tabVenderCliCambioDolar.Text);
        }
        private void btnComprobante_Click(object sender, EventArgs e)
        {
            if (tabCbIngresarComprobante.Visible == false &&
                ingLbCom.Visible == false &&
                ingLbN.Visible == false &&
                tabTxtIngresarN.Visible == false)

            {
                tabCbIngresarComprobante.Visible = true;
                ingLbCom.Visible = true;
                ingLbN.Visible = true;
                tabTxtIngresarN.Visible = true;
            }
            else
            {
                tabCbIngresarComprobante.Visible = false;
                ingLbCom.Visible = false;
                ingLbN.Visible = false;
                tabTxtIngresarN.Visible = false;
            }
        }

        private void btnEditarComprobante_Click(object sender, EventArgs e)
        {
            if (tabCbEditarComprobante.Visible == false &&
                editarLbCom.Visible == false &&
                editarLbN.Visible == false &&
                tabTxtEditarN.Visible == false)

            {
                tabCbEditarComprobante.Visible = true;
                editarLbCom.Visible = true;
                editarLbN.Visible = true;
                tabTxtEditarN.Visible = true;
            }
            else
            {
                tabCbEditarComprobante.Visible = false;
                editarLbCom.Visible = false;
                editarLbN.Visible = false;
                tabTxtEditarN.Visible = false;
            }
        }

        //ui de vender cliente
        //mostrar para indicar el material que se esta comprando para la venta
        private void tabVenderCliModoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabVenderCliModoPago.SelectedItem.ToString() == "Material")
            {
                tabVenderCliMaterialCompra.Visible = true;
                tabVenderCliComprarPeso.Visible = true;
                tabVenderCliLbMaterialCompra.Visible = true;
                tabVenderCliLbComprarPeso.Visible = true;
            }
            else
            {
                tabVenderCliMaterialCompra.Visible = false;
                tabVenderCliComprarPeso.Visible = false;
                tabVenderCliLbMaterialCompra.Visible = false;
                tabVenderCliLbComprarPeso.Visible = false;
            }

        }


        //ocultar submenu de devolucion de dinero
        private void tabComSwDevDin_OnValuechange(object sender, EventArgs e)
        {
            if (tabComSwDevDin.Value == false)
            {
                tabComSlide.Visible = false;
                tabComLbDevDin.Visible = false;
                tabComLbDevDinModPag.Visible = false;
                tabComCbDevDinModoPago.Visible = false;
                tabComLbDevDinFechaPago.Visible = false;
                tabComDevDinFechaPago.Visible = false;
                tabComDevDinHora.Visible = false;
                tabComDevDinMonedas.Visible = false;
                tabComDevDinMonUSD.Visible = false;
                tabComDevDinMonUSDLb.Visible = false;
                tabComDevDinMonSoles.Visible = false;
                tabComDevDinMonSolesLb.Visible = false;
                tabComLbDevDinMontoDev.Visible = false;
                tabComDevDinMontoDev.Visible = false;
                tabComBtnDevDin.Visible = false;
            }
            else
            {
                tabComSlide.Visible = true;
                tabComLbDevDin.Visible = true;
                tabComLbDevDinModPag.Visible = true;
                tabComCbDevDinModoPago.Visible = true;
                tabComLbDevDinFechaPago.Visible = true;
                tabComDevDinFechaPago.Visible = true;
                tabComDevDinHora.Visible = true;
                tabComDevDinMonedas.Visible = true;
                tabComDevDinMonUSD.Visible = true;
                tabComDevDinMonUSDLb.Visible = true;
                tabComDevDinMonSoles.Visible = true;
                tabComDevDinMonSolesLb.Visible = true;
                tabComLbDevDinMontoDev.Visible = true;
                tabComDevDinMontoDev.Visible = true;
                tabComBtnDevDin.Visible = true;
            }
        }

        //restricción de caracteres 
        private void tabRegistroTextBoxFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 46) || (e.KeyChar >= 58 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96)
                || (e.KeyChar >= 123 && e.KeyChar <= 129))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarStockCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 105) || (e.KeyChar >= 107 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 255) || (e.KeyChar == 47))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) ||
                (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarTamano_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 88) ||
               (e.KeyChar >= 90 && e.KeyChar <= 120) || (e.KeyChar >= 122 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        private void tabTxtEditarClaridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32) && (e.KeyChar != 49) && (e.KeyChar != 70) && (e.KeyChar != 102) &&
                (e.KeyChar != 86) && (e.KeyChar != 118) && (e.KeyChar != 83) && (e.KeyChar != 115) &&
                (e.KeyChar != 50) && (e.KeyChar != 51) && (e.KeyChar != 89) && (e.KeyChar != 121))
            {
                e.Handled = true;
                return;
            }
        }

    }

}
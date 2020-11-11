using System;
using Bunifu.UI.WinForms;
using System.Windows.Forms;

namespace prog_joyeria
{
    partial class Form1
    {
        private void CustomizeDesing()
        {
            pnlBuscar.Visible = false;
            pnlIngresar.Visible = false;
            pnlRegistros.Visible = false;
            pnlVender.Visible = false;
        }

        private void hideSubmenu()
        {
            if (pnlBuscar.Visible == true)
                pnlBuscar.Visible = false;
            if (pnlIngresar.Visible == true)
                pnlIngresar.Visible = false;
            if (pnlRegistros.Visible == true)
                pnlRegistros.Visible = false;
            if (pnlVender.Visible == true)
                pnlVender.Visible = false;

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
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlBuscar);
            mainPage.PageName = "tabBuscar";

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlIngresar);
            mainPage.PageName = "tabIngresar";
            

        }
        private void btnVender_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlVender);
            
        }

        private void btnDiamante_Click(object sender, EventArgs e)
        {
            mainPage.PageName = "tabDiamante";
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            mainPage.PageName = "tabIngresar";
        }
        private void btnVenderCliente_Click(object sender, EventArgs e)
        {
            mainPage.PageName = "tabVenderCliente";
        }
        private void btnRegistros_Click(object sender, EventArgs e)
        {
            mainPage.PageName = "tabRegistros";
        }
        private void btnComprobante_Click(object sender, EventArgs e)
        {
            if (ingCombCom.Visible==false &&
                ingLbCom.Visible==false &&
                ingLbN.Visible==false &&
                ingTextN.Visible==false)
                
            {
                ingCombCom.Visible = true;
                ingLbCom.Visible = true;
                ingLbN.Visible = true;
                ingTextN.Visible = true;
            }
            else
            {
                ingCombCom.Visible = false;
                ingLbCom.Visible = false;
                ingLbN.Visible = false;
                ingTextN.Visible = false;
            }   
        }

        
    }   

}
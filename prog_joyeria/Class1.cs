using Guna.UI.WinForms;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog_joyeria
{
    public class BtnMenu
    {

        private Form1 form;
        public GunaButton btn
        { get; set; }

        public Panel btnLb
        { get; set; }

        public BtnMenu(GunaButton btn,Panel btnLb)
           {
            
           }

        public void btn_MouseEnter(object sender, EventArgs e)
        {
            btn.BackColor = Color.FromArgb(39, 50, 170);
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            btnLb.BackColor = Color.FromArgb(1, 3, 19);
        }

        private void btnLb_MouseEnter(object sender, EventArgs e)
        {
            btnLb.BackColor = Color.FromArgb(39, 50, 170);
            btn.BaseColor = Color.FromArgb(12, 18, 41);

        }

        private void btnLb_MouseLeave(object sender, EventArgs e)
        {
            btnLb.BackColor = Color.FromArgb(1, 3, 19);
            btn.BaseColor = Color.FromArgb(1, 3, 19);
        }
    }
}

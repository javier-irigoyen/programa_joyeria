﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog_joyeria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            btnLb.BackColor=Color.FromArgb(39, 50, 170);
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            btnLb.BackColor = Color.FromArgb(1, 3, 19);
        }
    }  
    
}

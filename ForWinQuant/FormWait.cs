// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    FormWait
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/20 17:20:54
// Update Time         :    2020/5/20 17:20:54
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant
{
    public partial class FormWait : Form
    {
        private int count = 3;
        public FormWait(int count=3)
        {
            InitializeComponent();
            this.count = count;
        }

        private void timerWait_Tick(object sender, EventArgs e)
        {
            count -= 1;
            if (count == 0)
            {
                this.timerWait.Stop();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

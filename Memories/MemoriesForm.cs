﻿using System;
using System.Windows.Forms;

namespace Memories
{
    public partial class MemoriesForm : Form
    {
        public MemoriesForm()
        {
            InitializeComponent();
        }

        private void btnAutoOrganize_Click(object sender, EventArgs e)
        {
            Organizer.Helper.Run();
        }
    }
}

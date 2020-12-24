namespace Memories
{
    partial class MemoriesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAutoOrganize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAutoOrganize
            // 
            this.btnAutoOrganize.Location = new System.Drawing.Point(12, 314);
            this.btnAutoOrganize.Name = "btnAutoOrganize";
            this.btnAutoOrganize.Size = new System.Drawing.Size(776, 23);
            this.btnAutoOrganize.TabIndex = 0;
            this.btnAutoOrganize.Text = "Auto Organize";
            this.btnAutoOrganize.UseVisualStyleBackColor = true;
            this.btnAutoOrganize.Click += new System.EventHandler(this.btnAutoOrganize_Click);
            // 
            // MemoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAutoOrganize);
            this.Name = "MemoriesForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAutoOrganize;
    }
}


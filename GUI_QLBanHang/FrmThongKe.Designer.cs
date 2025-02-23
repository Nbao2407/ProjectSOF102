namespace TemplateProject1_QLBanHang
{
    partial class FrmThongKe
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
            this.tcThongKe = new System.Windows.Forms.TabControl();
            this.tpsanpham = new System.Windows.Forms.TabPage();
            this.tptonkho = new System.Windows.Forms.TabPage();
            this.dgvsp = new System.Windows.Forms.DataGridView();
            this.dgvtonkho = new System.Windows.Forms.DataGridView();
            this.tcThongKe.SuspendLayout();
            this.tpsanpham.SuspendLayout();
            this.tptonkho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtonkho)).BeginInit();
            this.SuspendLayout();
            // 
            // tcThongKe
            // 
            this.tcThongKe.Controls.Add(this.tpsanpham);
            this.tcThongKe.Controls.Add(this.tptonkho);
            this.tcThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcThongKe.Location = new System.Drawing.Point(0, 0);
            this.tcThongKe.Name = "tcThongKe";
            this.tcThongKe.SelectedIndex = 0;
            this.tcThongKe.Size = new System.Drawing.Size(489, 235);
            this.tcThongKe.TabIndex = 0;
            this.tcThongKe.Selected += new System.Windows.Forms.TabControlEventHandler(this.TcThongKe_Selected);
            // 
            // tpsanpham
            // 
            this.tpsanpham.CausesValidation = false;
            this.tpsanpham.Controls.Add(this.dgvsp);
            this.tpsanpham.Location = new System.Drawing.Point(4, 22);
            this.tpsanpham.Name = "tpsanpham";
            this.tpsanpham.Padding = new System.Windows.Forms.Padding(3);
            this.tpsanpham.Size = new System.Drawing.Size(481, 209);
            this.tpsanpham.TabIndex = 0;
            this.tpsanpham.Text = "Sản Phẩm Nhập Kho";
            this.tpsanpham.UseVisualStyleBackColor = true;
            // 
            // tptonkho
            // 
            this.tptonkho.CausesValidation = false;
            this.tptonkho.Controls.Add(this.dgvtonkho);
            this.tptonkho.Location = new System.Drawing.Point(4, 22);
            this.tptonkho.Name = "tptonkho";
            this.tptonkho.Padding = new System.Windows.Forms.Padding(3);
            this.tptonkho.Size = new System.Drawing.Size(481, 209);
            this.tptonkho.TabIndex = 1;
            this.tptonkho.Text = "Tồn Kho";
            this.tptonkho.UseVisualStyleBackColor = true;
            // 
            // dgvsp
            // 
            this.dgvsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsp.Location = new System.Drawing.Point(0, 0);
            this.dgvsp.Name = "dgvsp";
            this.dgvsp.Size = new System.Drawing.Size(549, 296);
            this.dgvsp.TabIndex = 0;
            // 
            // dgvtonkho
            // 
            this.dgvtonkho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtonkho.Location = new System.Drawing.Point(2, 0);
            this.dgvtonkho.Name = "dgvtonkho";
            this.dgvtonkho.Size = new System.Drawing.Size(480, 224);
            this.dgvtonkho.TabIndex = 0;
            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 235);
            this.Controls.Add(this.tcThongKe);
            this.MaximizeBox = false;
            this.Name = "FrmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmThongKe";
            this.tcThongKe.ResumeLayout(false);
            this.tpsanpham.ResumeLayout(false);
            this.tptonkho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtonkho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcThongKe;
        private System.Windows.Forms.TabPage tpsanpham;
        private System.Windows.Forms.TabPage tptonkho;
        private System.Windows.Forms.DataGridView dgvsp;
        private System.Windows.Forms.DataGridView dgvtonkho;
    }
}
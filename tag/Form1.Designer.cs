namespace tag
{
    partial class tag
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbIpt = new System.Windows.Forms.TextBox();
            this.tbOpt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbIpt
            // 
            this.tbIpt.Location = new System.Drawing.Point(27, 33);
            this.tbIpt.Multiline = true;
            this.tbIpt.Name = "tbIpt";
            this.tbIpt.Size = new System.Drawing.Size(478, 146);
            this.tbIpt.TabIndex = 1;
            this.tbIpt.TextChanged += new System.EventHandler(this.tbIpt_TextChanged);
            // 
            // tbOpt
            // 
            this.tbOpt.Location = new System.Drawing.Point(27, 217);
            this.tbOpt.Multiline = true;
            this.tbOpt.Name = "tbOpt";
            this.tbOpt.Size = new System.Drawing.Size(478, 146);
            this.tbOpt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "原文";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "标注结果";
            // 
            // tag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(532, 397);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOpt);
            this.Controls.Add(this.tbIpt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "tag";
            this.Text = "词性标注";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIpt;
        private System.Windows.Forms.TextBox tbOpt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


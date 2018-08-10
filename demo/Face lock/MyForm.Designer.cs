namespace Face_lock
{
    partial class MyForm
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
            this._sourcePictureBox = new System.Windows.Forms.PictureBox();
            this._trainButton = new System.Windows.Forms.Button();
            this._identityButton = new System.Windows.Forms.Button();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._sourcePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _sourcePictureBox
            // 
            this._sourcePictureBox.Location = new System.Drawing.Point(17, 54);
            this._sourcePictureBox.Name = "_sourcePictureBox";
            this._sourcePictureBox.Size = new System.Drawing.Size(341, 240);
            this._sourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._sourcePictureBox.TabIndex = 0;
            this._sourcePictureBox.TabStop = false;
            // 
            // _trainButton
            // 
            this._trainButton.Location = new System.Drawing.Point(206, 300);
            this._trainButton.Name = "_trainButton";
            this._trainButton.Size = new System.Drawing.Size(75, 23);
            this._trainButton.TabIndex = 6;
            this._trainButton.Text = "training";
            this._trainButton.UseVisualStyleBackColor = true;
            this._trainButton.Click += new System.EventHandler(this._trainButton_Click);
            // 
            // _identityButton
            // 
            this._identityButton.Enabled = false;
            this._identityButton.Location = new System.Drawing.Point(283, 300);
            this._identityButton.Name = "_identityButton";
            this._identityButton.Size = new System.Drawing.Size(75, 23);
            this._identityButton.TabIndex = 5;
            this._identityButton.Text = "identity";
            this._identityButton.UseVisualStyleBackColor = true;
            this._identityButton.Click += new System.EventHandler(this._identityButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體-ExtB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "AI 體驗營";
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 325);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._trainButton);
            this.Controls.Add(this._identityButton);
            this.Controls.Add(this._sourcePictureBox);
            this.Name = "MyForm";
            this.Text = "MyForm";
            this.Load += new System.EventHandler(this.MyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._sourcePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _sourcePictureBox;
        private System.Windows.Forms.Button _trainButton;
        private System.Windows.Forms.Button _identityButton;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Label label1;
    }
}
namespace Face_lock
{
    partial class IdentityResultForm
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
            this._catchPictureBox = new System.Windows.Forms.PictureBox();
            this._doorLabel = new System.Windows.Forms.Label();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._face = new System.Windows.Forms.DataGridViewImageColumn();
            this._name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._smile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._catchPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _catchPictureBox
            // 
            this._catchPictureBox.Location = new System.Drawing.Point(14, 43);
            this._catchPictureBox.Name = "_catchPictureBox";
            this._catchPictureBox.Size = new System.Drawing.Size(320, 240);
            this._catchPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._catchPictureBox.TabIndex = 1;
            this._catchPictureBox.TabStop = false;
            // 
            // _doorLabel
            // 
            this._doorLabel.BackColor = System.Drawing.Color.Transparent;
            this._doorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._doorLabel.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._doorLabel.Location = new System.Drawing.Point(0, 0);
            this._doorLabel.Name = "_doorLabel";
            this._doorLabel.Size = new System.Drawing.Size(890, 30);
            this._doorLabel.TabIndex = 8;
            this._doorLabel.Text = "處理中...";
            // 
            // _dataGridView
            // 
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._face,
            this._name,
            this._gender,
            this._age,
            this._smile});
            this._dataGridView.Location = new System.Drawing.Point(340, 43);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.RowTemplate.Height = 100;
            this._dataGridView.Size = new System.Drawing.Size(544, 240);
            this._dataGridView.TabIndex = 11;
            // 
            // _face
            // 
            this._face.HeaderText = "Face";
            this._face.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this._face.Name = "_face";
            // 
            // _name
            // 
            this._name.HeaderText = "Name";
            this._name.Name = "_name";
            this._name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _gender
            // 
            this._gender.HeaderText = "Gender";
            this._gender.Name = "_gender";
            // 
            // _age
            // 
            this._age.HeaderText = "Age";
            this._age.Name = "_age";
            // 
            // _smile
            // 
            this._smile.HeaderText = "Smile";
            this._smile.Name = "_smile";
            this._smile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._smile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IdentityResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 294);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this._doorLabel);
            this.Controls.Add(this._catchPictureBox);
            this.Name = "IdentityResultForm";
            this.Text = "IdentityResultForm";
            ((System.ComponentModel.ISupportInitialize)(this._catchPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _catchPictureBox;
        private System.Windows.Forms.Label _doorLabel;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewImageColumn _face;
        private System.Windows.Forms.DataGridViewTextBoxColumn _name;
        private System.Windows.Forms.DataGridViewTextBoxColumn _gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn _age;
        private System.Windows.Forms.DataGridViewTextBoxColumn _smile;
    }
}
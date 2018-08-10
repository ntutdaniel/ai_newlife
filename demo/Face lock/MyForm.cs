using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.Util;
using System.IO;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace Face_lock
{
    public partial class MyForm : Form
    {
        Model _model;

        public MyForm()
        {
            InitializeComponent();
            _model = new Model();
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            Application.Idle += HandleApplicationIdle;
            DirectoryInfo startUpPath = new DirectoryInfo(Application.StartupPath);
            string projectPath = startUpPath.Parent.Parent.Parent.FullName;
            _folderBrowserDialog.SelectedPath = projectPath + "\\PersonGroup";
        }

        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            _sourcePictureBox.Image = _model.GetSourceBitmap();
        }
        
        private async void _trainButton_Click(object sender, EventArgs e)
        {
            if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                await _model.ProcessTraining(_folderBrowserDialog.SelectedPath);
                _identityButton.Enabled = true;                
            }
        }

        private void _identityButton_Click(object sender, EventArgs e)
        {
            DirectoryInfo startUpPath = new DirectoryInfo(Application.StartupPath);
            string catchPath = startUpPath.Parent.Parent.Parent.FullName + "\\catch";
            Console.WriteLine(catchPath);
            _sourcePictureBox.Image.Save(catchPath + "\\catch.jpg");
            _model.ProcessIdentify(catchPath + "\\catch.jpg");
        }
    }
}

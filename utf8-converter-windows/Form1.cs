using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertToUtf8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    txtbxPath.Text = folderBrowserDialog1.SelectedPath;
                    progressBar1.Value = 0;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxPath.Text) || txtbxPath.Text.Trim() == string.Empty || txtbxWildcard.Text.Trim() == string.Empty)
                return;

            if (!Directory.Exists(txtbxPath.Text))
            {
                MessageBox.Show("Path does not exist", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            
            FileInfo[] files = new DirectoryInfo(txtbxPath.Text).GetFiles(txtbxWildcard.Text, SearchOption.AllDirectories);

            progressBar1.Value = 0;
            progressBar1.Maximum = files.Length;

            foreach (var f in files)
            {
                string s = File.ReadAllText(f.FullName);
                File.WriteAllText(f.FullName, s, Encoding.UTF8);
                progressBar1.PerformStep();
            }

        }
    }
}

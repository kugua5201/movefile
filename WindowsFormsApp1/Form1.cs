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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            textBox1.Visible = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        private string sourceFolder = "";
        private string targetFolder = "";
        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    sourceFolder = dialog.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    targetFolder = dialog.SelectedPath;
                }
            }
        }

        static void ProcessFilesRecursive(string folderPath, string targetFolder, ref int count)
        {
            string[] movFiles = Directory.GetFiles(folderPath, "*.mov");
            //string[] jpgFiles = Directory.GetFiles(folderPath, "*.jpg");
            //string[] heicjpgFiles = Directory.GetFiles(folderPath, "*.heic");
            foreach (string movFile in movFiles)
            {
                string fileName = Path.GetFileName(movFile);
                string randomString = GenerateRandomString(10);
                string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".mov";
                string targetPath = Path.Combine(targetFolder, targetFileName);
                File.Copy(movFile, targetPath);

                Console.WriteLine("源文件：" + movFile);
                Console.WriteLine("目标文件：" + targetPath);
                Console.WriteLine("计数：" + count);
                count++;
            }
            string[] subFolders = Directory.GetDirectories(folderPath);
            foreach (string subFolder in subFolders)
            {
                ProcessFilesRecursive(subFolder, targetFolder, ref count);
            }
        }
        static void ProcessFilesRecursive(string folderPath, string targetFolder, ref int count, string type)
        {
            string[] movFiles = Directory.GetFiles(folderPath, "*" + type);
            string outputFolder = targetFolder;
            string outputFile = Path.Combine(outputFolder, "output.txt");
            foreach (string movFile in movFiles)
            {
                string fileName = Path.GetFileName(movFile);
                string randomString = GenerateRandomString(10);
                string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + type;
                string targetPath = Path.Combine(targetFolder, targetFileName);
                File.Copy(movFile, targetPath);
                if (File.Exists(targetPath))
                {
                    DateTime currentTime = DateTime.Now;
                    using (StreamWriter writer = new StreamWriter(outputFile, true))
                    {
                        writer.WriteLine("文件：" + targetPath);
                        writer.WriteLine("计数：" + count + "，时间：" + currentTime.ToString());
                    }
                    count++;
                }
            }
            #region 其他测试
            //foreach (string jpgFile in jpgFiles)
            //{
            //    string fileName = Path.GetFileName(jpgFile);
            //    string randomString = GenerateRandomString(10);
            //    string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".jpg";
            //    string targetPath = Path.Combine(targetFolder, targetFileName);
            //    File.Copy(movFile, targetPath);

            //    Console.WriteLine("源文件：" + jpgFile);
            //    Console.WriteLine("目标文件：" + targetPath);
            //    Console.WriteLine("计数：" + count);
            //    count++;
            //}

            //foreach (string heicFile in heicjpgFiles)
            //{
            //    string fileName = Path.GetFileName(heicFile);
            //    string randomString = GenerateRandomString(10);
            //    string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".jpg";
            //    string targetPath = Path.Combine(targetFolder, targetFileName);
            //    File.Copy(movFile, targetPath);

            //    Console.WriteLine("源文件：" + heicFile);
            //    Console.WriteLine("目标文件：" + targetPath);
            //    Console.WriteLine("计数：" + count);
            //    count++;
            //}
            #endregion
            string[] subFolders = Directory.GetDirectories(folderPath);
            foreach (string subFolder in subFolders)
            {
                ProcessFilesRecursive(subFolder, targetFolder, ref count, type);
            }
        }

        string photoValue = "";
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                string defaultExtension = ".jpg";
                comboBox1.SelectedItem = defaultExtension;
                photoValue = comboBox1.SelectedItem.ToString();
            }
            else if (textBox1.Text == ""&&comboBox1.SelectedItem.ToString()=="无此列")
            {
                MessageBox.Show("请输入后在提交哦");
                return;
            }
            else if (textBox1.Text != "")
            {

                photoValue = textBox1.Text;
                if (!photoValue.StartsWith("."))
                {
                    photoValue = "." + photoValue;
                }

            }
            else
            {
                photoValue = comboBox1.SelectedItem.ToString();
            }
            if (string.IsNullOrEmpty(sourceFolder) || string.IsNullOrEmpty(targetFolder))
            {
                MessageBox.Show("请选择源文件夹和目标文件夹！");
                return;
            }
            int count = 1;
            ProcessFilesRecursive(sourceFolder, targetFolder, ref count, photoValue);
            MessageBox.Show("操作完成！");

        }
        private static string GenerateRandomString(int length)
        {
            Guid guid = Guid.NewGuid();
            string randomString = guid.ToString("N").Substring(0, length);
            return randomString;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "无此列")
            {
                label1.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                textBox1.Text = "";
                photoValue= comboBox1.SelectedItem.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections;

namespace ManageImages
{
    public partial class ManageImages : Form
    {
        ArrayList PicArr = new ArrayList(); 
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        int locX = 20;
        int locY = 10;
        int sizeWidth = 30;
        int sizeHeight = 30;
        public ManageImages()
        {
            InitializeComponent();
        }

        private void ManageImages_Load(object sender, EventArgs e)
        {
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            locX = 20;
            locY = 10;
            sizeWidth = 30;
            sizeHeight = 30;

            // load sections
            LoadSections();
        }

        public void LoadSections()
        {
            ddlSections.DataSource = Data.LoadSections();
            ddlSections.DisplayMember = "folder";
        }

        private void loadImagestoPanel(String imageName, String ImageFullName, int newLocX, int newLocY)
        {
            PictureBox ctrl = new PictureBox();
            ctrl.Image = Image.FromFile(ImageFullName);
            ctrl.BackColor = Color.Black;
            ctrl.Location = new Point(newLocX, newLocY);
            ctrl.Size = new System.Drawing.Size(sizeWidth, sizeHeight);
            ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseClick += new MouseEventHandler(control_MouseClick);
            pnControls.Controls.Add(ctrl);
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            //Control control = (Control)sender;
            //PictureBox pic = (PictureBox)control;
            //PreviewPictureBox.Image = pic.Image;
        }

        private void control_MouseClick(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            PictureBox pic = (PictureBox)control;
            PreviewPictureBox.Image = pic.Image;

            // load image info
            txtFilename.Text = PicArr[control.TabIndex].ToString();
            string s = Data.GetGenderInfo(txtFilename.Text, ddlSections.Text);
            ddlGender.SelectedIndex = ddlGender.FindStringExact(Data.GetGenderInfo(txtFilename.Text, ddlSections.Text));

        }

        private void loadControls()
        {
            int locnewX = locX;
            int locnewY = locY;

            foreach (Control p in pnControls.Controls)
            {
                if (locnewX >= pnControls.Width - sizeWidth - 10)
                {
                    locnewX = locX;
                    locY = locY + sizeHeight + 30;
                    locnewY = locY;
                }
                else
                {

                    locnewY = locY;
                }
                p.Location = new Point(locnewX, locnewY);
                p.Size = new System.Drawing.Size(sizeWidth, sizeHeight);

                locnewY = locY + sizeHeight + 10;
                locnewX = locnewX + sizeWidth + 10;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int SaveVal = 0;
            locX = 20;
            locY = 10;
            sizeWidth = 50;
            sizeHeight = 50;
            foreach (Control p in pnControls.Controls)
            {
                SaveVal = SaveVal + 1;
            }
            if (SaveVal > 0)
            {
                loadControls();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int SaveVal = 0;
            locX = 20;
            locY = 10;
            sizeWidth = 160;
            sizeHeight = 160;
            foreach (Control p in pnControls.Controls)
            {
                SaveVal = SaveVal + 1;
            }
            if (SaveVal > 0)
            {
                loadControls();
            }
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int SaveVal = 0;
            locX = 20;
            locY = 10;
            sizeWidth = 80;
            sizeHeight = 80;
            foreach (Control p in pnControls.Controls)
            {
                SaveVal = SaveVal + 1;
            }
            if (SaveVal > 0)
            {
                loadControls();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            locX = 20;
            locY = 10;
            sizeWidth = 30;
            sizeHeight = 30;
            if (pnControls.Controls.Count > 0)
            {
                loadControls();
            }
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo Folder;
            FileInfo[] Images;
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDlg.ShowNewFolderButton = false;
            DialogResult result = this.folderBrowserDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                Folder = new DirectoryInfo(folderBrowserDlg.SelectedPath);
                Images = Folder.GetFiles();

                pnControls.Controls.Clear();

                int locnewX = locX;
                int locnewY = locY;
                foreach (FileInfo img in Images)
                {
                    if (img.Extension.ToLower() == ".png" || img.Extension.ToLower() == ".jpg" || img.Extension.ToLower() == ".gif" || img.Extension.ToLower() == ".jpeg" || img.Extension.ToLower() == ".bmp" || img.Extension.ToLower() == ".tif")
                    {

                        if (locnewX >= pnControls.Width - sizeWidth - 10)
                        {
                            locnewX = locX;
                            locY = locY + sizeHeight + 30;
                            locnewY = locY;
                        }
                        else
                        {

                            locnewY = locY;
                        }

                        loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY);
                        locnewY = locY + sizeHeight + 10;
                        locnewX = locnewX + sizeWidth + 10;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pnControls.Width, pnControls.Height);
            pnControls.DrawToBitmap(bmp, new Rectangle(0, 0, pnControls.Width, pnControls.Height));
            SaveFileDialog dlg = new SaveFileDialog();
            // dlg.Filter = "JPG Files (*.JPG)|*.JPG";
            dlg.FileName = "*";
            dlg.DefaultExt = "bmp";
            dlg.ValidateNames = true;
            dlg.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |JPEG Image (.jpeg)|*.jpeg |Png Image (.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PreviewPictureBox.Image.Save(dlg.FileName);
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            string FolderName = ddlSections.Text;
            string FileName = "";
            if (txtFilename.Text == "")
            {
                FileName = PreviewPictureBox.Name;
            }
            else
            {
                FileName = txtFilename.Text;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Enter Description.");
                return;
            }
            FileInfo toUpload = new FileInfo("FileName");
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://208.118.63.29/site2/" + FolderName + "/" + FileName);
            request.KeepAlive = true;
            request.Proxy = null;
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("eplugplay-001", "Wutsup4321@");
            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead("C:\\RhinestoneImages\\sample.jpg");
            int length = 1024;
            byte[] buffer = new byte[length];
            int bytesRead = 0;
            do
            {
                bytesRead = file.Read(buffer, 0, length);
                ftpStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead != 0);
            file.Close();
            ftpStream.Close();

            //save to db
            Data.SaveImageToDb(FileName, txtDescription.Text, ddlGender.Text, FolderName);
            MessageBox.Show("Saved.");
        }

        private void loadWebsiteImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadImages(ddlSections.Text) == true)
            {
                LoadImages(ddlSections.Text);
            }
        }

        public string GetLocalImgPath(string folder)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(appPath);
            System.IO.DirectoryInfo directoryInfo2 = System.IO.Directory.GetParent(directoryInfo.FullName);
            string path = directoryInfo2.FullName + @"\Images\" + folder;
            return path;
        }

        public bool LoadImages(string folder)
        {
            bool AddedNew = false;
            DirectoryInfo Folder;
            FileInfo[] Images;
            this.folderBrowserDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDlg.ShowNewFolderButton = false;
            string s = GetLocalImgPath(folder);
            Folder = new DirectoryInfo(GetLocalImgPath(folder));
            Images = Folder.GetFiles();
            // download images from folder
            string[] files = GetFileList(folder);
            //List<string> Listfiles = new List<string>();
            PicArr.Clear();
            foreach (FileInfo file in Images)
            {
                PicArr.Add(file.Name);
            }
            foreach (string file in files)
            {
                if (!PicArr.Contains(file))
                {
                    AddedNew = true;
                    Download(folder, file);
                }
            }
            // load images from folder
            pnControls.Controls.Clear();
            int locnewX = locX;
            int locnewY = locY;
            foreach (FileInfo img in Images)
            {
                if (img.Extension.ToLower() == ".png" || img.Extension.ToLower() == ".jpg" || img.Extension.ToLower() == ".gif" || img.Extension.ToLower() == ".jpeg" || img.Extension.ToLower() == ".bmp" || img.Extension.ToLower() == ".tif")
                {

                    if (locnewX >= pnControls.Width - sizeWidth - 10)
                    {
                        locnewX = locX;
                        locY = locY + sizeHeight + 30;
                        locnewY = locY;
                    }
                    else
                    {

                        locnewY = locY;
                    }

                    loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY);
                    locnewY = locY + sizeHeight + 10;
                    locnewX = locnewX + sizeWidth + 10;
                }
            }
            return AddedNew;
        }

        public string[] GetFileList(string folder)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://208.118.63.29/site2/" + folder));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential("eplugplay-001", "Wutsup4321@");
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        private void Download(string folder, string file)
        {
            try
            {
                if (!file.Contains("xml"))
                {
                    string uri = "ftp://208.118.63.29/site2/" + "/" + folder + "/" + file;
                    Uri serverUri = new Uri(uri);
                    if (serverUri.Scheme != Uri.UriSchemeFtp)
                    {
                        return;
                    }
                    FtpWebRequest reqFTP;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://208.118.63.29/site2/" + folder + "/" + file));
                    reqFTP.Credentials = new NetworkCredential("eplugplay-001", "Wutsup4321@");
                    reqFTP.KeepAlive = false;
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Proxy = null;
                    reqFTP.UsePassive = false;
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    FileStream writeStream = new FileStream(GetLocalImgPath(folder) + "\\" + file, FileMode.Create);

                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);
                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, Length);
                    }
                    writeStream.Close();
                    response.Close();
                }
            }
            catch (WebException wEx)
            {
                MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }
        public void DeleteServerImg(string folder, string file)
        {
            try
            {
                string uri = "ftp://208.118.63.29/site2/" + "/" + folder + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return;
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://208.118.63.29/site2/" + folder + "/" + file));
                reqFTP.Credentials = new NetworkCredential("eplugplay-001", "Wutsup4321@");
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (WebException wEx)
            {
                MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            // delete from server
            DeleteServerImg(ddlSections.Text, txtFilename.Text);
            // delete from db
            Data.DeleteImageDb(txtFilename.Text, ddlGender.Text, ddlSections.Text);
            // delete from local
            LoadImages(ddlSections.Text);
            MessageBox.Show("Deleted.");
        }

        private void pnControls_Click(object sender, EventArgs e)
        {
            string s = PreviewPictureBox.Name;
        }

        private void ddlSections_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddlSections.DataSource != null)
            {
                if (ddlSections.Text != "System.Data.DataRowView")
                {
                    if (LoadImages(ddlSections.Text) == true)
                    {
                        LoadImages(ddlSections.Text);
                    }
                }
            }
        }
    }
}

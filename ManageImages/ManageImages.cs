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
using System.IO;
using System.Configuration;


namespace ManageImages
{
    public partial class ManageImages : Form
    {
        private string imgSize { get; set; }
        ArrayList PicArr = new ArrayList(); 
        private System.Windows.Forms.OpenFileDialog folderBrowserDlg;
        int locX = 20;
        int locY = 10;
        int sizeWidth = 30;
        int sizeHeight = 30;
        public ManageImages()
        {
            InitializeComponent();
        }

        public void LoadSections()
        {
            ddlSections.DisplayMember = "category";
            ddlSections.ValueMember = "id";
            ddlSections.DataSource = Data.LoadSections();
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

        public void Check()
        {
            if (Data.CheckImgExist(txtFilename.Text, ddlSections.SelectedValue.ToString()) == true)
            {
                btnSaveEdit.Enabled = true;
                btnDeleteImg.Enabled = true;
                btnUploadImage.Enabled = false;
                btnDeleteImg.Enabled = true;
                btnUploadImage.Enabled = false;
            }
            else
            {
                btnSaveEdit.Enabled = false;
                btnDeleteImg.Enabled = false;
                btnUploadImage.Enabled = true;
                btnDeleteImg.Enabled = false;
                btnUploadImage.Enabled = true;
            }
        }

        #region toolStrips

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
            imgSize = "x-small";
            int SaveVal = 0;
            locX = 20;
            locY = 10;
            sizeWidth = 30;
            sizeHeight = 30;
            foreach (Control p in pnControls.Controls)
            {
                SaveVal = SaveVal + 1;
            }
            if (SaveVal > 0)
            {
                loadControls();
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            imgSize = "small";
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


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            imgSize = "medium";
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
            imgSize = "large";
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
        #endregion

        #region Events

        private void ManageImages_Load(object sender, EventArgs e)
        {
            this.folderBrowserDlg = new System.Windows.Forms.OpenFileDialog();
            locX = 20;
            locY = 10;
            sizeWidth = 30;
            sizeHeight = 30;

            // load sections
            LoadSections();
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
            txtDescription.Text = Data.GetDescriptionInfo(txtFilename.Text, ddlSections.SelectedValue.ToString());
            ddlGender.SelectedIndex = ddlGender.FindStringExact(Data.GetGenderInfo(txtFilename.Text, ddlSections.SelectedValue.ToString()));
            Check();
        }


        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDlg.Filter = "(*.bmp, *.jpg, *.jpeg, *png)|*.bmp;*.jpg;*.jpeg;*.png";
            DialogResult result = this.folderBrowserDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] split = folderBrowserDlg.FileName.Split('\\');
                FileStream file = File.OpenRead(folderBrowserDlg.FileName);
                // save local
                SaveLocal(file, split[split.Length -1]);
                file.Close();
                LoadImages(ddlSections.SelectedValue.ToString());
                MessageBox.Show("Imported.");
            }
        }


        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (PreviewPictureBox.Image == null)
            {
                MessageBox.Show("Select image first.");
                return;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Enter Description.");
                return;
            }
            if (ddlGender.Text == "")
            {
                MessageBox.Show("Select Male or Female.");
                return;
            }
            if (Data.CheckImgExist(txtFilename.Text, ddlSections.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Already exist in website.");
                return;
            }

            if (MessageBox.Show("Upload?", "Upload?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string FolderName = ddlSections.SelectedValue.ToString();
                string FileName = "";
                if (txtFilename.Text == "")
                {
                    FileName = PreviewPictureBox.Name;
                }
                else
                {
                    FileName = txtFilename.Text;
                }
                FileInfo toUpload = new FileInfo("FileName");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://208.118.63.29/site2/" + FolderName + "/" + FileName);
                request.KeepAlive = true;
                request.Proxy = null;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
                Stream ftpStream = request.GetRequestStream();
                FileStream file = File.OpenRead(GetLocalImgPath(ddlSections.SelectedValue.ToString()) + "\\" + txtFilename.Text);
                int length = 1024;
                byte[] buffer = new byte[length];
                int bytesRead = 0;
                do
                {
                    bytesRead = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesRead);
                }
                while (bytesRead != 0);
                //// save local
                //SaveLocal(file);
                file.Close();
                ftpStream.Close();
                //save to db
                Data.SaveImageToDb(FileName, txtDescription.Text, ddlGender.Text, FolderName);
                Check();
                MessageBox.Show("Saved.");
            }
        }


        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (PreviewPictureBox.Image == null)
            {
                MessageBox.Show("Select image first.");
                return;
            }
            if (MessageBox.Show("Upload?", "Upload?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // delete from server
                DeleteServerImg(ddlSections.SelectedValue.ToString(), txtFilename.Text);
                // delete from db
                Data.DeleteImageDb(txtFilename.Text, ddlGender.Text, ddlSections.SelectedValue.ToString());
                // delete from local
                if (File.Exists(GetLocalImgPath(ddlSections.SelectedValue.ToString()) + "\\" + txtFilename.Text))
                {
                    try
                    {
                        //Directory.Delete(GetLocalImgPath(ddlSections.SelectedValue.ToString()));
                        File.Delete(GetLocalImgPath(ddlSections.SelectedValue.ToString()) + "\\" + txtFilename.Text);
                    }
                    catch (Exception)
                    {

                    }
                }
                LoadImages(ddlSections.SelectedValue.ToString());
                Check();
                MessageBox.Show("Deleted.");
            }
        }

        private void pnControls_Click(object sender, EventArgs e)
        {
            string s = PreviewPictureBox.Name;
        }

        private void ddlSections_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            PreviewPictureBox.Image = null;
            txtDescription.Text = "";
            ddlGender.SelectedIndex = -1;
            txtFilename.Text = "";
            btnDeleteImg.Enabled = false;
            btnUploadImage.Enabled = false;

            switch (ddlSections.SelectedIndex)
            {
                case 0: txtSection.Text = "Apparels"; break;
                case 1: txtSection.Text = "New Arrivals"; break;
                case 2: txtSection.Text = "Pants"; break;
                case 3: txtSection.Text = "Rhinestones"; break;
                case 4: txtSection.Text = "Shirts"; break;
                case 5: txtSection.Text = "Women's Shoes"; break;
            }

            if (ddlSections.DataSource != null)
            {
                if (ddlSections.SelectedIndex != -1)
                {
                    if (LoadImages(ddlSections.SelectedValue.ToString()) == true)
                    {
                        LoadImages(ddlSections.SelectedValue.ToString());
                    }
                }
            }
            if (ddlSections.SelectedIndex == 5)
            {
                ddlGender.SelectedIndex = 0;
                ddlGender.Enabled = false;
            }
            else
            {
                ddlGender.Enabled = true;
            }
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (Data.CheckImgExist(txtFilename.Text, ddlSections.SelectedValue.ToString()) == false)
            {
                MessageBox.Show("Selected image does not exist in server. Upload first before editing.");
                return;
            }
            if (PreviewPictureBox.Image == null)
            {
                MessageBox.Show("Select image first.");
                return;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Enter Description.");
                return;
            }
            if (ddlGender.Text == "")
            {
                MessageBox.Show("Select Male or Female.");
                return;
            }
            if (MessageBox.Show("Save Edit?", "Save Edit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Data.UpdateImageDb(txtFilename.Text, ddlGender.Text, txtDescription.Text);
                MessageBox.Show("Updated.");
            }
        }

        #endregion

        public void SaveLocal(FileStream file, string _fileName = "")
        {
            try
            {
                File.Copy(file.Name, GetLocalImgPath(ddlSections.SelectedValue.ToString()) + "\\" + _fileName);
            }
            catch (Exception)
            {

            }
        }

        public string GetLocalImgPath(string folder)
        {
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            //System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(appPath);
            //System.IO.DirectoryInfo directoryInfo2 = System.IO.Directory.GetParent(directoryInfo.FullName);
            //string path = directoryInfo2.FullName + @"\Images\" + folder;

            string[] Directories = new string[] { "C:\\ManageImages\\", "C:\\ManageImages\\ApparelsImages", "C:\\ManageImages\\NewArrivalsImages", "C:\\ManageImages\\PantsImages", "C:\\ManageImages\\RhinestoneImages", "C:\\ManageImages\\ShirtsImages", "C:\\ManageImages\\ShoesImages" };

            try
            {
                for (int i = 0; i < Directories.Length; i++)
                {
                    // If the directory doesn't exist, create it.
                    if (!Directory.Exists(Directories[i].ToString()))
                    {
                        Directory.CreateDirectory(Directories[i].ToString());
                    }
                }
            }
            catch (Exception)
            {
                // Fail silently
            }

            string path = "C:\\ManageImages\\" + folder;
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\ManageImages\\" + folder;
            return path;
        }

        public bool LoadImages(string folder)
        {
            bool AddedNew = false;
            DirectoryInfo Folder;
            FileInfo[] Images;
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
            switch (imgSize)
            {
                case "x-small": locX = 20; locY = 10; sizeWidth = 30; sizeHeight = 30; break;
                case "small": locX = 20; locY = 10; sizeWidth = 50; sizeHeight = 50; break;
                case "medium": locX = 20; locY = 0; sizeWidth = 80; sizeHeight = 80; break;
                case "large": locX = 20; locY = 10; sizeWidth = 160; sizeHeight = 160; break;
            }
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
                reqFTP.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
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
                    reqFTP.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
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
                reqFTP.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
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
                MessageBox.Show(wEx.Message, "Delete Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Error");
            }
        }

     
    }
}

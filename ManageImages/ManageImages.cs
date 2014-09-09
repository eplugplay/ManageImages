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

        private void loadImagestoPanel(String imageName, String ImageFullName, int newLocX, int newLocY, bool live)
        {
            PictureBox ctrl = new PictureBox();
            ctrl.Image = Image.FromFile(ImageFullName);
            ctrl.BorderStyle = BorderStyle.FixedSingle;
            
            if (live == true)
            {
                ctrl.BackColor = Color.Green;
            }
            else
            {
                ctrl.BackColor = Color.Red;
            }
            ctrl.Location = new Point(newLocX, newLocY);
            ctrl.Size = new System.Drawing.Size(sizeWidth, sizeHeight);
            ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseClick += new MouseEventHandler(control_MouseClick);
            pnControls.InvokeEx(x => x.Controls.Add(ctrl));
        }

        public void Check(string filename, string folder)
        {
            if (Data.CheckImgExist(filename, folder) == true)
            {
                btnSaveEdit.InvokeEx(x => x.Enabled = true);
                btnDeleteImg.InvokeEx(x => x.Enabled = true);
                btnUploadImage.InvokeEx(x => x.Enabled = false);
                btnDeleteImg.InvokeEx(x => x.Enabled = true);
                btnUploadImage.InvokeEx(x => x.Enabled = false);
            }
            else
            {
                btnSaveEdit.InvokeEx(x => x.Enabled = false);
                btnDeleteImg.InvokeEx(x => x.Enabled = false);
                btnUploadImage.InvokeEx(x => x.Enabled = true);
                btnDeleteImg.InvokeEx(x => x.Enabled = false);
                btnUploadImage.InvokeEx(x => x.Enabled = true);
            }
        }

        #region private class

        private class data
        {
            public data(string _folder, string _description, string _gender, string _filename, int _sectionIndex)
            {
                folder = _folder;
                description = _description;
                gender = _gender;
                filename = _filename;
                sectionIndex = _sectionIndex;
            }
            public string folder { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public string filename { get; set; }
            public int sectionIndex { get; set; }
        }

        #endregion

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
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
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
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
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
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
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
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (MessageBox.Show("Upload?", "Upload?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pbStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Text = "Uploading..");
                data _data = (data)e.Argument;
                string FolderName = _data.folder;
                string FileName = _data.filename;
                string Gender = _data.gender;
                string Description = _data.description;

                // 10% bar
                pbStatus.InvokeEx(x => x.Value = 10);
                FileInfo toUpload = new FileInfo("FileName");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://208.118.63.29/site2/" + FolderName + "/" + FileName);
                request.KeepAlive = true;
                request.Proxy = null;
                request.UseBinary = true;
                // 20% bar
                pbStatus.InvokeEx(x => x.Value = 20);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                // 30% bar
                pbStatus.InvokeEx(x => x.Value = 40);
                request.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
                Stream ftpStream = request.GetRequestStream();
                FileStream file = File.OpenRead(GetLocalImgPath(FolderName) + "\\" + txtFilename.Text);
                int length = 1024;
                byte[] buffer = new byte[length];
                int bytesRead = 0;
                do
                {
                    bytesRead = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesRead);
                }
                while (bytesRead != 0);
                // 60% bar
                pbStatus.InvokeEx(x => x.Value = 60);
                //// save local
                //SaveLocal(file);
                file.Close();
                ftpStream.Close();
                // 70% bar
                pbStatus.InvokeEx(x => x.Value = 70);
                //save to db
                Data.SaveImageToDb(FileName, Description, Gender, FolderName);
                // 80% bar
                pbStatus.InvokeEx(x => x.Value = 80);
                Check(FileName, FolderName);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            MessageBox.Show("Saved.");
        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (MessageBox.Show("Delete?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                data _data = (data)e.Argument;
                string FolderName = _data.folder;
                string FileName = _data.filename;
                string Gender = _data.gender;
                string Description = _data.description;

                pbStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Text = "Deleting..");
                // 20% bar
                pbStatus.InvokeEx(x => x.Value = 20);
                // delete from server
                DeleteServerImg(FolderName, FileName);
                // 40% bar
                pbStatus.InvokeEx(x => x.Value = 40);
                // delete from db
                Data.DeleteImageDb(FileName, Gender, FolderName);
                // 60% bar
                pbStatus.InvokeEx(x => x.Value = 60);
                // delete from local
                if (File.Exists(GetLocalImgPath(FolderName) + "\\" + FileName))
                {
                    try
                    {
                        //Directory.Delete(GetLocalImgPath(ddlSections.SelectedValue.ToString()));
                        File.Delete(GetLocalImgPath(FolderName) + "\\" + FileName);
                    }
                    catch (Exception)
                    {

                    }
                }
                // 80% bar
                pbStatus.InvokeEx(x => x.Value = 80);
                LoadImages(FolderName, 80, FileName);
                Check(FileName, FolderName);
                pbStatus.InvokeEx(x => x.Value = 100);
                MessageBox.Show("Deleted.");
            }
        }


        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            data _data = (data)e.Argument;
            pbStatus.InvokeEx(x => x.Visible = true);
            lblStatus.InvokeEx(x => x.Visible = true);
            ddlSections.InvokeEx(x => x.Enabled = false);
            // 20% bar
            pbStatus.InvokeEx(x => x.Value = 20);
            string FolderName = _data.folder;
            string FileName = _data.filename;
            string Gender = _data.gender;
            string Description = _data.description;
            int SelectedIndex = _data.sectionIndex;

            PreviewPictureBox.InvokeEx(x => x.Image = null);
            txtDescription.InvokeEx(x => x.Text = "");
            ddlGender.InvokeEx(x => x.SelectedIndex = -1);
            txtFilename.InvokeEx(x => x.Text = "");
            btnDeleteImg.InvokeEx(x => x.Enabled = false);
            btnUploadImage.InvokeEx(x => x.Enabled = false);
            // 40% bar
            pbStatus.InvokeEx(x => x.Value = 40);
            switch (SelectedIndex)
            {
                case 0: txtSection.InvokeEx(x => x.Text = "Apparels"); break;
                case 1: txtSection.InvokeEx(x => x.Text = "New Arrivals"); break;
                case 2: txtSection.InvokeEx(x => x.Text = "Pants"); break;
                case 3: txtSection.InvokeEx(x => x.Text = "Rhinestones"); break;
                case 4: txtSection.InvokeEx(x => x.Text = "Shirts"); break;
                case 5: txtSection.InvokeEx(x => x.Text = "Women's Shoes"); break;
            }
            if (SelectedIndex != -1)
            {
                if (LoadImages(FolderName, 40, FileName) == true)
                {
                    LoadImages(FolderName, 90, FileName);
                }
            }
            if (SelectedIndex == 5)
            {
                ddlGender.InvokeEx(x => x.SelectedIndex = 0);
                ddlGender.InvokeEx(x => x.Enabled = false);
            }
            else
            {
                ddlGender.InvokeEx(x => x.Enabled = true);
            }
            pbStatus.InvokeEx(x => x.Value = 100);
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            ddlSections.InvokeEx(x => x.Enabled = true);
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            //Control control = (Control)sender;
            //PictureBox pic = (PictureBox)control;
            //PreviewPictureBox.Image = pic.Image;
        }

        private void control_MouseClick(object sender, MouseEventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
            Control control = (Control)sender;
            PictureBox pic = (PictureBox)control;
            PreviewPictureBox.Image = pic.Image;

            // load image info
            txtFilename.Text = PicArr[control.TabIndex].ToString();
            txtDescription.Text = Data.GetDescriptionInfo(txtFilename.Text, ddlSections.SelectedValue.ToString());
            if (ddlSections.SelectedIndex == 5)
            {
                ddlGender.SelectedIndex = 0;
            }
            else
            {
                ddlGender.SelectedIndex = ddlGender.FindStringExact(Data.GetGenderInfo(txtFilename.Text, ddlSections.SelectedValue.ToString()));
            }
            Check(txtFilename.Text, ddlSections.SelectedValue.ToString());
        }


        private void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSections.DataSource != null)
            {
                backgroundWorker3.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedIndex));
            }
        }


        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                MessageBox.Show("Please wait until process is finished.");
                return;
            }
            FileStream file;
            this.folderBrowserDlg.Filter = "(*.bmp, *.jpg, *.jpeg, *png)|*.bmp;*.jpg;*.jpeg;*.png";
            this.folderBrowserDlg.Multiselect = true;
            DialogResult result = this.folderBrowserDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //string[] split = folderBrowserDlg.FileName.Split('\\');
                //file = File.OpenRead(folderBrowserDlg.FileName);
                //// save local
                //SaveLocal(file, split[split.Length - 1]);
                //file.Close();
                //LoadImages(ddlSections.SelectedValue.ToString(), 100);
                foreach (var f in folderBrowserDlg.FileNames)
                {
                    string[] split = f.Split('\\');
                    //file = File.OpenRead(folderBrowserDlg.FileName);
                    file = File.OpenRead(f);
                    // save local
                    SaveLocal(file, split[split.Length - 1]);
                    file.Close();
                }
                LoadImages(ddlSections.SelectedValue.ToString(), 100, txtFilename.Text);
                MessageBox.Show("Imported.");
            }
        }


        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
            if (PreviewPictureBox.Image == null)
            {
                MessageBox.Show("Select image first.");
                return;
            }
            if (txtDescription.Text == "")
            {
                txtDescription.Focus();
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

            backgroundWorker1.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedIndex));
        }


        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
            if (PreviewPictureBox.Image == null)
            {
                MessageBox.Show("Select image first.");
                return;
            }
            backgroundWorker2.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedIndex));
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                return;
            }
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
                txtDescription.Focus();
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

        public bool LoadImages(string folder, int cntBar, string filename)
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
                    if (!file.Contains(".xml"))
                    {
                        AddedNew = true;
                        Download(folder, file);
                        lblStatus.InvokeEx(x => x.Text = "Copying images..");
                        cntBar += cntBar / files.Length;
                        if (cntBar < 100)
                        {
                            pbStatus.InvokeEx(x => x.Value = cntBar);
                        }
                    }
                }
            }
            // load images from folder
            pnControls.InvokeEx(x => x.Controls.Clear());
            switch (imgSize)
            {
                case "x-small": locX = 20; locY = 10; sizeWidth = 30; sizeHeight = 30; break;
                case "small": locX = 20; locY = 10; sizeWidth = 50; sizeHeight = 50; break;
                case "medium": locX = 20; locY = 0; sizeWidth = 80; sizeHeight = 80; break;
                case "large": locX = 20; locY = 10; sizeWidth = 160; sizeHeight = 160; break;
                default: locX = 20; locY = 10; sizeWidth = 30; sizeHeight = 30; break;
            }
            int locnewX = locX;
            int locnewY = locY;
            lblStatus.InvokeEx(x => x.Text = "Loading images..");
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
                    if (Data.CheckImgExist(filename, folder) == true)
                    {
                        loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, true);
                    }
                    else
                    {
                        loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, false);
                    }
                    locnewY = locY + sizeHeight + 10;
                    locnewX = locnewX + sizeWidth + 10;

                    cntBar += (int)(cntBar / Images.Length) + 3;
                    if (cntBar < 100)
                    {
                        pbStatus.InvokeEx(x => x.Value = cntBar);
                    }
                }
            }
            //pbStatus.InvokeEx(x => x.Value = 100);
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

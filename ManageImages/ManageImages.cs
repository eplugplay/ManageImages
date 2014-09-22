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
using System.Drawing.Drawing2D;

namespace ManageImages
{
    public partial class ManageImages : Form
    {
        //private ManageImages frm = (ManageImages)Application.OpenForms["ManageImages"];
        private string imgSize { get; set; }
        private int lastImgX { get; set; }
        private int lastImgY { get; set; }
        // currently selected image
        private string currentImg { get; set; }
        List<string> PicArr = new List<string>();
        Dictionary<string, Image> MultiSelectedImages = new Dictionary<string, Image>();
        private System.Windows.Forms.OpenFileDialog folderBrowserDlg;
        int locX = 20;
        int locY = 10;
        int sizeWidth = 30;
        int sizeHeight = 30;
        public ManageImages()
        {
            InitializeComponent();
            this.FormClosing += DeleteLocalOnClose;
        }




        /******************************************************************
         ****************************************************************************
         ***************************************************************************
         * ************************** Private Class ********************************************/
        #region private class

        private class data
        {
            public data(string _folder, string _description, string _gender, string _filename, string _sectionValue, bool _hidden, string _toFolder, string _filterType, bool _isCopy)
            {
                folder = _folder;
                description = _description;
                gender = _gender;
                filename = _filename;
                sectionValue = _sectionValue;
                hidden = _hidden;
                ToFolder = _toFolder;
                filterType = _filterType;
                IsCopy = _isCopy;
            }
            public string folder { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public string filename { get; set; }
            public string sectionValue { get; set; }
            public bool hidden { get; set; }
            public string ToFolder { get; set; }
            public string filterType { get; set; }
            public bool IsCopy { get; set; }
        }

        #endregion





        /******************************************************************
         ****************************************************************************
         ***************************************************************************
         * ************************** Events ********************************************/

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

            // set inital size
            imgSize = "medium";
        }

        // delete images that are local but not on web server on exit
        private void DeleteLocalOnClose(object sender, FormClosingEventArgs e)
        {
            string[] Directories = new string[] { "C:\\ManageImages\\", "C:\\ManageImages\\ApparelsImages", "C:\\ManageImages\\NewArrivalsImages", "C:\\ManageImages\\PantsImages", "C:\\ManageImages\\RhinestoneImages", "C:\\ManageImages\\ShirtsImages", "C:\\ManageImages\\ShoesImages" };
            string folder = "";
            for (int i = 0; i < Directories.Length; i++)
            {
                if (i != 0)
                {
                    string[] Tempfolder = Directories[i].ToString().Split('\\');
                    folder = Tempfolder[2].ToString();
                    DirectoryInfo Folder = new DirectoryInfo(GetLocalImgPath(folder));
                    FileInfo[] Images = Folder.GetFiles();
                    try
                    {
                        foreach (FileInfo file in Images)
                        {
                            int fileLengthdb = 0;
                            string filenamedb = "";
                            // get length of image
                            DataTable dt = Data.GetInfo(file.Name, folder);
                            if (dt.Rows.Count != 0)
                            {
                                fileLengthdb = Convert.ToInt32(dt.Rows[0]["length"]);
                                filenamedb = dt.Rows[0]["filename"].ToString();
                            }
                            int LocalFileLength = GetLocalFileLength(folder, file.Name);
                            if (fileLengthdb.Equals(0) && filenamedb == "")
                            {
                                File.Delete(GetLocalImgPath(folder) + "\\" + file.Name);
                            }
                            else if (fileLengthdb != (LocalFileLength) && filenamedb.Equals(file.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                File.Delete(GetLocalImgPath(folder) + "\\" + file.Name);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            pbStatus.Value = 90;
            this.FormClosing -= DeleteLocalOnClose;
        }

 
        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            //Control control = (Control)sender;
            //PictureBox pic = (PictureBox)control;
            //PreviewPictureBox.Image = pic.Image;
        }

        private void control_MouseClick(object sender, MouseEventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            txtDescription.Text = "";
            Control control = (Control)sender;
            PictureBox pic = (PictureBox)control;
            ArrayList allArr = new ArrayList();
            // control left click on images
            if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Control) == Keys.Control)
            {
                AfterCtrlLeftClick();
                ArrayList KeysToAddArr = new ArrayList();
                ArrayList KeysToRemoveArr = new ArrayList();
                if (MultiSelectedImages.Count == 0)
                {
                    MultiSelectedImages.Add(pic.Name, pic.Image);
                    ColorImage(pic, "select");
                }
                else
                {
                    foreach (KeyValuePair<string, Image> pair in MultiSelectedImages)
                    {
                        allArr.Add(pair.Key);
                    }

                    foreach (KeyValuePair<string, Image> pair in MultiSelectedImages)
                    {
                        if (MultiSelectedImages.Keys.Contains(pic.Name))
                        {
                            KeysToRemoveArr.Add(pair.Key);
                            ColorImage(pic, "unselect");
                            break;
                        }
                        else
                        {
                            KeysToAddArr.Add(pair.Key);
                            ColorImage(pic, "select");
                            break;
                        }
                    }
                }

                foreach (string s in KeysToAddArr)
                {
                   MultiSelectedImages.Add(pic.Name, pic.Image);
                }

                for (int i = 0; i < allArr.Count; i++)
                {
                    if (pic.Name == allArr[i].ToString())
                    {
                        MultiSelectedImages.Remove(allArr[i].ToString());
                    }
                }
                pic.Refresh();
                PreviewPictureBox.Image = null;
                return;
            }

            // reset images
            foreach (KeyValuePair<string, Image> pair in MultiSelectedImages)
            {
                foreach (PictureBox p in pnControls.Controls)
                {
                    if (p.Name.Contains(pair.Key))
                    {
                        ColorImage(p, "unselect");
                        p.Refresh();
                    }
                }
            }

            MultiSelectedImages.Clear();
            PreviewPictureBox.Image = pic.Image;

            // load image info
            txtFilename.Text = control.Tag.ToString();
            // currently clicked img
            currentImg = pic.Tag.ToString();
            ddlGender.SelectedIndex = -1;
            DataTable dt = Data.GetInfo(txtFilename.Text, ddlSections.SelectedValue.ToString());
            if (dt.Rows.Count != 0)
            {
                txtDescription.Text = dt.Rows[0]["description"].ToString();
                // always selects women if shoe images category selected
                if (ddlSections.SelectedValue.ToString() == "ShoesImages")
                {
                    ddlGender.SelectedIndex = 0;
                }
                else
                {
                    ddlGender.SelectedIndex = ddlGender.FindStringExact(dt.Rows[0]["gender"].ToString());
                }

                switch (Convert.ToInt32(dt.Rows[0]["hidden"]))
                {
                    case 0: chkHideImage.Checked = false; break;
                    case 1: chkHideImage.Checked = true; break;
                }
            }
            if (ddlSections.SelectedValue.ToString() == "ShoesImages")
            {
                ddlGender.InvokeEx(x => x.SelectedIndex = 0);
                ddlGender.InvokeEx(x => x.Enabled = false);
            }
            AfterImgClick(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlSections.SelectedValue.ToString());
        }

        // save edit
        #region SaveEdit

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (Data.CheckImgExist(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlSections.SelectedValue.ToString()) == false)
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
            //if (MessageBox.Show("Save?", "Save Edit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            Data.UpdateImageDb(txtFilename.Text, ddlSections.SelectedValue.ToString(), GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlGender.Text, txtDescription.Text, chkHideImage.Checked, false);
            MessageBox.Show("Updated.");
            //}
        }

        #endregion

        // upload Image
        #region Upload Image



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (MessageBox.Show("Upload?", "Upload?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
                gpFilters.InvokeEx(x => x.Enabled = false);
                ddlSections.InvokeEx(x => x.Enabled = false);
                pbStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Text = "Uploading..");
                data _data = (data)e.Argument;
                string FolderName = _data.folder;
                string FileName = _data.filename;
                string Gender = _data.gender;
                string Description = _data.description;
                bool Hidden = _data.hidden;
                pbStatus.InvokeEx(x => x.Value = 10);
                // upload file
                UploadServerImg(FolderName, FolderName, FileName, 30);
                pbStatus.InvokeEx(x => x.Value = 60);
                pbStatus.InvokeEx(x => x.Value = 70);
                //save to db
                Data.SaveImageToDb(FileName, Description, Gender, FolderName, GetLocalFileLength(FolderName, FileName), Hidden);
                pbStatus.InvokeEx(x => x.Value = 100);
                AfterUpload();
                MessageBox.Show("Successfully uploaded.");
            //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gpFilters.InvokeEx(x => x.Enabled = true);
            ddlSections.InvokeEx(x => x.Enabled = true);
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            CloseButton.EnableDisable(this, true);
            lblStatus.InvokeEx(x => x.Text = "Please wait..");
        }




        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
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
            if (Data.CheckImgExist(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlSections.SelectedValue.ToString()) == true)
            {
                MessageBox.Show(string.Format("Already exist in section {0} of the website.", ddlSections.SelectedValue.ToString()));
                return;
            }
            string folderName = Data.CheckImgAllExist(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text));
            if(folderName != "")
            {
                MessageBox.Show(string.Format("Image already exist in section {0} of the website.", folderName));
                return;
            }
            CloseButton.EnableDisable(this, false);
            backgroundWorker1.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.Text, "", false));
        }

        #endregion

        //delete image
        #region Delete Image


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (MessageBox.Show("Delete Selected Image(s)?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gpFilters.InvokeEx(x => x.Enabled = false);
                ddlSections.InvokeEx(x => x.Enabled = false);
                data _data = (data)e.Argument;
                string FolderName = _data.folder;
                string FileName = _data.filename;
                string Gender = _data.gender;
                string Description = _data.description;

                pbStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Visible = true);
                lblStatus.InvokeEx(x => x.Text = "Deleting..");
                pbStatus.InvokeEx(x => x.Value = 40);

                // delete from db multiple images
                if (MultiSelectedImages.Count > 0)
                {
                    foreach (KeyValuePair<string, Image> pair in MultiSelectedImages)
                    {
                        DataTable dt = Data.GetInfo(pair.Key, FolderName);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                Data.DeleteImageDb(pair.Key, dt.Rows[0]["gender"].ToString(), FolderName, GetLocalFileLength(FolderName, pair.Key));
                                // delete image from server if it exists
                                DeleteServerImg(FolderName, pair.Key);
                            }
                        }
                        catch (Exception)
                        {

                        }

                        // delete from local
                        if (File.Exists(GetLocalImgPath(FolderName) + "\\" + pair.Key))
                        {
                            try
                            {
                                File.Delete(GetLocalImgPath(FolderName) + "\\" + pair.Key);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        // reload images
                        ReloadImages(pair.Key);
                    }
                    MultiSelectedImages.Clear();
                }
                    // single images
                else
                {
                    try
                    {
                        DataTable dt = Data.GetInfo(FileName, FolderName);
                        if (dt.Rows.Count > 0)
                        {
                            Data.DeleteImageDb(FileName, Gender, FolderName, GetLocalFileLength(FolderName, FileName));
                            // delete image from server if it exists
                            DeleteServerImg(FolderName, FileName);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    // delete from local
                    if (File.Exists(GetLocalImgPath(FolderName) + "\\" + FileName))
                    {
                        try
                        {
                            File.Delete(GetLocalImgPath(FolderName) + "\\" + FileName);
                        }
                        catch (Exception)
                        {

                        }
                    }

                    // reload images
                    ReloadImages(FileName);
                }

                AfterDelete();
                Thread.Sleep(1000);
                pbStatus.InvokeEx(x => x.Value = 100);


                //switch (imgSize)
                //{
                //    case "x-small": LoadToolStrip(20, 10, 30, 30); break;
                //    case "small": LoadToolStrip(20, 10, 50, 50); break;
                //    case "medium": LoadToolStrip(20, 10, 80, 80); pnControls.Refresh(); break;
                //    case "large": LoadToolStrip(20, 10, 160, 160); break;
                //    default: LoadToolStrip(20, 10, 30, 30); break;
                //}
                Thread.Sleep(1000);
                pbStatus.InvokeEx(x => x.Value = 100);
                ResetInfo();
                //MessageBox.Show("Deleted.");
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gpFilters.InvokeEx(x => x.Enabled = true);
            ddlSections.InvokeEx(x => x.Enabled = true);
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            CloseButton.EnableDisable(this, true);
            lblStatus.InvokeEx(x => x.Text = "Please wait..");
        }

        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            CloseButton.EnableDisable(this, false);
            //if (PreviewPictureBox.Image == null)
            //{
            //    MessageBox.Show("Select image first.");
            //    return;
            //}
            backgroundWorker2.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.Text, "", false));
        }
        #endregion


        #region Section SelectedIndex


        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {

            // reset multi selected images
            MultiSelectedImages.Clear();

            // selects all radio button by default
            rbAll.InvokeEx(x => x.Checked = true);

            // hide move to options
            btnMoveSection.InvokeEx(x => x.Visible = false);
            btnCopySection.InvokeEx(x => x.Visible = false);
            ddlMoveSection.InvokeEx(x => x.Visible = false);
            // clear panel
            pnControls.InvokeEx(x => x.Controls.Clear());
            // reset
            lastImgX = locX;
            lastImgY = locY;
            // set initial size here
            switch (imgSize)
            {
                case "x-small": imgSize = "x-small"; break;
                case "small": imgSize = "small"; break;
                case "medium": imgSize = "medium"; break;
                case "large": imgSize = "large"; break;
                default: imgSize = "x-small"; break;
            }

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
            string SelectedValue = _data.sectionValue;

            PreviewPictureBox.InvokeEx(x => x.Image = null);
            txtDescription.InvokeEx(x => x.Text = "");
            ddlGender.InvokeEx(x => x.SelectedIndex = -1);
            txtFilename.InvokeEx(x => x.Text = "");
            //btnDeleteImg.InvokeEx(x => x.Enabled = false);
            btnUploadImage.InvokeEx(x => x.Enabled = false);
            chkHideImage.InvokeEx(x => x.Checked = false);
            // 40% bar
            pbStatus.InvokeEx(x => x.Value = 40);
            switch (SelectedValue)
            {
                case "ApparelsImages": txtSection.InvokeEx(x => x.Text = "Apparels"); break;
                case "NewArrivalsImages": txtSection.InvokeEx(x => x.Text = "New Arrivals"); break;
                case "PantsImages": txtSection.InvokeEx(x => x.Text = "Pants"); break;
                case "RhinestoneImages": txtSection.InvokeEx(x => x.Text = "Rhinestones"); break;
                case "ShirtsImages": txtSection.InvokeEx(x => x.Text = "Shirts"); break;
                case "ShoesImages": txtSection.InvokeEx(x => x.Text = "Women's Shoes"); break;
            }
            if (SelectedValue != null)
            {
                if (LoadImages(FolderName, 40, FileName, false) == true)
                {
                    LoadImages(FolderName, 90, FileName, false);
                }
            }
            if (SelectedValue == "ShoesImages")
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

        private void ddlSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSections.DataSource != null)
            {
                backgroundWorker3.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.Text, "", false));
            }
        }
        #endregion

        #region Copy Section


        private void btnCopySection_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (ddlSections.Text == ddlMoveSection.Text)
            {
                MessageBox.Show("Please select a different section to copy.");
                return;
            }
            // check if copying image exist in the target folder
            if (Data.CheckImgExist(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlMoveSection.SelectedValue.ToString()) == true)
            {
                MessageBox.Show(string.Format("This image already exist in {0} section.", ddlMoveSection.SelectedValue.ToString()));
                return;
            }
            CloseButton.EnableDisable(this, false);
            backgroundWorker4.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "", true));
        }
        #endregion


        // Move section
        #region Move Section


        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            gpFilters.InvokeEx(x => x.Enabled = false);
            ddlSections.InvokeEx(x => x.Enabled = false);
            ddlMoveSection.InvokeEx(x => x.Enabled = false);
            lblStatus.InvokeEx(x => x.Visible = true);
            pbStatus.InvokeEx(x => x.Visible = true);
            pbStatus.InvokeEx(x => x.Value = 20);
            data _data = (data)e.Argument;
            if (_data.IsCopy == false)
            {
                // check if exist in db before moving


                lblStatus.InvokeEx(x => x.Text = "Moving..");
                MoveImage(_data.folder, _data.filename, _data.ToFolder, _data.gender, _data.description, GetLocalFileLength(_data.folder, _data.filename), chkHideImage.Checked, false);
                //MessageBox.Show("Moved");
            }
            else
            {
                lblStatus.InvokeEx(x => x.Text = "Copying..");
                MoveImage(_data.folder, _data.filename, _data.ToFolder, _data.gender, _data.description, GetLocalFileLength(_data.folder, _data.filename), chkHideImage.Checked, true);
                //MessageBox.Show("Copied");
            }
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gpFilters.InvokeEx(x => x.Enabled = true);
            ddlSections.InvokeEx(x => x.Enabled = true);
            ddlMoveSection.InvokeEx(x => x.Enabled = true);
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            CloseButton.EnableDisable(this, true);
            ResetInfo();
            lblStatus.InvokeEx(x => x.Text = "Please Wait..");
        }

        private void btnMoveSection_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }

            // check if copying image exist in the target folder
            if (ddlSections.Text == ddlMoveSection.Text)
            {
                MessageBox.Show("Please select a different section to move.");
                return;
            }
            if (Data.CheckImgExist(txtFilename.Text, GetLocalFileLength(ddlSections.SelectedValue.ToString(), txtFilename.Text), ddlMoveSection.SelectedValue.ToString()) == true)
            {
                MessageBox.Show(string.Format("This image already exist in {0} section.", ddlMoveSection.SelectedValue.ToString()));
                return;
            }
            CloseButton.EnableDisable(this, false);
            backgroundWorker4.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "", false));
        }

        #endregion

        #region Filters


        private void rbWomen_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (rbWomen.Checked == true)
            {
                ddlSections.InvokeEx(x => x.Enabled = false);
                womenToolStripMenuItem_Click(sender, e);
                ddlSections.InvokeEx(x => x.Enabled = true);
            }
        }

        private void rbMen_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (rbMen.Checked == true)
            {
                ddlSections.InvokeEx(x => x.Enabled = false);
                menToolStripMenuItem_Click(sender, e);
                ddlSections.InvokeEx(x => x.Enabled = true);
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (rbAll.Checked == true)
            {
                ddlSections.InvokeEx(x => x.Enabled = false);
                defaultToolStripMenuItem_Click(sender, e);
                ddlSections.InvokeEx(x => x.Enabled = true);
            }
        }

        private void rbHidden_CheckedChanged(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            if (rbHidden.Checked == true)
            {
                ddlSections.InvokeEx(x => x.Enabled = false);
                hiddenImagesToolStripMenuItem_Click(sender, e);
                ddlSections.InvokeEx(x => x.Enabled = true);
            }
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            pbStatus.InvokeEx(x => x.Visible = true);
            lblStatus.InvokeEx(x => x.Visible = true);
            data _data = (data)e.Argument;

            switch(_data.filterType)
            {
                case "hidden": lblStatus.InvokeEx(x => x.Text = "Searching hidden images.."); LoadFilteredImages(_data.folder, 40, _data.filename, false, "hidden", "1"); break;
                case "default": lblStatus.InvokeEx(x => x.Text = "loading all images.."); if (LoadImages(_data.folder, 40, _data.filename, false) == true) { LoadImages(_data.folder, 90, _data.filename, false); } break;
                case "Men": lblStatus.InvokeEx(x => x.Text = "Searching men images.."); LoadFilteredImages(_data.folder, 40, _data.filename, false, "gender", "Men"); break;
                case "Women": lblStatus.InvokeEx(x => x.Text = "Searching women images.."); LoadFilteredImages(_data.folder, 40, _data.filename, false, "gender", "Women"); break;
            }
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Visible = false);
            lblStatus.InvokeEx(x => x.Text = "Please wait..");
            ResetInfo();
        }

        private void hiddenImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }

            backgroundWorker5.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "hidden", false));
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }

            backgroundWorker5.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "default", false));
        }


        private void menToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }

            backgroundWorker5.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "Men", false));
        }

        private void womenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }

            backgroundWorker5.RunWorkerAsync(new data(ddlSections.SelectedValue.ToString(), txtDescription.Text, ddlGender.Text, txtFilename.Text, ddlSections.SelectedValue.ToString(), chkHideImage.Checked, ddlMoveSection.SelectedValue.ToString(), "Women", false));
        }

        #endregion


        #region Tool Strip Options
        private void ImportImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportImages();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadToolStrip(20, 10, 30, 30);
            imgSize = "x-small";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadToolStrip(20, 10, 50, 50);
            imgSize = "small";
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            LoadToolStrip(20, 10, 80, 80);
            imgSize = "medium";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            LoadToolStrip(20, 10, 160, 160);
            imgSize = "large";
        }


        private void checkDbToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #endregion




        /******************************************************************
         ****************************************************************************
         ***************************************************************************
         * ************************** Methods ********************************************/

        #region Methods

        private void ColorImage(PictureBox pic, string color)
        {
            Graphics g = Graphics.FromImage(pic.Image);
            Pen Pen;
            if (color == "select")
            {
                Pen = new Pen(Color.Red, 15);

            }
            else
            {
                Pen = new Pen(Color.Black, 15);
            }
            g.DrawRectangle(Pen, 0, 0, pic.Image.Width, pic.Image.Height);
            g.Save();
        }

        public void ImportImages()
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                MessageBox.Show("Please wait until process is finished.");
                return;
            }

            switch (imgSize)
            {
                case "x-small": sizeWidth = 30; sizeHeight = 30; break;
                case "small": sizeWidth = 50; sizeHeight = 50; break;
                case "medium": sizeWidth = 80; sizeHeight = 80; break;
                case "large": sizeWidth = 160; sizeHeight = 160; break;
                default: sizeWidth = 30; sizeHeight = 30; break;
            }
            if (pnControls.Controls.Count == 0)
            {
                lastImgX = locX;
                lastImgY = locY;
            }
            else
            {
                lastImgX = lastImgX;
                lastImgY = lastImgY;
            }
            FileStream file;
            this.folderBrowserDlg.Filter = "(*.bmp, *.jpg, *.jpeg, *png)|*.bmp;*.jpg;*.jpeg;*.png";
            this.folderBrowserDlg.Multiselect = true;
            DialogResult result = this.folderBrowserDlg.ShowDialog();
            bool exists = false;
            if (result == DialogResult.OK)
            {
                foreach (var f in folderBrowserDlg.FileNames)
                {
                    string[] split = f.Split('\\');
                    file = File.OpenRead(f);
                    if (lastImgX >= pnControls.Width - sizeWidth - 10)
                    {
                        lastImgX = locX;
                        locY = locY + sizeHeight + 30;
                        lastImgY = locY;
                    }
                    else
                    {
                        lastImgY = locY;
                    }
                    // add pic name 
                    PicArr.Add(split[split.Length - 1]);
                    // add new image
                    exists = loadImagestoPanel(split[split.Length - 1], f, lastImgX, lastImgY, false, false, "");
                    // save locally if it doesnt exist
                    if (exists == false)
                    {
                        // save local
                        SaveLocal(file, split[split.Length - 1]);
                        lastImgY = locY + sizeHeight + 10;
                        lastImgX = lastImgX + sizeWidth + 10;
                    }
                    file.Close();
                }

                //LoadImages(ddlSections.SelectedValue.ToString(), 100, txtFilename.Text, false);
                //if (exists == false)
                //{
                //    MessageBox.Show("Imported.");
                //}
            }
        }


        public void LoadToolStrip(int _locX, int _locY, int _sizeWidth, int _sizeHeight)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy || backgroundWorker4.IsBusy || backgroundWorker5.IsBusy)
            {
                return;
            }
            int SaveVal = 0;
            locX = _locX;
            locY = _locY;
            sizeWidth = _sizeWidth;
            sizeHeight = _sizeHeight;
            foreach (Control p in pnControls.Controls)
            {
                SaveVal = SaveVal + 1;
            }
            if (SaveVal > 0)
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
                lastImgX = locnewX;
                lastImgY = locnewY;
            }
        }


        public void LoadSections()
        {
            ddlSections.DisplayMember = "category";
            ddlSections.ValueMember = "id";
            ddlSections.DataSource = Data.LoadSections();

            ddlMoveSection.DisplayMember = "category";
            ddlMoveSection.ValueMember = "id";
            ddlMoveSection.DataSource = Data.LoadSections();
        }

        public void AfterImgClick(string filename, int length, string folder)
        {
            if (Data.CheckImgExist(filename, length, folder) == true)
            {
                btnSaveEdit.InvokeEx(x => x.Enabled = true);
                btnUploadImage.InvokeEx(x => x.Enabled = false);
                // show move to options
                btnMoveSection.InvokeEx(x => x.Visible = true);
                btnCopySection.InvokeEx(x => x.Visible = true);
                ddlMoveSection.InvokeEx(x => x.Visible = true);
            }
            else
            {
                btnSaveEdit.InvokeEx(x => x.Enabled = false);
                btnUploadImage.InvokeEx(x => x.Enabled = true);
                // show move to options
                btnMoveSection.InvokeEx(x => x.Visible = false);
                btnCopySection.InvokeEx(x => x.Visible = false);
                ddlMoveSection.InvokeEx(x => x.Visible = false);
                chkHideImage.InvokeEx(x => x.Checked = false);
            }
        }

        public void AfterCtrlLeftClick()
        {
            btnSaveEdit.InvokeEx(x => x.Enabled = false);
            btnUploadImage.InvokeEx(x => x.Enabled = false);
            // show move to options
            btnMoveSection.InvokeEx(x => x.Visible = false);
            btnCopySection.InvokeEx(x => x.Visible = false);
            ddlMoveSection.InvokeEx(x => x.Visible = false);
            chkHideImage.InvokeEx(x => x.Checked = false);
            txtDescription.InvokeEx(x => x.Clear());
            txtFilename.InvokeEx(x => x.Clear());

        }

        public void AfterUpload()
        {
            btnSaveEdit.InvokeEx(x => x.Enabled = true);
            btnUploadImage.InvokeEx(x => x.Enabled = false);
            // show move to options
            btnMoveSection.InvokeEx(x => x.Visible = true);
            btnCopySection.InvokeEx(x => x.Visible = true);
            ddlMoveSection.InvokeEx(x => x.Visible = true);
        }

        public void AfterDelete()
        {
            btnSaveEdit.InvokeEx(x => x.Enabled = false);
            //btnDeleteImg.InvokeEx(x => x.Enabled = false);
            btnUploadImage.InvokeEx(x => x.Enabled = true);
            // show move to options
            btnMoveSection.InvokeEx(x => x.Visible = false);
            btnCopySection.InvokeEx(x => x.Visible = false);
            ddlMoveSection.InvokeEx(x => x.Visible = false);
            chkHideImage.InvokeEx(x => x.Checked = false);
        }

        // resets after save
        public void ResetInfo()
        {
            //txtSection.InvokeEx(x => x.Text = "");
            txtFilename.InvokeEx(x => x.Text = "");
            txtDescription.InvokeEx(x => x.Text = "");
            ddlGender.InvokeEx(x => x.SelectedIndex = -1);
            PreviewPictureBox.InvokeEx(x => x.Image = null);
            chkHideImage.InvokeEx(x => x.Checked = false);
            // hide move to options
            btnMoveSection.InvokeEx(x => x.Visible = false);
            btnCopySection.InvokeEx(x => x.Visible = false);
            ddlMoveSection.InvokeEx(x => x.Visible = false);
        }

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

        // default load
        public bool LoadImages(string folder, int cntBar, string filename, bool uploaded)
        {
            bool AddedNew = false;
            DirectoryInfo Folder = new DirectoryInfo(GetLocalImgPath(folder));
            FileInfo[] Images = Folder.GetFiles();
            // download images from folder
            string[] files = GetFileList(folder);
            PicArr.Clear();
            int fileLengthdb = 0;
            string filenamedb = "";
            foreach (FileInfo file in Images)
            {
                PicArr.Add(file.Name);
            }
            try
            {
                foreach (string file in files)
                {
                    // get length of image
                    DataTable dt = Data.GetInfo(file, folder);
                    if (dt.Rows.Count != 0)
                    {
                        fileLengthdb = Convert.ToInt32(dt.Rows[0]["length"]);
                        filenamedb = dt.Rows[0]["filename"].ToString();
                    }
                    if (!PicArr.Contains(file))
                    {
                        if (!file.Contains(".xml"))
                        {
                            AddedNew = true;
                            Download(folder, file);
                        }
                    }
                    else
                    {
                        //int FtpFileLength = Convert.ToInt32(GetFtpFileLength(folder, file));
                        int LocalFileLength = GetLocalFileLength(folder, file);
                        if (fileLengthdb.Equals(0) && filenamedb == "")
                        {
                            Download(folder, file);
                        }
                        else if (fileLengthdb != (LocalFileLength) && filenamedb.Equals(file, StringComparison.OrdinalIgnoreCase))
                        {
                            Download(folder, file);
                        }
                    }
                    cntBar += cntBar / files.Length;
                    if (cntBar < 100)
                    {
                        pbStatus.InvokeEx(x => x.Value = cntBar);
                    }
                }
            }
            catch
            {

            }
            if (AddedNew == false)
            {
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
                        if (Data.CheckImgExist(img.Name, Convert.ToInt32(img.Length), folder) == true)
                        {
                            loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, true, uploaded, filename);
                        }
                        else
                        {
                            loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, false, uploaded, filename);
                        }
                        locnewY = locY + sizeHeight + 10;
                        locnewX = locnewX + sizeWidth + 10;

                        cntBar += (int)(cntBar / Images.Length) + 3;
                        if (Images.Length < 6)
                        {
                            cntBar += 35;
                        }
                        if (cntBar < 100)
                        {
                            pbStatus.InvokeEx(x => x.Value = cntBar);
                        }
                    }
                }

                lastImgX = locnewX;
                lastImgY = locnewY;
            }

            //pbStatus.InvokeEx(x => x.Value = 100);
            return AddedNew;
        }

        public bool LoadFilteredImages(string folder, int cntBar, string filename, bool uploaded, string columnName, string matchValue)
        {
            bool AddedNew = false;
            DirectoryInfo Folder = new DirectoryInfo(GetLocalImgPath(folder));
            FileInfo[] Images = Folder.GetFiles();
            // download images from ftp folder
            string[] files = GetFileList(folder);
            ArrayList ImgNameArr = new ArrayList();
            PicArr.Clear();
            int fileLengthdb = 0;
            string filterMatch = "";
            string filenamedb = "";
            foreach (FileInfo file in Images)
            {
                PicArr.Add(file.Name);
            }
            try
            {
                foreach (string file in files)
                {
                    // get length of image
                    DataTable dt = Data.GetInfo(file, folder);
                    if (dt.Rows.Count != 0)
                    {
                        fileLengthdb = Convert.ToInt32(dt.Rows[0]["length"]);
                        filenamedb = dt.Rows[0]["filename"].ToString();
                    }
                    // image exists from server and local
                    if (PicArr.Contains(file))
                    {
                        if (!file.Contains(".xml"))
                        {
                            int LocalFileLength = GetLocalFileLength(folder, file);
                            // doesnt exist in website
                            if (fileLengthdb.Equals(0) && filenamedb == "")
                            {
                                ;
                            }
                            // exists in website, now check if its hidden and only allow those matched to load
                            else if (fileLengthdb == (LocalFileLength) && filenamedb.Equals(file, StringComparison.OrdinalIgnoreCase))
                            {
                                // check to see if its hidden
                                filterMatch = dt.Rows[0][columnName].ToString();
                                if (filterMatch.Equals(matchValue))
                                {
                                    ImgNameArr.Add(file);
                                }
                            }
                        }
                    }

                    cntBar += cntBar / files.Length;
                    if (cntBar < 100)
                    {
                        pbStatus.InvokeEx(x => x.Value = cntBar);
                    }
                }
            }
            catch
            {

            }
            if (AddedNew == false)
            {
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
                    if (ImgNameArr.Contains(img.Name))
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
                            if (Data.CheckImgExist(img.Name, Convert.ToInt32(img.Length), folder) == true)
                            {
                                loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, true, uploaded, filename);
                            }
                            else
                            {
                                loadImagestoPanel(img.Name, img.FullName, locnewX, locnewY, false, uploaded, filename);
                            }
                            locnewY = locY + sizeHeight + 10;
                            locnewX = locnewX + sizeWidth + 10;

                            cntBar += (int)(cntBar / Images.Length) + 3;
                            if (Images.Length < 6)
                            {
                                cntBar += 35;
                            }
                            if (cntBar < 100)
                            {
                                pbStatus.InvokeEx(x => x.Value = cntBar);
                            }
                        }
                    }
                }

                lastImgX = locnewX;
                lastImgY = locnewY;
            }

            //pbStatus.InvokeEx(x => x.Value = 100);
            return AddedNew;
        }

        private bool loadImagestoPanel(String imageName, String ImageFullName, int newLocX, int newLocY, bool live, bool uploaded, string uploadedImgName)
        {
            bool exists = false;
            //PictureBox ctrl = new PictureBox();
            //ctrl.Image = Image.FromFile(ImageFullName);
            //ctrl.BorderStyle = BorderStyle.FixedSingle;

            foreach (PictureBox p in pnControls.Controls)
            {
                if (p.Name.ToString() == imageName)
                {
                    MessageBox.Show(string.Format("Image {0} already exist.", imageName));
                    exists = true;
                }
            }

            if (exists == false)
            {
                try
                {
                    PictureBox ctrl = new PictureBox();
                    ctrl.Image = FromFile(ImageFullName);

                    ColorImage(ctrl, "unselect");
                    //ctrl.BorderStyle = BorderStyle.FixedSingle;
                    ctrl.Tag = imageName;
                    ctrl.Name = imageName;
                    // if just uploaded select uploaded, set uploaded image to preview
                    if (uploaded == true)
                    {
                        if (uploadedImgName == imageName)
                        {
                            PreviewPictureBox.Image = ctrl.Image;
                        }
                    }
                    ctrl.Location = new Point(newLocX, newLocY);
                    ctrl.Size = new System.Drawing.Size(sizeWidth, sizeHeight);
                    ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
                    ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
                    ctrl.MouseClick += new MouseEventHandler(control_MouseClick);
                    pnControls.InvokeEx(x => x.Controls.Add(ctrl));
                }
                catch
                {

                }
            }
            return exists;
        }

        public static Image FromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return img;
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

        //// gets file length from ftp
        //public long GetFtpFileLength(string folder, string file)
        //{
        //    long downloadFiles;
        //    StringBuilder result = new StringBuilder();
        //    WebResponse response = null;
        //    StreamReader reader = null;
        //    try
        //    {
        //        FtpWebRequest reqFTP;
        //        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://208.118.63.29/site2/" + folder + "//" + file));
        //        reqFTP.UseBinary = true;
        //        reqFTP.Proxy = null;
        //        reqFTP.KeepAlive = false;
        //        reqFTP.UsePassive = false;
        //        reqFTP.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
        //        reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
        //        response = (FtpWebResponse)reqFTP.GetResponse();
        //        long size = response.ContentLength;
        //        response.Close();
        //        return size;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //        downloadFiles = 0;
        //        return downloadFiles;
        //    }
        //}

        public int GetLocalFileLength(string FolderName, string FileName)
        {
            FileInfo toReturn = new FileInfo(GetLocalImgPath(FolderName) + "\\" + FileName);
            return Convert.ToInt32(toReturn.Length);
        }

        private void Download(string folder, string file)
        {
            try
            {
                lblStatus.InvokeEx(x => x.Text = "Copying images..");
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
            catch (WebException wEx)
            {
                MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

        public string GetLocalImgPath(string folder)
        {
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            //System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(appPath);
            //System.IO.DirectoryInfo directoryInfo2 = System.IO.Directory.GetParent(directoryInfo.FullName);
            //string path = directoryInfo2.FullName + @"\Images\" + folder;

            string[] Directories = new string[] { "C:\\ManageImages\\", "C:\\ManageImages\\ApparelsImages", "C:\\ManageImages\\NewArrivalsImages", "C:\\ManageImages\\PantsImages", "C:\\ManageImages\\RhinestoneImages", "C:\\ManageImages\\ShirtsImages", "C:\\ManageImages\\ShoesImages", "C:\\ManageImages\\CapsImages" };

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


        public void UploadServerImg(string FromFolderName, string ToFolderName, string FileName, int pbCnt)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://208.118.63.29/site2/" + ToFolderName + "/" + FileName);
            request.KeepAlive = true;
            request.Proxy = null;
            request.UseBinary = true;
            pbStatus.InvokeEx(x => x.Value = pbCnt + 20);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            pbStatus.InvokeEx(x => x.Value = pbCnt + 20);
            request.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead(GetLocalImgPath(FromFolderName) + "\\" + FileName);
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
        }

        public void UploadCopyServerImg(string FromFolderName, string ToFolderName, string FromFileName, string ToFileName, int pbCnt)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://208.118.63.29/site2/" + ToFolderName + "/" + ToFileName);
            request.KeepAlive = true;
            request.Proxy = null;
            request.UseBinary = true;
            pbStatus.InvokeEx(x => x.Value = pbCnt + 20);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            pbStatus.InvokeEx(x => x.Value = pbCnt + 20);
            request.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead(GetLocalImgPath(FromFolderName) + "\\" + FromFileName);
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

        // moves images to a different section
        public void MoveImage(string fromFolder, string filename, string toFolder, string gender, string description, int length, bool hidden, bool IsCopy)
        {
            try
            {
                pbStatus.InvokeEx(x => x.Value = 40);
                // copy to target server/folder
                if (IsCopy == false)
                {
                    UploadServerImg(fromFolder, toFolder, filename, 20);
                    pbStatus.InvokeEx(x => x.Value = 50);
                    File.Copy(GetLocalImgPath(fromFolder) + "//" + filename, GetLocalImgPath(toFolder) + "//" + filename);
                    // delete local file
                    File.Delete(GetLocalImgPath(fromFolder) + "//" + filename);
                    pbStatus.InvokeEx(x => x.Value = 70);
                    // delete image from server
                    DeleteServerImg(fromFolder, filename);
                    pbStatus.InvokeEx(x => x.Value = 90);
                    // update to db moved folder
                    Data.UpdateImageDb(filename, toFolder, length, gender, description, hidden, true);
                    ReloadImages(filename);
                }
                else
                {
                    string[] copyFileNameSplit = filename.Split('.');
                    string copyFileName = copyFileNameSplit[0].ToString() + " Copy." + copyFileNameSplit[1].ToString();
                    UploadCopyServerImg(fromFolder, toFolder, filename, copyFileName, 20);
                    File.Copy(GetLocalImgPath(fromFolder) + "//" + filename, GetLocalImgPath(toFolder) + "//" + copyFileName);
                    Data.SaveImageToDb(copyFileName, description, gender, toFolder, length, hidden);
                }

                pbStatus.InvokeEx(x => x.Value = 100);
            }
            catch
            {

            }
        }

        public void ReloadImages(string imgName)
        {
            foreach (PictureBox p in pnControls.Controls)
            {
                if (p.Name == imgName)
                {
                    pnControls.InvokeEx(x => x.Controls.Remove(p));
                }
            }
        }

        #endregion
    }
}

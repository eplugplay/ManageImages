namespace ManageImages
{
    partial class ManageImages
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageImages));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.womenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnControls = new System.Windows.Forms.Panel();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.ddlGender = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkHideImage = new System.Windows.Forms.CheckBox();
            this.txtSection = new System.Windows.Forms.TextBox();
            this.btnSaveEdit = new System.Windows.Forms.Button();
            this.lblSection = new System.Windows.Forms.Label();
            this.ddlSections = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCopySection = new System.Windows.Forms.Button();
            this.btnMoveSection = new System.Windows.Forms.Button();
            this.btnDeleteImg = new System.Windows.Forms.Button();
            this.ddlMoveSection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpbxMain = new System.Windows.Forms.GroupBox();
            this.gpFilters = new System.Windows.Forms.GroupBox();
            this.rbHidden = new System.Windows.Forms.RadioButton();
            this.rbWomen = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbMen = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpbxMain.SuspendLayout();
            this.gpFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(977, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportImagesToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ImportImagesToolStripMenuItem
            // 
            this.ImportImagesToolStripMenuItem.Name = "ImportImagesToolStripMenuItem";
            this.ImportImagesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ImportImagesToolStripMenuItem.Text = "Import Image";
            this.ImportImagesToolStripMenuItem.Click += new System.EventHandler(this.ImportImagesToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.hiddenImagesToolStripMenuItem,
            this.menToolStripMenuItem,
            this.womenToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.filterToolStripMenuItem.Text = "Filter By";
            this.filterToolStripMenuItem.Visible = false;
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // hiddenImagesToolStripMenuItem
            // 
            this.hiddenImagesToolStripMenuItem.Name = "hiddenImagesToolStripMenuItem";
            this.hiddenImagesToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.hiddenImagesToolStripMenuItem.Text = "Hidden";
            this.hiddenImagesToolStripMenuItem.Click += new System.EventHandler(this.hiddenImagesToolStripMenuItem_Click);
            // 
            // menToolStripMenuItem
            // 
            this.menToolStripMenuItem.Name = "menToolStripMenuItem";
            this.menToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.menToolStripMenuItem.Text = "Men";
            this.menToolStripMenuItem.Click += new System.EventHandler(this.menToolStripMenuItem_Click);
            // 
            // womenToolStripMenuItem
            // 
            this.womenToolStripMenuItem.Name = "womenToolStripMenuItem";
            this.womenToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.womenToolStripMenuItem.Text = "Women";
            this.womenToolStripMenuItem.Click += new System.EventHandler(this.womenToolStripMenuItem_Click);
            // 
            // pnControls
            // 
            this.pnControls.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnControls.AutoScroll = true;
            this.pnControls.BackColor = System.Drawing.Color.White;
            this.pnControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnControls.Location = new System.Drawing.Point(6, 67);
            this.pnControls.Name = "pnControls";
            this.pnControls.Size = new System.Drawing.Size(522, 521);
            this.pnControls.TabIndex = 19;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(423, 60);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 23);
            this.pbStatus.TabIndex = 38;
            this.pbStatus.Visible = false;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(977, 71);
            this.toolStrip.TabIndex = 20;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ManageImages.Properties.Resources.photos_20;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 68);
            this.toolStripButton1.Text = "toolStripButton9";
            this.toolStripButton1.ToolTipText = "Extra Small";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 68);
            this.toolStripButton2.Text = "toolStripButton9";
            this.toolStripButton2.ToolTipText = "Small";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 68);
            this.toolStripButton3.Text = "toolStripButton9";
            this.toolStripButton3.ToolTipText = "Medium";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(68, 68);
            this.toolStripButton4.Text = "Large";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PreviewPictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PreviewPictureBox.Location = new System.Drawing.Point(31, 51);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(302, 298);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewPictureBox.TabIndex = 26;
            this.PreviewPictureBox.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.BackColor = System.Drawing.Color.SlateGray;
            this.btnUploadImage.Enabled = false;
            this.btnUploadImage.Font = new System.Drawing.Font("Gulim", 8.5F);
            this.btnUploadImage.ForeColor = System.Drawing.Color.White;
            this.btnUploadImage.Location = new System.Drawing.Point(122, 158);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(81, 29);
            this.btnUploadImage.TabIndex = 5;
            this.btnUploadImage.Text = "Upload";
            this.btnUploadImage.UseVisualStyleBackColor = false;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(37, 67);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(79, 20);
            this.lblFileName.TabIndex = 30;
            this.lblFileName.Text = "File Name:";
            // 
            // txtFilename
            // 
            this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(122, 65);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(185, 20);
            this.txtFilename.TabIndex = 55;
            this.txtFilename.TabStop = false;
            // 
            // ddlGender
            // 
            this.ddlGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlGender.FormattingEnabled = true;
            this.ddlGender.ItemHeight = 13;
            this.ddlGender.Items.AddRange(new object[] {
            "Women",
            "Men"});
            this.ddlGender.Location = new System.Drawing.Point(122, 133);
            this.ddlGender.Name = "ddlGender";
            this.ddlGender.Size = new System.Drawing.Size(185, 21);
            this.ddlGender.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 18);
            this.label2.TabIndex = 33;
            this.label2.Text = "Gender:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(26, 102);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(90, 16);
            this.lblDescription.TabIndex = 35;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(122, 100);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(185, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(303, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 39;
            this.label4.Text = "Select Section:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.chkHideImage);
            this.groupBox2.Controls.Add(this.txtSection);
            this.groupBox2.Controls.Add(this.btnSaveEdit);
            this.groupBox2.Controls.Add(this.btnUploadImage);
            this.groupBox2.Controls.Add(this.lblSection);
            this.groupBox2.Controls.Add(this.txtFilename);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ddlGender);
            this.groupBox2.Controls.Add(this.lblDescription);
            this.groupBox2.Controls.Add(this.lblFileName);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(575, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 198);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Info:";
            // 
            // chkHideImage
            // 
            this.chkHideImage.AutoSize = true;
            this.chkHideImage.Location = new System.Drawing.Point(217, 14);
            this.chkHideImage.Name = "chkHideImage";
            this.chkHideImage.Size = new System.Drawing.Size(90, 17);
            this.chkHideImage.TabIndex = 0;
            this.chkHideImage.Text = "Hide Image";
            this.toolTip.SetToolTip(this.chkHideImage, "Hide Image");
            this.chkHideImage.UseVisualStyleBackColor = true;
            // 
            // txtSection
            // 
            this.txtSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSection.Location = new System.Drawing.Point(122, 35);
            this.txtSection.Name = "txtSection";
            this.txtSection.ReadOnly = true;
            this.txtSection.Size = new System.Drawing.Size(185, 20);
            this.txtSection.TabIndex = 39;
            this.txtSection.TabStop = false;
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = System.Drawing.Color.SlateGray;
            this.btnSaveEdit.Enabled = false;
            this.btnSaveEdit.Font = new System.Drawing.Font("Gulim", 8.5F);
            this.btnSaveEdit.ForeColor = System.Drawing.Color.White;
            this.btnSaveEdit.Location = new System.Drawing.Point(225, 158);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(81, 30);
            this.btnSaveEdit.TabIndex = 3;
            this.btnSaveEdit.Text = "Save Edit";
            this.btnSaveEdit.UseVisualStyleBackColor = false;
            this.btnSaveEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // lblSection
            // 
            this.lblSection.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSection.Location = new System.Drawing.Point(52, 37);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(64, 18);
            this.lblSection.TabIndex = 40;
            this.lblSection.Text = "Section:";
            // 
            // ddlSections
            // 
            this.ddlSections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSections.FormattingEnabled = true;
            this.ddlSections.Items.AddRange(new object[] {
            "-- Select"});
            this.ddlSections.Location = new System.Drawing.Point(411, 38);
            this.ddlSections.Name = "ddlSections";
            this.ddlSections.Size = new System.Drawing.Size(117, 21);
            this.ddlSections.TabIndex = 6;
            this.ddlSections.SelectedIndexChanged += new System.EventHandler(this.ddlSections_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.btnCopySection);
            this.groupBox3.Controls.Add(this.btnMoveSection);
            this.groupBox3.Controls.Add(this.btnDeleteImg);
            this.groupBox3.Controls.Add(this.ddlMoveSection);
            this.groupBox3.Controls.Add(this.PreviewPictureBox);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(575, 302);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 398);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image Preview";
            // 
            // btnCopySection
            // 
            this.btnCopySection.BackColor = System.Drawing.Color.SlateGray;
            this.btnCopySection.Font = new System.Drawing.Font("Gulim", 8.5F);
            this.btnCopySection.ForeColor = System.Drawing.Color.White;
            this.btnCopySection.Location = new System.Drawing.Point(252, 17);
            this.btnCopySection.Name = "btnCopySection";
            this.btnCopySection.Size = new System.Drawing.Size(82, 28);
            this.btnCopySection.TabIndex = 45;
            this.btnCopySection.Text = "Copy to:";
            this.toolTip.SetToolTip(this.btnCopySection, "Keeps selected image in current section and copies it to another section.");
            this.btnCopySection.UseVisualStyleBackColor = false;
            this.btnCopySection.Visible = false;
            this.btnCopySection.Click += new System.EventHandler(this.btnCopySection_Click);
            // 
            // btnMoveSection
            // 
            this.btnMoveSection.BackColor = System.Drawing.Color.SlateGray;
            this.btnMoveSection.Font = new System.Drawing.Font("Gulim", 8.5F);
            this.btnMoveSection.ForeColor = System.Drawing.Color.White;
            this.btnMoveSection.Location = new System.Drawing.Point(164, 17);
            this.btnMoveSection.Name = "btnMoveSection";
            this.btnMoveSection.Size = new System.Drawing.Size(82, 28);
            this.btnMoveSection.TabIndex = 44;
            this.btnMoveSection.Text = "Move to:";
            this.toolTip.SetToolTip(this.btnMoveSection, "Deletes selected image in current section and moves it to another section.");
            this.btnMoveSection.UseVisualStyleBackColor = false;
            this.btnMoveSection.Visible = false;
            this.btnMoveSection.Click += new System.EventHandler(this.btnMoveSection_Click);
            // 
            // btnDeleteImg
            // 
            this.btnDeleteImg.BackColor = System.Drawing.Color.SlateGray;
            this.btnDeleteImg.Font = new System.Drawing.Font("Gulim", 8.5F);
            this.btnDeleteImg.ForeColor = System.Drawing.Color.White;
            this.btnDeleteImg.Location = new System.Drawing.Point(253, 355);
            this.btnDeleteImg.Name = "btnDeleteImg";
            this.btnDeleteImg.Size = new System.Drawing.Size(81, 29);
            this.btnDeleteImg.TabIndex = 4;
            this.btnDeleteImg.Text = "Delete";
            this.btnDeleteImg.UseVisualStyleBackColor = false;
            this.btnDeleteImg.Click += new System.EventHandler(this.btnDeleteImg_Click);
            // 
            // ddlMoveSection
            // 
            this.ddlMoveSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMoveSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlMoveSection.FormattingEnabled = true;
            this.ddlMoveSection.Items.AddRange(new object[] {
            "-- Select"});
            this.ddlMoveSection.Location = new System.Drawing.Point(31, 21);
            this.ddlMoveSection.Name = "ddlMoveSection";
            this.ddlMoveSection.Size = new System.Drawing.Size(127, 21);
            this.ddlMoveSection.TabIndex = 42;
            this.ddlMoveSection.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 19);
            this.label3.TabIndex = 38;
            this.label3.Text = "Images From Website";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(420, 44);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(87, 13);
            this.lblStatus.TabIndex = 41;
            this.lblStatus.Text = "Please Wait...";
            this.lblStatus.Visible = false;
            // 
            // grpbxMain
            // 
            this.grpbxMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpbxMain.Controls.Add(this.gpFilters);
            this.grpbxMain.Controls.Add(this.pnControls);
            this.grpbxMain.Controls.Add(this.label4);
            this.grpbxMain.Controls.Add(this.label3);
            this.grpbxMain.Controls.Add(this.ddlSections);
            this.grpbxMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpbxMain.Location = new System.Drawing.Point(27, 98);
            this.grpbxMain.Name = "grpbxMain";
            this.grpbxMain.Size = new System.Drawing.Size(542, 602);
            this.grpbxMain.TabIndex = 41;
            this.grpbxMain.TabStop = false;
            // 
            // gpFilters
            // 
            this.gpFilters.Controls.Add(this.rbHidden);
            this.gpFilters.Controls.Add(this.rbWomen);
            this.gpFilters.Controls.Add(this.rbAll);
            this.gpFilters.Controls.Add(this.rbMen);
            this.gpFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpFilters.Location = new System.Drawing.Point(140, 8);
            this.gpFilters.Name = "gpFilters";
            this.gpFilters.Size = new System.Drawing.Size(241, 32);
            this.gpFilters.TabIndex = 48;
            this.gpFilters.TabStop = false;
            this.gpFilters.Text = "Filter By:";
            // 
            // rbHidden
            // 
            this.rbHidden.AutoSize = true;
            this.rbHidden.Location = new System.Drawing.Point(172, 12);
            this.rbHidden.Name = "rbHidden";
            this.rbHidden.Size = new System.Drawing.Size(65, 17);
            this.rbHidden.TabIndex = 47;
            this.rbHidden.TabStop = true;
            this.rbHidden.Text = "Hidden";
            this.rbHidden.UseVisualStyleBackColor = true;
            this.rbHidden.CheckedChanged += new System.EventHandler(this.rbHidden_CheckedChanged);
            // 
            // rbWomen
            // 
            this.rbWomen.AutoSize = true;
            this.rbWomen.Location = new System.Drawing.Point(53, 12);
            this.rbWomen.Name = "rbWomen";
            this.rbWomen.Size = new System.Drawing.Size(67, 17);
            this.rbWomen.TabIndex = 44;
            this.rbWomen.TabStop = true;
            this.rbWomen.Text = "Women";
            this.rbWomen.UseVisualStyleBackColor = true;
            this.rbWomen.CheckedChanged += new System.EventHandler(this.rbWomen_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(8, 12);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(39, 17);
            this.rbAll.TabIndex = 46;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbMen
            // 
            this.rbMen.AutoSize = true;
            this.rbMen.Location = new System.Drawing.Point(121, 12);
            this.rbMen.Name = "rbMen";
            this.rbMen.Size = new System.Drawing.Size(49, 17);
            this.rbMen.TabIndex = 45;
            this.rbMen.TabStop = true;
            this.rbMen.Text = "Men";
            this.rbMen.UseVisualStyleBackColor = true;
            this.rbMen.CheckedChanged += new System.EventHandler(this.rbMen_CheckedChanged);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker4_RunWorkerCompleted);
            // 
            // backgroundWorker5
            // 
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            this.backgroundWorker5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker5_RunWorkerCompleted);
            // 
            // ManageImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 712);
            this.Controls.Add(this.grpbxMain);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ManageImages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Images";
            this.Load += new System.EventHandler(this.ManageImages_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.grpbxMain.ResumeLayout(false);
            this.gpFilters.ResumeLayout(false);
            this.gpFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel pnControls;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.ToolStripMenuItem ImportImagesToolStripMenuItem;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.ComboBox ddlGender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlSections;
        private System.Windows.Forms.Button btnDeleteImg;
        private System.Windows.Forms.Button btnSaveEdit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSection;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpbxMain;
        private System.Windows.Forms.CheckBox chkHideImage;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox ddlMoveSection;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.Button btnMoveSection;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiddenImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem womenToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
        private System.Windows.Forms.RadioButton rbMen;
        private System.Windows.Forms.RadioButton rbWomen;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.GroupBox gpFilters;
        private System.Windows.Forms.RadioButton rbHidden;
        private System.Windows.Forms.Button btnCopySection;
    }
}


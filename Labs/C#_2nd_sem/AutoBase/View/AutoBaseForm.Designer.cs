namespace AutoBase.View
{
    partial class AutoBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoBaseForm));
            dispatcherPictureBox = new PictureBox();
            chiefPictureBox = new PictureBox();
            workerHousePictureBox = new PictureBox();
            repairPictureBox = new PictureBox();
            garagePictureBox = new PictureBox();
            dialogDispatcher = new PictureBox();
            dialogChief = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dispatcherPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chiefPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)workerHousePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repairPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)garagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dialogDispatcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dialogChief).BeginInit();
            SuspendLayout();
            // 
            // dispatcherPictureBox
            // 
            dispatcherPictureBox.Image = (Image)resources.GetObject("dispatcherPictureBox.Image");
            dispatcherPictureBox.Location = new Point(12, 68);
            dispatcherPictureBox.Name = "dispatcherPictureBox";
            dispatcherPictureBox.Size = new Size(86, 66);
            dispatcherPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            dispatcherPictureBox.TabIndex = 0;
            dispatcherPictureBox.TabStop = false;
            // 
            // chiefPictureBox
            // 
            chiefPictureBox.Image = (Image)resources.GetObject("chiefPictureBox.Image");
            chiefPictureBox.Location = new Point(839, 287);
            chiefPictureBox.Name = "chiefPictureBox";
            chiefPictureBox.Size = new Size(97, 105);
            chiefPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            chiefPictureBox.TabIndex = 1;
            chiefPictureBox.TabStop = false;
            // 
            // workerHousePictureBox
            // 
            workerHousePictureBox.Image = (Image)resources.GetObject("workerHousePictureBox.Image");
            workerHousePictureBox.Location = new Point(146, 237);
            workerHousePictureBox.Name = "workerHousePictureBox";
            workerHousePictureBox.Size = new Size(128, 84);
            workerHousePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            workerHousePictureBox.TabIndex = 2;
            workerHousePictureBox.TabStop = false;
            // 
            // repairPictureBox
            // 
            repairPictureBox.Image = (Image)resources.GetObject("repairPictureBox.Image");
            repairPictureBox.Location = new Point(659, 92);
            repairPictureBox.Name = "repairPictureBox";
            repairPictureBox.Size = new Size(128, 84);
            repairPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            repairPictureBox.TabIndex = 3;
            repairPictureBox.TabStop = false;
            // 
            // garagePictureBox
            // 
            garagePictureBox.Image = (Image)resources.GetObject("garagePictureBox.Image");
            garagePictureBox.Location = new Point(352, 92);
            garagePictureBox.Name = "garagePictureBox";
            garagePictureBox.Size = new Size(128, 84);
            garagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            garagePictureBox.TabIndex = 4;
            garagePictureBox.TabStop = false;
            // 
            // dialogDispatcher
            // 
            dialogDispatcher.Location = new Point(33, 0);
            dialogDispatcher.Name = "dialogDispatcher";
            dialogDispatcher.Size = new Size(125, 62);
            dialogDispatcher.SizeMode = PictureBoxSizeMode.StretchImage;
            dialogDispatcher.TabIndex = 5;
            dialogDispatcher.TabStop = false;
            // 
            // dialogChief
            // 
            dialogChief.Location = new Point(854, 219);
            dialogChief.Name = "dialogChief";
            dialogChief.Size = new Size(125, 62);
            dialogChief.SizeMode = PictureBoxSizeMode.StretchImage;
            dialogChief.TabIndex = 6;
            dialogChief.TabStop = false;
            // 
            // AutoBaseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 579);
            Controls.Add(dialogChief);
            Controls.Add(dialogDispatcher);
            Controls.Add(garagePictureBox);
            Controls.Add(repairPictureBox);
            Controls.Add(workerHousePictureBox);
            Controls.Add(chiefPictureBox);
            Controls.Add(dispatcherPictureBox);
            Name = "AutoBaseForm";
            Text = "AutoBaseForm";
            Load += AutoBaseForm_Load;
            ((System.ComponentModel.ISupportInitialize)dispatcherPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)chiefPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)workerHousePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)repairPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)garagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dialogDispatcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)dialogChief).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox dispatcherPictureBox;
        private PictureBox chiefPictureBox;
        private PictureBox workerHousePictureBox;
        private PictureBox repairPictureBox;
        private PictureBox garagePictureBox;
        private PictureBox dialogDispatcher;
        private PictureBox dialogChief;
    }
}
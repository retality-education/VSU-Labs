namespace LandscapeDesign.View
{
    partial class FormView
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
            ideaPicture = new PictureBox();
            designerShopPicture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ideaPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)designerShopPicture).BeginInit();
            SuspendLayout();
            // 
            // ideaPicture
            // 
            ideaPicture.Image = Properties.Resources.Idea;
            ideaPicture.Location = new Point(446, 205);
            ideaPicture.Name = "ideaPicture";
            ideaPicture.Size = new Size(36, 39);
            ideaPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            ideaPicture.TabIndex = 7;
            ideaPicture.TabStop = false;
            ideaPicture.Visible = false;
            // 
            // designerShopPicture
            // 
            designerShopPicture.Image = Properties.Resources.DesignerShop;
            designerShopPicture.Location = new Point(24, 549);
            designerShopPicture.Name = "designerShopPicture";
            designerShopPicture.Size = new Size(285, 192);
            designerShopPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            designerShopPicture.TabIndex = 8;
            designerShopPicture.TabStop = false;
            // 
            // FormView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1030, 759);
            Controls.Add(ideaPicture);
            Controls.Add(designerShopPicture);
            Name = "FormView";
            Text = "FormView";
            Load += FormView_Load;
            ((System.ComponentModel.ISupportInitialize)ideaPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)designerShopPicture).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox ideaPicture;
        private PictureBox designerShopPicture;
    }
}
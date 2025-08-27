namespace AutoBaseModel.Views
{
    partial class MainForm
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
            garage = new PictureBox();
            repair = new PictureBox();
            workerHouse = new PictureBox();
            boss = new PictureBox();
            dispatcher = new PictureBox();
            money = new Label();
            dialogBoss = new Label();
            ((System.ComponentModel.ISupportInitialize)garage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repair).BeginInit();
            ((System.ComponentModel.ISupportInitialize)workerHouse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)boss).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dispatcher).BeginInit();
            SuspendLayout();
            // 
            // garage
            // 
            garage.Image = Properties.Resources.garage;
            garage.Location = new Point(148, 277);
            garage.Name = "garage";
            garage.Size = new Size(126, 121);
            garage.SizeMode = PictureBoxSizeMode.StretchImage;
            garage.TabIndex = 0;
            garage.TabStop = false;
            // 
            // repair
            // 
            repair.Image = Properties.Resources.Repair;
            repair.Location = new Point(675, 277);
            repair.Name = "repair";
            repair.Size = new Size(133, 121);
            repair.SizeMode = PictureBoxSizeMode.StretchImage;
            repair.TabIndex = 1;
            repair.TabStop = false;
            // 
            // workerHouse
            // 
            workerHouse.Image = Properties.Resources.house;
            workerHouse.Location = new Point(420, 137);
            workerHouse.Name = "workerHouse";
            workerHouse.Size = new Size(99, 82);
            workerHouse.SizeMode = PictureBoxSizeMode.StretchImage;
            workerHouse.TabIndex = 2;
            workerHouse.TabStop = false;
            // 
            // boss
            // 
            boss.Image = Properties.Resources.boss;
            boss.Location = new Point(354, 22);
            boss.Name = "boss";
            boss.Size = new Size(61, 82);
            boss.SizeMode = PictureBoxSizeMode.StretchImage;
            boss.TabIndex = 3;
            boss.TabStop = false;
            // 
            // dispatcher
            // 
            dispatcher.Image = Properties.Resources.dispatcher;
            dispatcher.Location = new Point(420, 424);
            dispatcher.Name = "dispatcher";
            dispatcher.Size = new Size(99, 82);
            dispatcher.SizeMode = PictureBoxSizeMode.StretchImage;
            dispatcher.TabIndex = 4;
            dispatcher.TabStop = false;
            // 
            // money
            // 
            money.AutoSize = true;
            money.Location = new Point(392, 378);
            money.Name = "money";
            money.Size = new Size(0, 20);
            money.TabIndex = 5;
            // 
            // dialogBoss
            // 
            dialogBoss.AutoSize = true;
            dialogBoss.Location = new Point(421, 31);
            dialogBoss.Name = "dialogBoss";
            dialogBoss.Size = new Size(151, 20);
            dialogBoss.TabIndex = 6;
            dialogBoss.Text = "РАБОТАЙТЕ ЛУЧШЕ!";
            dialogBoss.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 714);
            Controls.Add(dialogBoss);
            Controls.Add(money);
            Controls.Add(dispatcher);
            Controls.Add(boss);
            Controls.Add(workerHouse);
            Controls.Add(repair);
            Controls.Add(garage);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)garage).EndInit();
            ((System.ComponentModel.ISupportInitialize)repair).EndInit();
            ((System.ComponentModel.ISupportInitialize)workerHouse).EndInit();
            ((System.ComponentModel.ISupportInitialize)boss).EndInit();
            ((System.ComponentModel.ISupportInitialize)dispatcher).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox garage;
        private PictureBox repair;
        private PictureBox workerHouse;
        private PictureBox boss;
        private PictureBox dispatcher;
        private Label money;
        private Label dialogBoss;
    }
}
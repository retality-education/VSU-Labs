namespace HomeFinanceApp.Views.Forms
{
    partial class FinanceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinanceForm));
            table = new PictureBox();
            savingsPicture = new PictureBox();
            moneysPicture = new PictureBox();
            moneysLabel = new Label();
            savingsLabel = new Label();
            button1 = new Button();
            memberMoney3 = new PictureBox();
            memberMoney2 = new PictureBox();
            memberMoney1 = new PictureBox();
            memberMoney4 = new PictureBox();
            button2 = new Button();
            comboBox1 = new ComboBox();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)table).BeginInit();
            ((System.ComponentModel.ISupportInitialize)savingsPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)moneysPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney4).BeginInit();
            SuspendLayout();
            // 
            // table
            // 
            table.Image = (Image)resources.GetObject("table.Image");
            table.Location = new Point(139, 90);
            table.Name = "table";
            table.Size = new Size(648, 352);
            table.SizeMode = PictureBoxSizeMode.StretchImage;
            table.TabIndex = 0;
            table.TabStop = false;
            // 
            // savingsPicture
            // 
            savingsPicture.Image = (Image)resources.GetObject("savingsPicture.Image");
            savingsPicture.Location = new Point(607, 144);
            savingsPicture.Name = "savingsPicture";
            savingsPicture.Size = new Size(70, 46);
            savingsPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            savingsPicture.TabIndex = 1;
            savingsPicture.TabStop = false;
            // 
            // moneysPicture
            // 
            moneysPicture.Image = (Image)resources.GetObject("moneysPicture.Image");
            moneysPicture.Location = new Point(434, 181);
            moneysPicture.Name = "moneysPicture";
            moneysPicture.Size = new Size(72, 34);
            moneysPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            moneysPicture.TabIndex = 3;
            moneysPicture.TabStop = false;
            moneysPicture.Visible = false;
            // 
            // moneysLabel
            // 
            moneysLabel.AutoSize = true;
            moneysLabel.Location = new Point(443, 158);
            moneysLabel.Name = "moneysLabel";
            moneysLabel.Size = new Size(0, 20);
            moneysLabel.TabIndex = 4;
            // 
            // savingsLabel
            // 
            savingsLabel.AutoSize = true;
            savingsLabel.Location = new Point(617, 121);
            savingsLabel.Name = "savingsLabel";
            savingsLabel.Size = new Size(0, 20);
            savingsLabel.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(837, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "Запустить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // memberMoney3
            // 
            memberMoney3.Image = (Image)resources.GetObject("memberMoney3.Image");
            memberMoney3.Location = new Point(534, 264);
            memberMoney3.Name = "memberMoney3";
            memberMoney3.Size = new Size(72, 34);
            memberMoney3.SizeMode = PictureBoxSizeMode.StretchImage;
            memberMoney3.TabIndex = 7;
            memberMoney3.TabStop = false;
            memberMoney3.Visible = false;
            // 
            // memberMoney2
            // 
            memberMoney2.Image = (Image)resources.GetObject("memberMoney2.Image");
            memberMoney2.Location = new Point(701, 181);
            memberMoney2.Name = "memberMoney2";
            memberMoney2.Size = new Size(72, 34);
            memberMoney2.SizeMode = PictureBoxSizeMode.StretchImage;
            memberMoney2.TabIndex = 8;
            memberMoney2.TabStop = false;
            memberMoney2.Visible = false;
            // 
            // memberMoney1
            // 
            memberMoney1.Image = (Image)resources.GetObject("memberMoney1.Image");
            memberMoney1.Location = new Point(434, 107);
            memberMoney1.Name = "memberMoney1";
            memberMoney1.Size = new Size(72, 34);
            memberMoney1.SizeMode = PictureBoxSizeMode.StretchImage;
            memberMoney1.TabIndex = 9;
            memberMoney1.TabStop = false;
            memberMoney1.Visible = false;
            // 
            // memberMoney4
            // 
            memberMoney4.Image = (Image)resources.GetObject("memberMoney4.Image");
            memberMoney4.Location = new Point(151, 181);
            memberMoney4.Name = "memberMoney4";
            memberMoney4.Size = new Size(72, 34);
            memberMoney4.SizeMode = PictureBoxSizeMode.StretchImage;
            memberMoney4.TabIndex = 10;
            memberMoney4.TabStop = false;
            memberMoney4.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(660, 2);
            button2.Name = "button2";
            button2.Size = new Size(161, 48);
            button2.TabIndex = 11;
            button2.Text = "Показать статистику семьи";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Сын", "Дочь", "Мать", "Отец" });
            comboBox1.Location = new Point(184, 13);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(135, 28);
            comboBox1.TabIndex = 12;
            // 
            // button3
            // 
            button3.Location = new Point(12, 2);
            button3.Name = "button3";
            button3.Size = new Size(166, 48);
            button3.TabIndex = 13;
            button3.Text = "Показать статистичку члена семьи";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // FinanceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 521);
            Controls.Add(button3);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(memberMoney4);
            Controls.Add(memberMoney1);
            Controls.Add(memberMoney2);
            Controls.Add(memberMoney3);
            Controls.Add(button1);
            Controls.Add(savingsLabel);
            Controls.Add(moneysLabel);
            Controls.Add(moneysPicture);
            Controls.Add(savingsPicture);
            Controls.Add(table);
            Name = "FinanceForm";
            Text = "FinanceForm";
            ((System.ComponentModel.ISupportInitialize)table).EndInit();
            ((System.ComponentModel.ISupportInitialize)savingsPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)moneysPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney3).EndInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney2).EndInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney1).EndInit();
            ((System.ComponentModel.ISupportInitialize)memberMoney4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox table;
        private PictureBox savingsPicture;
        private PictureBox moneysPicture;
        private Label moneysLabel;
        private Label savingsLabel;
        private Button button1;
        private PictureBox memberMoney3;
        private PictureBox memberMoney2;
        private PictureBox memberMoney1;
        private PictureBox memberMoney4;
        private Button button2;
        private ComboBox comboBox1;
        private Button button3;
    }
}
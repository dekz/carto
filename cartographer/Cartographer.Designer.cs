namespace cartographer
{
    partial class Cartographer
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
            this.loadKML = new System.Windows.Forms.Button();
            this.convertData = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.xlsLab = new System.Windows.Forms.Label();
            this.midLab = new System.Windows.Forms.Label();
            this.mifLab = new System.Windows.Forms.Label();
            this.xlsBut = new System.Windows.Forms.Button();
            this.midBut = new System.Windows.Forms.Button();
            this.mifBut = new System.Windows.Forms.Button();
            this.mifPB = new System.Windows.Forms.PictureBox();
            this.midPB = new System.Windows.Forms.PictureBox();
            this.xlsPB = new System.Windows.Forms.PictureBox();
            this.convertPB = new System.Windows.Forms.PictureBox();
            this.pointBox = new System.Windows.Forms.CheckedListBox();
            this.generateBut = new System.Windows.Forms.Button();
            this.loadBut = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mifPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.midPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xlsPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.convertPB)).BeginInit();
            this.SuspendLayout();
            // 
            // loadKML
            // 
            this.loadKML.Location = new System.Drawing.Point(813, 460);
            this.loadKML.Name = "loadKML";
            this.loadKML.Size = new System.Drawing.Size(168, 23);
            this.loadKML.TabIndex = 0;
            this.loadKML.Text = "Load KML File";
            this.loadKML.UseVisualStyleBackColor = true;
            this.loadKML.Click += new System.EventHandler(this.loadKML_Click);
            // 
            // convertData
            // 
            this.convertData.Location = new System.Drawing.Point(864, 78);
            this.convertData.Name = "convertData";
            this.convertData.Size = new System.Drawing.Size(117, 23);
            this.convertData.TabIndex = 2;
            this.convertData.Text = "Convert Data";
            this.convertData.UseVisualStyleBackColor = true;
            this.convertData.Click += new System.EventHandler(this.convertData_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(814, 489);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(168, 23);
            this.exit.TabIndex = 3;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // xlsLab
            // 
            this.xlsLab.AutoSize = true;
            this.xlsLab.Location = new System.Drawing.Point(861, 58);
            this.xlsLab.Name = "xlsLab";
            this.xlsLab.Size = new System.Drawing.Size(70, 13);
            this.xlsLab.TabIndex = 4;
            this.xlsLab.Text = "No XLS Data";
            // 
            // midLab
            // 
            this.midLab.AutoSize = true;
            this.midLab.Location = new System.Drawing.Point(861, 35);
            this.midLab.Name = "midLab";
            this.midLab.Size = new System.Drawing.Size(70, 13);
            this.midLab.TabIndex = 5;
            this.midLab.Text = "No MID Data";
            this.midLab.Click += new System.EventHandler(this.label2_Click);
            // 
            // mifLab
            // 
            this.mifLab.AutoSize = true;
            this.mifLab.Location = new System.Drawing.Point(861, 11);
            this.mifLab.Name = "mifLab";
            this.mifLab.Size = new System.Drawing.Size(68, 13);
            this.mifLab.TabIndex = 6;
            this.mifLab.Text = "No MIF Data";
            // 
            // xlsBut
            // 
            this.xlsBut.Location = new System.Drawing.Point(937, 53);
            this.xlsBut.Name = "xlsBut";
            this.xlsBut.Size = new System.Drawing.Size(44, 23);
            this.xlsBut.TabIndex = 8;
            this.xlsBut.Text = "Load";
            this.xlsBut.UseVisualStyleBackColor = true;
            this.xlsBut.Click += new System.EventHandler(this.xlsBut_Click);
            // 
            // midBut
            // 
            this.midBut.Location = new System.Drawing.Point(937, 30);
            this.midBut.Name = "midBut";
            this.midBut.Size = new System.Drawing.Size(44, 23);
            this.midBut.TabIndex = 9;
            this.midBut.Text = "Load";
            this.midBut.UseVisualStyleBackColor = true;
            this.midBut.Click += new System.EventHandler(this.midBut_Click);
            // 
            // mifBut
            // 
            this.mifBut.Location = new System.Drawing.Point(937, 6);
            this.mifBut.Name = "mifBut";
            this.mifBut.Size = new System.Drawing.Size(44, 23);
            this.mifBut.TabIndex = 10;
            this.mifBut.Text = "Load";
            this.mifBut.UseVisualStyleBackColor = true;
            this.mifBut.Click += new System.EventHandler(this.mifBut_Click);
            // 
            // mifPB
            // 
            this.mifPB.Location = new System.Drawing.Point(833, 8);
            this.mifPB.Name = "mifPB";
            this.mifPB.Size = new System.Drawing.Size(16, 16);
            this.mifPB.TabIndex = 11;
            this.mifPB.TabStop = false;
            // 
            // midPB
            // 
            this.midPB.Location = new System.Drawing.Point(833, 33);
            this.midPB.Name = "midPB";
            this.midPB.Size = new System.Drawing.Size(16, 16);
            this.midPB.TabIndex = 12;
            this.midPB.TabStop = false;
            // 
            // xlsPB
            // 
            this.xlsPB.Location = new System.Drawing.Point(833, 55);
            this.xlsPB.Name = "xlsPB";
            this.xlsPB.Size = new System.Drawing.Size(16, 16);
            this.xlsPB.TabIndex = 13;
            this.xlsPB.TabStop = false;
            // 
            // convertPB
            // 
            this.convertPB.Location = new System.Drawing.Point(833, 81);
            this.convertPB.Name = "convertPB";
            this.convertPB.Size = new System.Drawing.Size(16, 16);
            this.convertPB.TabIndex = 14;
            this.convertPB.TabStop = false;
            // 
            // pointBox
            // 
            this.pointBox.FormattingEnabled = true;
            this.pointBox.Location = new System.Drawing.Point(814, 107);
            this.pointBox.Name = "pointBox";
            this.pointBox.Size = new System.Drawing.Size(167, 244);
            this.pointBox.TabIndex = 15;
            // 
            // generateBut
            // 
            this.generateBut.Location = new System.Drawing.Point(814, 413);
            this.generateBut.Name = "generateBut";
            this.generateBut.Size = new System.Drawing.Size(166, 23);
            this.generateBut.TabIndex = 16;
            this.generateBut.Text = "Generate";
            this.generateBut.UseVisualStyleBackColor = true;
            this.generateBut.Click += new System.EventHandler(this.generateBut_Click);
            // 
            // loadBut
            // 
            this.loadBut.Location = new System.Drawing.Point(758, 35);
            this.loadBut.Name = "loadBut";
            this.loadBut.Size = new System.Drawing.Size(69, 23);
            this.loadBut.TabIndex = 17;
            this.loadBut.Text = "Load Last";
            this.loadBut.UseVisualStyleBackColor = true;
            this.loadBut.Click += new System.EventHandler(this.loadBut_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(814, 387);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 17);
            this.radioButton1.TabIndex = 18;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Current Party";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(905, 387);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(80, 17);
            this.radioButton2.TabIndex = 19;
            this.radioButton2.Text = "Seat Safety";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(811, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Colour Mode";
            // 
            // Cartographer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 524);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.loadBut);
            this.Controls.Add(this.generateBut);
            this.Controls.Add(this.pointBox);
            this.Controls.Add(this.convertPB);
            this.Controls.Add(this.xlsPB);
            this.Controls.Add(this.midPB);
            this.Controls.Add(this.mifPB);
            this.Controls.Add(this.mifBut);
            this.Controls.Add(this.midBut);
            this.Controls.Add(this.xlsBut);
            this.Controls.Add(this.mifLab);
            this.Controls.Add(this.midLab);
            this.Controls.Add(this.xlsLab);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.convertData);
            this.Controls.Add(this.loadKML);
            this.Name = "Cartographer";
            this.Text = "Cartographer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.mifPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.midPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xlsPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.convertPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadKML;
        private System.Windows.Forms.Button convertData;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label xlsLab;
        private System.Windows.Forms.Label midLab;
        private System.Windows.Forms.Label mifLab;
        private System.Windows.Forms.Button xlsBut;
        private System.Windows.Forms.Button midBut;
        private System.Windows.Forms.Button mifBut;
        private System.Windows.Forms.PictureBox mifPB;
        private System.Windows.Forms.PictureBox midPB;
        private System.Windows.Forms.PictureBox xlsPB;
        private System.Windows.Forms.PictureBox convertPB;
        private System.Windows.Forms.CheckedListBox pointBox;
        private System.Windows.Forms.Button generateBut;
        private System.Windows.Forms.Button loadBut;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;

    }
}


namespace iris_classification
{
    partial class Form1
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
            this.Learn = new System.Windows.Forms.Button();
            this.LoadArray = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHidden = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLearnRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.chkEnhanced = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEpochs = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTestCount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtVir = new System.Windows.Forms.TextBox();
            this.txtVer = new System.Windows.Forms.TextBox();
            this.txtSetPer = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCorrect = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkWtoFile = new System.Windows.Forms.CheckBox();
            this.chkDisplay = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbltrainingCount = new System.Windows.Forms.Label();
            this.lblTestingCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Learn
            // 
            this.Learn.Enabled = false;
            this.Learn.Location = new System.Drawing.Point(200, 169);
            this.Learn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Learn.Name = "Learn";
            this.Learn.Size = new System.Drawing.Size(150, 44);
            this.Learn.TabIndex = 0;
            this.Learn.Text = "GO";
            this.Learn.UseVisualStyleBackColor = true;
            this.Learn.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoadArray
            // 
            this.LoadArray.Location = new System.Drawing.Point(200, 85);
            this.LoadArray.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoadArray.Name = "LoadArray";
            this.LoadArray.Size = new System.Drawing.Size(150, 44);
            this.LoadArray.TabIndex = 1;
            this.LoadArray.Text = "load Data";
            this.LoadArray.UseVisualStyleBackColor = true;
            this.LoadArray.Click += new System.EventHandler(this.LoadArray_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(702, 92);
            this.txtInput.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(148, 31);
            this.txtInput.TabIndex = 2;
            this.txtInput.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(568, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Inputs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(568, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hidden";
            // 
            // txtHidden
            // 
            this.txtHidden.Location = new System.Drawing.Point(702, 142);
            this.txtHidden.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtHidden.Name = "txtHidden";
            this.txtHidden.Size = new System.Drawing.Size(148, 31);
            this.txtHidden.TabIndex = 10;
            this.txtHidden.Text = "9";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 194);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Outputs";
            // 
            // txtOutputs
            // 
            this.txtOutputs.Location = new System.Drawing.Point(702, 192);
            this.txtOutputs.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutputs.Name = "txtOutputs";
            this.txtOutputs.Size = new System.Drawing.Size(148, 31);
            this.txtOutputs.TabIndex = 7;
            this.txtOutputs.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 288);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Learn Rate";
            // 
            // txtLearnRate
            // 
            this.txtLearnRate.Location = new System.Drawing.Point(702, 287);
            this.txtLearnRate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLearnRate.Name = "txtLearnRate";
            this.txtLearnRate.Size = new System.Drawing.Size(148, 31);
            this.txtLearnRate.TabIndex = 9;
            this.txtLearnRate.Text = "0.3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(570, 383);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Inital Repeat";
            // 
            // txtRepeat
            // 
            this.txtRepeat.Location = new System.Drawing.Point(702, 383);
            this.txtRepeat.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.Size = new System.Drawing.Size(148, 31);
            this.txtRepeat.TabIndex = 11;
            this.txtRepeat.Text = "10";
            // 
            // chkEnhanced
            // 
            this.chkEnhanced.AutoSize = true;
            this.chkEnhanced.Location = new System.Drawing.Point(576, 337);
            this.chkEnhanced.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkEnhanced.Name = "chkEnhanced";
            this.chkEnhanced.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkEnhanced.Size = new System.Drawing.Size(225, 29);
            this.chkEnhanced.TabIndex = 13;
            this.chkEnhanced.Text = "Reluctant Learning";
            this.chkEnhanced.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "epochs";
            // 
            // txtEpochs
            // 
            this.txtEpochs.Location = new System.Drawing.Point(702, 237);
            this.txtEpochs.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtEpochs.Name = "txtEpochs";
            this.txtEpochs.Size = new System.Drawing.Size(148, 31);
            this.txtEpochs.TabIndex = 14;
            this.txtEpochs.Text = "40000";
            this.txtEpochs.TextChanged += new System.EventHandler(this.txtEpochs_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTestCount);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtVir);
            this.groupBox1.Controls.Add(this.txtVer);
            this.groupBox1.Controls.Add(this.txtSetPer);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCorrect);
            this.groupBox1.Location = new System.Drawing.Point(58, 238);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(452, 421);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // txtTestCount
            // 
            this.txtTestCount.Location = new System.Drawing.Point(192, 190);
            this.txtTestCount.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtTestCount.Name = "txtTestCount";
            this.txtTestCount.Size = new System.Drawing.Size(148, 31);
            this.txtTestCount.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 144);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(260, 25);
            this.label15.TabIndex = 29;
            this.label15.Text = "Total Epochs in Test Data";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 96);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 25);
            this.label14.TabIndex = 28;
            this.label14.Text = "Test Data";
            // 
            // txtVir
            // 
            this.txtVir.Location = new System.Drawing.Point(192, 358);
            this.txtVir.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtVir.Name = "txtVir";
            this.txtVir.Size = new System.Drawing.Size(148, 31);
            this.txtVir.TabIndex = 27;
            // 
            // txtVer
            // 
            this.txtVer.Location = new System.Drawing.Point(192, 312);
            this.txtVer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtVer.Name = "txtVer";
            this.txtVer.Size = new System.Drawing.Size(148, 31);
            this.txtVer.TabIndex = 26;
            // 
            // txtSetPer
            // 
            this.txtSetPer.Location = new System.Drawing.Point(192, 254);
            this.txtSetPer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSetPer.Name = "txtSetPer";
            this.txtSetPer.Size = new System.Drawing.Size(148, 31);
            this.txtSetPer.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(54, 258);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 25);
            this.label13.TabIndex = 24;
            this.label13.Text = "Iris-setosa";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Lime;
            this.label12.Location = new System.Drawing.Point(54, 317);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 25);
            this.label12.TabIndex = 24;
            this.label12.Text = "Iris-versicolor";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(54, 363);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 25);
            this.label11.TabIndex = 20;
            this.label11.Text = "Iris-virginica";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 46);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 25);
            this.label7.TabIndex = 5;
            this.label7.Text = "Inputs";
            // 
            // txtCorrect
            // 
            this.txtCorrect.Location = new System.Drawing.Point(192, 44);
            this.txtCorrect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCorrect.Name = "txtCorrect";
            this.txtCorrect.Size = new System.Drawing.Size(148, 31);
            this.txtCorrect.TabIndex = 4;
            this.txtCorrect.Text = "0%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(858, 506);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "Iris-setosa";
            // 
            // chkWtoFile
            // 
            this.chkWtoFile.AutoSize = true;
            this.chkWtoFile.Checked = true;
            this.chkWtoFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWtoFile.Location = new System.Drawing.Point(576, 433);
            this.chkWtoFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkWtoFile.Name = "chkWtoFile";
            this.chkWtoFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkWtoFile.Size = new System.Drawing.Size(154, 29);
            this.chkWtoFile.TabIndex = 23;
            this.chkWtoFile.Text = "write to File";
            this.chkWtoFile.UseVisualStyleBackColor = true;
            // 
            // chkDisplay
            // 
            this.chkDisplay.AutoSize = true;
            this.chkDisplay.Checked = true;
            this.chkDisplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplay.Location = new System.Drawing.Point(576, 479);
            this.chkDisplay.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkDisplay.Name = "chkDisplay";
            this.chkDisplay.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDisplay.Size = new System.Drawing.Size(115, 29);
            this.chkDisplay.TabIndex = 24;
            this.chkDisplay.Text = "Display";
            this.chkDisplay.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1582, 21);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(141, 25);
            this.label16.TabIndex = 25;
            this.label16.Text = "Training Data";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(2704, 21);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 25);
            this.label17.TabIndex = 29;
            this.label17.Text = "Test Data";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(858, 479);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 25);
            this.label18.TabIndex = 28;
            this.label18.Text = "Iris-virginica";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Lime;
            this.label19.Location = new System.Drawing.Point(858, 531);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 25);
            this.label19.TabIndex = 27;
            this.label19.Text = "Iris-versicolor";
            // 
            // lbltrainingCount
            // 
            this.lbltrainingCount.AutoSize = true;
            this.lbltrainingCount.Location = new System.Drawing.Point(1582, 1198);
            this.lbltrainingCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbltrainingCount.Name = "lbltrainingCount";
            this.lbltrainingCount.Size = new System.Drawing.Size(82, 25);
            this.lbltrainingCount.TabIndex = 30;
            this.lbltrainingCount.Text = "label21";
            // 
            // lblTestingCount
            // 
            this.lblTestingCount.AutoSize = true;
            this.lblTestingCount.Location = new System.Drawing.Point(2704, 1198);
            this.lblTestingCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTestingCount.Name = "lblTestingCount";
            this.lblTestingCount.Size = new System.Drawing.Size(82, 25);
            this.lblTestingCount.TabIndex = 31;
            this.lblTestingCount.Text = "label21";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(3296, 1473);
            this.Controls.Add(this.lblTestingCount);
            this.Controls.Add(this.lbltrainingCount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.chkDisplay);
            this.Controls.Add(this.chkWtoFile);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEpochs);
            this.Controls.Add(this.chkEnhanced);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRepeat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLearnRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOutputs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHidden);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.LoadArray);
            this.Controls.Add(this.Learn);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Learn;
        private System.Windows.Forms.Button LoadArray;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHidden;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLearnRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.CheckBox chkEnhanced;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEpochs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCorrect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkWtoFile;
        private System.Windows.Forms.TextBox txtVir;
        private System.Windows.Forms.TextBox txtVer;
        private System.Windows.Forms.TextBox txtSetPer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTestCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkDisplay;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbltrainingCount;
        private System.Windows.Forms.Label lblTestingCount;
    }
}


namespace ArduinoSerialTest
{
    partial class ArduinoForm
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
            this.connectBtn = new System.Windows.Forms.Button();
            this.comPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.baudRate = new System.Windows.Forms.ComboBox();
            this.portsRefresh = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button9 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxADCgain = new System.Windows.Forms.ComboBox();
            this.comboBoxADCgainCH = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.comboBoxRes = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelSpeed = new System.Windows.Forms.Label();
            this.numericUpDownBlockSize = new System.Windows.Forms.NumericUpDown();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.buttonBlockSize = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).BeginInit();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(654, 7);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(116, 37);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(91, 16);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(121, 21);
            this.comPort.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Boud Rate";
            // 
            // baudRate
            // 
            this.baudRate.FormattingEnabled = true;
            this.baudRate.Items.AddRange(new object[] {
            "115200",
            "1000000",
            "19200",
            "9600"});
            this.baudRate.Location = new System.Drawing.Point(444, 16);
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(133, 21);
            this.baudRate.TabIndex = 4;
            // 
            // portsRefresh
            // 
            this.portsRefresh.Location = new System.Drawing.Point(218, 16);
            this.portsRefresh.Name = "portsRefresh";
            this.portsRefresh.Size = new System.Drawing.Size(61, 23);
            this.portsRefresh.TabIndex = 12;
            this.portsRefresh.Text = "Refresh";
            this.portsRefresh.UseVisualStyleBackColor = true;
            this.portsRefresh.Click += new System.EventHandler(this.portsRefresh_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 95);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(250, 434);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 31);
            this.label3.TabIndex = 14;
            this.label3.Text = "Log";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(278, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "SEND";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Začni vzorčenje";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Prekini vzorčenje";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(275, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Nastavi periodo vzorčenja";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Nastavi število vzorcev za povprečenje";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(275, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Nastavi zaporedje kanalov";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(275, 423);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Nastavi vrednost analognega izhoda";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 480);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Nastavi način";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(278, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "SEND";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(278, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "SEND";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(278, 269);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 25;
            this.button4.Text = "SEND";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(278, 324);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "SEND";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(278, 378);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 27;
            this.button6.Text = "SEND";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(278, 439);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 28;
            this.button7.Text = "SEND";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(468, 214);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "uS";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(602, 441);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "mV";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(362, 212);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown1.TabIndex = 34;
            this.numericUpDown1.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(362, 269);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown2.TabIndex = 35;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(362, 324);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown3.TabIndex = 36;
            this.numericUpDown3.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown5.Location = new System.Drawing.Point(496, 439);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown5.TabIndex = 38;
            this.numericUpDown5.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(275, 308);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(168, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Nastavi število zaporednih meritev";
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(278, 496);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 40;
            this.button8.Text = "SEND";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1, 2, 3, 4",
            "1, 2, 4, 3",
            "1, 3, 2, 4",
            "1, 3, 4, 2",
            "1, 4, 2, 3",
            "1, 4, 3, 2",
            "2, 1, 3, 4",
            "2, 1, 4, 3",
            "2, 3, 1, 4",
            "2, 3, 4, 1",
            "2, 4, 1, 3",
            "2, 4, 3, 1",
            "3, 1, 2, 4",
            "3, 1, 4, 2",
            "3, 2, 1, 4",
            "3, 2, 4, 1",
            "3, 4, 1, 2",
            "3, 4, 2, 1",
            "4, 1, 2, 3",
            "4, 1, 3, 2",
            "4, 2, 1, 3",
            "4, 2, 3, 1",
            "4, 3, 1, 2",
            "4, 3, 2, 1",
            "1, 2, 3, 0",
            "1, 3, 2, 0",
            "2, 1, 3, 0",
            "2, 3, 1, 0",
            "3, 1, 2, 0",
            "3, 2, 1, 0",
            "1, 2, 4, 0",
            "1, 4, 2, 0",
            "2, 1, 4, 0",
            "2, 4, 1, 0",
            "4, 1, 2, 0",
            "4, 2, 1, 0",
            "1, 3, 4, 0",
            "1, 4, 3, 0",
            "3, 1, 4, 0",
            "3, 4, 1, 0",
            "4, 1, 3, 0",
            "4, 3, 1, 0",
            "2, 3, 4, 0",
            "2, 4, 3, 0",
            "3, 2, 4, 0",
            "3, 4, 2, 0",
            "4, 2, 3, 0",
            "4, 3, 2, 0",
            "1, 2, 0, 0",
            "2, 1, 0, 0",
            "1, 3, 0, 0",
            "3, 1, 0, 0",
            "1, 4, 0, 0",
            "4, 1, 0, 0",
            "2, 3, 0, 0",
            "3, 2, 0, 0",
            "2, 4, 0, 0",
            "4, 2, 0, 0",
            "3, 4, 0, 0",
            "4, 3, 0, 0",
            "1, 0, 0, 0",
            "2, 0, 0, 0",
            "3, 0, 0, 0",
            "4, 0, 0, 0 "});
            this.comboBox1.Location = new System.Drawing.Point(362, 380);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 41;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(362, 439);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown4.TabIndex = 42;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(468, 441);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "CH";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ASCII",
            "BIN"});
            this.comboBox2.Location = new System.Drawing.Point(362, 498);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 21);
            this.comboBox2.TabIndex = 45;
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(496, 111);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 46;
            this.button9.Text = "SEND";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(493, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "Nastavi ADC gain";
            // 
            // comboBoxADCgain
            // 
            this.comboBoxADCgain.FormattingEnabled = true;
            this.comboBoxADCgain.Items.AddRange(new object[] {
            "0,5",
            "1",
            "2"});
            this.comboBoxADCgain.Location = new System.Drawing.Point(633, 111);
            this.comboBoxADCgain.Name = "comboBoxADCgain";
            this.comboBoxADCgain.Size = new System.Drawing.Size(50, 21);
            this.comboBoxADCgain.TabIndex = 48;
            // 
            // comboBoxADCgainCH
            // 
            this.comboBoxADCgainCH.FormattingEnabled = true;
            this.comboBoxADCgainCH.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBoxADCgainCH.Location = new System.Drawing.Point(577, 111);
            this.comboBoxADCgainCH.Name = "comboBoxADCgainCH";
            this.comboBoxADCgainCH.Size = new System.Drawing.Size(50, 21);
            this.comboBoxADCgainCH.TabIndex = 49;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(602, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "CH";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(651, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 13);
            this.label17.TabIndex = 51;
            this.label17.Text = "Gain";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(493, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 13);
            this.label18.TabIndex = 52;
            this.label18.Text = "Nastavi ADC res.";
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(496, 161);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 53;
            this.button10.Text = "SEND";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // comboBoxRes
            // 
            this.comboBoxRes.FormattingEnabled = true;
            this.comboBoxRes.Items.AddRange(new object[] {
            "12 bits",
            "10 bits"});
            this.comboBoxRes.Location = new System.Drawing.Point(577, 161);
            this.comboBoxRes.Name = "comboBoxRes";
            this.comboBoxRes.Size = new System.Drawing.Size(106, 21);
            this.comboBoxRes.TabIndex = 54;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(122, 64);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelSpeed.TabIndex = 55;
            this.labelSpeed.Text = "label19";
            // 
            // numericUpDownBlockSize
            // 
            this.numericUpDownBlockSize.Location = new System.Drawing.Point(577, 212);
            this.numericUpDownBlockSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownBlockSize.Name = "numericUpDownBlockSize";
            this.numericUpDownBlockSize.Size = new System.Drawing.Size(106, 20);
            this.numericUpDownBlockSize.TabIndex = 56;
            this.numericUpDownBlockSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Location = new System.Drawing.Point(187, 64);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(41, 13);
            this.labelSampleRate.TabIndex = 57;
            this.labelSampleRate.Text = "label19";
            // 
            // buttonBlockSize
            // 
            this.buttonBlockSize.Enabled = false;
            this.buttonBlockSize.Location = new System.Drawing.Point(496, 209);
            this.buttonBlockSize.Name = "buttonBlockSize";
            this.buttonBlockSize.Size = new System.Drawing.Size(75, 23);
            this.buttonBlockSize.TabIndex = 58;
            this.buttonBlockSize.Text = "SEND";
            this.buttonBlockSize.UseVisualStyleBackColor = true;
            this.buttonBlockSize.Click += new System.EventHandler(this.buttonBlockSize_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(493, 193);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(93, 13);
            this.label19.TabIndex = 59;
            this.label19.Text = "Nastavi block size";
            // 
            // ArduinoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 542);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.buttonBlockSize);
            this.Controls.Add(this.labelSampleRate);
            this.Controls.Add(this.numericUpDownBlockSize);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.comboBoxRes);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.comboBoxADCgainCH);
            this.Controls.Add(this.comboBoxADCgain);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.portsRefresh);
            this.Controls.Add(this.baudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.connectBtn);
            this.Name = "ArduinoForm";
            this.Text = "Arduino Test";
            this.Load += new System.EventHandler(this.ArduinoForm_Load);
            this.Leave += new System.EventHandler(this.ArduinoForm_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.ComboBox comPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox baudRate;
        private System.Windows.Forms.Button portsRefresh;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxADCgain;
        private System.Windows.Forms.ComboBox comboBoxADCgainCH;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ComboBox comboBoxRes;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownBlockSize;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Button buttonBlockSize;
        private System.Windows.Forms.Label label19;
    }
}


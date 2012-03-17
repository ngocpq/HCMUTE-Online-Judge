namespace BienDich_Cham
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
        this.button1 = new System.Windows.Forms.Button();
        this.rtxtSourceCode = new System.Windows.Forms.RichTextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.cboLanguage = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.rtxtKetQua = new System.Windows.Forms.RichTextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.btnBienDich = new System.Windows.Forms.Button();
        this.btnChamDiem = new System.Windows.Forms.Button();
        this.lblExe = new System.Windows.Forms.Label();
        this.txtExePath = new System.Windows.Forms.TextBox();
        this.btnBrowse = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.txtFileCham = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.btnBrowseCode = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(69, 12);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(126, 56);
        this.button1.TabIndex = 0;
        this.button1.Text = "button1";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // rtxtSourceCode
        // 
        this.rtxtSourceCode.Location = new System.Drawing.Point(12, 105);
        this.rtxtSourceCode.Name = "rtxtSourceCode";
        this.rtxtSourceCode.Size = new System.Drawing.Size(565, 38);
        this.rtxtSourceCode.TabIndex = 1;
        this.rtxtSourceCode.Text = "";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 89);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(88, 13);
        this.label1.TabIndex = 2;
        this.label1.Text = "File Source Code";
        // 
        // cboLanguage
        // 
        this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboLanguage.FormattingEnabled = true;
        this.cboLanguage.Items.AddRange(new object[] {
            "CPP",
            "C++"});
        this.cboLanguage.Location = new System.Drawing.Point(217, 78);
        this.cboLanguage.Name = "cboLanguage";
        this.cboLanguage.Size = new System.Drawing.Size(121, 21);
        this.cboLanguage.TabIndex = 3;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(214, 62);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(56, 13);
        this.label2.TabIndex = 2;
        this.label2.Text = "Ngon Ngu";
        // 
        // rtxtKetQua
        // 
        this.rtxtKetQua.Location = new System.Drawing.Point(12, 421);
        this.rtxtKetQua.Name = "rtxtKetQua";
        this.rtxtKetQua.Size = new System.Drawing.Size(402, 211);
        this.rtxtKetQua.TabIndex = 4;
        this.rtxtKetQua.Text = "";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(12, 405);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(46, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Ket Qua";
        // 
        // btnBienDich
        // 
        this.btnBienDich.Location = new System.Drawing.Point(583, 190);
        this.btnBienDich.Name = "btnBienDich";
        this.btnBienDich.Size = new System.Drawing.Size(75, 63);
        this.btnBienDich.TabIndex = 5;
        this.btnBienDich.Text = "Bien Dich";
        this.btnBienDich.UseVisualStyleBackColor = true;
        this.btnBienDich.Click += new System.EventHandler(this.btnBienDich_Click);
        // 
        // btnChamDiem
        // 
        this.btnChamDiem.Location = new System.Drawing.Point(559, 489);
        this.btnChamDiem.Name = "btnChamDiem";
        this.btnChamDiem.Size = new System.Drawing.Size(75, 23);
        this.btnChamDiem.TabIndex = 5;
        this.btnChamDiem.Text = "Cham Diem";
        this.btnChamDiem.UseVisualStyleBackColor = true;
        this.btnChamDiem.Click += new System.EventHandler(this.btnChamDiem_Click);
        // 
        // lblExe
        // 
        this.lblExe.AutoSize = true;
        this.lblExe.Location = new System.Drawing.Point(254, 33);
        this.lblExe.Name = "lblExe";
        this.lblExe.Size = new System.Drawing.Size(64, 13);
        this.lblExe.TabIndex = 6;
        this.lblExe.Text = "Compile Out";
        // 
        // txtExePath
        // 
        this.txtExePath.Location = new System.Drawing.Point(324, 30);
        this.txtExePath.Name = "txtExePath";
        this.txtExePath.Size = new System.Drawing.Size(310, 20);
        this.txtExePath.TabIndex = 7;
        // 
        // btnBrowse
        // 
        this.btnBrowse.Location = new System.Drawing.Point(640, 27);
        this.btnBrowse.Name = "btnBrowse";
        this.btnBrowse.Size = new System.Drawing.Size(75, 23);
        this.btnBrowse.TabIndex = 8;
        this.btnBrowse.Text = "Browse";
        this.btnBrowse.UseVisualStyleBackColor = true;
        this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(424, 435);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(53, 13);
        this.label4.TabIndex = 6;
        this.label4.Text = "File Chấm";
        // 
        // txtFileCham
        // 
        this.txtFileCham.Location = new System.Drawing.Point(420, 451);
        this.txtFileCham.Name = "txtFileCham";
        this.txtFileCham.Size = new System.Drawing.Size(310, 20);
        this.txtFileCham.TabIndex = 7;
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(743, 452);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 9;
        this.button2.Text = "Browse";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // btnBrowseCode
        // 
        this.btnBrowseCode.Location = new System.Drawing.Point(590, 101);
        this.btnBrowseCode.Name = "btnBrowseCode";
        this.btnBrowseCode.Size = new System.Drawing.Size(100, 53);
        this.btnBrowseCode.TabIndex = 10;
        this.btnBrowseCode.Text = "Browse";
        this.btnBrowseCode.UseVisualStyleBackColor = true;
        this.btnBrowseCode.Click += new System.EventHandler(this.btnBrowseCode_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(861, 652);
        this.Controls.Add(this.btnBrowseCode);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.btnBrowse);
        this.Controls.Add(this.txtFileCham);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.txtExePath);
        this.Controls.Add(this.lblExe);
        this.Controls.Add(this.btnChamDiem);
        this.Controls.Add(this.btnBienDich);
        this.Controls.Add(this.rtxtKetQua);
        this.Controls.Add(this.cboLanguage);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.rtxtSourceCode);
        this.Controls.Add(this.button1);
        this.Name = "Form1";
        this.Text = "Form1";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.RichTextBox rtxtSourceCode;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboLanguage;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RichTextBox rtxtKetQua;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnBienDich;
    private System.Windows.Forms.Button btnChamDiem;
    private System.Windows.Forms.Label lblExe;
    private System.Windows.Forms.TextBox txtExePath;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtFileCham;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button btnBrowseCode;
  }
}


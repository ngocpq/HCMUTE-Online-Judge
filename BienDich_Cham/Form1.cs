using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChamDiem;
using ChamDiem.SoSanh;

namespace BienDich_Cham
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      ChamDiem.BienDichVaCham chamDiem = new ChamDiem.BienDichVaCham();
      InputThiSinh thiSinh = new InputThiSinh();
      thiSinh.MaThiSinh = "dlfl";
      thiSinh.TestCases = new List<ChamDiem.ITestCase>();
      //...............
      chamDiem.OnChamError += new ChamDiem.ChamError(chamDiem_OnChamError);
      chamDiem.OnChamComplete += new ChamDiem.ChamComplete(chamDiem_OnChamComplete);
      ChamDiem.KetQuaThiSinh kq= chamDiem.ChamBaiAsysn(new InputThiSinh());
      
    }

    void chamDiem_OnChamComplete(object sender, ChamDiem.KetQuaThiSinh kq)
    {
        throw new NotImplementedException();
    }

    void chamDiem_OnChamError(object sender, ChamDiem.KetQuaThiSinh kq)
    {
        throw new NotImplementedException();
    }

    private void btnBienDich_Click(object sender, EventArgs e)
    {
        IBienDich bienDich = (new BienDichFactory()).GetBienDichObjectByNgonNgu(cboLanguage.Text);
        KetQuaBienDich kq = bienDich.BienDich(rtxtSourceCode.Text, txtExePath.Text);
        rtxtKetQua.Text = String.Format("Bien dich thanh cong: {0}\nMessage: {1}", kq.BienDichThanhCong, kq.Message);
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        SaveFileDialog dlg = new SaveFileDialog();
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            txtExePath.Text = dlg.FileName;
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        cboLanguage.SelectedIndex = 0;
    }

    private void btnChamDiem_Click(object sender, EventArgs e)
    {
        IChamDiem chamDiem = new ChamEXE();
        List<ITestCase> tescase = new List<ITestCase>();
        //Tinh tong 2 so nguyen
        tescase.Add(new TestCase("1 1\n", "2", 1000));
        tescase.Add(new TestCase("2000 300\n", "2300", 1000));
        tescase.Add(new TestCase("421 234\n", "655", 1000));
        tescase.Add(new TestCase("4 1\n", "5", 1000));
        tescase.Add(new TestCase("3000 1\n", "3001", 1000));
        tescase.Add(new TestCase("5002 1000\n", "6102", 1000));
        tescase.Add(new TestCase("11 12\n", "23", 1000));

        IFileComparer ss = new SoSanhSoNguyen(); //SoSanhExternal();
        ss.Init(txtFileCham.Text);
        KetQuaCham kq = chamDiem.Cham(txtExePath.Text, tescase, ss);
        rtxtKetQua.Text = String.Format("Tổng điểm: {0}", kq.TongDiem) + Environment.NewLine;
        foreach (KetQuaTestCase tc in kq.KetQuaTestCases)
        {
            rtxtKetQua.Text += String.Format("Test1: Kết quả {0}, Thông điệp {1}", tc.KetQua, tc.ThongDiep) + Environment.NewLine;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "Exe file|*.exe";
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            txtFileCham.Text = dlg.FileName;
        }
    }

    private void btnBrowseCode_Click(object sender, EventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "CPP file|*.cpp|C File|*.c";
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            rtxtSourceCode.Text = dlg.FileName;
        }   
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;
using ChamDiem;
//using DevExpress.Web.Mvc;
//using DevExpress.Web.ASPxHtmlEditor;
using System.Web.UI.WebControls;
//using DevExpress.Web.ASPxFileManager;
//using DevExpress.Web.ASPxUploadControl;
//using FredCK.FCKeditorV2;
namespace SPKTOnline.Controllers
{
    public class Student_SubmitController : Controller
    {
        //
        // GET: /Student_Submit/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        CheckRoles checkRole = new CheckRoles();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult TryTest(int ID)
        {
            Problem p = db.Problems.FirstOrDefault(m => m.ID == ID);
            Student_Submit st = new Student_Submit();
            st.Problem = p;
            return View(st);
        }

        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Lecturer,Student")]
        public ActionResult TryTest(Student_Submit st)
        {

            st.StudentID = User.Identity.Name;
            st.TrangThaiBienDich = 0;
            st.TrangThaiCham = (int)TrangThaiCham.ChuaCham;
            st.LanguageID = 1;
            st.SubmitTime = DateTime.Now;
            db.Student_Submit.AddObject(st);

            LogUtility.WriteDebug("Bat dau save");
            db.SaveChanges();
            LogUtility.WriteDebug("Save lan 1");
            ChamDiemServise chamThiService = new ChamDiemServise();
            //Chay va doi
            //
            st.TrangThaiCham = (int)TrangThaiCham.DangCham;
            LogUtility.WriteDebug("Save lan 2");
            db.SaveChanges();
            LogUtility.WriteDebug("Bat dau cham");
            KetQuaThiSinh kq = chamThiService.ChamBai(st.ProblemID, st.SourceCode, st.Language.Name);
            LogUtility.WriteDebug("Cham xong");
            kq.SubmitID = st.ID;
            chamThiService_ChamThiCompleted(null, kq);
            //
            //Chay ko doi
            //chamThiService.ChamThiCompleted += new ChamThiServiceEventHandler(chamThiService_ChamThiCompleted);
            //chamThiService.ChamBaiThread(st);
            //st.TrangThaiCham = (int)TrangThaiCham.DangCham;
            //db.SaveChanges();
            return RedirectToAction("TryTestResult", "Result", new { ID = st.ID, Message = "Bạn đã gửi bài làm thành công" });//trả ra thông tin ở trang kết quả.
        }

        void chamThiService_ChamThiCompleted(object sender, KetQuaThiSinh kq)
        {

            Student_Submit st = db.Student_Submit.FirstOrDefault(t => t.ID == kq.SubmitID);
            st.TrangThaiCham = (int)TrangThaiCham.DaCham;
            st.TrangThaiBienDich = kq.KetQuaBienDich.BienDichThanhCong ? 1 : 0;
            if (kq.KetQuaBienDich.BienDichThanhCong)
            {
                LogUtility.WriteDebug("Bien dich thanh cong");
                foreach (var rs in kq.KetQuaCham.KetQuaTestCases)
                {
                    TestCas tc = ((TestCas)rs.TestCase);
                    TestCaseResult tcResult = new TestCaseResult();
                    tcResult.TestCaseID = tc.MaTestCase;
                    tcResult.StudentSubmitID = st.ID;
                    tcResult.Score = rs.KetQua == KetQuaTestCase.LoaiKetQua.Dung ? (tc.Diem * tc.Problem.Score) / 100 : 0;
                    tcResult.Comment = rs.ThongDiep;
                    tcResult.ExecutionTime = (int)rs.ThoiGianChay;                    
                    //TODO: Them Error
                    //tcResult.Error = rs.Error;
                    db.TestCaseResults.AddObject(tcResult);
                }
            }
            else
            {
                LogUtility.WriteDebug("Bien dich that bai");
                st.CompilerError = kq.KetQuaBienDich.Message;
            }
            LogUtility.WriteDebug("Luu ket qua");
            db.SaveChanges();
        }
        public ActionResult Htmlpartial()
        {
            return PartialView("Htmlpartial");
        }
        //public ActionResult CustomCssImageUpload()
        //{
        //    HtmlEditorExtension.SaveUploadedImage("heCustomCss", HtmlEditorDemosHelper.ImageUploadValidationSettings, HtmlEditorDemosHelper.UploadDirectory);
        //    return null;
        //}


        //public class HtmlEditorModel
        //{
        //    public HtmlEditorModel(string html) : this(html, null) { }
        //    public HtmlEditorModel(string html, IEnumerable<string> cssFiles)
        //    {
        //        Html = html;
        //        CssFiles = cssFiles;
        //    }

        //    public string Html { get; set; }
        //    public IEnumerable<string> CssFiles { get; set; }
        //}

        //public class HtmlEditorDemosHelper
        //{
        //    public const string ImagesDirectory = "~/Content/HtmlEditor/Images/";
        //    public const string ThumbnailsDirectory = "~/Content/HtmlEditor/Thumbnails/";
        //    public const string UploadDirectory = ImagesDirectory + "Upload/";
        //    public const string ImportContentDirectory = "~/Content/HtmlEditor/Imported";

        //    public static readonly ValidationSettings ImageUploadValidationSettings 
        //        = new ValidationSettings
        //    {
        //        AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" },
        //        MaxFileSize = 4000000
        //    };


        //    static HtmlEditorValidationSettings validation;
        //    public static HtmlEditorValidationSettings ValidationSettings
        //    {
        //        get
        //        {
        //            if (validation == null)
        //            {
        //                validation = new HtmlEditorValidationSettings();
        //                validation.RequiredField.IsRequired = true;
        //            }
        //            return validation;
        //        }
        //    }

        //    static MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        //    public static HtmlEditorImageSelectorSettings ImageSelectorSettings
        //    {
        //        get
        //        {
        //            if (imageSelectorSettings == null)
        //            {
        //                imageSelectorSettings = new MVCxHtmlEditorImageSelectorSettings(null);
        //                HtmlEditorDemosHelper.SetHtmlEditorImageSelectorSettings(imageSelectorSettings);
        //            }
        //            return imageSelectorSettings;
        //        }
        //    }

        //    public static void OnValidation(object sender, HtmlEditorValidationEventArgs e)
        //    {
        //        const int MaxLength = 50;
        //        string CustomErrorText = string.Format("Custom validation fails because the HTML content&prime;s length exceeds {0} characters.", MaxLength);

        //        if (e.Html.Length > MaxLength)
        //        {
        //            e.IsValid = false;
        //            e.ErrorText = CustomErrorText;
        //        }
        //    }

        //    public static MVCxHtmlEditorImageSelectorSettings SetHtmlEditorImageSelectorSettings(MVCxHtmlEditorImageSelectorSettings settingsImageSelector)
        //    {
        //        settingsImageSelector.UploadCallbackRouteValues = new { Controller = "HtmlEditor", Action = "FeaturesImageSelectorUpload" };

        //        settingsImageSelector.Enabled = true;
        //        settingsImageSelector.CommonSettings.RootFolder = HtmlEditorDemosHelper.ImagesDirectory;
        //        settingsImageSelector.CommonSettings.ThumbnailFolder = HtmlEditorDemosHelper.ThumbnailsDirectory;
        //        settingsImageSelector.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" };
        //        settingsImageSelector.EditingSettings.AllowCreate = true;
        //        settingsImageSelector.EditingSettings.AllowDelete = true;
        //        settingsImageSelector.EditingSettings.AllowMove = true;
        //        settingsImageSelector.EditingSettings.AllowRename = true;
        //        settingsImageSelector.UploadSettings.Enabled = true;
        //        settingsImageSelector.FoldersSettings.ShowLockedFolderIcons = true;

        //        settingsImageSelector.PermissionSettings.AccessRules.Add(
        //            new FileManagerFolderAccessRule
        //            {
        //                Path = "",
        //                Upload = Rights.Deny
        //            },
        //            new FileManagerFolderAccessRule
        //            {
        //                Path = "Upload",
        //                Upload = Rights.Allow
        //            });
        //        return settingsImageSelector;
        //    }

        //    public static HtmlEditorSettings SetHtmlEditorExportSettings(HtmlEditorSettings settings)
        //    {
        //        settings.Name = "heImportExport";
        //        settings.CallbackRouteValues = new { Controller = "HtmlEditor", Action = "ImportExportPartial" };
        //        settings.ExportRouteValues = new { Controller = "HtmlEditor", Action = "ExportTo" };
        //        settings.Width = Unit.Percentage(100);

        //        var toolbar = settings.Toolbars.Add();
        //        toolbar.Items.Add(new ToolbarUndoButton());
        //        toolbar.Items.Add(new ToolbarRedoButton());
        //        toolbar.Items.Add(new ToolbarBoldButton(true));
        //        toolbar.Items.Add(new ToolbarItalicButton());
        //        toolbar.Items.Add(new ToolbarUnderlineButton());
        //        toolbar.Items.Add(new ToolbarStrikethroughButton());
        //        toolbar.Items.Add(new ToolbarInsertImageDialogButton(true));
        //        ToolbarExportDropDownButton saveButton = new ToolbarExportDropDownButton(true);
        //        saveButton.CreateDefaultItems();
        //        toolbar.Items.Add(saveButton);

        //        settings.CssFiles.Add("~/Content/HtmlEditor/DemoCss/Export.css");

        //        return settings;
        //    }
        //}

        //public class HtmlEditorFeaturesDemoOptions
        //{
        //    public HtmlEditorFeaturesDemoOptions()
        //    {
        //        UpdateDeprecatedElements = true;
        //        UpdateBoldItalic = true;
        //        EnterMode = HtmlEditorEnterMode.Default;
        //        //AllowContextMenu = DefaultBoolean.True;
        //        AllowDesignView = true;
        //        AllowHtmlView = true;
        //        AllowPreview = true;
        //    }

        //    public bool AllowScripts { get; set; }
        //    public bool AllowIFrames { get; set; }
        //    public bool AllowFormElements { get; set; }
        //    public bool UpdateDeprecatedElements { get; set; }
        //    public bool UpdateBoldItalic { get; set; }
        //    public HtmlEditorEnterMode EnterMode { get; set; }
        //  //  public DefaultBoolean AllowContextMenu { get; set; }
        //    public bool AllowDesignView { get; set; }
        //    public bool AllowHtmlView { get; set; }

        //}
    }
}

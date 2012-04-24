// Developer Express Code Central Example:
// How to export HtmlEditor content via the built-in toolbar button and via the custom action button
// 
// This example is standalone implementation of the online HTML Editor -
// Import/Export (http://demos.devexpress.com/MVC/HtmlEditor/ImportExport)
// demo.
// It illustrates how to export the HtmlEditor's content to several rich
// text formats via a built-in toolbar item and custom Controller Action.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3584

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Data.Linq;
using DevExpress.Web.Mvc;
using System.Data.Objects;
using DevExpress.Web.ASPxHtmlEditor;
using SPKTOnline.Models;
using System.Web;

namespace SPKTOnline.Controllers
{
    public  class HtmlEditorController : Controller {
        const string SampleDocumentPath = "~/Content/HtmlEditor/SampleImportDocument.rtf";

        public ActionResult ImportExport() {
            ViewData["SampleDocumentPath"] = SampleDocumentPath;
            string htmlContentPath = Server.MapPath("~/Views/Home/ShowFile.cshtml");
            string html = System.IO.File.ReadAllText(htmlContentPath);
            return View("ImportExport", new HtmlEditorModel(html));

          }

        public ActionResult ImportExportPartial() {
            return PartialView("ImportExportPartial");
        }

        public ActionResult ExportTo(HtmlEditorExportFormat format) {
            return HtmlEditorExtension.Export(
                HtmlEditorDemosHelper.SetHtmlEditorExportSettings(new HtmlEditorSettings()),
                format
            );
        }

        /**/
        [HttpPost]
        public ActionResult ExportContentTo() {
            HtmlEditorExportFormat format = EditorExtension.GetValue<HtmlEditorExportFormat>("cmbFormat");
            
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            HtmlEditorExtension.Export(HtmlEditorDemosHelper.SetHtmlEditorExportSettings(new HtmlEditorSettings()), stream, format);

            string fileFormat = format.ToString().ToLower().TrimStart('.');

            FileStreamResult result = new FileStreamResult(stream, "application/" + fileFormat);
            result.FileDownloadName = string.Format("{0}.{1}", DateTime.Now.ToShortDateString().Replace(".", "_"), fileFormat);
            result.FileStream.Position = 0;
            
            return result;
        }
        /**/

        public ActionResult ImportSampleDocument() {
            ViewData["SampleDocumentPath"] = SampleDocumentPath;
            HtmlEditorModel model = null;
            HtmlEditorExtension.Import(
                "~/Content/HtmlEditor/SampleImportDocument.rtf",
                HtmlEditorDemosHelper.ImportContentDirectory,
                (html, cssFiles) => model = new HtmlEditorModel(html, cssFiles)
            );
            return View("ImportExport", model);
        }
    }

    public class HtmlEditorModel {
        public HtmlEditorModel(string html) : this(html, null) { }
        public HtmlEditorModel(string html, IEnumerable<string> cssFiles) {
            Html = html;
            CssFiles = cssFiles;
        }

        public string Html { get; set; }
        public IEnumerable<string> CssFiles { get; set; }
    }

    public class HtmlEditorDemosHelper {
        public const string ImportContentDirectory = "~/Content/HtmlEditor/Imported";

        public static HtmlEditorSettings SetHtmlEditorExportSettings(HtmlEditorSettings settings) {
            settings.Name = "heImportExport";
            settings.CallbackRouteValues = new { Controller = "HtmlEditor", Action = "ImportExportPartial" };
            settings.ExportRouteValues = new { Controller = "HtmlEditor", Action = "ExportTo" };

            var toolbar = settings.Toolbars.Add();
            toolbar.Items.Add(new ToolbarUndoButton());
            toolbar.Items.Add(new ToolbarRedoButton());
            toolbar.Items.Add(new ToolbarBoldButton(true));
            toolbar.Items.Add(new ToolbarItalicButton());
            toolbar.Items.Add(new ToolbarUnderlineButton());
            toolbar.Items.Add(new ToolbarStrikethroughButton());
            toolbar.Items.Add(new ToolbarInsertImageDialogButton(true));
            ToolbarExportDropDownButton saveButton = new ToolbarExportDropDownButton(true);
            saveButton.CreateDefaultItems();
            toolbar.Items.Add(saveButton);

            settings.PreRender = (sender, e) => {
                ASPxHtmlEditor editor = (ASPxHtmlEditor)sender;
                editor.Width = Unit.Percentage(100);
            };

            return settings;
        }

    }
}

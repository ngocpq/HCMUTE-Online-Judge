using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SPKTOnline.Reponsitories;
using SPKTOnline.Management;
using System.Xml.Linq;
using System.Xml;

namespace SPKTOnline.Controllers
{
    public class UploadProblemController : Controller
    {
        //
        // GET: /UploadProblem/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        ProblemRepository ProblemRep = new ProblemRepository();
        CheckRoles checkRole = new CheckRoles();

        [Authorize(Roles = "Admin,Lecturer")]
        public ActionResult Upload(int? ContestID, int ClassID = 0)
        {
            //khi tạo problem từ contest thì ta có ContestID!=0 và ClassID!=0, if ContestID!=0 và ClassID==0 thì cho ClassID=Contest.ClassID
            //khi tạo problem từ Class thì contestID =0, ClassID !=0
            //Khi tạo problem từ Bên ngoài. thì ContestID=null, ClassID=0
            Problem pro = new Problem();
            
            if (ClassID != 0)
            {
                if (ContestID == 0)
                {
                    Class cl = db.Classes.FirstOrDefault(c => c.ID == ClassID);
                    ViewBag.ClassID = new MultiSelectList(db.Classes.Where(c => c.LecturerID == User.Identity.Name), "ID", "ID", new String[] { "Mã lớp: " + ClassID.ToString()+" Môn: "+cl.Subject.Name+" Nhóm: "+cl.Group+" "+cl.Term});
                    pro.Classes.Add(db.Classes.FirstOrDefault(c => c.ID == ClassID));
                    pro.SubjectID = db.Classes.FirstOrDefault(c => c.ID == ClassID).SubjectID;
                }
                else
                {
                    pro.Classes.Add(db.Classes.FirstOrDefault(c => c.ID == ClassID));
                    pro.SubjectID = db.Classes.FirstOrDefault(c => c.ID == ClassID).SubjectID;
                }
                pro.ContestID = ContestID;
            }
            else
            {
                ViewBag.ClassID = new MultiSelectList(db.Classes.Where(c => c.LecturerID == User.Identity.Name), "ID", "Name");
                ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(User.Identity.Name), "ID", "Name", ProblemRep.GetListSubjectByLecturerID(User.Identity.Name).First());
            }
            ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name",db.Difficulties.First());

            ViewBag.ComparerID = new SelectList(db.Comparers, "ID", "Name", db.Comparers.First());
            pro.AvailableTime = DateTime.Now;
            pro.Score = 10;

            return View(pro);
            //}

        }
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult Upload(Problem problem, HttpPostedFileBase filebase)
        {
            if (filebase != null)
            {
                if (filebase.ContentLength > 0)
                {
                    //TODO: đọc từ file cấu hình
                    problem.LecturerID = User.Identity.Name;
                    problem.Name = Path.GetFileNameWithoutExtension(filebase.FileName);
                    
                    problem.MemoryLimit = 1000;
                    problem.TimeLimit = 1000;
                    
                    if (problem.ContestID == 0)
                    {
                        problem.ContestID = null;
                    }
                    db.Problems.AddObject(problem);
                    try
                    {
                        Unzipfile(problem, filebase.InputStream);
                        if (problem.ClassID != null)
                        {
                            foreach (String s in problem.ClassID)
                            {
                                foreach (Class c in db.Classes)
                                {
                                    if (c.ID == int.Parse(s))
                                    {
                                        problem.Classes.Add(c);
                                        problem.SubjectID = c.SubjectID;
                                    }
                                }
                            }
                        }
                        db.SaveChanges();
                        if ((problem.ContestID == null || problem.ContestID == 0) && problem.Classes.Count() == 0)
                            return RedirectToAction("Browse", "Problem", new { ID = problem.SubjectID });
                        if (problem.Classes.Count() > 0)
                            return RedirectToAction("ClassDetailOfLecturer", "Class", new { ID = problem.Classes.Last().ID });
                        else
                            return RedirectToAction("CreateContestCont", "Contest", new { ContestID = problem.ContestID });//TODO:Sua EXAM
                    }
                    catch (Exception ex)
                    {                        
                        //throw (ex);
                        LogUtility.WriteLog(ex);
                        ViewBag.ErrorMessage = "Có lỗi trong khi upload đề bài";
                    }

                }

            }

            //TODO: redirect show problem
            ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(User.Identity.Name), "ID", "Name", problem.SubjectID);
            //ViewBag.Message = "Thông tin không đúng";
            return View(problem);
        }

        public string Unzipfile(Problem problem, Stream inputStream)
        {
            Dictionary<string, TestCas> dTestcase = new Dictionary<string, TestCas>();
            ZipInputStream s = new ZipInputStream(inputStream);
            ZipEntry ZEntry;
            string sFileName = "";
            String configXML = null;
            while ((ZEntry = s.GetNextEntry()) != null)
            {
                string directoryName = "";
                directoryName = Path.GetDirectoryName(ZEntry.Name);

                if (directoryName != "" && directoryName != null && !dTestcase.ContainsKey(directoryName))
                {
                    TestCas tcTemp = new TestCas();
                    tcTemp.MaDB = problem.ID;
                    dTestcase.Add(directoryName, tcTemp);
                    db.TestCases.AddObject(tcTemp);
                    problem.TestCases.Add(tcTemp);
                }

                sFileName = Path.GetFileName(ZEntry.Name);
                if (sFileName == String.Empty)
                    continue;
                #region Doc file entry
                int size = 2048;
                using (MemoryStream memStream = new MemoryStream(size))
                {
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            memStream.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    memStream.Position = 0;
                    switch (Path.GetExtension(sFileName).ToLower())
                    {
                        case ".inp":
                            using (StreamReader docFile = new System.IO.StreamReader(memStream))
                            {
                                TestCas tc = dTestcase[directoryName];
                                tc.Input = docFile.ReadToEnd();
                                docFile.Close();
                            }
                            break;
                        case ".out":
                            using (StreamReader docFile = new System.IO.StreamReader(memStream))
                            {
                                TestCas tc = dTestcase[directoryName];
                                tc.Output = docFile.ReadToEnd();
                                docFile.Close();
                            }
                            break;
                        case ".xml":
                            //Doc file cau hinh
                            if (Path.GetFileNameWithoutExtension(sFileName).ToLower() == problem.Name)
                                using (StreamReader docFile = new System.IO.StreamReader(memStream))
                                {
                                    configXML = docFile.ReadToEnd();
                                    docFile.Close();
                                }
                            break;
                        case ".doc":
                        case ".docx":
                        case ".pdf":
                            byte[] problemfile = memStream.ToArray();
                            SPKTOnline.Models.File file = new SPKTOnline.Models.File();
                            file.Content = problemfile;
                            file.Type = Path.GetExtension(sFileName);
                            file.Name = sFileName;
                            file.DownloadCount = 0;
                            db.Files.AddObject(file);
                            problem.File = file;

                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            //TODO: Gan du lieu Problem tu cau hinh xml
            double phanTramDiem = Math.Round(100.0 / problem.TestCases.Count, 2);
            foreach (TestCas tc in dTestcase.Values)
            {
                tc.Diem = phanTramDiem;
            }
            s.Close();
            return sFileName;
        }

    }

}

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

namespace SPKTOnline.Controllers
{
    public class UploadProblemController : Controller
    {
        //
        // GET: /UploadProblem/
         OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        ProblemRepository ProblemRep = new ProblemRepository();
        CheckRoles checkRole = new CheckRoles();

        [Authorize(Roles = "Admin,Lecturer")]
        public ActionResult Upload(int? ID )
        {
            ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(User.Identity.Name), "ID", "Name");
            //if (ID != 0)
            //{
            Problem pro = new Problem();
            pro.ExamID = ID;
            pro.AvailableTime = DateTime.Now;
            pro.Score = 10;
            return View(pro);
            //}

        }
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult Upload(Problem problem, HttpPostedFileBase filebase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    if (filebase != null)
                    {
                        if (filebase.ContentLength > 0)
                        {                            
                            fileName = Guid.NewGuid().ToString() + Path.GetExtension(filebase.FileName);
                            //TODO: đọc từ file cấu hình
                            problem.LecturerID = User.Identity.Name;
                            problem.Name = Path.GetFileNameWithoutExtension(filebase.FileName);
                            problem.DifficultyID = 1;
                            problem.MemoryLimit = 1000;
                            problem.TimeLimit = 1000;
                            problem.ComparerID = 1;
                            if (problem.ExamID == 0)
                            {
                                problem.ExamID = null;
                            }
                            db.Problems.AddObject(problem);
                            //db.SaveChanges();
                            Unzipfile(problem, filebase.InputStream);//, "~\\uploads");
                            db.SaveChanges();
                            if (problem.ExamID==null|| problem.ExamID == 0)
                            {

                                return RedirectToAction("Browse", "Problem", new { ID = problem.SubjectID });
                            }
                            else
                            {
                                return RedirectToAction("CreateExamCont", "Exam", new { ID = problem.ExamID });
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //TODO: redirect show problem
            ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(User.Identity.Name), "ID", "Name", problem.SubjectID);
            return View();
        }
        
        public string Unzipfile(Problem problem, Stream inputStream)//, string UnzipPath)
        {
            try
            {
                Dictionary<string, TestCas> dTestcase = new Dictionary<string, TestCas>();
                ZipInputStream s = new ZipInputStream(inputStream);
                ZipEntry ZEntry;
                string sFileName = "";
                List<String> listTestCase = new List<string>();
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
                    try
                    {
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
                                    break;
                                case ".doc":
                                case ".docx":
                                case ".pdf":

                                    byte[] problemfile = memStream.ToArray();

                                    SPKTOnline.Models.File file = new SPKTOnline.Models.File();
                                    file.Content = problemfile;
                                    file.Type = Path.GetExtension(sFileName);
                                    file.Name = sFileName;
                                    db.Files.AddObject(file);

                                    problem.File = file;


                                    FileStream streamWriter1 = System.IO.File.Create(@"D:\HKII-2011-2012\KLTN\SPKTOnline\SPKTOnline\uploads\" + sFileName);
                                    streamWriter1.Write(file.Content, 0, problemfile.Length);
                                    streamWriter1.Close();
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(ex.Message);
                    }
                }

                s.Close();
                //TestCas test;
                //  db.SaveChanges();

                return sFileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}

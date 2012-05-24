using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem
{
    public class FileManager
    {
        public String BaseDirPath { get; set; }

        public string DirPath { get; private set; }
        bool _DeleteOnDispose = true;
        public bool DeleteOnDispose
        {
            get { return _DeleteOnDispose; }
            set
            {
                _DeleteOnDispose = value;
            }
        }

        public int _CurentFileName = 0;
        public string GetNextTempFile(string ext)
        {
            string fileName ;
            do
            {
                fileName = Path.Combine(DirPath, Path.ChangeExtension(_CurentFileName.ToString(), ext));
                _CurentFileName++;
            } while (File.Exists(fileName));
            return fileName;
        }
        
        public FileManager(String baseDir)
        {
            BaseDirPath = baseDir;
            DateTime now = DateTime.Now;
            string dirName = now.ToString("yyMMddHHmmss") + now.Millisecond.ToString();
            int i = 0;
            do
            {
                DirPath = Path.Combine(BaseDirPath, dirName + "_" + i.ToString());
                i++;
            } while (Directory.Exists(DirPath));
            Directory.CreateDirectory(DirPath);
        }
        
        public string GetPath(string fileName)
        {
            return Path.Combine(DirPath, fileName);
        }
        ~FileManager()
        {
            if (DeleteOnDispose)
            {
                try
                {
                    Directory.Delete(DirPath, true);
                }
                catch
                { }
            }
        }
    }
}

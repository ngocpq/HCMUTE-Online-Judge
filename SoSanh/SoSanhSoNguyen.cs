using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem.SoSanh
{
    public class SoSanhSoNguyen : IFileComparer
    {
        public void Init(String para) { }

        public bool SoSanh(string output, ITestCase testcase, out string message)
        {
            message = "";
            string[] s1Lines = output.Split('\n');
            string[] s2Lines = testcase.Output.Split('\n');
            int i = 0;
            while (i < s1Lines.Length && i < s2Lines.Length)
            {
                if (SoSanhLine(s1Lines[i], s2Lines[i]) == false)
                {
                    message = "SAI KẾT QUẢ 1";
                    return false;
                }
                i++;
            }
            while (i < s1Lines.Length)
            {
                if (!IsEmptyLine(s1Lines[i]))
                {
                    message = "SAI KẾT QUẢ 2";
                    return false;
                }

            }
            while (i < s2Lines.Length)
            {
                if (!IsEmptyLine(s2Lines[i]))
                {
                    message = "SAI KẾT QUẢ 3";
                    return false;
                }
            }
            message = "ĐÚNG KẾT QUẢ";
            return true;
        }
        
        private static bool IsEmptyLine(string s)
        {
            return s.Trim() == "";
        }
        static bool SoSanhLine(string s1, string s2)
        {
            s1 = s1.Trim(' ', '\t');
            s2 = s2.Trim(' ', '\t');
            string[] num1 = s1.Split(' ', '\t');
            string[] num2 = s2.Split(' ', '\t');
            if (num1.Length != num2.Length)
                return false;
            for (int i = 0; i < num1.Length; i++)
            {
                try
                {
                    long n1 = long.Parse(num1[i]);
                    long n2 = long.Parse(num2[i]);
                    if (n1 != n2)
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }        
    }
    
}

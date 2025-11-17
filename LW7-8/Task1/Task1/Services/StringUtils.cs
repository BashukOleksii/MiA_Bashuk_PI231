using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Services
{
    public class StringUtils
    {
        public string Resource { get; private set; }

        public StringUtils(string reosource)
        {
            if (string.IsNullOrEmpty(reosource))
                throw new ArgumentNullException();

            this.Resource = reosource;
        }
        public StringUtils() : this("Test") { }
        

        public string Reverse()
        {
            string reverce = "";

            for(int i = Resource.Length - 1; i >= 0; i--)
                reverce += Resource[i];

            return reverce;
        }    

        public bool IsPalindrom()
        {
            string palindromRow = Resource.Replace(" ", "").ToLower();
            for(int i = 0; i < palindromRow.Length; i++)
                if (palindromRow[i] != palindromRow[palindromRow.Length - 1- i])
                    return false;

            return true;
        }

        public int CountWord()
        {
            int count = 0;

            string find = Resource.Trim();

            for (int i = 0; i < find.Length; i++)
                if (find[i] == ' ')
                    count++;

            return count + 1;
        }

        public static int MaxLen(params string[] rows)
        {
            int maxLen = 0;

            if(rows is null || rows.Length == 0)
                throw new ArgumentNullException();

            foreach(var r in rows)
                if(r.Length > maxLen)
                    maxLen = r.Length;

            return maxLen;
        }

        public char[] ToCharArray()
        {
            char[] array = new char[Resource.Length];

            for(int i = 0; i < Resource.Length; i++)
                array[i] = Resource[i];

            return array;
        }

        public string AddToEnd(string add)
        {
            if(add is null || string.IsNullOrEmpty(add))
                throw new ArgumentNullException();

            return Resource + add;
        }
    }
}

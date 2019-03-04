using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version.Core
{
    public class DVersion : IVersion
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Revision { get; set; }

        public int Build { get; set; }

        public DVersion() { }

        public DVersion(string v)
        {
            var array = v.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (array.Length > 0)
            {
                Major = Convert.ToInt32(array[0]);
            }
            if (array.Length > 1)
            {
                Minor = Convert.ToInt32(array[1]);
            }
            if (array.Length > 2)
            {
                Revision = Convert.ToInt32(array[2]);
            }
            if (array.Length > 3)
            {
                Build = Convert.ToInt32(array[3]);
            }
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Revision}.{Build}";
        }
    }
}

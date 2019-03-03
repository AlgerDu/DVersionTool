using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version
{
    public interface IShell
    {
        void Write(string msg);

        void WriteLine(string msg);
    }
}

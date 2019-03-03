using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version
{
    public interface IDvtCore
    {
        void SetPath(string path);

        void RunCmd(params string[] args);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version
{
    public interface IVersion
    {
        int Major { get; }

        int Minor { get; }

        int Revision { get; }

        int Build { get; }
    }
}

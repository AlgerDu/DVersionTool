using System;
using System.Collections.Generic;
using System.Text;
using D.Utils;
using Microsoft.Extensions.Logging;

namespace D.Tool.Version.Core
{
    public class StandardProjectFile :
        XmlProjectFileTmplate
        , IProjectFile
    {
        IShell _shell;
        string _path;

        public StandardProjectFile(
            ILogger<StandardProjectFile> logger
            , IShell shell
            , string path
            )
            : base(logger)
        {
            _shell = shell;
            _path = path;
        }

        #region IProjectFile
        public TargetFramework HTF => TargetFramework.netstandard20;

        public TargetFramework LTF => TargetFramework.netstandard20;

        public IVersion AssemblyVersion => throw new NotImplementedException();

        public IVersion AssemblyFileVersion => throw new NotImplementedException();

        public IResult SetVersion(IVersion newVersion)
        {
            LoadFile(_path);

            SetNodeTxt("Project/PropertyGroup/Version", newVersion.ToString());
            SetNodeTxt("Project/PropertyGroup/FileVersion", newVersion.ToString());

            Save(_path);

            return Result.CreateSuccess();
        }
        #endregion
    }
}

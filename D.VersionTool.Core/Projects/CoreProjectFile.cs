using D.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version.Core
{
    public class CoreProjectFile :
        XmlProjectFileTmplate
        , IProjectFile
    {
        IShell _shell;
        string _path;

        public CoreProjectFile(
            ILogger<CoreProjectFile> logger
            , IShell shell
            , string path
            )
            : base(logger)
        {
            _shell = shell;
            _path = path;
        }

        #region IProjectFile
        public TargetFramework HTF => TargetFramework.netcoreapp21;

        public TargetFramework LTF => TargetFramework.netcoreapp21;

        public IVersion AssemblyVersion => throw new NotImplementedException();

        public IVersion AssemblyFileVersion => throw new NotImplementedException();

        public IResult SetVersion(IVersion newVersion)
        {
            LoadFile(_path);

            SetNodeTxt("Project/PropertyGroup/AssemblyVersion", newVersion.ToString());
            SetNodeTxt("Project/PropertyGroup/Version", newVersion.ToString());
            SetNodeTxt("Project/PropertyGroup/FileVersion", newVersion.ToString());

            Save(_path);

            return Result.CreateSuccess();
        }
        #endregion
    }
}

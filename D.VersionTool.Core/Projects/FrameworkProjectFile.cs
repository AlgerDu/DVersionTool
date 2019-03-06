using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using D.Utils;
using Microsoft.Extensions.Logging;

namespace D.Tool.Version.Core
{
    public class FrameworkProjectFile : IProjectFile
    {
        IShell _shell;
        string _path;

        public FrameworkProjectFile(
            ILogger<FrameworkProjectFile> logger
            , IShell shell
            , string path
            )
        {
            _shell = shell;
            _path = path;
        }

        public TargetFramework HTF => TargetFramework.net461;

        public TargetFramework LTF => TargetFramework.net461;

        public IVersion AssemblyVersion => throw new NotImplementedException();

        public IVersion AssemblyFileVersion => throw new NotImplementedException();

        public IResult SetVersion(IVersion newVersion)
        {
            var dPath = Path.GetDirectoryName(_path);

            dPath += "\\Properties\\AssemblyInfo.cs";

            var text = File.ReadAllText(dPath);

            text = Regex.Replace(text, "AssemblyVersion\\(\"[\\d.]+\"\\)", $"AssemblyVersion(\"{newVersion}\")", RegexOptions.Multiline);
            text = Regex.Replace(text, "AssemblyFileVersion\\(\"[\\d.]+\"\\)", $"AssemblyFileVersion(\"{newVersion}\")", RegexOptions.Multiline);

            File.Delete(dPath);
            File.AppendAllText(dPath, text);

            return Result.CreateSuccess();
        }
    }
}

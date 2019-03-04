using D.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version
{
    public interface IProjectFile
    {
        TargetFramework HTF { get; }

        TargetFramework LTF { get;   }

        /// <summary>
        /// 程序集版本
        /// </summary>
        IVersion AssemblyVersion { get; }

        /// <summary>
        /// 程序集文件版本
        /// </summary>
        IVersion AssemblyFileVersion { get; }

        /// <summary>
        /// 设置项目的版本号，并且更新到文件
        /// </summary>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        IResult SetVersion(IVersion newVersion);
    }
}

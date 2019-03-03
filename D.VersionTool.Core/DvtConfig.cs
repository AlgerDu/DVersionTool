using System;
using System.Collections.Generic;
using System.Text;

namespace D.Tool.Version
{
    public class DvtConfig
    {
        /// <summary>
        /// 上一个版本号
        /// </summary>
        public string LastVersion { get; set; }

        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrVersion { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public string[] Projects { get; set; }

        /// <summary>
        /// 忽略的项目
        /// </summary>
        public string[] IgnoreProjects { get; set; }

        public DvtConfig()
        {
            Projects = new string[0];
            IgnoreProjects = new string[0];
        }
    }
}

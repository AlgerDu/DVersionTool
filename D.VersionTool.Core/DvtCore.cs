using D.Tool.Version.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace D.Tool.Version
{
    public class DvtCore : IDvtCore
    {
        ILogger _logger;
        ILoggerFactory _loggerFactory;
        IShell _shell;

        string _runningPath;

        public DvtCore(
            ILogger<DvtCore> logger
            , ILoggerFactory loggerFactory
            , IShell shell
            )
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _shell = shell;
        }

        public void RunCmd(params string[] args)
        {
            if (args.Length <= 0)
            {
                ShowDescription();
                return;
            }

            var cmd = args[0].ToLower();

            switch (cmd)
            {
                case "config":
                    RunConfigCmd(args);
                    break;

                case "add":
                    RunAddCmd(args);
                    break;

                case "set":
                    RunSetCmd(args);
                    break;
            }
        }

        public void SetPath(string path)
        {
            _runningPath = path;
        }

        private void ShowDescription()
        {
            _shell.Write("D.VersionTool 第一版");
        }

        private void RunConfigCmd(params string[] args)
        {
            var config = LoadOrCreateDefault();

            var projectPaths = DealFloder(new DirectoryInfo(_runningPath));

            List<string> toCtlProject = new List<string>();

            foreach (var path in projectPaths)
            {
                var hasIgnore = config.IgnoreProjects.Where(pp => pp == path).Count() > 0;

                if (!hasIgnore)
                {
                    toCtlProject.Add(path);
                }
            }

            config.Projects = toCtlProject.ToArray();

            SaveConfig(config);
        }

        private void RunSetCmd(params string[] args)
        {
            var config = LoadOrCreateDefault();

            var version = new DVersion(config.CurrVersion);

            foreach (var projectPath in config.Projects)
            {
                var pf = CreateProjectFile(_runningPath + projectPath);

                if (pf != null)
                    pf.SetVersion(version);
            }
        }

        private void RunAddCmd(params string[] args)
        {
            var config = LoadOrCreateDefault();

            var version = new DVersion(config.CurrVersion);

            version.Build++;

            foreach (var projectPath in config.Projects)
            {
                var pf = CreateProjectFile(_runningPath + projectPath);

                if (pf != null)
                    pf.SetVersion(version);
            }
        }

        private string[] DealFloder(DirectoryInfo floder)
        {
            List<string> projectPaths = new List<string>();

            foreach (var file in floder.GetFiles())
            {
                if (file.Extension.ToLower() == ".csproj")
                {
                    projectPaths.Add(file.FullName.Replace(_runningPath, ""));
                }
            }

            foreach (var child in floder.GetDirectories())
            {
                projectPaths.AddRange(DealFloder(child));
            }

            return projectPaths.ToArray();
        }

        private DvtConfig LoadOrCreateDefault()
        {
            var configPath = _runningPath + "dvt.config.json";

            if (File.Exists(configPath))
            {
                var devConfig = JsonConvert.DeserializeObject<DvtConfig>(File.ReadAllText(configPath));

                if (devConfig == null)
                {
                    devConfig = new DvtConfig();
                }

                _shell.WriteLine($"开始更新配置");
                return devConfig;
            }
            else
            {
                _shell.WriteLine($"配置文件不存在，创建默认的配置文件 dvt.config.json");
                return new DvtConfig();
            }
        }

        private void SaveConfig(DvtConfig dvtConfig)
        {
            var configPath = _runningPath + "dvt.config.json";

            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }

            if (!Directory.Exists(Path.GetDirectoryName(configPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(configPath));
            }

            File.AppendAllText(configPath, JsonConvert.SerializeObject(dvtConfig, Newtonsoft.Json.Formatting.Indented));
        }

        private IProjectFile CreateProjectFile(string path)
        {
            var xml = new XmlDocument();
            xml.Load(path);

            XmlNodeList nodes = xml.SelectNodes("/Project/PropertyGroup/TargetFramework");

            if (nodes.Count == 1)
            {
                var tf = nodes[0].InnerText;

                if (tf.StartsWith("netcoreapp"))
                {
                    return new CoreProjectFile(
                        _loggerFactory.CreateLogger<CoreProjectFile>()
                        , _shell
                        , path);
                }
                else if (tf.StartsWith("netstandard"))
                {
                    return new StandardProjectFile(
                        _loggerFactory.CreateLogger<StandardProjectFile>()
                        , _shell
                        , path);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}

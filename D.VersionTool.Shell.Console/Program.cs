using Autofac;
using Autofac.Extensions.DependencyInjection;
using D.Tool.Version;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.VersionTool.Shell.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();
            var runningPath = Directory.GetCurrentDirectory();


            var core = container.Resolve<IDvtCore>();

            core.SetPath(runningPath + "\\");
            core.RunCmd(args);
        }

        static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.AddMicrosoftExtensions();

            builder.RegisterType<ConsoleShell>().As<IShell>().AsSelf().SingleInstance();

            builder.RegisterType<DvtCore>().As<IDvtCore>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }

    class ConsoleShell : IShell
    {
        public void Write(string msg)
        {
            System.Console.Write(msg);
        }

        public void WriteLine(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ActofacExtensions
    {
        public static void AddMicrosoftExtensions(this ContainerBuilder builder)
        {
            var service = new ServiceCollection();
            service.AddLogging();
            service.AddOptions();

            builder.Populate(service);
        }
    }
}

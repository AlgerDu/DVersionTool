using Autofac;
using Autofac.Extensions.DependencyInjection;
using D.Tool.Version;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.VersionTool
{
    [TestClass]
    public class TestDvtCore
    {
        readonly IContainer _container;

        IDvtCore _dvtCore;

        public TestDvtCore()
        {
            _container = CreateContainer();

            _dvtCore = _container.Resolve<IDvtCore>();

            _dvtCore.SetPath(@"E:\DProject\D.VersionTool\");
        }

        [TestMethod]
        public void TestDescript()
        {
            _dvtCore.RunCmd();
        }

        [TestMethod]
        public void TestConfig_R()
        {
            _dvtCore.RunCmd(
                "config"
                , "-r"
                );
        }

        [TestMethod]
        public void TestSet()
        {
            _dvtCore.RunCmd(
                "set"
                );
        }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.AddMicrosoftExtensions();

            builder.RegisterType<TestShell>().As<IShell>().AsSelf().SingleInstance();

            builder.RegisterType<DvtCore>().As<IDvtCore>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }

    public class TestShell : IShell
    {
        public void Write(string msg)
        {

        }

        public void WriteLine(string msg)
        {
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

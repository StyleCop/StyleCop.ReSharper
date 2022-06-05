using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;
using System.Threading;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.StyleCop.Tests
{
    [ZoneDefinition]
    public interface IStyleCopTestZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>
    {
    }

    [SetUpFixture]
    public class TestEnvironment : ExtensionTestEnvironmentAssembly<IStyleCopTestZone>
    {
    }
}

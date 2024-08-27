using Lombiq.Tests.UI.SecurityScanning;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SecurityScanning.Web.Tests.UI.Tests;

public class SecurityScanningTests : UITestBase
{
    public SecurityScanningTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Fact]
    public Task FullSecurityScanShouldPass() =>
        ExecuteTestAfterSetupAsync(context =>
            context.RunAndConfigureAndAssertFullSecurityScanForContinuousIntegrationAsync());
}

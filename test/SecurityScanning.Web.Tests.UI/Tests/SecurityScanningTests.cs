using Lombiq.Tests.UI.SecurityScanning;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using YamlDotNet.RepresentationModel;

namespace SecurityScanning.Web.Tests.UI.Tests;

public class SecurityScanningTests : UITestBase
{
    public SecurityScanningTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Fact]
    public Task BasicSecurityScanShouldPass() =>
        ExecuteTestAfterBrowserSetupWithoutBrowserAsync(
            context => context.RunAndAssertBaselineSecurityScanAsync(),
            changeConfiguration: configuration => configuration.UseAssertAppLogsForSecurityScan());

    [Fact]
    public Task SecurityScanWithCustomConfigurationShouldPass() =>
        ExecuteTestAfterBrowserSetupWithoutBrowserAsync(
            context => context.RunAndAssertBaselineSecurityScanAsync(
                configuration => configuration
                    .ExcludeUrlWithRegex(".*blog.*")
                    .DisablePassiveScanRule(10020, "The response does not include either Content-Security-Policy with 'frame-ancestors' directive.")
                    .DisableScanRuleForUrlWithRegex(".*/about", 10038, "Content Security Policy (CSP) Header Not Set")
                    .ModifyZapPlan(plan => plan
                        .AddFalsePositiveRuleFilter(
                            ".*",
                            10202,
                            "Absence of Anti-CSRF Tokens",
                            "The search form doesn't alter the state of the application so anti-CSRF tokens are not needed.",
                            node =>
                            {
                                node.Children["evidence"] = ".*my-search-form.*";
                                node.Children["evidenceRegex"] = "true";
                            }))
                    .SignIn(),
                sarifLog => sarifLog.Runs[0].Results.Count.ShouldBe(0)),
            changeConfiguration: configuration => configuration.UseAssertAppLogsForSecurityScan());

    [Fact]
    public Task SecurityScanWithCustomAutomationFrameworkPlanShouldPass() =>
        ExecuteTestAfterBrowserSetupWithoutBrowserAsync(
            context => context.RunAndAssertSecurityScanAsync(
                "Tests/CustomZapAutomationFrameworkPlan.yml",
                configuration => configuration
                    .ModifyZapPlan(plan =>
                    {
                        var spiderJob = plan.GetSpiderJob();
                        var spiderParameters = (YamlMappingNode)spiderJob["parameters"];
                        spiderParameters.Add("maxDepth", "8");
                    }),
                sarifLog => SecurityScanningConfiguration.AssertSecurityScanHasNoAlerts(context, sarifLog)),
            changeConfiguration: configuration => configuration.UseAssertAppLogsForSecurityScan());

    [Fact]
    public Task FullSecurityScanShouldPass() =>
        ExecuteTestAfterSetupAsync(context =>
            context.RunAndConfigureAndAssertFullSecurityScanForContinuousIntegrationAsync());
}

using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Pages;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using SecurityScanning.Web.Tests.UI.Constants;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace SecurityScanning.Web.Tests.UI.Helpers;

public static class SetupHelpers
{
    public static async Task<Uri> RunSetupAsync(UITestContext context)
    {
        var homepageUri = await context.GoToSetupPageAndSetupOrchardCoreAsync(
            new OrchardCoreSetupParameters(context)
            {
                SiteName = "SecurityScanning",
                RecipeId = RecipeIds.Tests,
                TablePrefix = "SecurityScanning",
                SiteTimeZoneValue = "America/Phoenix",
            });

        context.Get(By.CssSelector(".navbar-brand")).Text.ShouldBe("SecurityScanning");

        return homepageUri;
    }
}

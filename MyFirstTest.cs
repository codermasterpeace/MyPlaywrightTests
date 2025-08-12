using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MyPlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MyFirstTest : PageTest
{
    [Test]
    public async Task ShouldHaveCorrectTitle()
    {
        await Page.GotoAsync("https://playwright.dev/");
        await Expect(Page).ToHaveTitleAsync("Fast and reliable end-to-end testing for modern web apps | Playwrightss");
    }
}
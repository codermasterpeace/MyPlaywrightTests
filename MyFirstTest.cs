using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting; // Use the correct MSTest 'using' statement
using System.Threading.Tasks;

namespace MyPlaywrightTests;

[TestClass] // Use the correct MSTest attribute
public class MyFirstTest : TracingTest // <-- IMPORTANT: Inherit from TracingTest
{
    [TestMethod] // Use the correct MSTest attribute
    public async Task ShouldHaveCorrectTitle()
    {
        await Page.GotoAsync("https://playwright.dev/");
        
        // IMPORTANT: Keep this test failing so we can confirm the trace upload works.
        await Expect(Page).ToHaveTitleAsync("This title is wrong to make the test fail");
    }
}
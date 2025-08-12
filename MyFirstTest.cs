using NUnit.Framework; // Use the correct NUnit 'using' statement
using System.Threading.Tasks;

namespace MyPlaywrightTests;

[TestFixture] // Use the correct NUnit attribute
public class MyFirstTest : TracingTest // <-- IMPORTANT: Inherit from TracingTest
{
    [Test] // Use the correct NUnit attribute
    public async Task ShouldHaveCorrectTitle()
    {
        await Page.GotoAsync("https://playwright.dev/");
        
        // IMPORTANT: Keep this test failing so we can confirm the trace upload works.
        await Expect(Page).ToHaveTitleAsync("This title is wrong to make the test fail");
    }
}
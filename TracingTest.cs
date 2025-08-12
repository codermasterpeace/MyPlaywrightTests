namespace MyPlaywrightTests; // <-- THIS IS THE FIX. ADD THIS LINE.

using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Threading.Tasks;

// This is our new base class for tests that need tracing on failure.
// It is written specifically for the NUnit framework.
public class TracingTest : PageTest
{
    // [SetUp] runs before every test in NUnit.
    [SetUp]
    public async Task StartTracing()
    {
        // Start tracing before each test begins.
        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.FullName,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    // [TearDown] runs after every test in NUnit.
    [TearDown]
    public async Task StopTracingOnFailure()
    {
        // This is the magic for NUnit: We check the test's outcome.
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            // NUnit's TestContext gives us the working directory.
            var resultsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults");
            Directory.CreateDirectory(resultsDir);

            // Create a unique path for the trace file using the test ID.
            string tracePath = Path.Combine(resultsDir, $"{TestContext.CurrentContext.Test.ID}.zip");

            // Stop tracing and save the file to the path we just defined.
            await Context.Tracing.StopAsync(new()
            {
                Path = tracePath
            });

            // This attaches the file to the NUnit test results.
            TestContext.AddTestAttachment(tracePath);
        }
    }
}
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

// This is our new base class for tests that need tracing on failure.
// It is written specifically for the MSTest framework.
[TestClass]
public class TracingTest : PageTest
{
    // MSTest provides the TestContext as a property that it fills in automatically.
    public TestContext TestContext { get; set; }

    // [TestInitialize] runs before every test. This is MSTest's version of [SetUp].
    [TestInitialize]
    public async Task StartTracing()
    {
        // Start tracing before each test begins.
        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.TestName,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    // [TestCleanup] runs after every test. This is MSTest's version of [TearDown].
    [TestCleanup]
    public async Task StopTracingOnFailure()
    {
        // This is the magic: We only save the trace if the test did not pass.
        if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
        {
            // Create a "TestResults" directory if it doesn't exist.
            var resultsDir = Path.Combine(TestContext.DeploymentDirectory, "TestResults");
            Directory.CreateDirectory(resultsDir);

            // Create a unique path for the trace file using the test name.
            string tracePath = Path.Combine(resultsDir, $"{TestContext.TestName}.zip");

            // Stop tracing and save the file to the path we just defined.
            await Context.Tracing.StopAsync(new()
            {
                Path = tracePath
            });

            // This attaches the file to the test results, making it visible in logs.
            TestContext.AddResultFile(tracePath);
        }
    }
}
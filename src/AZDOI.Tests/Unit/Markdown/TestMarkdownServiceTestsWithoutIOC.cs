using Cake.Core;
using Cake.Core.IO;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace AZDOI.Tests.Unit.Markdown;

public class TestMarkdownServiceTestsWithoutIOC
{
    [Fact]
    public async Task WriteIndex_ShouldWriteExpectedMarkdownContent()
    {
        // Given
        //var (fileSystem, service) = ServiceProviderFixture.GetRequiredService<FakeFileSystem, TestMarkdownService>();
        ICakeEnvironment environment = FakeEnvironment.CreateUnixEnvironment();
        IFileSystem fileSystem = new FakeFileSystem(environment);
        ICakeContext context = Substitute.For<ICakeContext>();
        context.FileSystem.Returns(fileSystem);
        var timeProvider = new FakeTimeProvider();
        var service = new TestMarkdownService(
            context,
            timeProvider
            );

        string markdownContent = "# MyOrg DevOps Organization";

        // When
        var result = await service.TestWriteIndex(markdownContent);

        // Then
        await Verify(
            new
            {
                Content = result,
                Equal = markdownContent == result,
                FileExists = fileSystem.GetFile("/index.md").Exists
            }
        );
    }
}
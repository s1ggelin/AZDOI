using Cake.Core;
using Cake.Core.IO;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace AZDOI.Tests.Unit.Markdown;

public class TestMarkdownServiceTestsWithoutAnything
{
    [Fact]
    public async Task WriteIndex_ShouldWriteExpectedMarkdownContent()
    {
        // Given
        ICakeEnvironment environment = FakeEnvironment.CreateUnixEnvironment();
        IFileSystem fileSystem = new FakeFileSystem(environment);
        ICakeContext context = Substitute.For<ICakeContext>();
        context.FileSystem.Returns(fileSystem);
        var timeProvider = new FakeTimeProvider();
        var service = new TestMarkdownService(
            context,
            timeProvider
            );
        const string markdownContent = "# MyOrg DevOps Organization";
        const string expect =
            $$"""
                ---
                modifiedby: AZDOI
                modified: 2000-01-01 00:00
                ---
                {{markdownContent}}
                """;

        // When
        var result = await service.TestWriteIndex(markdownContent);
        var file = fileSystem.GetFile("/index.md");

        // Then
        Assert.Equal(expect, result);
        Assert.True(file.Exists, "File doesn't exist.");
    }
}
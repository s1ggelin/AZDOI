namespace AZDOI.Tests.Unit.Markdown;

public class TestMarkdownServiceTestsWithoutSnapshot
{
    [Fact]
    public async Task WriteIndex_ShouldWriteExpectedMarkdownContent()
    {
        // Given
        var (fileSystem, service) = ServiceProviderFixture.GetRequiredService<FakeFileSystem, TestMarkdownService>();
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
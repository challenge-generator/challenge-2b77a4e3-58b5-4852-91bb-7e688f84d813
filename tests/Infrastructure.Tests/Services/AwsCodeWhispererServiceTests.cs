using Xunit;
using Moq;
using FluentAssertions;
using Domain.Models;
using Infrastructure.Services;

namespace Infrastructure.Tests.Services;

[Trait("Category", "Integration")]
public class AwsCodeWhispererServiceTests
{
    private readonly Mock<AwsCodeWhispererService> _serviceMock;
    private readonly string _testApiKey;

    public AwsCodeWhispererServiceTests()
    {
        _testApiKey = "test-api-key-for-integration-testing";
        _serviceMock = new Mock<AwsCodeWhispererService>(_testApiKey);
    }

    [Fact]
    public async Task GenerateSuggestionsAsync_WithValidRequest_ReturnsSuggestions()
    {
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Unit test class",
            suggestionsCount: 3);

        var result = await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CorrectCodeAsync_WithValidRequest_ReturnsCorrection()
    {
        var request = new CodeCorrectionRequest(
            problematicCode: "var x =",
            errorDescription: "Syntax error",
            severityLevel: 1);

        var result = await _serviceMock.Object.CorrectCodeAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GenerateDocumentationAsync_WithValidRequest_ReturnsDocumentation()
    {
        var request = new DocumentationRequest(
            codeSnippet: "public void Process() { // Processes data }",
            comment: "Processes the incoming data stream",
            scopeLines: 10);

        var result = await _serviceMock.Object.GenerateDocumentationAsync(request, CancellationToken.None);

        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GenerateSuggestionsAsync_WithInvalidCode_ThrowsException(string invalidCode)
    {
        var request = new CodeSuggestionRequest(
            currentCode: invalidCode,
            contextDescription: "Test",
            suggestionsCount: 3);

        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public async Task GenerateSuggestionsAsync_WithInvalidSuggestionCount_ThrowsException(int invalidCount)
    {
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Test",
            suggestionsCount: invalidCount);

        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _serviceMock.Object.GenerateSuggestionsAsync(request, CancellationToken.None));
    }
}
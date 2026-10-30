using Domain.Interfaces;
using Domain.Models;
using Application.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Services;

public class CodeSuggestionServiceTests
{
    private readonly Mock<ICodeSuggestionService> _mockSuggestionService;
    private readonly CodeSuggestionService _sut;

    public CodeSuggestionServiceTests()
    {
        _mockSuggestionService = new Mock<ICodeSuggestionService>();
        _sut = new CodeSuggestionService(_mockSuggestionService.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithValidRequest_ReturnsSuggestions()
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "public class Test { }",
            contextDescription: "Unit test class",
            suggestionsCount: 3);

        var expectedResponse = new CodeSuggestionResponse(
            suggestions: new List<CodeSuggestion>
            {
                new("public class Test { public void Method() { } }", "Add method", 0.9)
            },
            originalContext: "Unit test class");

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(It.IsAny<CodeSuggestionRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _sut.GenerateSuggestionsAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasValidSuggestions().Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithEmptyCode_ThrowsException()
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "",
            contextDescription: "Empty code",
            suggestionsCount: 3);

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(It.IsAny<CodeSuggestionRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CodeSuggestionException("Code cannot be empty"));

        // Act & Assert
        await Assert.ThrowsAsync<CodeSuggestionException>(
            async () => await _sut.GenerateSuggestionsAsync(request, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ValidateSuggestions_WithValidSuggestions_ReturnsValidResults()
    {
        // Arrange
        var suggestions = new List<CodeSuggestion>
        {
            new("code1", "desc1", 0.9),
            new("code2", "desc2", 0.7)
        };

        var validationResults = new Dictionary<CodeSuggestion, bool>
        {
            { suggestions[0], true },
            { suggestions[1], false }
        };

        _mockSuggestionService
            .Setup(s => s.ValidateSuggestions(It.IsAny<IEnumerable<CodeSuggestion>>()))
            .Returns(validationResults);

        // Act
        var result = _sut.ValidateSuggestions(suggestions);

        // Assert
        result.Should().HaveCount(2);
        result[suggestions[0]].Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetPerformanceMetricsAsync_ReturnsMetrics()
    {
        // Arrange
        var metrics = new List<SuggestionMetrics>
        {
            new(10, 8, 0.8),
            new(5, 4, 0.8)
        };

        _mockSuggestionService
            .Setup(s => s.GetPerformanceMetricsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(metrics);

        // Act
        var result = await _sut.GetPerformanceMetricsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [Trait("Category", "Unit")]
    public async Task GenerateSuggestionsAsync_WithDifferentSuggestionCounts_RequestsCorrectCount(int suggestionCount)
    {
        // Arrange
        var request = new CodeSuggestionRequest(
            currentCode: "public void Test() { }",
            contextDescription: "Test method",
            suggestionsCount: suggestionCount);

        _mockSuggestionService
            .Setup(s => s.GenerateSuggestionsAsync(
                It.Is<CodeSuggestionRequest>(r => r.SuggestionsCount == suggestionCount),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CodeSuggestionResponse(
                Enumerable.Empty<CodeSuggestion>(),
                "Test"));

        // Act
        await _sut.GenerateSuggestionsAsync(request, CancellationToken.None);

        // Assert
        _mockSuggestionService.Verify(
            s => s.GenerateSuggestionsAsync(
                It.Is<CodeSuggestionRequest>(r => r.SuggestionsCount == suggestionCount),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
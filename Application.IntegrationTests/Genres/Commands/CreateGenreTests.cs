﻿using Movies.Application.Genres.Commands.CreateGenre;

namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class CreateGenreTests : TestBase
{
    // ValidationException: .NotEmpty()
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        // Act
        var command = new CreateGenreCommand(Name: "");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    // ValidationException: .MaximumLength(50);
    [Test]
    public async Task ShouldRequireMaximumFields()
    {
        // Act
        var command = new CreateGenreCommand(Name: "TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    // Method
    [Test]
    public async Task ShouldCreateGenre()
    {
        // Act
        var command = new CreateGenreCommand(Name: "New genre");

        var genreId = await SendAsync(command);

        var genre = await FindAsync<Genre>(genreId);
        
        // Assert
        genre.Should().NotBeNull();
        genre.Name.Should().Be("New genre");
        genre.LastModified.Should().BeNull();
    }
}
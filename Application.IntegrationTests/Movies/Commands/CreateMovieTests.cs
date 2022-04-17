﻿namespace Movies.Application.IntegrationTests.Movies.Commands;

using static Testing;

public class CreateMovieTests : TestBase
{
    [Test]
    public async Task ShouldCreateMovie()
    {
        // Arrange
        var genreIdA = await SendAsync(new CreateGenreCommand(Name: "New name A"));
        var genreIdB = await SendAsync(new CreateGenreCommand(Name: "New name B"));

        var personIdA = await SendAsync(new CreatePersonCommand(FullName: "New fullName A"));
        var personIdB = await SendAsync(new CreatePersonCommand(FullName: "New fullName B"));
        var personIdC = await SendAsync(new CreatePersonCommand(FullName: "New fullName C"));
        
        // Act
        var command = new CreateMovieCommand
        (
            Title: "New title",
            Release: 2022,
            Duration: "2h",
            MaturityRating: "18+",
            Summary: "New summary",
            Genres: new List<int> {genreIdA, genreIdB},
            Persons: new List<MoviePersonDto>
            {
                new(PersonId: personIdA, Role: 1, Order: 1),
                new(PersonId: personIdB, Role: 2, Order: 1),
                new(PersonId: personIdC, Role: 2, Order: 2)
            }
        );

        var movieId = await SendAsync(command);

        var query = new GetMovieDetailsQuery(Id: movieId);
        
        var movie = await SendAsync(query);

        // Assert
        movie.Should().NotBeNull();
        movie.Title.Should().Be("New title");
        movie.Release.Should().Be(2022);
        movie.Duration.Should().Be("2h");
        movie.MaturityRating.Should().Be("18+");
        movie.Summary.Should().Be("New summary");
        movie.Genres.Count.Should().Be(2);
        movie.DirectedBy.Count.Should().Be(1);
        movie.Cast.Count.Should().Be(2);
    }
}
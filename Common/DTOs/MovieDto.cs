﻿using Movies.Domain.Entities;
using Movies.Domain.Enums;

namespace Movies.Common.DTOs;

public record MoviesDto(int Id, string Title, ICollection<FilesDto> Images)
{
    public static explicit operator MoviesDto(Movie movie) => 
        new(
            movie.Id, 
            movie.Title,
            movie.Images
                .Select(file => new FilesDto(file.Id, file.Url))
                .ToList()
        );
}

public record MovieDetailsDto(
    int Id, 
    string Title, 
    int Release, 
    string Duration, 
    string MaturityRating, 
    string Summary,
    ICollection<FilesDto> Images,
    ICollection<GenresDto> Genres, 
    ICollection<PersonsDto> DirectedBy, 
    ICollection<PersonsDto> Cast
)
{
    public static explicit operator MovieDetailsDto(Movie movie)
        => new(
            movie.Id,
            movie.Title,
            movie.Release,
            movie.Duration,
            movie.MaturityRating,
            movie.Summary,
            movie.Images
                .Select(file => new FilesDto(file.Id, file.Url))
                .ToList(),
            movie.MovieGenres
                .Select(movieGenre => new GenresDto(movieGenre.GenreId, movieGenre.Genre.Name))
                .ToList(),
            movie.MoviePersons
                .Where(moviePerson => moviePerson.Role == (byte)Role.Director)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(moviePerson.PersonId, moviePerson.Person.FullName))
                .ToList(),
            movie.MoviePersons
                .Where(moviePerson => moviePerson.Role == (byte)Role.Cast)
                .OrderBy(moviePerson => moviePerson.Order)
                .Select(moviePerson => new PersonsDto(moviePerson.PersonId, moviePerson.Person.FullName))
                .ToList()
        );
}

public record MoviePersonDto(int PersonId, byte Role, int Order);
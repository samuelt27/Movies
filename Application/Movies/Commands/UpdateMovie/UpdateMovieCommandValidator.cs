﻿namespace Movies.Application.Movies.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(60);

        RuleFor(p => p.Release)
            .NotEmpty()
            .LessThanOrEqualTo((short)10895)
            .GreaterThanOrEqualTo((short)1895);

        RuleFor(p => p.Duration)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(p => p.MaturityRating)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(p => p.Summary)
            .NotEmpty()
            .MaximumLength(1200);

        RuleFor(p => p.Genres)
            .NotEmpty();

        RuleFor(p => p.Persons)
            .NotEmpty()
            .ForEach(initialCollection => initialCollection
                .ChildRules(inlineValidator => inlineValidator
                    .RuleFor(moviePersonDto => moviePersonDto.Role)
                        .LessThanOrEqualTo((byte)2)
                        .GreaterThanOrEqualTo((byte)1)
                )
            );
    }
}
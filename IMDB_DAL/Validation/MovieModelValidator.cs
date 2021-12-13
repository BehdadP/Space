using IMDB_DAL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB_DAL.Validation
{

    public class SearchMovieNameModelValidator : AbstractValidator<SearchRequest>
    {
        public SearchMovieNameModelValidator()
        {
            RuleFor(x => x.MovieName).NotEmpty();
            RuleFor(x => x.MovieName).MinimumLength(2);
        }
    }
}
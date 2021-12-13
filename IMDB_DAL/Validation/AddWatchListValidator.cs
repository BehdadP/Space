using IMDB_DAL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using IMDB_DAL.Models;

namespace IMDB_DAL.Validation
{

    public class AddWatchListValidator : AbstractValidator<WatchList>
    {
        public AddWatchListValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty();
            RuleFor(x => x.UserEmail).EmailAddress();
            RuleFor(x => x.IMDBId).NotEmpty();
            RuleFor(x => x.IMDBId).MaximumLength(10);
            RuleFor(x => x.UserEmail).MaximumLength(50);



        }
    }
}
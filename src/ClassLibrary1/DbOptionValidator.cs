using FluentValidation;

namespace ClassLibrary1;

public class DbOptionValidator : AbstractValidator<DbOption>
{
    public DbOptionValidator()
    {
        RuleFor(x => x.DbConnRO).NotEmpty();
        RuleFor(x => x.DbConnRW).NotEmpty();
    }
}

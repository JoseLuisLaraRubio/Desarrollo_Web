namespace GymApp.ApiService.Features.PersonalInfo.Data;

using FluentValidation;

using GymApp.Database.Entities;

public sealed class PersonalInfoValidator : AbstractValidator<PersonalInfo>
{
    public PersonalInfoValidator()
    {
        this.RuleFor(p => p.FullName).Length(1, 50);

        this.RuleFor(p => p.Height).GreaterThan(0);

        this.RuleFor(p => p.Weight).GreaterThan(0);
    }
}

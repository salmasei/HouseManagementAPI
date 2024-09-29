using FluentValidation;
using HouseManagementAPI.Models;

namespace HouseManagementAPI.Validation
{
    public class HouseModelValidator : AbstractValidator<HouseModel>
    {
        public HouseModelValidator()
        {
            RuleFor(h => h.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(h => h.NumberOfFloors).GreaterThan(0).WithMessage("Number of floors must be greater than zero.");
            RuleFor(h => h.UnitType)
                .Must(ut => new[] { "house", "apartment", "serialhouse" }.Contains(ut))
                .WithMessage("UnitType must be 'house', 'apartment', or 'serialhouse'.");
            RuleForEach(h => h.Features)
                .Must(f => new[] { "balcony", "fireplace", "parking", "elevator", "morning_sun", "evening_sun" }
                .Contains(f))
                .WithMessage("Feature must be one of 'balcony', 'fireplace', 'parking', etc.");
        }
    }
}

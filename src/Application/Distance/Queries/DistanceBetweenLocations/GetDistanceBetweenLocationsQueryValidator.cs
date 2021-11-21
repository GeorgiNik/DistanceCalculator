using FluentValidation;

namespace DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations
{
    public class GetDistanceBetweenLocationsQueryValidator : AbstractValidator<GetDistanceBetweenLocationsQuery>
    {

        public GetDistanceBetweenLocationsQueryValidator()
        {
            RuleFor(v => v.StartLocationName)
                .NotEmpty().WithMessage($"{nameof(GetDistanceBetweenLocationsQuery.StartLocationName)} is required.")
                .MaximumLength(200).WithMessage($"{nameof(GetDistanceBetweenLocationsQuery.StartLocationName)} must not exceed 200 characters.");

            RuleFor(v => v.EndLocationName)
                .NotEmpty().WithMessage($"{nameof(GetDistanceBetweenLocationsQuery.EndLocationName)} is required.")
                .MaximumLength(200).WithMessage($"{nameof(GetDistanceBetweenLocationsQuery.EndLocationName)} must not exceed 200 characters.");
        }
    }
}
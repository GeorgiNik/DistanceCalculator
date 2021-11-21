using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Application.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        private readonly IDistanceCalculatorData _context;

        public CreateLocationCommandValidator(IDistanceCalculatorData context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Locations.AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}
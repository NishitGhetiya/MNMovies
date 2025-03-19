using FluentValidation;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Validations
{
    public class UserValidation : AbstractValidator<UserModel>
    {
        public UserValidation()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("User Name Is Required");
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("Please enter Valid Email");
            RuleFor(u => u.Password).NotEmpty().MaximumLength(6).MinimumLength(6).WithMessage("Password must be in 6 digit");
            RuleFor(u => u.MobileNo).NotEmpty().MaximumLength(10).MinimumLength(10).WithMessage("Mobile No. must be in 10 digit");
            RuleFor(u => u.Role)
                            .NotNull()
                            .WithMessage("Role is required")
                            .Must(role => role == true || role == false)
                            .WithMessage("Role must be valid (Admin or User)");
        }
    }
    public class UserLoginValidation : AbstractValidator<UserLoginModel>
    {
        public UserLoginValidation()
        {
            RuleFor(ul => ul.UserName).NotEmpty().WithMessage("User Name Is Required");
            RuleFor(ul => ul.Password).NotEmpty().MaximumLength(6).MinimumLength(6).WithMessage("Password must be in 6 digit");
        }
    }
    public class UserRegisterValidation : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidation()
        {
            RuleFor(ur => ur.UserName).NotEmpty().WithMessage("User Name Is Required");
            RuleFor(ur => ur.Email).NotEmpty().EmailAddress().WithMessage("Please enter Valid Email");
            RuleFor(ur => ur.Password).NotEmpty().MaximumLength(6).MinimumLength(6).WithMessage("Password must be in 6 digit");
            RuleFor(ur => ur.MobileNo).NotEmpty().MaximumLength(10).MinimumLength(10).WithMessage("Mobile No. must be in 10 digit");
            RuleFor(ur => ur.Role)
                            .NotNull()
                            .WithMessage("Role is required")
                            .Must(role => role == true || role == false)
                            .WithMessage("Role must be valid (Admin or User)");
        }
    }
}
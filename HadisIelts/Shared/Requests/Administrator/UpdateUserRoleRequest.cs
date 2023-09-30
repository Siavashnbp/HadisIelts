using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Shared.Requests.Administrator
{
    public record UpdateUserRoleRequest(string Email, ApplicationRoles Role, bool Value)
        : IRequest<UpdateUserRoleRequest.Response>
    {
        public const string EndPointUri = "/api/administrator/updateUserRole";
        public record Response(UserRolesSharedModel UserRoles) : ServerResponse;
    }
    public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}

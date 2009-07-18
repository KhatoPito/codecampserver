using System;
using July09v31.Core.Domain.Model;

namespace July09v31.Core.Services
{
    public interface ISecurityContext
    {
        bool HasPermissionsFor(Speaker speaker);
        bool HasPermissionsFor(Conference conference);
        bool HasPermissionsFor(UserGroup usergroup);
        bool HasPermissionsForUserGroup(Guid Id);
        bool IsAdmin();
        bool HasPermissionsFor(Session session);
    }
}
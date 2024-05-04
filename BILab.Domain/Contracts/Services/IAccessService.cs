namespace BILab.Domain.Contracts.Services {
    public interface IAccessService {
        Guid GetUserIdFromRequest();
        bool IsAdministatorRequest();
        bool IsAuthorizedRequest();
        List<string> GetAuthorizeUserRoles();
        bool IsHasAccess(Guid userId);
        string GetSchemeFromRequest();
        string GetHostFromRequest();
    }
}

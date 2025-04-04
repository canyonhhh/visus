namespace visus.Models.Enums
{
    public static class RoleHelper
    {
        public static string GetRoleName(UserRole role)
        {
            return role.ToString();
        }

        public static UserRole? ParseRole(string roleName)
        {
            if (Enum.TryParse<UserRole>(roleName, out var role))
            {
                return role;
            }
            return null;
        }

        public static IEnumerable<string> GetAllRoleNames()
        {
            return Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(r => GetRoleName(r));
        }
    }
}
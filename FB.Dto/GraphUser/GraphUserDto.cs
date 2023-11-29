
namespace FB.Dto
{
 public  class GraphUserDto
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string UserPrincipalName { get; set; }
        public string Mail { get; set; }

    }

    public class GraphAccessDto
    {
        public string TenantId { get; set; }
        public bool IsAdminConsent { get; set; }
    }
}

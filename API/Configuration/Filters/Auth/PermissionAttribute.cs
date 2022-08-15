using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Configuration.Filters.Auth
{
    public class PermissionAttribute: TypeFilterAttribute
    {
        //Permission definition of filter
        public PermissionAttribute(Permission permission) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}

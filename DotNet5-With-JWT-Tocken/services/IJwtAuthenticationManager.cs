using DotNet5_With_JWT_Tocken.Controllers;
using System.Collections.Generic;

namespace DotNet5_With_JWT_Tocken.services
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string UserName, string Password); 
    }
}

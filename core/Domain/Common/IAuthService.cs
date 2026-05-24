using Domain.Common.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken);
    }
}

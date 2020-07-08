using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get => "anonimous"; }

        public bool IsAuthenticated { get => false; }
    }
}

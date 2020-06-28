using BookStore.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.UnitTest.Common
{
    public class AnonymousUserService : ICurrentUserService
    {
        public string UserId => "anonymous";

        public bool IsAuthenticated => false;
    }
}

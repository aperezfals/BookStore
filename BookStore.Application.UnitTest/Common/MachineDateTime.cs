using BookStore.Common;
using System;

namespace BookStore.Application.UnitTest.Common
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

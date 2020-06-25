﻿using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Author Author { get; set; }
    }
}

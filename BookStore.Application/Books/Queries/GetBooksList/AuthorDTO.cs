using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Books.Queries.GetBooksList
{
    public class AuthorDTO : IMapFrom
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDTO>();
        }
    }
}

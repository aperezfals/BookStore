using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookStore.Application.Authors.Queries.GetAuthorsList
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

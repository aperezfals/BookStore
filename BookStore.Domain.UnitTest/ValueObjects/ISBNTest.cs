using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Domain.UnitTest.ValueObjects
{
    [TestFixture]
    public class ISBNTest
    {
        [TestCase("9789089986542")]
        [TestCase("9788497592208")]
        public void CreateISBN13ShouldBeValid(string isbnString)
        {
            Assert.DoesNotThrow(() =>
            {
                ISBN isbn = isbnString;
            });
        }

        [TestCase("0764526413")]
        [TestCase("2123456802")]
        public void CreateISBN10ShouldBeValid(string isbnString)
        {
            Assert.DoesNotThrow(() =>
            {
                ISBN isbn = isbnString;
            });
        }

        public void FromISBN10CreatesRightISBN13()
        {
            ISBN isbn = "2123456802";
            Assert.AreEqual("97282123456803", isbn.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("9789089986541")]
        [TestCase("97a9089986542")]
        [TestCase("aaaaaaaaaaaaa")]
        [TestCase("19788497592208")]
        public void CreateISBN13ShouldBeNotValid(string isbnString)
        {
            Assert.Throws<InvalidISBNException>(() =>
            {
                ISBN isbn = isbnString;
            });
        }
    }
}

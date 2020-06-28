using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BookStore.Domain.ValueObjects
{
    public class ISBN : ValueObject
    {
        public string ISBN10 { get; private set; }

        public string ISBN13 { get; private set; }

        private ISBN()
        {

        }
        
        public static ISBN For(string isbn)
        {
            var newISBN = new ISBN();
            newISBN.SetIsbn(isbn);
            return newISBN;
        }

        public static implicit operator string(ISBN isbn)
        {
            return isbn.ToString();
        }

        public static implicit operator ISBN(string isbn)
        {
            return For(isbn);
        }

        public override string ToString()
        {
            return ISBN13;
        }

        public static bool IsValid(string isbn)
        {
            return IsValid(isbn, out _);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ISBN13;
        }

        private static string Convert10to13(string isbn)
        {
            // remove - and space
            string isbn10 = CleanIsbn(isbn);
            // 1) Drop the check digit (the last digit)
            isbn10 = isbn10.Substring(0, 9);
            // 2) Add the prefix '978'
            string isbn13 = "978" + isbn10;
            // 3) Recalculate check digit
            isbn13 = isbn13 + Isbn13Checksum(isbn13);
            return isbn13;
        }

        private static string Convert13to10(string isbn)
        {
            string isbn13 = CleanIsbn(isbn);
           // 1) Drop the check digit (the last digit) and prefix '978'
            string isbn10 = isbn13.Substring(3, 9);
            // 2) Recalculate your check digit using the modules 10 check digit routine.
            isbn10 = isbn10 + Isbn10Checksum(isbn10);
            return isbn10;
        }

        private static bool IsValid(string isbn, out string correctISBN)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new InvalidISBNException("");

            // remove - and space
            isbn = CleanIsbn(isbn);
            if (isbn.Length == 10)
            {
                return ValidateIsbn10(isbn, out correctISBN);
            }
            else if (isbn.Length == 13)
            {
                return ValidateIsbn13(isbn, out correctISBN);
            }
            else
            {
                correctISBN = "";
                return false;
            }
        }

        private static string Isbn10Checksum(string isbn)
        {
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit;
                if (!int.TryParse(isbn[i].ToString(), out digit))
                    throw new InvalidISBNException(isbn);

                sum += (10 - i) * digit;
            }
            
            float rem = sum % 11;
            
            if (rem == 0)
                return "0";
            else if (rem == 1)
                return "X";
            else
                return (11 - rem).ToString();
        }

        private static string Isbn13Checksum(string isbn)
        {
            float sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit;
                if (!int.TryParse(isbn[i].ToString(), out digit))
                    throw new InvalidISBNException(isbn);
                sum += ((i % 2 == 0) ? 1 : 3) * digit;
            }
            
            float rem = sum % 10;
            if (rem == 0)
                return "0";
            else
                return (10 - rem).ToString();
        }

        private static bool ValidateIsbn10(string isbn, out string correctISBN)
        {
            correctISBN = isbn.Substring(0, 9) + Isbn10Checksum(isbn);
            return (correctISBN == isbn);
        }

        private static bool ValidateIsbn13(string isbn, out string correctISBN)
        {
            correctISBN = isbn.Substring(0, 12) + Isbn13Checksum(isbn);
            return (correctISBN == isbn);
        }

        private static string CleanIsbn(string isbn)
        {
            return isbn.Replace("-", "").Replace(" ", "");
        }

        private void SetIsbn(string isbn)
        {
            if (!IsValid(isbn))
                throw new InvalidISBNException(isbn);

            isbn = CleanIsbn(isbn);
            if (isbn.Length == 10)
            {
                ISBN10 = isbn;
                ISBN13 = Convert10to13(isbn);
            }
            else if (isbn.Length == 13)
            {
                ISBN13 = isbn;
                ISBN10 = Convert13to10(isbn);
            }
        }
    }
}

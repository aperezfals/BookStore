using BookStore.Domain.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.UnitTest.Common
{
    [TestFixture]
    public class ValueObjectTest
    {

        [Test]
        public void EqualsGivenDifferentValuesShouldReturnFalse()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 1);

            Assert.False(point1.Equals(point2));
        }

        [Test]
        public void EqualsGivenMatchingValuesShouldReturnTrue()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(1, 2);

            Assert.True(point1.Equals(point2));
        }

        private class Point : ValueObject
        {
            public int X { get; set; }
            public int Y { get; set; }

            private Point() { }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return X;
                yield return Y;
            }
        }
    }
}

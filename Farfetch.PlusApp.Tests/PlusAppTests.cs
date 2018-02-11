using System;
using NUnit.Framework;

namespace Farfetch.PlusApp.Tests
{
    [TestFixture]
    public class PlusAppTests
    {
        [Test]
        public void Should_AddANumber()
        {
            Number number = new Number();
            int val = number.AddNumber(4);

            int ca = 4;
        }
    }
}

using System;
using NBehave.Spec.NUnit;

namespace July09v31.UnitTests
{
    public static class SpecExtensions
    {
        public static void ShouldBeOfLength(this Array array, int length)
        {
            array.Length.ShouldEqual(length);
        }
    }
}
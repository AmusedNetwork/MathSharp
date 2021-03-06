﻿using System.Collections.Generic;
using OpenTK;
using System.Runtime.Intrinsics;
using Xunit;
using static MathSharp.UnitTests.TestHelpers;

namespace MathSharp.UnitTests.VectorTests.VectorSingle.BasicMathsTests
{
    public class Negate
    {
        public static IEnumerable<object[]> Data =>
            new[]
            {
                new object[] { Vector128.Create(1f), new Vector4(-1f) },
                new object[] { Vector128.Create(-1f), new Vector4(1f) },
                new object[] { Vector128.Create(-1f, float.NegativeInfinity, -1f, -0.00000000000000000001f), new Vector4(1f, float.PositiveInfinity, 1f, 0.00000000000000000001f) },
                new object[] { Vector128.Create(float.NegativeInfinity), new Vector4(float.PositiveInfinity) },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void Negate_Theory(Vector128<float> vector, Vector4 expected)
        {
            Vector128<float> result = Vector.Negate(vector);

            Assert.True(AreEqual(expected, result), $"Expected {expected}, got {result}");
        }
    }
}
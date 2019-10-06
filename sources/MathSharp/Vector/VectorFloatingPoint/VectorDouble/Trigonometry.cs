﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MathSharp.Constants;
using static MathSharp.Utils.Helpers;

namespace MathSharp
{
    using Vector4DParam1_3 = Vector256<double>;

    // The bane of every yr11's existence
    // TRIG
    public static partial class Vector
    {
        private static readonly Vector256<double> SinCoefficient0D = Vector256.Create(-0.16666667f, +0.0083333310f, -0.00019840874f, +2.7525562e-06f);
        private static readonly Vector256<double> SinCoefficient1D = Vector256.Create(-2.3889859e-08f, -0.16665852f, +0.0083139502f, -0.00018524670f);
        private const double SinCoefficient1DScalar = -2.3889859e-08f;

        [MethodImpl(MaxOpt)]
        public static Vector256<double> Sin(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);
                vec = Select(neg, vec, comp);

                Vector256<double> vectorSquared = Square(vec);

                // Polynomial approx
                Vector256<double> sc0 = SinCoefficient0D;

                Vector256<double> constants = Vector256.Create(SinCoefficient1DScalar);
                Vector256<double> result = FastMultiplyAdd(constants, vectorSquared, PermuteWithW(sc0));

                constants = PermuteWithZ(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithY(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithX(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                return result;
            }

            return SoftwareFallback(vector);

            static Vector256<double> SoftwareFallback(Vector4DParam1_3 vector)
            {
                return Vector256.Create(
                    Math.Sin(X(vector)),
                    Math.Sin(Y(vector)),
                    Math.Sin(Z(vector)),
                    Math.Sin(W(vector))
                );
            }
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<double> SinApprox(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);
                vec = Select(neg, vec, comp);

                Vector256<double> vectorSquared = Square(vec);

                // Fast polynomial approx
                var sc1 = SinCoefficient1D;

                var constants = PermuteWithW(sc1);
                var result = FastMultiplyAdd(constants, vectorSquared, PermuteWithZ(sc1));

                constants = PermuteWithY(sc1);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                return result;
            }

            return Sin(vector);
        }

        private static readonly Vector256<double> CosCoefficient0D = Vector256.Create(-0.5f, +0.041666638f, -0.0013888378f, +2.4760495e-05f);
        private static readonly Vector256<double> CosCoefficient1D = Vector256.Create(-2.6051615e-07f, -0.49992746f, +0.041493919f, -0.0012712436f);
        private const double CosCoefficient1DScalar = -2.6051615e-07f;

        [MethodImpl(MaxOpt)]
        public static Vector256<double> Cos(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);

                vec = Select(neg, vec, comp);

                Vector256<double> vectorSquared = Square(vec);

                vec = Select(DoubleConstants.NegativeOne, DoubleConstants.One, comp);

                // Polynomial approx
                Vector256<double> cc0 = CosCoefficient0D;

                Vector256<double> constants = Vector256.Create(CosCoefficient1DScalar);
                Vector256<double> result = FastMultiplyAdd(constants, vectorSquared, PermuteWithW(cc0));

                constants = PermuteWithZ(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithY(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithX(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                return result;
            }

            return SoftwareFallback(vector);

            static Vector256<double> SoftwareFallback(Vector4DParam1_3 vector)
            {
                return Vector256.Create(
                    Math.Cos(X(vector)),
                    Math.Cos(Y(vector)),
                    Math.Cos(Z(vector)),
                    Math.Cos(W(vector))
                );
            }
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<double> CosApprox(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);

                vec = Select(neg, vec, comp);

                Vector256<double> vectorSquared = Square(vec);

                vec = Select(DoubleConstants.NegativeOne, DoubleConstants.One, comp);

                // Fast polynomial approx
                var cc1 = CosCoefficient1D;

                var constants = PermuteWithW(cc1);
                var result = FastMultiplyAdd(constants, vectorSquared, PermuteWithZ(cc1));

                constants = PermuteWithY(cc1);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                return result;
            }

            return Cos(vector);
        }

        private static readonly Vector256<double> TanCoefficients0D = Vector256.Create(1.0f, -4.667168334e-1f, 2.566383229e-2f, -3.118153191e-4f);
        private static readonly Vector256<double> TanCoefficients1D = Vector256.Create(4.981943399e-7f, -1.333835001e-1f, 3.424887824e-3f, -1.786170734e-5f);
        private static readonly Vector256<double> TanConstantsD = Vector256.Create(1.570796371f, 6.077100628e-11f, 0.000244140625f, 0.63661977228f);
        [MethodImpl(MaxOpt)]
        public static Vector256<double> Tan(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                var twoDivPi = PermuteWithW(TanConstantsD);

                var tc0 = PermuteWithX(TanConstantsD);
                var tc1 = PermuteWithY(TanConstantsD);
                var epsilon = PermuteWithZ(TanConstantsD);

                var va = Multiply(vector, twoDivPi);
                va = Round(va);

                var vc = FastNegateMultiplyAdd(va, tc0, vector);

                var vb = Abs(va);

                vc = FastNegateMultiplyAdd(va, tc1, vc);

                vb = ConvertToInt64(vb).AsDouble();

                var vc2 = Square(vc);

                var t7 = PermuteWithW(TanCoefficients1D);
                var t6 = PermuteWithZ(TanCoefficients1D);
                var t4 = PermuteWithX(TanCoefficients1D);
                var t3 = PermuteWithW(TanCoefficients0D);
                var t5 = PermuteWithY(TanCoefficients1D);
                var t2 = PermuteWithZ(TanCoefficients0D);
                var t1 = PermuteWithY(TanCoefficients0D);
                var t0 = PermuteWithX(TanCoefficients0D);

                var vbIsEven = And(vb, DoubleConstants.Epsilon).AsInt32();
                vbIsEven = CompareBitwiseEqual<int, int>(vbIsEven, Vector256<int>.Zero);

                var n = FastMultiplyAdd(vc2, t7, t6);
                var d = FastMultiplyAdd(vc2, t4, t3);
                n = FastMultiplyAdd(vc2, n, t5);
                d = FastMultiplyAdd(vc2, d, t2);
                n = Multiply(vc2, n);
                d = FastMultiplyAdd(vc2, d, t1);
                n = FastMultiplyAdd(vc, n, vc);

                var nearZero = InBounds(vc, epsilon);

                d = FastMultiplyAdd(vc2, d, t0);

                n = Select(n, vc, nearZero);
                d = Select(d, DoubleConstants.One, nearZero);

                var r0 = Negate(n);
                var r1 = Divide(n, d);
                r0 = Divide(d, r0);

                var isZero = CompareEqual(vector, Vector256<double>.Zero);

                var result = Select(r0, r1, vbIsEven);

                result = Select(result, Vector256<double>.Zero, isZero);

                return result;
            }

            return SoftwareFallback(vector);

            static Vector256<double> SoftwareFallback(Vector4DParam1_3 vector)
            {
                return Vector256.Create(
                    Math.Tan(X(vector)),
                    Math.Tan(Y(vector)),
                    Math.Tan(Z(vector)),
                    Math.Tan(W(vector))
                );
            }
        }

        private static readonly Vector256<double> TanEstCoefficientsD = Vector256.Create(2.484f, -1.954923183e-1f, 2.467401101f, ScalarDoubleConstants.OneDivPi);
        [MethodImpl(MaxOpt)]
        public static Vector256<double> TanApprox(Vector4DParam1_3 vector)
        {
            if (Avx.IsSupported)
            {
                var oneDivPi = PermuteWithW(TanEstCoefficientsD);

                var v1 = Multiply(vector, oneDivPi);
                v1 = Round(v1);

                v1 = FastNegateMultiplyAdd(DoubleConstants.Pi, v1, vector);

                var t0 = PermuteWithX(TanEstCoefficientsD);
                var t1 = PermuteWithY(TanEstCoefficientsD);
                var t2 = PermuteWithZ(TanEstCoefficientsD);

                var v2T2 = FastNegateMultiplyAdd(v1, v1, t2);
                var v2 = Square(v1);
                var v1T0 = Multiply(v1, t0);
                var v1T1 = Multiply(v1, t1);

                var d = ReciprocalApprox(v2T2);
                var n = FastMultiplyAdd(v2, v1T1, v1T0);

                return Multiply(n, d);
            }

            return Tan(vector);
        }

        [MethodImpl(MaxOpt)]
        public static void SinCos(Vector4DParam1_3 vector, out Vector256<double> sin, out Vector256<double> cos)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);

                vec = Select(neg, vec, comp);

                Vector256<double> vectorSquared = Square(vec);

                var cosVec = Select(DoubleConstants.NegativeOne, DoubleConstants.One, comp);


                // Polynomial approx
                Vector256<double> sc0 = SinCoefficient0D;

                Vector256<double> constants = Vector256.Create(SinCoefficient1DScalar);
                Vector256<double> result = FastMultiplyAdd(constants, vectorSquared, PermuteWithW(sc0));

                constants = PermuteWithZ(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithY(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithX(sc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                sin = result;

                // Polynomial approx
                Vector256<double> cc0 = CosCoefficient0D;

                constants = Vector256.Create(CosCoefficient1DScalar);
                result = FastMultiplyAdd(constants, vectorSquared, PermuteWithW(cc0));

                constants = PermuteWithZ(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithY(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                constants = PermuteWithX(cc0);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, cosVec);

                cos = result;

                return;
            }

            SoftwareFallback(vector, out sin, out cos);

            static void SoftwareFallback(Vector4DParam1_3 vector, out Vector256<double> sin, out Vector256<double> cos)
            {
                sin = Sin(vector);
                cos = Cos(vector);
            }
        }

        [MethodImpl(MaxOpt)]
        public static void SinCosApprox(Vector4DParam1_3 vector, out Vector256<double> sin, out Vector256<double> cos)
        {
            if (Avx.IsSupported)
            {
                Vector256<double> vec = Mod2Pi(vector);

                Vector256<double> sign = ExtractSign(vec);
                Vector256<double> tmp = Or(DoubleConstants.Pi, sign); // Pi with the sign from vector

                Vector256<double> abs = AndNot(sign, vec); // Gets the absolute of vector

                Vector256<double> neg = Subtract(tmp, vec);

                Vector256<double> comp = CompareLessThanOrEqual(abs, DoubleConstants.PiDiv2);

                vec = Select(neg, vec, comp);
                Vector256<double> vectorSquared = Square(vec);

                var cosVec = Select(DoubleConstants.NegativeOne, DoubleConstants.One, comp);


                // Fast polynomial approx
                var sc1 = SinCoefficient1D;

                var constants = PermuteWithW(sc1);
                var result = FastMultiplyAdd(constants, vectorSquared, PermuteWithZ(sc1));

                constants = PermuteWithY(sc1);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, vec);

                sin = result;

                // Fast polynomial approx
                var cc1 = CosCoefficient1D;

                constants = PermuteWithW(cc1);
                result = FastMultiplyAdd(constants, vectorSquared, PermuteWithZ(cc1));

                constants = PermuteWithY(cc1);
                result = FastMultiplyAdd(result, vectorSquared, constants);

                result = FastMultiplyAdd(result, vectorSquared, DoubleConstants.One);

                result = Multiply(result, cosVec);

                cos = result;

                return;
            }

            SinCos(vector, out sin, out cos);
        }
    }
}

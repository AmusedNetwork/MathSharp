﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace MathSharp.StorageTypes
{
    public readonly struct Vector2F
    {
        public readonly float X, Y;

        public Vector2F(Vector2F xy) => this = xy;

        public unsafe Vector2F(Vector2FAligned xy) => this = *(Vector2F*)&xy;

        public Vector2F(float xy) : this(xy, xy) { }

        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
    public readonly struct Vector3F
    {
        public readonly float X, Y, Z;

        public Vector3F(Vector3F xyz) => this = xyz;

        public unsafe Vector3F(Vector3FAligned xyz) => this = *(Vector3F*)&xyz;

        public Vector3F(float xyz) : this(xyz, xyz, xyz) { }

        public Vector3F(Vector2F xy, float z) : this(xy.X, xy.Y, z) { }

        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }


    public readonly struct Vector4F
    {
        public readonly float X, Y, Z, W;

        public Vector4F(Vector4F xyzw) => this = xyzw;
        public unsafe Vector4F(Vector4FAligned xy) => this = *(Vector4F*)&xy;

        public Vector4F(float xyzw) : this(xyzw, xyzw, xyzw, xyzw) { }

        public Vector4F(Vector2F xy, Vector2F zw) : this(xy.X, xy.Y, zw.X, zw.Y) { }

        public Vector4F(Vector2F xy, float z, float w) : this(xy.X, xy.Y, z, w) { }

        public Vector4F(Vector3F xyz, float w) : this(xyz.X, xyz.Y, xyz.Z, w) { }

        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    public readonly struct Vector2FAligned
    {
        public readonly float X, Y;

        public Vector2FAligned(Vector2FAligned xy) => this = xy;
        public Vector2FAligned(Vector2F xy) : this(xy.X, xy.Y) { }


        public Vector2FAligned(float xy) : this(xy, xy) { }

        public Vector2FAligned(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    public readonly struct Vector3FAligned
    {
        public readonly float X, Y, Z;

        public Vector3FAligned(Vector3FAligned xyz) => this = xyz;
        public Vector3FAligned(Vector3F xyz) : this(xyz.X, xyz.Y, xyz.Z) { }

        public Vector3FAligned(float xyz) : this(xyz, xyz, xyz) { }

        public Vector3FAligned(Vector2F xy, float z) : this(xy.X, xy.Y, z) { }

        public Vector3FAligned(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }


    [StructLayout(LayoutKind.Sequential, Size = 16)]
    public readonly struct Vector4FAligned
    {
        public readonly float X, Y, Z, W;

        public Vector4FAligned(Vector4FAligned xyzw) => this = xyzw;

        public unsafe Vector4FAligned(Vector4F xyzw) => this = *(Vector4FAligned*)&xyzw;

        public Vector4FAligned(float xyzw) : this(xyzw, xyzw, xyzw, xyzw) { }

        public Vector4FAligned(Vector2F xy, Vector2F zw) : this(xy.X, xy.Y, zw.X, zw.Y) { }

        public Vector4FAligned(Vector2F xy, float z, float w) : this(xy.X, xy.Y, z, w) { }

        public Vector4FAligned(Vector3F xyz, float w) : this(xyz.X, xyz.Y, xyz.Z, w) { }

        public Vector4FAligned(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }

    public static unsafe class VectorExtensions
    {
        public static Vector128<float> Load(ref this Vector2F vector) => Vector.Load2D(in vector.X);
        public static Vector128<float> Load(Vector2F* p) => Vector.Load2D((float*)p);

        public static Vector128<float> Load(ref this Vector3F vector) => Vector.Load3D(in vector.X); 
        public static Vector128<float> Load(Vector3F* p) => Vector.Load3D((float*)p);

        public static Vector128<float> Load(ref this Vector4F vector) => Vector.Load4D(in vector.X);
        public static Vector128<float> Load(Vector4F* p) => Vector.Load4D((float*)p);

        public static Vector128<float> Load(ref this Vector2FAligned vector) => Vector.Load2DAligned(in vector.X);
        public static Vector128<float> Load(ref this Vector3FAligned vector) => Vector.Load3DAligned(in vector.X);
        public static Vector128<float> Load(ref this Vector4FAligned vector) => Vector.Load4DAligned(in vector.X);

        public static void Store(this Vector128<float> vector, Vector2F* destination) => Vector.Store2D(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector2F destination)
        {
            fixed (Vector2F* p = &destination)
            {
                Store(vector, p);
            }
        }

        public static void Store(this Vector128<float> vector, Vector3F* destination) => Vector.Store3D(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector3F destination)
        {
            fixed (Vector3F* p = &destination)
            {
                Store(vector, p);
            }
        }

        public static void Store(this Vector128<float> vector, Vector4F* destination) => Vector.Store4D(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector4F destination)
        {
            fixed (Vector4F* p = &destination)
            {
                Store(vector, p);
            }
        }

        public static void Store(this Vector128<float> vector, Vector2FAligned* destination) => Vector.Store2DAligned(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector2FAligned destination)
        {
            fixed (Vector2FAligned* p = &destination)
            {
                Store(vector, p);
            }
        }

        public static void Store(this Vector128<float> vector, Vector3FAligned* destination) => Vector.Store3DAligned(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector3FAligned destination)
        {
            fixed (Vector3FAligned* p = &destination)
            {
                Store(vector, p);
            }
        }

        public static void Store(this Vector128<float> vector, Vector4FAligned* destination) => Vector.Store4DAligned(vector, (float*)destination);
        public static void Store(this Vector128<float> vector, out Vector4FAligned destination)
        {
            fixed (Vector4FAligned* p = &destination)
            {
                Store(vector, p);
            }
        }
    }
}
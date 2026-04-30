//
// Copyright (C) 2026 kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;
using System.Globalization;
using GTA.Math;
using Xunit;

namespace ScriptHookVDotNet_APIv3_Tests.Math
{
    public class Vector3Tests
    {
        private const float OneOverRoot2 = 0.70710678f;
        private const float OneOverRoot3 = 0.57735026f;
        private const float MinNonSafePositiveIntAsFloat = 16_777_216f;
        private const float MaxNonSafeNegativeIntAsFloat = -16_777_216f;

        public static TheoryData<Vector3, float> Length_Data =>
            new TheoryData<Vector3, float>
            {
                { new Vector3(0f, 0f, 0f), 0f },
                { new Vector3(3f, 4f, 0f), 5f },
                { new Vector3(1f, 2f, 2f), 3f },
            };

        public static TheoryData<Vector3, float> LengthSquared_Data =>
            new TheoryData<Vector3, float>
            {
                { new Vector3(0f, 0f, 0f), 0f },
                { new Vector3(3f, 4f, 0f), 25f },
                { new Vector3(1f, 2f, 2f), 9f },
            };

        public static TheoryData<Vector3, Vector3, float> Dot_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), 0f },
                { new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f), 32f },
                { new Vector3(-1f, -2f, -3f), new Vector3(1f, 2f, 3f), -14f },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Cross_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                { new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 1f) },
                { new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 0f) },
                { new Vector3(1f, 2f, 3f), new Vector3(4f, 5f, 6f), new Vector3(-3f, 6f, -3f) },
                {
                    new Vector3(1000f, 1000f, 1000f),
                    new Vector3(1100f, 16000f, 14000f),
                    new Vector3(-2_000_000f, -12_900_000f, 14_900_000f)
                },
            };

        public static TheoryData<Vector3, Vector3> Normalize_Data =>
            new TheoryData<Vector3, Vector3>
            {
                { new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f) },
                { new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f) },
                { new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f) },
                { new Vector3(1f, 1f, 1f), new Vector3(OneOverRoot3, OneOverRoot3, OneOverRoot3) },
                { new Vector3(3f, 4f, 0f), new Vector3(0.6f, 0.8f, 0f) },
            };

        public static TheoryData<Vector3, Vector3, Vector3, Vector3> Clamp_Data =>
            new TheoryData<Vector3, Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(-10f, 0.5f, 3f),
                    new Vector3(-5f, -1f, 1f),
                    new Vector3(5f, 1f, 2f),
                    new Vector3(-5f, 0.5f, 2f)
                },
                {
                    new Vector3(0f, 0f, 0f),
                    new Vector3(-1f, -1f, -1f),
                    new Vector3(1f, 1f, 1f),
                    new Vector3(0f, 0f, 0f)
                },
                {
                    new Vector3(MaxNonSafeNegativeIntAsFloat - 2f, 2013f, float.MaxValue),
                    new Vector3(MaxNonSafeNegativeIntAsFloat, MaxNonSafeNegativeIntAsFloat,
                    MaxNonSafeNegativeIntAsFloat),
                    new Vector3(MinNonSafePositiveIntAsFloat, MinNonSafePositiveIntAsFloat,
                    MinNonSafePositiveIntAsFloat),
                    new Vector3(MaxNonSafeNegativeIntAsFloat, 2013f, MinNonSafePositiveIntAsFloat)
                },
            };

        public static TheoryData<Vector3, Vector3, float, Vector3> Lerp_Data =>
            new TheoryData<Vector3, Vector3, float, Vector3>
            {
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(5f, 6f, 7f),
                    0f,
                    new Vector3(1f, 2f, 3f)
                },
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(5f, 6f, 7f),
                    0.5f,
                    new Vector3(3f, 4f, 5f)
                },
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(5f, 6f, 7f),
                    1f,
                    new Vector3(5f, 6f, 7f)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Minimize_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(1f, 10f, -2f),
                    new Vector3(2f, 9f, -3f),
                    new Vector3(1f, 9f, -3f)
                },
                {
                    new Vector3(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat, 0f),
                    new Vector3(0f, 0f, MaxNonSafeNegativeIntAsFloat),
                    new Vector3(0f, MaxNonSafeNegativeIntAsFloat, MaxNonSafeNegativeIntAsFloat)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Maximize_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(1f, 10f, -2f),
                    new Vector3(2f, 9f, -3f),
                    new Vector3(2f, 10f, -2f)
                },
                {
                    new Vector3(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat, 0f),
                    new Vector3(0f, 0f, MaxNonSafeNegativeIntAsFloat),
                    new Vector3(MinNonSafePositiveIntAsFloat, 0f, 0f)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Addition_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(5f, 6f, 7f),
                    new Vector3(6f, 8f, 10f)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Subtraction_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(5f, 6f, 7f),
                    new Vector3(1f, 2f, 3f),
                    new Vector3(4f, 4f, 4f)
                },
            };

        public static TheoryData<Vector3, Vector3> Negation_Data =>
            new TheoryData<Vector3, Vector3>
            {
                { new Vector3(1f, -2f, 3f), new Vector3(-1f, 2f, -3f) },
                {
                    new Vector3(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat,
                    MinNonSafePositiveIntAsFloat),
                    new Vector3(MaxNonSafeNegativeIntAsFloat, MinNonSafePositiveIntAsFloat,
                    MaxNonSafeNegativeIntAsFloat)
                },
                {
                    new Vector3(MaxNonSafeNegativeIntAsFloat, MinNonSafePositiveIntAsFloat,
                    MaxNonSafeNegativeIntAsFloat),
                    new Vector3(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat,
                    MinNonSafePositiveIntAsFloat)
                },
                { new Vector3(1e10f, -1e10f, 1e10f), new Vector3(-1e10f, 1e10f, -1e10f) },
            };

        public static TheoryData<Vector3, float, Vector3> Scale_Data =>
            new TheoryData<Vector3, float, Vector3>
            {
                { new Vector3(1f, 2f, 3f), 2.5f, new Vector3(2.5f, 5f, 7.5f) },
            };

        public static TheoryData<Vector3, float, Vector3> Division_Data =>
            new TheoryData<Vector3, float, Vector3>
            {
                { new Vector3(4f, 8f, 12f), 2f, new Vector3(2f, 4f, 6f) },
            };

        public static TheoryData<Vector3, Vector3> Equality_Data_Same =>
            new TheoryData<Vector3, Vector3>
            {
                { new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) },
                { new Vector3(1f, 2f, 3f), new Vector3(1f, 2f, 3f) },
            };

        public static TheoryData<Vector3, Vector3> Equality_Data_Different =>
            new TheoryData<Vector3, Vector3>
            {
                { new Vector3(0f, 0f, 0f), new Vector3(0.0001f, 0f, 0f) },
                { new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f) },
                { new Vector3(1f, 2f, 3f), new Vector3(3f, 2f, 1f) },
            };

        public static TheoryData<Vector3, object> Equals_Object_Data_Not_Vector3 =>
            new TheoryData<Vector3, object>
            {
                { new Vector3(0f, 0f, 0f), new object() },
                { new Vector3(1f, 2f, 3f), "not a vector" },
            };

        public static TheoryData<Vector3, Vector2> Implicit_Cast_To_Vector2_Data =>
            new TheoryData<Vector3, Vector2>
            {
                { new Vector3(1f, 2f, 3f), new Vector2(1f, 2f) },
                { new Vector3(-1f, -2f, -3f), new Vector2(-1f, -2f) },
            };

        public static TheoryData<Vector3, Vector3, float> DistanceTo_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(0f, 0f, 0f), new Vector3(3f, 4f, 0f), 5f },
                { new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 0f },
                { new Vector3(0f, 0f, 0f), new Vector3(1f, 2f, 2f), 3f },
            };

        public static TheoryData<Vector3, Vector3, float> DistanceToSquared_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(0f, 0f, 0f), new Vector3(3f, 4f, 0f), 25f },
                { new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 0f },
                { new Vector3(0f, 0f, 0f), new Vector3(1f, 2f, 2f), 9f },
            };

        public static TheoryData<Vector3, Vector3, float> DistanceTo2D_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(0f, 0f, 5f), new Vector3(3f, 4f, 99f), 5f },
                { new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 10f), 0f },
            };

        public static TheoryData<Vector3, Vector3, float> DistanceToSquared2D_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(0f, 0f, 5f), new Vector3(3f, 4f, 99f), 25f },
                { new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 10f), 0f },
            };

        public static TheoryData<Vector3, Vector3, float> Angle_Data =>
            new TheoryData<Vector3, Vector3, float>
            {
                { new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), 0f },
                { new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), 90f },
                { new Vector3(1f, 0f, 0f), new Vector3(-1f, 0f, 0f), 180f },
            };

        public static TheoryData<Vector3, Vector3, Vector3, float> SignedAngle_Data =>
            new TheoryData<Vector3, Vector3, Vector3, float>
            {
                { new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 1f), 90f },
                { new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), -90f },
            };

        public static TheoryData<Vector3, float> ToHeading_Data =>
            new TheoryData<Vector3, float>
            {
                { new Vector3(0f, 1f, 0f), 360f },
                { new Vector3(0f, -1f, 0f), 180f },
                { new Vector3(1f, 0f, 0f), 270f },
                { new Vector3(-1f, 0f, 0f), 90f },
            };

        public static TheoryData<Vector3, int, Vector3> Round_Data =>
            new TheoryData<Vector3, int, Vector3>
            {
                { new Vector3(1.4f, 2.6f, 3.1f), 0, new Vector3(1f, 3f, 3f) },
                { new Vector3(1.14f, 2.26f, 3.31f), 1, new Vector3(1.1f, 2.3f, 3.3f) },
            };

        public static TheoryData<Vector3, float> Around_Data =>
            new TheoryData<Vector3, float>
            {
                { new Vector3(1.4f, 2.6f, 3.1f), 0.5f },
                { new Vector3(1.14f, 2.26f, 3.31f), 0.25f },
                { new Vector3(1.14f, 2.26f, 3.31f), 1f },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Multiply_ComponentWise_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(4f, 5f, 6f),
                    new Vector3(4f, 10f, 18f)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Project_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                { new Vector3(1f, 2f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f) },
                { new Vector3(1f, 1f, 1f), new Vector3(0f, 0f, 2f), new Vector3(0f, 0f, 1f) },
                { new Vector3(1f, 3f, 1f), new Vector3(0f, 1f, 0f), new Vector3(0f, 3f, 0f) },
                { new Vector3(-1f, 0f, 0f), new Vector3(1f, 1f, 1f), new Vector3(-0.333333f, -0.333333f, -0.333333f) },
            };

        public static TheoryData<Vector3, Vector3, Vector3> ProjectOnPlane_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                { new Vector3(1f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 0f) },
                { new Vector3(1f, 2f, 0f), new Vector3(0f, 0f, 1f), new Vector3(1f, 2f, 0f) },
                {
                    new Vector3(OneOverRoot2, OneOverRoot2, -1f),
                    new Vector3(OneOverRoot2, OneOverRoot2, 1f),
                    new Vector3(OneOverRoot2, OneOverRoot2, -1f)
                },
            };

        public static TheoryData<Vector3, Vector3, Vector3> Reflect_Data =>
            new TheoryData<Vector3, Vector3, Vector3>
            {
                { new Vector3(1f, -1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, 1f, 0f) },
                { new Vector3(1f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, -1f, 0f) },
                { new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, -1f) },
                {
                    new Vector3(-OneOverRoot2, -OneOverRoot2, 0f),
                    new Vector3(0.5f, 0.5f, OneOverRoot2),
                    new Vector3(0f, 0f, 1f)
                },
            };

        public static TheoryData<Vector3> ToString_Data =>
            new TheoryData<Vector3>
            {
                { new Vector3(0f, 0f, 0f) },
                { new Vector3(1f, 2f, 3f) },
                { new Vector3(0.25f, 0.5f, 2.5f) },
            };

        public class ToStringFormat_Data_Class : TheoryData<Vector3, string>
        {
            public ToStringFormat_Data_Class()
            {
                Vector3[] vectors = new Vector3[]
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(0.25f, 0.5f, 2.5f),
                    new Vector3(1000f, -20000.5f, -300000.75f)
                };
                string[] formats = new string[]
                {
                    "N",
                    "F3",
                    "e4"
                };
                foreach (Vector3 vector in vectors)
                {
                    foreach (string format in formats)
                    {
                        Add(vector, format);
                    }
                }
            }
        }

        public class ToStringIFormatProvider_Data_Class : TheoryData<Vector3, string, IFormatProvider>
        {
            public ToStringIFormatProvider_Data_Class()
            {
                Vector3[] vectors = new Vector3[]
                {
                    new Vector3(1f, 2f, 3f),
                    new Vector3(0.25f, 0.5f, 2.5f),
                    new Vector3(1000f, -20000.5f, -300000.75f)
                };
                string[] formats = new string[]
                {
                    "N",
                    "F3",
                    "e4"
                };
                IFormatProvider[] providers = new IFormatProvider[]
                {
                    CultureInfo.InvariantCulture,
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR")
                };

                foreach (Vector3 vector in vectors)
                {
                    foreach (string format in formats)
                    {
                        foreach (IFormatProvider provider in providers)
                        {
                            Add(vector, format, provider);
                        }
                    }
                }
            }
        }

        [Fact]
        public void Zero_returns_vector_with_all_components_zero()
        {
            Vector3 actual = Vector3.Zero;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void One_returns_vector_with_all_components_one()
        {
            Vector3 actual = Vector3.One;
            Assert.Equal(1f, actual.X);
            Assert.Equal(1f, actual.Y);
            Assert.Equal(1f, actual.Z);
        }

        [Fact]
        public void UnitX_returns_unit_vector_along_x_axis()
        {
            Vector3 actual = Vector3.UnitX;
            Assert.Equal(1f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void UnitY_returns_unit_vector_along_y_axis()
        {
            Vector3 actual = Vector3.UnitY;
            Assert.Equal(0f, actual.X);
            Assert.Equal(1f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void UnitZ_returns_unit_vector_along_z_axis()
        {
            Vector3 actual = Vector3.UnitZ;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(1f, actual.Z);
        }

        [Fact]
        public void WorldUp_returns_0_0_1()
        {
            Vector3 actual = Vector3.WorldUp;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(1f, actual.Z);
        }

        [Fact]
        public void WorldDown_returns_0_0_minus1()
        {
            Vector3 actual = Vector3.WorldDown;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(-1f, actual.Z);
        }

        [Fact]
        public void WorldNorth_returns_0_1_0()
        {
            Vector3 actual = Vector3.WorldNorth;
            Assert.Equal(0f, actual.X);
            Assert.Equal(1f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void WorldSouth_returns_0_minus1_0()
        {
            Vector3 actual = Vector3.WorldSouth;
            Assert.Equal(0f, actual.X);
            Assert.Equal(-1f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void WorldEast_returns_1_0_0()
        {
            Vector3 actual = Vector3.WorldEast;
            Assert.Equal(1f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void WorldWest_returns_minus1_0_0()
        {
            Vector3 actual = Vector3.WorldWest;
            Assert.Equal(-1f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void RelativeRight_returns_1_0_0()
        {
            Vector3 actual = Vector3.RelativeRight;
            Assert.Equal(1f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void RelativeLeft_returns_minus1_0_0()
        {
            Vector3 actual = Vector3.RelativeLeft;
            Assert.Equal(-1f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void RelativeFront_returns_0_1_0()
        {
            Vector3 actual = Vector3.RelativeFront;
            Assert.Equal(0f, actual.X);
            Assert.Equal(1f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void RelativeBack_returns_0_minus1_0()
        {
            Vector3 actual = Vector3.RelativeBack;
            Assert.Equal(0f, actual.X);
            Assert.Equal(-1f, actual.Y);
            Assert.Equal(0f, actual.Z);
        }

        [Fact]
        public void RelativeTop_returns_0_0_1()
        {
            Vector3 actual = Vector3.RelativeTop;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(1f, actual.Z);
        }

        [Fact]
        public void RelativeBottom_returns_0_0_minus1()
        {
            Vector3 actual = Vector3.RelativeBottom;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
            Assert.Equal(-1f, actual.Z);
        }

        [Theory]
        [InlineData(0, 1f)]
        [InlineData(1, 2f)]
        [InlineData(2, 3f)]
        public void Indexer_getter_returns_component_at_index(int index, float expected)
        {
            Vector3 value = new Vector3(1f, 2f, 3f);
            Assert.Equal(expected, value[index]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Indexer_setter_sets_component_at_index(int index)
        {
            Vector3 value = new Vector3(1f, 2f, 3f);
            value[index] = 5f;
            Assert.Equal(5f, value[index]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void Indexer_getter_throws_argument_out_of_range_exception_on_invalid_index(int index)
        {
            Vector3 value = new Vector3(1f, 2f, 3f);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = value[index]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void Indexer_setter_throws_argument_out_of_range_exception_on_invalid_index(int index)
        {
            Vector3 value = new Vector3(1f, 2f, 3f);
            Assert.Throws<ArgumentOutOfRangeException>(() => value[index] = 42f);
        }

        [Theory]
        [MemberData(nameof(Length_Data))]
        public void Length_returns_magnitude(Vector3 input, float expected)
        {
            float actual = input.Length();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(LengthSquared_Data))]
        public void LengthSquared_returns_squared_magnitude(Vector3 input, float expected)
        {
            float actual = input.LengthSquared();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Dot_Data))]
        public void Dot_returns_dot_product(Vector3 left, Vector3 right, float expected)
        {
            float actual = Vector3.Dot(left, right);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Dot_Data))]
        public void Dot_is_commutative(Vector3 left, Vector3 right, float expected)
        {
            float actual = Vector3.Dot(left, right);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");

            float actualReversedArgs = Vector3.Dot(right, left);
            // Must be exactly equal, not just approximately equal, because multiplying is commutative for floating
            // point numbers, so the result of the dot product must be exactly the same regardless of the order of
            // the arguments.
            Assert.True(actual == actualReversedArgs,
                $"Assert failed. `Dot` should be commutative. " +
                $"actual: {actual}, actualReversedArgs: {actualReversedArgs}");
        }

        [Theory]
        [MemberData(nameof(Cross_Data))]
        public void Cross_returns_cross_product(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Cross(left, right);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Cross_Data))]
        public void Cross_is_anti_commutative(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Cross(left, right);
            EqualsApprox(actual, expected, 1e-5f, "The left vector cross the right vector is too different from " +
                "the passed expected vector.");

            Vector3 actualReversedArgs = -Vector3.Cross(right, left);
            EqualsExact(actualReversedArgs, actual, "`Cross` is expected to be anti-commutative, but the passed " +
                "right vector cross the passed left vector is not the exact negation of the left vector cross " +
                "the right vector.");
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Normalize_instance_method_normalizes_vector_in_place(Vector3 input, Vector3 expected)
        {
            Vector3 actual = input;
            actual.Normalize();
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Fact]
        public void Normalize_instance_method_does_not_change_zero_vector()
        {
            Vector3 value = Vector3.Zero;
            value.Normalize();
            EqualsApprox(value, Vector3.Zero, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Normalized_property_returns_normalized_copy(Vector3 input, Vector3 expected)
        {
            Vector3 origValue = input;
            Vector3 actual = origValue.Normalized;
            EqualsApprox(actual, expected, 1e-5f);
            EqualsApprox(origValue, input, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Static_normalize_returns_normalized_copy(Vector3 input, Vector3 expected)
        {
            Vector3 origValue = input;
            Vector3 actual = Vector3.Normalize(input);
            EqualsApprox(actual, expected, 1e-5f);
            EqualsApprox(origValue, input, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Clamp_Data))]
        public void Clamp_restricts_components_to_range(Vector3 value, Vector3 min, Vector3 max, Vector3 expected)
        {
            Vector3 actual = Vector3.Clamp(value, min, max);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Lerp_Data))]
        public void Lerp_returns_linear_interpolation(Vector3 start, Vector3 end, float amount, Vector3 expected)
        {
            Vector3 actual = Vector3.Lerp(start, end, amount);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Minimize_Data))]
        public void Minimize_returns_component_wise_minimum(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Minimize(left, right);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Maximize_Data))]
        public void Maximize_returns_component_wise_maximum(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Maximize(left, right);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Addition_Data))]
        public void Addition_operator_returns_sum(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = left + right;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Subtraction_Data))]
        public void Subtraction_operator_returns_difference(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = left - right;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Negation_Data))]
        public void Unary_minus_operator_negates_components(Vector3 value, Vector3 expected)
        {
            Vector3 actual = -value;
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Vector_times_scalar_operator_scales_components(Vector3 value, float scale, Vector3 expected)
        {
            Vector3 actual = value * scale;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Scalar_times_vector_operator_scales_components(Vector3 value, float scale, Vector3 expected)
        {
            Vector3 actual = scale * value;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Division_Data))]
        public void Division_operator_scales_components(Vector3 value, float scale, Vector3 expected)
        {
            Vector3 actual = value / scale;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equality_operator_returns_true_if_all_components_are_equal(Vector3 left, Vector3 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Inequality_operator_returns_false_if_all_components_are_equal(Vector3 left, Vector3 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.False(left != right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Different))]
        public void Equality_operator_returns_false_if_some_components_are_different(Vector3 left, Vector3 right)
        {
            Assert.False(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.False(left == right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Different))]
        public void Inequality_operator_returns_true_if_some_components_are_different(Vector3 left, Vector3 right)
        {
            Assert.False(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left != right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equals_method_returns_true_if_all_components_are_equal(Vector3 left, Vector3 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equals_method_returns_true_if_passed_argument_is_object_casted_from_the_same_vector(Vector3 left, Vector3 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left.Equals((object)right));
        }

        [Theory]
        [MemberData(nameof(Equals_Object_Data_Not_Vector3))]
        public void Equals_method_returns_false_if_passed_argument_is_not_a_Vector3(Vector3 input, object obj)
        {
            Assert.False(obj.GetType() == typeof(Vector3));
            Assert.False(input.Equals(obj));
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void GetHashCode_returns_same_value_for_equal_vectors(Vector3 left, Vector3 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y &&
                        left.Z == right.Z,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(DistanceTo_Data))]
        public void DistanceTo_returns_distance_to_other_vector(Vector3 from, Vector3 to, float expected)
        {
            float actual = from.DistanceTo(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared_Data))]
        public void DistanceToSquared_returns_squared_distance_to_other_vector(Vector3 from, Vector3 to, float expected)
        {
            float actual = from.DistanceToSquared(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceTo2D_Data))]
        public void DistanceTo2D_returns_distance_ignoring_z_component(Vector3 from, Vector3 to, float expected)
        {
            float actual = from.DistanceTo2D(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared2D_Data))]
        public void DistanceToSquared2D_returns_squared_distance_ignoring_z_component(Vector3 from, Vector3 to, float expected)
        {
            float actual = from.DistanceToSquared2D(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceTo_Data))]
        public void Static_Distance_returns_distance_between_vectors(Vector3 position1, Vector3 position2, float expected)
        {
            float actual = Vector3.Distance(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared_Data))]
        public void Static_DistanceSquared_returns_squared_distance_between_vectors(Vector3 position1, Vector3 position2, float expected)
        {
            float actual = Vector3.DistanceSquared(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceTo2D_Data))]
        public void Static_Distance2D_returns_distance_ignoring_z_component(Vector3 position1, Vector3 position2, float expected)
        {
            float actual = Vector3.Distance2D(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared2D_Data))]
        public void Static_DistanceSquared2D_returns_squared_distance_ignoring_z_component(Vector3 position1, Vector3 position2, float expected)
        {
            float actual = Vector3.DistanceSquared2D(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Angle_Data))]
        public void Angle_returns_angle_in_degrees_between_vectors(Vector3 from, Vector3 to, float expected)
        {
            float actual = Vector3.Angle(from, to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(SignedAngle_Data))]
        public void SignedAngle_returns_signed_angle_in_degrees(Vector3 from, Vector3 to, Vector3 planeNormal, float expected)
        {
            float actual = Vector3.SignedAngle(from, to, planeNormal);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(ToHeading_Data))]
        public void ToHeading_returns_heading_in_degrees(Vector3 input, float expected)
        {
            float actual = input.ToHeading();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Round_Data))]
        public void Round_rounds_each_component_to_decimal_places(Vector3 input, int decimalPlaces, Vector3 expected)
        {
            Vector3 actual = input.Round(decimalPlaces);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Fact]
        public void RandomXY_returns_normalized_vector_with_random_x_and_y_and_zero_z()
        {
            Vector3 actual = Vector3.RandomXY();
            Assert.True(actual.Z == 0f,
                $"Assert failed. Z component of vector returned by `RandomXY` should be exactly zero. " +
                $"actual: {actual.ToString()}");

            float length = actual.Length();
            Assert.True(System.Math.Abs(length - 1f) <= 1e-5f,
                $"Assert failed. Vector returned by `RandomXY` should be normalized. " +
                $"actual: {actual.ToString()}, length: {length}");
        }
        [Fact]
        public void RandomXYZ_returns_normalized_vector_with_random_x_y_and_z()
        {
            Vector3 actual = Vector3.RandomXYZ();
            float length = actual.Length();
            Assert.True(System.Math.Abs(length - 1f) <= 1e-5f,
                $"Assert failed. Vector returned by `RandomXYZ` should be normalized. " +
                $"actual: {actual.ToString()}, length: {length}");
        }

        [Theory]
        [MemberData(nameof(Around_Data))]
        public void Around_returns_vector_with_random_x_and_y_scaled_by_distance_plus_base_vector3(Vector3 basePosition, float distance)
        {
            Vector3 generatedPos = basePosition.Around(distance);
            float distanceBetweenTwoPos = (generatedPos - basePosition).Length();

            Assert.True(System.Math.Abs(distanceBetweenTwoPos - distance) <= 1e-5f,
                $"Assert failed. The distance between the generated position and the base position should have " +
                $"length equal to the passed distance. Actual Distance: {distanceBetweenTwoPos}, " +
                $"Expected Distance: {distance}");
        }

        [Theory]
        [MemberData(nameof(Addition_Data))]
        public void Static_Add_returns_sum(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Add(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Subtraction_Data))]
        public void Static_Subtract_returns_difference(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Subtract(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Static_Multiply_scales_vector_by_scalar(Vector3 value, float scale, Vector3 expected)
        {
            Vector3 actual = Vector3.Multiply(value, scale);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Multiply_ComponentWise_Data))]
        public void Static_Multiply_returns_component_wise_product(Vector3 left, Vector3 right, Vector3 expected)
        {
            Vector3 actual = Vector3.Multiply(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Division_Data))]
        public void Static_Divide_scales_vector_by_scalar(Vector3 value, float scale, Vector3 expected)
        {
            Vector3 actual = Vector3.Divide(value, scale);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Negation_Data))]
        public void Static_Negate_returns_negated_vector(Vector3 value, Vector3 expected)
        {
            Vector3 actual = Vector3.Negate(value);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Project_Data))]
        public void Project_returns_vector_projected_onto_normal(Vector3 vector, Vector3 onNormal, Vector3 expected)
        {
            Vector3 actual = Vector3.Project(vector, onNormal);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(ProjectOnPlane_Data))]
        public void ProjectOnPlane_returns_vector_projected_onto_plane(Vector3 vector, Vector3 planeNormal, Vector3 expected)
        {
            Vector3 actual = Vector3.ProjectOnPlane(vector, planeNormal);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Reflect_Data))]
        public void Reflect_returns_reflected_vector(Vector3 vector, Vector3 normal, Vector3 expected)
        {
            Assert.True(System.Math.Abs(normal.Length() - 1f) <= 1e-5f,
                $"Assert failed. Normal vector should be normalized. " +
                $"normal: {normal.ToString()}, length: {normal.Length()}");

            Vector3 actual = Vector3.Reflect(vector, normal);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Fact]
        public void ToArray_returns_array_with_xyz_components()
        {
            Vector3 value = new Vector3(1f, 2f, 3f);
            float[] actual = value.ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Equal(1f, actual[0]);
            Assert.Equal(2f, actual[1]);
            Assert.Equal(3f, actual[2]);
        }

        [Theory]
        [MemberData(nameof(Implicit_Cast_To_Vector2_Data))]
        public void Implicit_cast_to_vector2_discards_z_component(Vector3 input, Vector2 expected)
        {
            Vector2 actual = input;
            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
        }

        [Theory]
        [MemberData(nameof(ToString_Data))]
        public void ToString_without_format_returns_expected_component_labels(Vector3 value)
        {
            string expected = $"X:{value.X.ToString()} Y:{value.Y.ToString()} Z:{value.Z.ToString()}";
            string actual = value.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ToString_Data))]
        public void ToString_with_null_format_matches_default_ToString(Vector3 value)
        {
            Assert.Equal(value.ToString(), value.ToString(null));
        }

        [Theory]
        [ClassData(typeof(ToStringFormat_Data_Class))]
        public void ToString_with_format_formats_all_components(Vector3 value, string format)
        {
            string expected = $"X:{value.X.ToString(format)} Y:{value.Y.ToString(format)} " +
                 $"Z:{value.Z.ToString(format)}";
            string actual = value.ToString(format);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(ToStringIFormatProvider_Data_Class))]
        public void ToString_with_format_and_provider_formats_all_components(Vector3 value, string format, IFormatProvider provider)
        {
            string expected = $"X:{value.X.ToString(format, provider)} Y:{value.Y.ToString(format, provider)} " +
                 $"Z:{value.Z.ToString(format, provider)}";
            string actual = value.ToString(format, provider);
            Assert.Equal(expected, actual);
        }

        private static void EqualsExact(Vector3 left, Vector3 right)
        {
            Assert.True(left == right,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
        }
        private static void EqualsExact(Vector3 left, Vector3 right, string additionalAssertMessage)
        {
            Assert.True(left == right,
                $"Assert failed. {additionalAssertMessage} left: {left.ToString()}, right: {right.ToString()}");
        }

        private static void EqualsApprox(Vector3 left, Vector3 right, float tolerance)
        {
            Assert.True(System.Math.Abs(left.X - right.X) <= tolerance &&
                        System.Math.Abs(left.Y - right.Y) <= tolerance &&
                        System.Math.Abs(left.Z - right.Z) <= tolerance,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
        }
        private static void EqualsApprox(Vector3 left, Vector3 right, float tolerance, string additionalAssertMessage)
        {
            Assert.True(System.Math.Abs(left.X - right.X) <= tolerance &&
                        System.Math.Abs(left.Y - right.Y) <= tolerance &&
                        System.Math.Abs(left.Z - right.Z) <= tolerance,
                $"Assert failed. {additionalAssertMessage} left: {left.ToString()}, right: {right.ToString()}");
        }
    }
}

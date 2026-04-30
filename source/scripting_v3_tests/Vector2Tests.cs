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
    public class Vector2Tests
    {
        private const float MinNonSafePositiveIntAsFloat = 16_777_216f;
        private const float MaxNonSafeNegativeIntAsFloat = -16_777_216f;

        public static TheoryData<Vector2, float> Length_Data =>
            new TheoryData<Vector2, float>
            {
                { new Vector2(0f, 0f), 0f },
                { new Vector2(3f, 4f), 5f },
                { new Vector2(1f, 0f), 1f },
            };

        public static TheoryData<Vector2, float> LengthSquared_Data =>
            new TheoryData<Vector2, float>
            {
                { new Vector2(0f, 0f), 0f },
                { new Vector2(3f, 4f), 25f },
                { new Vector2(1f, 0f), 1f },
            };

        public static TheoryData<Vector2, Vector2, float> Dot_Data =>
            new TheoryData<Vector2, Vector2, float>
            {
                { new Vector2(1f, 0f), new Vector2(0f, 1f), 0f },
                { new Vector2(1f, 2f), new Vector2(3f, 4f), 11f },
                { new Vector2(-1f, -2f), new Vector2(1f, 2f), -5f },
            };

        public static TheoryData<Vector2, Vector2> Normalize_Data =>
            new TheoryData<Vector2, Vector2>
            {
                { new Vector2(1f, 0f), new Vector2(1f, 0f) },
                { new Vector2(0f, 1f), new Vector2(0f, 1f) },
                { new Vector2(3f, 4f), new Vector2(0.6f, 0.8f) },
                { new Vector2(1f, 1f), new Vector2(0.70710678f, 0.70710678f) },
            };

        public static TheoryData<Vector2, Vector2, Vector2, Vector2> Clamp_Data =>
            new TheoryData<Vector2, Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(-10f, 0.5f),
                    new Vector2(-5f, -1f),
                    new Vector2(5f, 1f),
                    new Vector2(-5f, 0.5f)
                },
                {
                    new Vector2(0f, 0f),
                    new Vector2(-1f, -1f),
                    new Vector2(1f, 1f),
                    new Vector2(0f, 0f)
                },
                {
                    new Vector2(MaxNonSafeNegativeIntAsFloat - 2f, float.MaxValue),
                    new Vector2(MaxNonSafeNegativeIntAsFloat, MaxNonSafeNegativeIntAsFloat),
                    new Vector2(MinNonSafePositiveIntAsFloat, MinNonSafePositiveIntAsFloat),
                    new Vector2(MaxNonSafeNegativeIntAsFloat, MinNonSafePositiveIntAsFloat)
                },
            };

        public static TheoryData<Vector2, Vector2, float, Vector2> Lerp_Data =>
            new TheoryData<Vector2, Vector2, float, Vector2>
            {
                {
                    new Vector2(1f, 2f),
                    new Vector2(5f, 6f),
                    0f,
                    new Vector2(1f, 2f)
                },
                {
                    new Vector2(1f, 2f),
                    new Vector2(5f, 6f),
                    0.5f,
                    new Vector2(3f, 4f)
                },
                {
                    new Vector2(1f, 2f),
                    new Vector2(5f, 6f),
                    1f,
                    new Vector2(5f, 6f)
                },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Minimize_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(1f, 10f),
                    new Vector2(2f, 9f),
                    new Vector2(1f, 9f)
                },
                {
                    new Vector2(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat),
                    new Vector2(0f, 0f),
                    new Vector2(0f, MaxNonSafeNegativeIntAsFloat)
                },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Maximize_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(1f, 10f),
                    new Vector2(2f, 9f),
                    new Vector2(2f, 10f)
                },
                {
                    new Vector2(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat),
                    new Vector2(0f, 0f),
                    new Vector2(MinNonSafePositiveIntAsFloat, 0f)
                },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Addition_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(1f, 2f),
                    new Vector2(5f, 6f),
                    new Vector2(6f, 8f)
                },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Subtraction_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(5f, 6f),
                    new Vector2(1f, 2f),
                    new Vector2(4f, 4f)
                },
            };

        public static TheoryData<Vector2, Vector2> Negation_Data =>
            new TheoryData<Vector2, Vector2>
            {
                { new Vector2(1f, -2f), new Vector2(-1f, 2f) },
                {
                    new Vector2(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat),
                    new Vector2(MaxNonSafeNegativeIntAsFloat, MinNonSafePositiveIntAsFloat)
                },
                {
                    new Vector2(MaxNonSafeNegativeIntAsFloat, MinNonSafePositiveIntAsFloat),
                    new Vector2(MinNonSafePositiveIntAsFloat, MaxNonSafeNegativeIntAsFloat)
                },
                { new Vector2(1e10f, -1e10f), new Vector2(-1e10f, 1e10f) },
            };

        public static TheoryData<Vector2, float, Vector2> Scale_Data =>
            new TheoryData<Vector2, float, Vector2>
            {
                { new Vector2(1f, 2f), 2.5f, new Vector2(2.5f, 5f) },
            };

        public static TheoryData<Vector2, float, Vector2> Division_Data =>
            new TheoryData<Vector2, float, Vector2>
            {
                { new Vector2(4f, 8f), 2f, new Vector2(2f, 4f) },
            };

        public static TheoryData<Vector2, Vector2> Equality_Data_Same =>
            new TheoryData<Vector2, Vector2>
            {
                { new Vector2(0f, 0f), new Vector2(0f, 0f) },
                { new Vector2(1f, 2f), new Vector2(1f, 2f) },
            };

        public static TheoryData<Vector2, Vector2> Equality_Data_Different =>
            new TheoryData<Vector2, Vector2>
            {
                { new Vector2(0f, 0f), new Vector2(0.0001f, 0f) },
                { new Vector2(0f, 0f), new Vector2(0f, 1f) },
                { new Vector2(1f, 2f), new Vector2(2f, 1f) },
            };

        public static TheoryData<Vector2, object> Equals_Object_Data_Not_Vector2 =>
            new TheoryData<Vector2, object>
            {
                { new Vector2(0f, 0f), new object() },
                { new Vector2(1f, 2f), "not a vector" },
            };

        public static TheoryData<Vector2, Vector3> Implicit_Cast_To_Vector3_Data =>
            new TheoryData<Vector2, Vector3>
            {
                { new Vector2(1f, 2f), new Vector3(1f, 2f, 0f) },
                { new Vector2(-1f, -2f), new Vector3(-1f, -2f, 0f) },
            };

        public static TheoryData<Vector2, Vector2, float> DistanceTo_Data =>
            new TheoryData<Vector2, Vector2, float>
            {
                { new Vector2(0f, 0f), new Vector2(3f, 4f), 5f },
                { new Vector2(1f, 1f), new Vector2(1f, 1f), 0f },
            };

        public static TheoryData<Vector2, Vector2, float> DistanceToSquared_Data =>
            new TheoryData<Vector2, Vector2, float>
            {
                { new Vector2(0f, 0f), new Vector2(3f, 4f), 25f },
                { new Vector2(1f, 1f), new Vector2(1f, 1f), 0f },
            };

        public static TheoryData<Vector2, Vector2, float> Angle_Data =>
            new TheoryData<Vector2, Vector2, float>
            {
                { new Vector2(1f, 0f), new Vector2(1f, 0f), 0f },
                { new Vector2(1f, 0f), new Vector2(0f, 1f), 90f },
                { new Vector2(1f, 0f), new Vector2(-1f, 0f), 180f },
            };

        public static TheoryData<Vector2, Vector2, float> SignedAngle_Data =>
            new TheoryData<Vector2, Vector2, float>
            {
                { new Vector2(1f, 0f), new Vector2(0f, 1f), 90f },
                { new Vector2(0f, 1f), new Vector2(1f, 0f), -90f },
            };

        public static TheoryData<Vector2, float> ToHeading_Data =>
            new TheoryData<Vector2, float>
            {
                { new Vector2(0f, 1f), 360f },
                { new Vector2(0f, -1f), 180f },
                { new Vector2(1f, 0f), 270f },
                { new Vector2(-1f, 0f), 90f },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Multiply_ComponentWise_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                {
                    new Vector2(1f, 2f),
                    new Vector2(4f, 5f),
                    new Vector2(4f, 10f)
                },
            };

        public static TheoryData<Vector2, Vector2, Vector2> Reflect_Data =>
            new TheoryData<Vector2, Vector2, Vector2>
            {
                { new Vector2(1f, -1f), new Vector2(0f, 1f), new Vector2(1f, 1f) },
                { new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(1f, -1f) },
                { new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, -1f) },
            };

        public static TheoryData<Vector2> ToString_Data =>
            new TheoryData<Vector2>
            {
                { new Vector2(0f, 0f) },
                { new Vector2(1f, 2f) },
                { new Vector2(0.25f, 0.5f) },
            };

        public class ToStringFormat_Data_Class : TheoryData<Vector2, string>
        {
            public ToStringFormat_Data_Class()
            {
                Vector2[] vectors = new Vector2[]
                {
                    new Vector2(1f, 2f),
                    new Vector2(0.25f, 0.5f),
                    new Vector2(1000f, -20000.5f),
                };
                string[] formats = new string[]
                {
                    "N",
                    "F3",
                    "e4"
                };
                foreach (Vector2 vector in vectors)
                {
                    foreach (string format in formats)
                    {
                        Add(vector, format);
                    }
                }
            }
        }

        public class ToStringIFormatProvider_Data_Class : TheoryData<Vector2, string, IFormatProvider>
        {
            public ToStringIFormatProvider_Data_Class()
            {
                Vector2[] vectors = new Vector2[]
                {
                    new Vector2(1f, 2f),
                    new Vector2(0.25f, 0.5f),
                    new Vector2(1000f, -20000.5f),
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

                foreach (Vector2 vector in vectors)
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
            Vector2 actual = Vector2.Zero;
            Assert.Equal(0f, actual.X);
            Assert.Equal(0f, actual.Y);
        }

        [Fact]
        public void One_returns_vector_with_all_components_one()
        {
            Vector2 actual = Vector2.One;
            Assert.Equal(1f, actual.X);
            Assert.Equal(1f, actual.Y);
        }

        [Fact]
        public void UnitX_returns_unit_vector_along_x_axis()
        {
            Vector2 actual = Vector2.UnitX;
            Assert.Equal(1f, actual.X);
            Assert.Equal(0f, actual.Y);
        }

        [Fact]
        public void UnitY_returns_unit_vector_along_y_axis()
        {
            Vector2 actual = Vector2.UnitY;
            Assert.Equal(0f, actual.X);
            Assert.Equal(1f, actual.Y);
        }

        [Fact]
        public void Up_returns_0_1()
        {
            Vector2 actual = Vector2.Up;
            Assert.Equal(0f, actual.X);
            Assert.Equal(1f, actual.Y);
        }

        [Fact]
        public void Down_returns_0_minus1()
        {
            Vector2 actual = Vector2.Down;
            Assert.Equal(0f, actual.X);
            Assert.Equal(-1f, actual.Y);
        }

        [Fact]
        public void Right_returns_1_0()
        {
            Vector2 actual = Vector2.Right;
            Assert.Equal(1f, actual.X);
            Assert.Equal(0f, actual.Y);
        }

        [Fact]
        public void Left_returns_minus1_0()
        {
            Vector2 actual = Vector2.Left;
            Assert.Equal(-1f, actual.X);
            Assert.Equal(0f, actual.Y);
        }

        [Theory]
        [InlineData(0, 1f)]
        [InlineData(1, 2f)]
        public void Indexer_getter_returns_component_at_index(int index, float expected)
        {
            Vector2 value = new Vector2(1f, 2f);
            Assert.Equal(expected, value[index]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Indexer_setter_sets_component_at_index(int index)
        {
            Vector2 value = new Vector2(1f, 2f);
            value[index] = 5f;
            Assert.Equal(5f, value[index]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void Indexer_getter_throws_argument_out_of_range_exception_on_invalid_index(int index)
        {
            Vector2 value = new Vector2(1f, 2f);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = value[index]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void Indexer_setter_throws_argument_out_of_range_exception_on_invalid_index(int index)
        {
            Vector2 value = new Vector2(1f, 2f);
            Assert.Throws<ArgumentOutOfRangeException>(() => value[index] = 42f);
        }

        [Theory]
        [MemberData(nameof(Length_Data))]
        public void Length_returns_magnitude(Vector2 input, float expected)
        {
            float actual = input.Length();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(LengthSquared_Data))]
        public void LengthSquared_returns_squared_magnitude(Vector2 input, float expected)
        {
            float actual = input.LengthSquared();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Dot_Data))]
        public void Dot_returns_dot_product(Vector2 left, Vector2 right, float expected)
        {
            float actual = Vector2.Dot(left, right);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Dot_Data))]
        public void Dot_is_commutative(Vector2 left, Vector2 right, float expected)
        {
            float actual = Vector2.Dot(left, right);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");

            float actualReversedArgs = Vector2.Dot(right, left);
            // Must be exactly equal, not just approximately equal, because multiplying is commutative for floating
            // point numbers, so the result of the dot product must be exactly the same regardless of the order of
            // the arguments.
            Assert.True(actual == actualReversedArgs,
                $"Assert failed. `Dot` should be commutative. " +
                $"actual: {actual}, actualReversedArgs: {actualReversedArgs}");
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Normalize_instance_method_normalizes_vector_in_place(Vector2 input, Vector2 expected)
        {
            Vector2 actual = input;
            actual.Normalize();
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Fact]
        public void Normalize_instance_method_does_not_change_zero_vector()
        {
            Vector2 value = Vector2.Zero;
            value.Normalize();
            EqualsApprox(value, Vector2.Zero, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Normalized_property_returns_normalized_copy(Vector2 input, Vector2 expected)
        {
            Vector2 origValue = input;
            Vector2 actual = origValue.Normalized;
            EqualsApprox(actual, expected, 1e-5f);
            EqualsApprox(origValue, input, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Normalize_Data))]
        public void Static_normalize_returns_normalized_copy(Vector2 input, Vector2 expected)
        {
            Vector2 origValue = input;
            Vector2 actual = Vector2.Normalize(input);
            EqualsApprox(actual, expected, 1e-5f);
            EqualsApprox(origValue, input, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Clamp_Data))]
        public void Clamp_restricts_components_to_range(Vector2 value, Vector2 min, Vector2 max, Vector2 expected)
        {
            Vector2 actual = Vector2.Clamp(value, min, max);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Lerp_Data))]
        public void Lerp_returns_linear_interpolation(Vector2 start, Vector2 end, float amount, Vector2 expected)
        {
            Vector2 actual = Vector2.Lerp(start, end, amount);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Minimize_Data))]
        public void Minimize_returns_component_wise_minimum(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = Vector2.Minimize(left, right);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Maximize_Data))]
        public void Maximize_returns_component_wise_maximum(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = Vector2.Maximize(left, right);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Addition_Data))]
        public void Addition_operator_returns_sum(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = left + right;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Subtraction_Data))]
        public void Subtraction_operator_returns_difference(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = left - right;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Negation_Data))]
        public void Unary_minus_operator_negates_components(Vector2 value, Vector2 expected)
        {
            Vector2 actual = -value;
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Vector_times_scalar_operator_scales_components(Vector2 value, float scale, Vector2 expected)
        {
            Vector2 actual = value * scale;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Scalar_times_vector_operator_scales_components(Vector2 value, float scale, Vector2 expected)
        {
            Vector2 actual = scale * value;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Division_Data))]
        public void Division_operator_scales_components(Vector2 value, float scale, Vector2 expected)
        {
            Vector2 actual = value / scale;
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equality_operator_returns_true_if_all_components_are_equal(Vector2 left, Vector2 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left == right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Inequality_operator_returns_false_if_all_components_are_equal(Vector2 left, Vector2 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.False(left != right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Different))]
        public void Equality_operator_returns_false_if_some_components_are_different(Vector2 left, Vector2 right)
        {
            Assert.False(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.False(left == right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Different))]
        public void Inequality_operator_returns_true_if_some_components_are_different(Vector2 left, Vector2 right)
        {
            Assert.False(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left != right);
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equals_method_returns_true_if_all_components_are_equal(Vector2 left, Vector2 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left.Equals(right));
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void Equals_method_returns_true_if_passed_argument_is_object_casted_from_the_same_vector(Vector2 left, Vector2 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.True(left.Equals((object)right));
        }

        [Theory]
        [MemberData(nameof(Equals_Object_Data_Not_Vector2))]
        public void Equals_method_returns_false_if_passed_argument_is_not_a_Vector2(Vector2 input, object obj)
        {
            Assert.False(obj.GetType() == typeof(Vector2));
            Assert.False(input.Equals(obj));
        }

        [Theory]
        [MemberData(nameof(Equality_Data_Same))]
        public void GetHashCode_returns_same_value_for_equal_vectors(Vector2 left, Vector2 right)
        {
            Assert.True(left.X == right.X &&
                        left.Y == right.Y,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(DistanceTo_Data))]
        public void DistanceTo_returns_distance_to_other_vector(Vector2 from, Vector2 to, float expected)
        {
            float actual = from.DistanceTo(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared_Data))]
        public void DistanceToSquared_returns_squared_distance_to_other_vector(Vector2 from, Vector2 to, float expected)
        {
            float actual = from.DistanceToSquared(to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceTo_Data))]
        public void Static_Distance_returns_distance_between_vectors(Vector2 position1, Vector2 position2, float expected)
        {
            float actual = Vector2.Distance(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(DistanceToSquared_Data))]
        public void Static_DistanceSquared_returns_squared_distance_between_vectors(Vector2 position1, Vector2 position2, float expected)
        {
            float actual = Vector2.DistanceSquared(position1, position2);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-5f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(Angle_Data))]
        public void Angle_returns_angle_in_degrees_between_vectors(Vector2 from, Vector2 to, float expected)
        {
            float actual = Vector2.Angle(from, to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(SignedAngle_Data))]
        public void SignedAngle_returns_signed_angle_in_degrees(Vector2 from, Vector2 to, float expected)
        {
            float actual = Vector2.SignedAngle(from, to);
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Theory]
        [MemberData(nameof(ToHeading_Data))]
        public void ToHeading_returns_heading_in_degrees(Vector2 input, float expected)
        {
            float actual = input.ToHeading();
            Assert.True(System.Math.Abs(actual - expected) <= 1e-4f,
                $"Assert failed. actual: {actual}, expected: {expected}");
        }

        [Fact]
        public void RandomXY_returns_normalized_vector()
        {
            Vector2 actual = Vector2.RandomXY();
            float length = actual.Length();
            Assert.True(System.Math.Abs(length - 1f) <= 1e-5f,
                $"Assert failed. Vector returned by `RandomXY` should be normalized. " +
                $"actual: {actual.ToString()}, length: {length}");
        }

        [Theory]
        [MemberData(nameof(Addition_Data))]
        public void Static_Add_returns_sum(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = Vector2.Add(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Subtraction_Data))]
        public void Static_Subtract_returns_difference(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = Vector2.Subtract(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Scale_Data))]
        public void Static_Multiply_scales_vector_by_scalar(Vector2 value, float scale, Vector2 expected)
        {
            Vector2 actual = Vector2.Multiply(value, scale);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Multiply_ComponentWise_Data))]
        public void Static_Multiply_returns_component_wise_product(Vector2 left, Vector2 right, Vector2 expected)
        {
            Vector2 actual = Vector2.Multiply(left, right);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Division_Data))]
        public void Static_Divide_scales_vector_by_scalar(Vector2 value, float scale, Vector2 expected)
        {
            Vector2 actual = Vector2.Divide(value, scale);
            EqualsApprox(actual, expected, 1e-6f);
        }

        [Theory]
        [MemberData(nameof(Negation_Data))]
        public void Static_Negate_returns_negated_vector(Vector2 value, Vector2 expected)
        {
            Vector2 actual = Vector2.Negate(value);
            EqualsExact(actual, expected);
        }

        [Theory]
        [MemberData(nameof(Reflect_Data))]
        public void Reflect_returns_reflected_vector(Vector2 vector, Vector2 normal, Vector2 expected)
        {
            Assert.True(System.Math.Abs(normal.Length() - 1f) <= 1e-5f,
                $"Assert failed. Normal vector should be normalized. " +
                $"normal: {normal.ToString()}, length: {normal.Length()}");

            Vector2 actual = Vector2.Reflect(vector, normal);
            EqualsApprox(actual, expected, 1e-5f);
        }

        [Theory]
        [MemberData(nameof(Implicit_Cast_To_Vector3_Data))]
        public void Implicit_cast_to_vector3_sets_z_to_zero(Vector2 input, Vector3 expected)
        {
            Vector3 actual = input;
            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
            Assert.Equal(expected.Z, actual.Z);
        }

        [Theory]
        [MemberData(nameof(ToString_Data))]
        public void ToString_without_format_returns_expected_component_labels(Vector2 value)
        {
            string expected = $"X:{value.X.ToString()} Y:{value.Y.ToString()}";
            string actual = value.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ToString_Data))]
        public void ToString_with_null_format_matches_default_ToString(Vector2 value)
        {
            Assert.Equal(value.ToString(), value.ToString(null));
        }

        [Theory]
        [ClassData(typeof(ToStringFormat_Data_Class))]
        public void ToString_with_format_formats_all_components(Vector2 value, string format)
        {
            string expected = $"X:{value.X.ToString(format)} Y:{value.Y.ToString(format)}";
            string actual = value.ToString(format);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(ToStringIFormatProvider_Data_Class))]
        public void ToString_with_format_and_provider_formats_all_components(Vector2 value, string format, IFormatProvider provider)
        {
            string expected = $"X:{value.X.ToString(format, provider)} Y:{value.Y.ToString(format, provider)}";
            string actual = value.ToString(format, provider);
            Assert.Equal(expected, actual);
        }

        private static void EqualsExact(Vector2 left, Vector2 right)
        {
            Assert.True(left == right,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
        }

        private static void EqualsApprox(Vector2 left, Vector2 right, float tolerance)
        {
            Assert.True(System.Math.Abs(left.X - right.X) <= tolerance &&
                        System.Math.Abs(left.Y - right.Y) <= tolerance,
                $"Assert failed. left: {left.ToString()}, right: {right.ToString()}");
        }
    }
}

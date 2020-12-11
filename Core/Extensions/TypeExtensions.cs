// ReSharper disable MemberCanBePrivate.Global
namespace Macabresoft.Core {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensions that apply to <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions {

        /// <summary>
        /// Gets all methods for a <see cref="Type"/> as <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>All methods.</returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this Type type) {
            var result = new List<MethodInfo>();
            var currentType = type;

            while (currentType != null) {
                var methods = currentType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                result.AddRange(methods.Where(x => result.All(y => y.MetadataToken != x.MetadataToken)));
                currentType = currentType.BaseType;
            }

            return result;
        }

        /// <summary>
        /// Gets all methods for a <see cref="Type"/> that have the specified attribute as <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodAttribute">The type of attribute by which to filter methods.</param>
        /// <returns>All methods with the specified attribute.</returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this Type type, Type methodAttribute) {
            return type.GetAllMethods().Where(method => Attribute.IsDefined(method, methodAttribute));
        }

        /// <summary>
        /// Gets all fields and properties for a <see cref="Type"/> that have the specified attribute as <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="memberAttribute">The type of attribute by which to filter members.</param>
        /// <returns>All fields and properties with the specified attribute.</returns>
        public static IEnumerable<MemberInfo> GetAllFieldsAndProperties(this Type type, Type memberAttribute) {
            return type.GetAllFieldsAndProperties().Where(prop => Attribute.IsDefined(prop, memberAttribute));
        }

        /// <summary>
        /// Gets all fields and properties for a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>All fields and properties.</returns>
        public static IEnumerable<MemberInfo> GetAllFieldsAndProperties(this Type type) {
            var result = new List<MemberInfo>();
            var currentType = type;

            while (currentType != null) {
                var fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                var properties = currentType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                result.AddRange(fields.Where(x => result.All(y => y.MetadataToken != x.MetadataToken)));
                result.AddRange(properties.Where(x => result.All(y => y.MetadataToken != x.MetadataToken)));
                currentType = currentType.BaseType;
            }

            return result;
        }

        /// <summary>
        /// Gets the <see cref="MemberInfo"/> that has the specified name for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the member.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfoForFieldOrProperty(this Type type, string name) {
            var currentType = type;
            MemberInfo result = null;
            while (currentType != null && result == null) {
                result = (MemberInfo)currentType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance) ??
                currentType.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                currentType = currentType.BaseType;
            }

            return result;
        }

        /// <summary>
        /// Gets the member return type from a <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>The return type.</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Type GetMemberReturnType(this MemberInfo memberInfo) {
            Type type = null;

            switch (memberInfo.MemberType) {
                case MemberTypes.Constructor:
                    type = memberInfo.DeclaringType;
                    break;

                case MemberTypes.Event:
                    if (memberInfo is EventInfo eventInfo) {
                        type = eventInfo.EventHandlerType;
                    }
                    break;

                case MemberTypes.Field:
                    if (memberInfo is FieldInfo fieldInfo) {
                        type = fieldInfo.FieldType;
                    }
                    break;

                case MemberTypes.Method:
                    if (memberInfo is MethodInfo methodInfo) {
                        type = methodInfo.ReturnType;
                    }
                    break;

                case MemberTypes.Property:
                    if (memberInfo is PropertyInfo propertyInfo) {
                        type = propertyInfo.PropertyType;
                    }
                    break;

                case MemberTypes.NestedType:
                case MemberTypes.TypeInfo:
                    if (memberInfo is TypeInfo typeInfo) {
                        type = typeInfo.UnderlyingSystemType;
                    }
                    break;
            }

            if (type == null) {
                throw new NotSupportedException("You were doing something totally crazy and unexpected.");
            }

            return type;
        }

        /// <summary>
        /// Gets the value for the specified <see cref="MemberInfo"/> on an object.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="owner">The owner of the <see cref="MemberInfo"/>.</param>
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">Can only get value for member types that are properties or fields.</exception>
        public static object GetValue(this MemberInfo memberInfo, object owner) {
            if (memberInfo is PropertyInfo propertyInfo) {
                return propertyInfo.GetValue(owner);
            }
            else if (memberInfo is FieldInfo fieldInfo) {
                return fieldInfo.GetValue(owner);
            }

            throw new NotSupportedException("Can only get value for member types that are properties or fields.");
        }

        /// <summary>
        /// Sets a value for the specified <see cref="MemberInfo"/> on an object.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="owner">The owner of the <see cref="MemberInfo"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="NotSupportedException">Can only set value for member types that are properties or fields.</exception>
        public static void SetValue(this MemberInfo memberInfo, object owner, object value) {
            if (memberInfo is PropertyInfo propertyInfo) {
                propertyInfo.SetValue(owner, value, null);
            }
            else if (memberInfo is FieldInfo fieldInfo) {
                fieldInfo.SetValue(owner, value);
            }
            else {
                throw new NotSupportedException("Can only set value for member types that are properties or fields.");
            }
        }
    }
}
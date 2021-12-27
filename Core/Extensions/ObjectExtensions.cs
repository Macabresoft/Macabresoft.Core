namespace Macabresoft.Core;

using System;
using System.Reflection;

/// <summary>
/// Extensions that apply to any <see cref="object" />.
/// </summary>
public static class ObjectExtensions {
    /// <summary>
    /// Gets a property's value on an object.
    /// </summary>
    /// <param name="baseObject">The object to which the property belongs.</param>
    /// <param name="pathToProperty">The path to the requested property split by '.' when peering deeper into the abyss.</param>
    /// <returns>The value of the property.</returns>
    public static object GetPropertyValue(this object baseObject, string pathToProperty) {
        var splitPath = pathToProperty.Split('.');
        var target = baseObject;

        foreach (var memberName in splitPath) {
            var member = target.GetType().GetMemberInfoForFieldOrProperty(memberName);
            target = member.GetValue(target);
        }

        return target;
    }

    /// <summary>
    /// Sets a property on an object.
    /// </summary>
    /// <param name="baseObject">The object to which the property belongs.</param>
    /// <param name="pathToProperty">The path to the requested property split by '.' when peering deeper into the abyss.</param>
    /// <param name="newValue">The new value.</param>
    /// <exception cref="NotSupportedException">Invalid property path provided.</exception>
    public static void SetProperty(this object baseObject, string pathToProperty, object newValue) {
        var splitPath = pathToProperty.Split('.');
        var target = baseObject;
        MemberInfo member = null;

        for (var i = 0; i < splitPath.Length; i++) {
            var memberName = splitPath[i];
            member = target.GetType().GetMemberInfoForFieldOrProperty(memberName);
            if (member != null && i < splitPath.Length - 1) {
                target = member.GetValue(target);
            }
            else {
                break;
            }
        }

        if (member != null) {
            member.SetValue(target, newValue);
        }
        else {
            throw new NotSupportedException("Invalid property path provided.");
        }
    }
}
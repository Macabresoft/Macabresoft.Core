namespace Macabresoft.Core;

using System.IO;
using System.Linq;

/// <summary>
/// A helper for <see cref="Path" />.
/// </summary>
public static class PathHelper {
    /// <summary>
    /// The string that will move a path up one directory.
    /// </summary>
    public const string UpOneDirectory = "..";

    /// <summary>
    /// Gets the path to an ancestor directory, using 'up one level' syntax and Path.Combine(...).
    /// </summary>
    /// <param name="numberOfLevelsToMoveUp">The number of levels to move up.</param>
    /// <returns>The path string that will bring the user up the specified number of levels.</returns>
    public static string GetPathToAncestorDirectory(byte numberOfLevelsToMoveUp) {
        var individualUps = Enumerable.Repeat(UpOneDirectory, numberOfLevelsToMoveUp).ToArray();
        return Path.Combine(individualUps);
    }
}
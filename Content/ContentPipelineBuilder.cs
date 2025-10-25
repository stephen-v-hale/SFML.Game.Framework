
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace SFML.Game.Framework.Content;

#nullable disable

/// <summary>
/// Builds a custom binary asset file (.as) that stores multiple game files without extensions.
/// </summary>
public class ContentPipelineBuilder
{
    private readonly List<string> files = new();

    /// <summary>
    /// Adds a file to the asset build list.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    public void AddFile( string path )
    {
        if ( !File.Exists( path ) )
            throw new FileNotFoundException( $"File not found: {path}" );

        files.Add( path );
    }

    /// <summary>
    /// Deletes a file from the build list by its current file name (without extension).
    /// </summary>
    /// <param name="name">The name of the file to delete (without extension).</param>
    public void DeleteItem( string name )
    {
        var item = files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f).Equals(name, StringComparison.OrdinalIgnoreCase));
        if ( item != null )
        {
            files.Remove( item );
        }
        else
        {
            throw new ArgumentException( $"File '{name}' not found in the build list." );
        }
    }

    /// <summary>
    /// Renames a file in the build list (does not rename the actual file on disk).
    /// </summary>
    /// <param name="oldName">Current file name (without extension).</param>
    /// <param name="newName">New name to use in the asset package (without extension).</param>
    public void RenameItem( string oldName, string newName )
    {
        int index = files.FindIndex(f => Path.GetFileNameWithoutExtension(f).Equals(oldName, StringComparison.OrdinalIgnoreCase));
        if ( index >= 0 )
        {
            string fullPath = files[index];
            string dir = Path.GetDirectoryName(fullPath);
            string ext = Path.GetExtension(fullPath);

            // Keep the original file path but use new name in the build process
            string newFilePath = Path.Combine(dir ?? string.Empty, newName + ext);
            files[index] = newFilePath;
        }
        else
        {
            throw new ArgumentException( $"File '{oldName}' not found in the build list." );
        }
    }

    /// <summary>
    /// Builds and writes all added files into one custom binary .as file.
    /// </summary>
    /// <param name="outputPath">Path to the .as file to generate.</param>
    public void Build( string outputPath )
    {
        using BinaryWriter writer = new(File.Open(outputPath, FileMode.Create));

        // Write custom file header
        writer.Write( Encoding.ASCII.GetBytes( "ASSETBIN" ) ); // 8-byte header
        writer.Write( ( byte )1 ); // version 1
        writer.Write( files.Count );

        foreach ( string filePath in files )
        {
            // Get file name without extension
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            byte[] nameBytes = Encoding.UTF8.GetBytes(fileName);
            byte[] fileData = File.ReadAllBytes(filePath);

            writer.Write( ( ushort )nameBytes.Length );
            writer.Write( nameBytes );
            writer.Write( ( long )fileData.Length );
            writer.Write( fileData );

            File.Delete( filePath );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Content;


#nullable disable

/// <summary>
/// Reads and retrieves assets from a custom .as file built by <see cref="AssetBuilder"/>.
/// </summary>
public class ContentPipelineReader
{
    private readonly string filePath;
    private readonly Dictionary<string, FileEntry> fileIndex = new();

    /// <summary>
    /// Represents a file entry inside the .as file.
    /// </summary>
    private class FileEntry
    {
        public long DataOffset;
        public long DataLength;
    }

    /// <summary>
    /// Initializes and indexes a .as asset file.
    /// </summary>
    /// <param name="filePath">Path to the .as file.</param>
    public ContentPipelineReader( string filePath )
    {
        this.filePath = filePath;
        IndexFile();
    }

    /// <summary>
    /// Reads a file from the .as package by name and returns its raw bytes.
    /// </summary>
    /// <param name="name">The name of the asset file (without extension).</param>
    /// <returns>Byte array containing the file data.</returns>
    public byte[] GetByName( string name )
    {
        if ( !fileIndex.TryGetValue( name, out FileEntry entry ) )
            throw new FileNotFoundException( $"Asset '{name}' not found in {Path.GetFileName( filePath )}" );

        using BinaryReader reader = new(File.OpenRead(filePath));
        reader.BaseStream.Position = entry.DataOffset;
        return reader.ReadBytes( ( int )entry.DataLength );
    }


    /// <summary>
    /// Returns all asset names and their sizes in bytes.
    /// </summary>
    /// <returns>A dictionary where the key is the asset name and the value is its size in bytes.</returns>
    public Dictionary<string, long> GetAllAssets()
    {
        Dictionary<string, long> assets = new Dictionary<string, long>();

        foreach ( var kvp in fileIndex )
        {
            assets[kvp.Key] = kvp.Value.DataLength;
        }

        return assets;
    }

    /// <summary>
    /// Scans the .as file and builds an index of all files inside.
    /// </summary>
    private void IndexFile()
    {
        using BinaryReader reader = new(File.OpenRead(filePath));

        // Validate header
        string header = Encoding.ASCII.GetString(reader.ReadBytes(8));
        if ( header != "ASSETBIN" )
        {
            Game.logger.Write( "Invaild .as file header", Logging.EntryType.Fatel, this );
            throw new InvalidDataException( "Invalid .as file header." );
        }

        byte version = reader.ReadByte();
        int fileCount = reader.ReadInt32();

        for ( int i = 0; i < fileCount; i++ )
        {
            ushort nameLength = reader.ReadUInt16();
            string name = Encoding.UTF8.GetString(reader.ReadBytes(nameLength));
            long length = reader.ReadInt64();

            long dataOffset = reader.BaseStream.Position;

            // Store entry for quick lookup
            fileIndex[name] = new FileEntry
            {
                DataOffset = dataOffset,
                DataLength = length
            };

            Game.logger.Write( $"Asset '{name}' loaded with size {length}@{dataOffset} from {filePath}", this );

            // Skip file data
            reader.BaseStream.Position += length;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Logging;

public class Logger
{
    /// <summary>
    /// Gets the start time of this log.
    /// </summary>
    public DateTime StartTime { get; }

    private List<Entry> logs = new List<Entry>();

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets this <see cref="Logger"/> outputs the log file.
    /// </summary>
    public VerboseType Type { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="Logger"/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Logger( string name, VerboseType type )
    {
        if ( string.IsNullOrWhiteSpace( name ) ) throw new ArgumentNullException( name );

        this.Name = name;
        this.Type = type;

        StartTime = DateTime.Now;

        Console.WriteLine( "======================================================================" );
        Console.WriteLine( "Log Started @ " + StartTime.ToString() + " With name '" + Name + "'" );
        Console.WriteLine( "======================================================================" );
    }

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="level"></param>
    /// <param name="sender"></param>
    public void Write( string message, EntryType level, object sender )
    {
        logs.Add( new Entry() { Message = message, Level = level, Sender = sender } );

        Console.ForegroundColor = level switch
        {
            EntryType.Information=> ConsoleColor.Gray,
            EntryType.Fatel => ConsoleColor.Red,
            EntryType.Warning => ConsoleColor.Yellow,
            EntryType.Critical => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White,
        };

        Console.WriteLine( message );
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    public void Write( string message, EntryType level ) => Write( message, level, "No Object Given" );

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    public void Write( string message ) => Write( message, EntryType.Information, "No Object Given" );

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    public void Write( string message, object sender ) => Write( message, EntryType.Information, sender );

    /// <summary>
    /// Saves this <see cref="Logger"/>
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SaveToFile( string fileName )
    {
        if ( string.IsNullOrWhiteSpace( fileName ) ) throw new ArgumentNullException( nameof( fileName ) );

        StreamWriter writer = new StreamWriter(fileName);
        if ( Type == VerboseType.Detailed )
        {
            writer.WriteLine( "======================================================================" );
            writer.WriteLine( "Log Started @ " + StartTime.ToString() + " With name '" + Name+"'" );
            writer.WriteLine( "======================================================================" );

        }
        foreach ( var entry in logs )
        {
            switch ( Type )
            {
                case VerboseType.Simple:
                writer.WriteLine( entry.Message );
                break;
                case VerboseType.Detailed:
                writer.WriteLine( $"{entry.CreationDate}: {entry.Level.ToString()} ({entry.Sender.ToString()}) {entry.Message}" );
                break;
            }
        }
        writer.Close();

    }
}

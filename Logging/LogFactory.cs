using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Logging;

#nullable disable
public static class LogFactory
{
    private static List<Logger> loggers = new List<Logger>();

    /// <summary>
    /// Creates a logger
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    public static void CreateLogger( string name, VerboseType type ) => loggers.Add( new Logger( name, type ) );

    /// <summary>
    /// Creates a logger
    /// </summary>
    public static void CreateLogger( string name ) => CreateLogger( name, VerboseType.Detailed);

    /// <summary>
    /// Gets a specific <see cref="Logger"/> from this <see cref="LogFactory"/>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Logger GetLogger(string name)
    {
        foreach ( var log in loggers )
            if ( log.Name == name )
                return log;

        return null;
    }
}

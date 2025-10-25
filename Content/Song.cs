using System;
using System.Collections.Generic;
using System.Text;

using SFML.Audio;

namespace SFML.Game.Framework.Content;

/// <summary>
/// Defines a section of a song that can be used for looping.
/// </summary>
public struct SongLoopPoint
{
    /// <summary>
    /// The total duration of the looping segment.
    /// </summary>
    public TimeSpan Length;

    /// <summary>
    /// The starting point of the looping segment.
    /// </summary>
    public TimeSpan Start;
}

/// <summary>
/// Represents a streamed music track (song) that can be played, looped, or controlled.
/// Uses <see cref="SFML.Audio.Music"/> internally for efficient disk streaming.
/// </summary>
public class Song : IDisposable
{
    private readonly Music music;

    /// <summary>
    /// Gets or sets the playback volume of the song (0–100).
    /// </summary>
    public float Volume
    {
        get => music.Volume;
        set => music.Volume = Math.Clamp( value, 0f, 100f );
    }

    /// <summary>
    /// Gets or sets the playback pitch (speed multiplier).
    /// 1.0 = normal pitch, higher = faster playback.
    /// </summary>
    public float Pitch
    {
        get => music.Pitch;
        set => music.Pitch = value;
    }

    /// <summary>
    /// Gets or sets whether the song should automatically loop when it finishes.
    /// </summary>
    public bool Loop
    {
        get => music.Loop;
        set => music.Loop = value;
    }

    /// <summary>
    /// Gets a value indicating whether the song has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the total duration of the song.
    /// </summary>
    public TimeSpan Duration => TimeSpan.FromSeconds( music.Duration.AsSeconds() );

    /// <summary>
    /// Gets the minimum distance for 3D spatial attenuation (used if positional audio is applied).
    /// </summary>
    public float MinDistance => music.MinDistance;

    /// <summary>
    /// Gets or sets the starting offset (in time) for playback.
    /// </summary>
    public TimeSpan StartOffset
    {
        get => TimeSpan.FromSeconds( music.PlayingOffset.AsSeconds() );
        set => music.PlayingOffset = SFML.System.Time.FromSeconds( ( float )value.TotalSeconds );
    }

    /// <summary>
    /// Gets or sets the looping points within the song.
    /// </summary>
    public SongLoopPoint LoopPoints
    {
        get => new SongLoopPoint()
        {
            Length = TimeSpan.FromSeconds( music.LoopPoints.Length.AsSeconds() ),
            Start = TimeSpan.FromSeconds( music.LoopPoints.Offset.AsSeconds() )
        };
        set => music.LoopPoints = new Music.TimeSpan(
            SFML.System.Time.FromSeconds( ( float )value.Start.TotalSeconds ),
            SFML.System.Time.FromSeconds( ( float )value.Length.TotalSeconds )
        );
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Song"/> from a file on disk.
    /// </summary>
    /// <param name="fileName">Path to the music file (e.g., .ogg, .wav, .flac).</param>
    /// <exception cref="ArgumentNullException">Thrown if fileName is null or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    public Song( string fileName )
    {
        if ( string.IsNullOrWhiteSpace( fileName ) )
            throw new ArgumentNullException( nameof( fileName ) );
        if ( !File.Exists( fileName ) )
            throw new FileNotFoundException( fileName );

        music = new Music( fileName );

        Game.logger.Write( "Song loaded from " + fileName, this );
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Song"/> from raw memory data.
    /// </summary>
    /// <param name="data">The byte array containing the music data.</param>
    /// <exception cref="ArgumentNullException">Thrown if data is null.</exception>
    public Song( byte[] data )
    {
        if ( data == null )
            throw new ArgumentNullException( nameof( data ) );

        music = new Music( data );

        Game.logger.Write( "Song loaded from byte[] with a length of " + data.Length + "'", this );
    }

    /// <summary>
    /// Begins playback of the song.
    /// </summary>
    public void Play() => music.Play();

    /// <summary>
    /// Stops playback of the song and resets it to the beginning.
    /// </summary>
    public void Stop() => music.Stop();

    /// <summary>
    /// Releases all unmanaged resources associated with this song.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the song has already been disposed.</exception>
    public void Dispose()
    {
        if ( IsDisposed )
            throw new ObjectDisposedException( nameof( Song ) );

        music.Dispose();
        IsDisposed = true;
    }
}
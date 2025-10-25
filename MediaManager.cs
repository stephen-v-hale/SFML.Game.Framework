using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;

namespace SFML.Game.Framework;
#nullable disable
/// <summary>
/// Manages all media assets such as <see cref="SoundEffect"/>s and <see cref="Song"/>s.
/// Provides methods to add, remove, retrieve, and play sounds or songs by name.
/// </summary>
public class MediaManager
{
    private Dictionary<string, SoundEffect> soundEffect = new Dictionary<string, SoundEffect>();
    private Dictionary<string, Song> songs = new Dictionary<string, Song>();

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaManager"/> class.
    /// </summary>
    public MediaManager()
    {
        Game.logger.Write( "Media manager initalized", this );
    }

    /// <summary>
    /// Adds a sound effect to the manager.
    /// </summary>
    /// <param name="soundEffectName">The unique name of the sound effect.</param>
    /// <param name="effect">The <see cref="SoundEffect"/> instance.</param>
    public void Add( string soundEffectName, SoundEffect effect ) => soundEffect.Add( soundEffectName, effect );

    /// <summary>
    /// Adds a song to the manager.
    /// </summary>
    /// <param name="songName">The unique name of the song.</param>
    /// <param name="song">The <see cref="Song"/> instance.</param>
    public void Add( string songName, Song song ) => songs.Add( songName, song );

    /// <summary>
    /// Removes a sound effect from the manager.
    /// </summary>
    /// <param name="soundEffectName">The name of the sound effect to remove.</param>
    public void RemoveSound( string soundEffectName ) => soundEffect.Remove( soundEffectName );

    /// <summary>
    /// Removes a song from the manager.
    /// </summary>
    /// <param name="songName">The name of the song to remove.</param>
    public void RemoveSong( string songName ) => songs.Remove( songName );

    /// <summary>
    /// Retrieves a song by name.
    /// </summary>
    /// <param name="songName">The name of the song.</param>
    /// <returns>The <see cref="Song"/> instance.</returns>
    /// <exception cref="Exception">Thrown if the song does not exist.</exception>
    public Song GetSong( string songName )
    {
        if ( songs.TryGetValue( songName, out Song value ) )
            return value;

        Game.logger.Write( $"Unable to find song with name '{songName}'", Logging.EntryType.Critical, this );
        throw new Exception( $"Unable to find song with name '{songName}'" );
    }

    /// <summary>
    /// Retrieves a sound effect by name.
    /// </summary>
    /// <param name="soundEffectName">The name of the sound effect.</param>
    /// <returns>The <see cref="SoundEffect"/> instance.</returns>
    /// <exception cref="Exception">Thrown if the sound effect does not exist.</exception>
    public SoundEffect GetSoundEffect( string soundEffectName )
    {
        if ( soundEffect.TryGetValue( soundEffectName, out SoundEffect value ) )
            return value;

        Game.logger.Write( $"Unable to find sound with name '{soundEffectName}'", Logging.EntryType.Critical, this );
        throw new Exception( $"Unable to find sound with name '{soundEffectName}'" );
    }

    /// <summary>
    /// Plays a sound effect by name.
    /// </summary>
    /// <param name="soundEffectName">The name of the sound effect to play.</param>
    public void PlaySE( string soundEffectName )
    {
        try
        {
            var sound = GetSoundEffect(soundEffectName);
            sound.Play();
        }
        catch ( Exception ex )
        {
            Game.logger.Write( $"Unable to play sound effect with name '{soundEffectName}'\nMessage: {ex.Message}", this );
        }
    }

    /// <summary>
    /// Plays a song by name.
    /// </summary>
    /// <param name="songName">The name of the song to play.</param>
    public void Play( string songName )
    {
        try
        {
            var song = GetSong(songName);
            song.Play();
        }
        catch ( Exception ex )
        {
            Game.logger.Write( $"Unable to play song with name '{songName}'\nMessage: {ex.Message}", this );
        }
    }

    /// <summary>
    /// Sets the volume for a sound effect or song by name.
    /// </summary>
    /// <param name="name">The name of the media.</param>
    /// <param name="volume">The volume value (0.0f to 100.0f).</param>
    /// <param name="isSoundEffect">True if adjusting a sound effect, false if a song.</param>
    public void SetVolume( string name, float volume, bool isSoundEffect )
    {
        try
        {
            if ( isSoundEffect )
                GetSoundEffect( name ).Volume = volume;
            else
                GetSong( name ).Volume = volume;
        }
        catch ( Exception ex )
        {
            var type = isSoundEffect ? "sound" : "song";
            Game.logger.Write( $"Unable to set volume for {type} '{name}'\nMessage: {ex.Message}", this );
        }
    }
}
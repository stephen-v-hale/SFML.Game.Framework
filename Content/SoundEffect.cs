using System;
using System.Collections.Generic;
using System.Text;

using SFML.Audio;

namespace SFML.Game.Framework.Content;

public class SoundEffect
{
    SoundBuffer soundBuffer;
    Sound sound;

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    public float Volume
    {
        get => sound.Volume;
        set => sound.Volume = value;
    }

    /// <summary>
    /// Gets or sets the pitch.
    /// </summary>
    public float Pitch
    {
        get => sound.Pitch;
        set => sound.Pitch = value;
    }

    /// <summary>
    /// Gets or sets whether to loop this <see cref="SoundEffect"/>
    /// </summary>
    public bool Loop
    {
        get => sound.Loop;
        set => sound.Loop = value;
    }

    /// <summary>
    /// Gets whether this <see cref="SoundEffect"/> has been disposed.
    /// </summary>
    public bool IsDisposed
    {
        get;
        private set;
    }

    /// <summary>
    /// Initalize a new instance of <see cref="SoundEffect"/>
    /// </summary>
    /// <param name="soundEffectPath">The file name of the sound effect.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    public SoundEffect(string soundEffectPath)
    {
        if ( string.IsNullOrWhiteSpace( soundEffectPath ) ) throw new ArgumentNullException( nameof( soundEffectPath ) );
        if ( !File.Exists( soundEffectPath ) ) throw new FileNotFoundException( soundEffectPath );

        soundBuffer = new SoundBuffer( soundEffectPath );
        sound = new Sound( soundBuffer );

        Game.logger.Write( "SoundEffect loaded from " + soundEffectPath, this );
    }

    /// <summary>
    /// Initialize a new instance of <see cref="SoundEffect"/>
    /// </summary>
    /// <param name="soundData">The byte[] array that contains the <see cref="SoundEffect"/> data.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SoundEffect( byte[] soundData )
    {
        if ( soundData == null )
            throw new ArgumentNullException( nameof( soundData ) );

        soundBuffer = new SoundBuffer( soundData );
        sound = new Sound( soundBuffer );

        Game.logger.Write( "SoundEffect loaded from bytes[] of length '" + soundData.Length + "'", this );
    }

    /// <summary>
    /// Play this <see cref="SoundEffect"/>
    /// </summary>
    public void Play() => sound.Play();

    /// <summary>
    /// Stops this <see cref="SoundEffect"/>
    /// </summary>
    public void Stop() => sound.Stop();

    /// <summary>
    /// Disposes this <see cref="SoundEffect"/>
    /// </summary>
    /// <exception cref="ObjectDisposedException"></exception>
    public void Dispose()
    {
        if ( !IsDisposed )
        {
            soundBuffer.Dispose();
            sound.Dispose();

            IsDisposed = true;
        }
        else
            throw new ObjectDisposedException(nameof(SoundEffect));
    }
}


using System.ComponentModel;

namespace SFML.Game.Framework.Components;


public class TimerComponent : IGameComponent
{
    private float _intervalTick =0;
    /// <summary>
    /// Raised when this <see cref="TimerComponent"/> passes the <see cref="TimerComponent.Interval"/>
    /// </summary>
    public event GameComponentEventHandler Tick;

    public bool IsDisposed => false;
    public Game Game { get; }

    /// <summary>
    /// Gets or sets whether this <see cref="TimerComponent"/> is enabled.
    /// </summary>
    /// <remarks>True; if <see cref="TimerComponent"/> is enabled; otherwise false.</remarks>
    public bool Enabled { get; set; } = true;

#nullable disable

    /// <summary>
    /// Initialize a new instance of <see cref="TimerComponent"/>
    /// </summary>
    /// <param name="game">The game this <see cref="IGameComponent"/> is child of.</param>
    public TimerComponent(Game game)
    {
        this.Game = game;
    }
    /// <summary>
    /// Gets or sets the amount of seconds has to pass before this <see cref="Timer"/> send the Tick Event.
    /// </summary>
    [DefaultValue(100f)]
    public float Interval
    {
        get;
        set;
    }

    public void Dispose()
    {
        return;
    }

    public void Initialize()
    {
    }

    public void LoadContent()
    {
    }

    public void Update( GameTime gameTime )
    {
        if ( Enabled )
        {
            _intervalTick += gameTime.Delta;

            if ( _intervalTick >= Interval )
            {
                Tick?.Invoke();

                _intervalTick = 0;
            }
        }
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    /// <remarks>Does the same as <see cref="TimerComponent.Enabled"/> is equal to <see cref="true"/></remarks>
    public void Start() => Enabled = true;

    /// <summary>
    /// Stops the timer.
    /// </summary>
    /// <remarks>Does the same as <see cref="TimerComponent.Enabled"/> is equal to <see cref="false"/></remarks>
    public void Stop() => Enabled = false;
}

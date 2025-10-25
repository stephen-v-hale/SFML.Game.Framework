/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: ControlBase.cs
 *-------------------------------------------------
*/
#nullable disable
using SFML.Game.Framework.Content;
using SFML.Game.Framework.Graphics;
using SFML.Game.Framework.Input;
using SFML.Game.Framework.Input.Devices;

namespace SFML.Game.Framework.Scene.UI;

/// <summary>
/// Represents event arguments for a key event on a control.
/// </summary>
public record class ControlKeyEventArgs( ControlBase Sender, Keys Key );
/// <summary>
/// Represents event arguments for a mouse event on a control.
/// </summary>
public record class ControlMouseEventArgs( ControlBase Sender, Point Position, MouseButton buttonsPressed );
/// <summary>
/// Represents a delegate for mouse event handlers.
/// </summary>
public delegate void MouseEventHandler( ControlMouseEventArgs e );
/// <summary>
/// Represents a delegate for key event handlers.
/// </summary>
public delegate void KeyEventHandler( ControlKeyEventArgs e );
/// <summary>
/// Represents a delegate for control event handlers.
/// </summary>
public delegate void ControlEventHandler();

/// <summary>
/// Provides a base class for UI controls, defining common properties and abstract methods for drawing, updating, and input handling.
/// </summary>
public abstract class ControlBase
{
    private bool isMouseOver = false;
    private Point lastMousePos = Point.Zero;

    /// <summary>
    /// Occurs when the control is clicked.
    /// </summary>
    public event ControlEventHandler Click;
    /// <summary>
    /// Occurs when a key is pressed while the control is focused.
    /// </summary>
    public event KeyEventHandler KeyDown;
    /// <summary>
    /// Occurs when a key is released while the control is focused.
    /// </summary>
    public event KeyEventHandler KeyUp;
    /// <summary>
    /// Occurs when a mouse button is pressed over the control.
    /// </summary>
    public event MouseEventHandler MouseDown;
    /// <summary>
    /// Occurs when a mouse button is released over the control.
    /// </summary>
    public event MouseEventHandler MouseUp;
    /// <summary>
    /// Occurs when the mouse pointer moves over the control.
    /// </summary>
    public event MouseEventHandler MouseMove;
    /// <summary>
    /// Occurs when the mouse pointer leaves the control.
    /// </summary>
    public event MouseEventHandler MouseLeave;
    /// <summary>
    /// Occurs when the mouse pointer hovers over the control.
    /// </summary>
    public event MouseEventHandler MouseHover;

    /// <summary>
    /// Gets or sets the position of the control.
    /// </summary>
    public Point Position
    {
        get;
        set;
    } = Point.Zero;

    /// <summary>
    /// Gets or sets the font used by the control.
    /// </summary>
    public Font Font
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the text displayed by the control.
    /// </summary>
    public string Text
    {
        get;
        set;
    } = "No Text";

    /// <summary>
    /// Gets or sets the foreground color of the control.
    /// </summary>
    public Color Foreground
    {
        get;
        set;
    } = Color.White;

    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    public Color Background
    {
        get;
        set;
    } = Color.Transparent;

    /// <summary>
    /// Initializes a new instance of the <see cref="ControlBase"/> class.
    /// </summary>
    public ControlBase() { }

    /// <summary>
    /// Draws the control using the specified frame time and graphics batch.
    /// </summary>
    /// <param name="frameTime">The timing information for the current frame.</param>
    /// <param name="graphicsBatch">The graphics batch used for drawing.</param>
    public abstract void Draw( GameTime frameTime, GraphicsDrawer graphicsBatch );

    /// <summary>
    /// Updates the control's state using the specified frame time.
    /// </summary>
    /// <param name="frameTime">The timing information for the current frame.</param>
    public abstract void Update( GameTime frameTime );

    /// <summary>
    /// Handles input for the control using the specified frame time.
    /// </summary>
    public virtual void HandleInput()
    {
        var keyboard = InputSystem.GetDevice<Keyboard>();
        var mouse = InputSystem.GetDevice<Mouse>();

        if ( keyboard == null ) return;
        if ( mouse == null ) return;

        string[] keys = Enum.GetNames(typeof(Keys));
        string[] mouseButton = Enum.GetNames(typeof(MouseButton));

        foreach ( var key in keys )
        {
            var k = (Keys)Enum.Parse(typeof(Keys), key);

            if ( keyboard.IsKeyDown( key ) ) KeyDown?.Invoke( new ControlKeyEventArgs( this, k ) );
            if ( keyboard.IsKeyUp( key ) ) KeyUp?.Invoke( new ControlKeyEventArgs( this, k ) );
        }

        if ( isMouseOver )
        {
            foreach ( var button in mouseButton )
            {
                var mb = (MouseButton)Enum.Parse(typeof(MouseButton), button);
                if ( mouse.IsButtonDown( mb ) ) MouseDown?.Invoke( new ControlMouseEventArgs( this, mouse.Position, mb ) );
                if ( !mouse.IsButtonDown( mb ) ) MouseUp?.Invoke( new ControlMouseEventArgs( this, mouse.Position, mb ) );
            }
        }
        else
        {
            MouseLeave?.Invoke( new ControlMouseEventArgs( this, mouse.Position, MouseButton.None ) );
        }

        var mouseBounds = new Rectangle(
            0, 0, 
            (int)mouse.Position.X, 
            (int)mouse.Position.Y
        );

        var bounds = new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            (int)Font.MeasureString(Text).X,
            (int)Font.MeasureString(Text).Y
         );

        if ( mouseBounds.Intersects( bounds ) )
        {
            isMouseOver = true;

            MouseHover?.Invoke( new ControlMouseEventArgs( this, mouse.Position, MouseButton.None ) );

            if ( lastMousePos.X != mouse.Position.X || lastMousePos.Y != mouse.Position.Y )
            {
                MouseMove?.Invoke( new ControlMouseEventArgs( this, mouse.Position, MouseButton.None ) );
                lastMousePos = mouse.Position;
            }

            if ( mouse.IsButtonDown( MouseButton.Left ) )
            {
                Click?.Invoke();
            }
        }
    }

    /// <summary>
    /// Initializes the control. Can be overridden by derived classes.
    /// </summary>
    public virtual void Initialize() { }
}

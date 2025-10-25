/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: KeyboardState.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;

public class KeyboardState
{
    private bool[] _pressedKeys;
    private int _count = 101;

    /// <summary>
    /// Initalize a new instance of <see cref="KeyboardState"/>
    /// </summary>
    public KeyboardState()
    {
        _pressedKeys = new bool[_count];
    }

    /// <summary>
    /// Gets the current states.
    /// </summary>
    public void GetState()
    {
        for(int i = 0; i < _pressedKeys.Length -1;i++)
        {
            _pressedKeys[i] = SFML.Window.Keyboard.IsKeyPressed((SFML.Window.Keyboard.Key)i);
        }
    }

    /// <summary>
    /// Gets a bool value indicating whether a <see cref="Keys"/> has been pressed.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsKeyDown( Keys key ) => _pressedKeys[( int )key];

    /// <summary>
    /// Gets a bool value indicating whether a <see cref="Keys"/> are not being pressed.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsKeyUp(Keys key) => !_pressedKeys[( int )key];
}
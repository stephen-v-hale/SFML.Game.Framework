using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Input.Devices;

namespace SFML.Game.Framework.Input.Actions;

public class KeyboardAction : InputAction
{
    Keyboard keyboard;

    /// <summary>
    /// Gets the <see cref="List{Keys}"/>.
    /// </summary>
    public List<Keys> Keys { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="KeyboardAction"/>
    /// </summary>
    /// <param name="deviceIndex"></param>
    public KeyboardAction(InputDeviceIndex deviceIndex = InputDeviceIndex.One)
    {
        keyboard = new Keyboard( "k", deviceIndex );
        Keys = new List<Keys>();
    }

    public override bool Occured()
    {
        keyboard.Update();

        foreach ( var key in Keys )
            if ( keyboard.IsKeyDown( key ) )
                return true;

        return false;
    }
}

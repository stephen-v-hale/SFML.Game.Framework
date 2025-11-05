using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Input.Devices;

namespace SFML.Game.Framework.Input.Actions;

public  class ControllerAction : InputAction
{
    private Controller _controller;

    /// <summary>
    /// Gets the <see cref="Controller"/>
    /// </summary>
    public Controller Controller => _controller;

    /// <summary>
    /// Gets the <see cref="List{ControllerButton}"/>
    /// </summary>
    public List<ControllerButton> Buttons { get; }

    /// <summary>
    /// Initalize a new instance of <see cref="ControllerAction"/>
    /// </summary>
    /// <param name="index"></param>
    public ControllerAction(InputDeviceIndex index = InputDeviceIndex.One)
    {
        this._controller = new Controller( "c", index );
        this.Buttons = new List<ControllerButton>();
    }

    public override bool Occured()
    {
        Controller.Update();

        foreach ( var button in Buttons )
            if ( Controller.IsButtonDown( button ) )
                return true;

        return false;
    }
}

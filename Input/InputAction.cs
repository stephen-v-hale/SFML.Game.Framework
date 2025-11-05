using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Input;

public abstract class InputAction
{
    /// <summary>
    /// Raised when the specific input action was made.
    /// </summary>
    /// <returns>True; if action completed.; otherwise false.</returns>
    public abstract bool Occured();
}

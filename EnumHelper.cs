using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

internal class EnumHelper
{
    public static T Parse<T>(string str)
    {
        return ( T )Enum.Parse( typeof( T ), str );
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Animation;

using SFML.Game.Framework.Content;

public class Animation
{
    public string Name { get; }
    public List<AnimationFrame> Frames { get; } = new List<AnimationFrame>();
    public bool Loop { get; set; } = true;

    public Animation( string name )
    {
        Name = name;
    }

    public void AddFrame( AnimationFrame frame )
    {
        Frames.Add( frame );

        Game.logger.Write( "Animation frame added to '" + Name + "'", this );
    }

    public void AddFrame( Texture2D texture, float duration )
    {
        Frames.Add( new AnimationFrame( texture, duration ) );

        Game.logger.Write( "Animation frame added to '" + Name + "'", this );
    }
}
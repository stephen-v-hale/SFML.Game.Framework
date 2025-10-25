using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Animation;
using SFML.Game.Framework.Graphics;

public class AnimationManager
{
    private readonly Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    public Animator Animator { get; } = new Animator();

    public AnimationManager() => Game.logger.Write( "AnimationManager Initalized", this );

    public void AddAnimation( Animation animation )
    {
        animations[animation.Name] = animation;

        Game.logger.Write( "Animation Added", this );
    }

    public void Play( string name )
    {
        if ( animations.TryGetValue( name, out var anim ) )
        {
            Animator.Play( anim );

            Game.logger.Write( $"Animation '{name}' playing", this );
        }
    }

    public void Update( float delta )
    {
        Animator.Update( delta );
    }

    public void Draw( GraphicsDrawer drawer )
    {
        Animator.Draw( drawer );
    }
}


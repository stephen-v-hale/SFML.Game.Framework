using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using SFML.Game.Framework.Content;
using SFML.Game.Framework.PostProcessing;
using SFML.Graphics;
using SFML.System;

namespace SFML.Game.Framework.Graphics;
#nullable disable
public class GraphicsDrawer : IDisposable
{
    private RenderStates _states;
    private bool _begin = false;
    private RenderTarget2D _target = null;



    /// <summary>
    /// Gets the <see cref="GraphicsDevice"/>
    /// </summary>
    public IGraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets a bool value indicating whether this <see cref="GraphicsDrawer"/> has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Initialize a new instance of <see cref="GraphicsDrawer"/>
    /// </summary>
    /// <param name="graphicsDevice"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public GraphicsDrawer( IGraphicsDevice graphicsDevice )
    {
        if ( graphicsDevice == null )
            throw new ArgumentNullException( nameof( graphicsDevice ) );

        this.GraphicsDevice = graphicsDevice;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin()
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = null;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = null;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( PostProcessingEffect effect )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = effect.Shader.SFMLShader;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, PostProcessingEffect effect )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = effect.Shader.SFMLShader;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, PostProcessingEffect effect, RenderTarget2D target )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = target.Texture.texture;
        _states.Shader = effect.Shader.SFMLShader;
        _target = target;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, PostProcessingEffect effect, RenderTarget2D target, Transform2D transform)
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = transform.Transform;
        _states.Texture = target.Texture.texture;
        _states.Shader = effect.Shader.SFMLShader;
        _target = target;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, SFML.Game.Framework.Content.Shader shader, Transform2D transform, Texture2D texture )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = transform.Transform;
        _states.Texture = texture.texture;
        _states.Shader = shader.SFMLShader;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, SFML.Game.Framework.Content.Shader shader, Transform2D transform, RenderTarget2D target )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = transform.Transform;
        _states.Texture = target.Texture.texture;
        _states.Shader = shader.SFMLShader;

        _target = target;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, SFML.Game.Framework.Content.Shader shader, RenderTarget2D target )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = target.Texture.texture;
        _states.Shader = shader.SFMLShader;
        _target = target;
        _begin = true;
    }



    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, RenderTarget2D target )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = target.Texture.texture;
        _states.Shader = null;
        _target = target;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( RenderTarget2D target )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = Transform.Identity;
        _states.Texture = target.Texture.texture;
        _states.Shader = null;
        _target = target;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( SFML.Game.Framework.Content.Shader shader)
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = shader.SFMLShader;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( Transform2D transform )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = transform.Transform;
        _states.Texture = null;
        _states.Shader = null;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( SFML.Game.Framework.Content.Shader shader, Transform2D transform )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = transform.Transform;
        _states.Texture = null;
        _states.Shader = shader.SFMLShader;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( Texture2D texture, Transform2D transform )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = BlendMode.Alpha;
        _states.Transform = transform.Transform;
        _states.Texture = texture.texture;
        _states.Shader = null;
        _begin = true;
    }


    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, Transform2D transform )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = transform.Transform;
        _states.Texture = null;
        _states.Shader = null;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, SFML.Game.Framework.Content.Shader shader )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = Transform.Identity;
        _states.Texture = null;
        _states.Shader = shader.SFMLShader;
        _begin = true;
    }

    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> for drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Begin( BlendState blendState, SFML.Game.Framework.Content.Shader shader, Transform2D transform )
    {
        if ( _begin ) throw new Exception( "Please call end(); before calling begin again" );

        _states = new RenderStates();
        _states.BlendMode = blendState switch
        {
            BlendState.Additive => BlendMode.Add,
            BlendState.Alpha => BlendMode.Alpha,
            BlendState.Multiply => BlendMode.Multiply,
            BlendState.None => BlendMode.None,
            _ => BlendMode.None,
        };
        _states.Transform = transform.Transform;
        _states.Texture = null;
        _states.Shader = shader.SFMLShader;
        _begin = true;
    }

#nullable disable
    /// <summary>
    /// Prepare this <see cref="GraphicsDrawer"/> to end drawing.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void End()
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling end again" );

        _states.Shader?.Dispose();
        _states.Texture?.Dispose();

        if ( _target != null )
        {
            _target.Display();
            _target.Dispose();
            _target = null;
        }

        _begin = false;
    }

    /// <summary>
    /// Draws a string.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="font"></param>
    /// <param name="charSize"></param>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="scale"></param>
    /// <param name="rotation"></param>
    /// <param name="outlineThickness"></param>
    /// <param name="outlineColor"></param>
    public void DrawString( string text, SFML.Game.Framework.Content.Font font, int charSize, Vector2 position, Color color, Vector2 scale, float rotation, float outlineThickness, Color outlineColor )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Text t = new Text(text, font.ToSFML())
        {
            Scale = SFML.Game.Framework.Vector2.ToVector2f(scale),
            Rotation = rotation,
            CharacterSize = (uint)charSize,
            Position = SFML.Game.Framework.Vector2.ToVector2f(position),
            OutlineThickness = outlineThickness,
            FillColor = color.ToSFMLColor(),
            OutlineColor = outlineColor.ToSFMLColor(),
            Style = font.ConvertToSFMLStyle(font.Style),
        };

        GraphicsDevice.Draw( t, _states );

        t.Dispose();
    }

    /// <summary>
    /// Draws a string with default scale (1,1), rotation 0, no outline.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position, Color color )
    {
        DrawString( text, font, charSize, position, color, new Vector2( 1f, 1f ), 0f, 0f, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with scale and rotation, no outline.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position, Color color, Vector2 scale, float rotation )
    {
        DrawString( text, font, charSize, position, color, scale, rotation, 0f, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with scale, rotation, and outline color/thickness.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position, Color color, Vector2 scale, float rotation, float outlineThickness )
    {
        DrawString( text, font, charSize, position, color, scale, rotation, outlineThickness, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with only position and default charSize, color, scale, rotation, and outline.
    /// </summary>
    public void DrawString( string text, Content.Font font, Vector2 position )
    {
        DrawString( text, font, 16, position, Color.White, new Vector2( 1f, 1f ), 0f, 0f, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with character size, position, and color only.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position )
    {
        DrawString( text, font, charSize, position, Color.White, new Vector2( 1f, 1f ), 0f, 0f, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with character size, position, color, and rotation only.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position, Color color, float rotation )
    {
        DrawString( text, font, charSize, position, color, new Vector2( 1f, 1f ), rotation, 0f, Color.Transparent );
    }

    /// <summary>
    /// Draws a string with character size, position, color, scale only.
    /// </summary>
    public void DrawString( string text, Content.Font font, int charSize, Vector2 position, Color color, Vector2 scale )
    {
        DrawString( text, font, charSize, position, color, scale, 0f, 0f, Color.Transparent );
    }
    /// <summary>
    /// Draws a texture with full parameters.
    /// </summary>
    public void DrawTexture( Texture2D texture, Rectangle srcRect, Vector2 origin, Color color, float rotation, Vector2 scale )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Sprite sprite = new Sprite(texture.texture)
        {
            TextureRect = Rectangle.ToIntRect(srcRect),
            Position = new SFML.System.Vector2f(srcRect.X, srcRect.Y),
            Origin = Vector2.ToVector2f(origin),
            Rotation = rotation,
            Scale = Vector2.ToVector2f(scale),
            Color = color.ToSFMLColor()
        };

        GraphicsDevice.Draw( sprite, _states );
        sprite.Dispose();
    }

    /// <summary>
    /// Draws the full texture at a position with default scale (1,1), rotation 0, and white color.
    /// </summary>
    public void DrawTexture( Texture2D texture, Vector2 position )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Sprite sprite = new Sprite(texture.texture)
        {
            Position = new SFML.System.Vector2f(position.X, position.Y)
        };

        GraphicsDevice.Draw( sprite, _states );
        sprite.Dispose();
    }

    /// <summary>
    /// Draws the texture at a position with specified color, default scale and rotation.
    /// </summary>
    public void DrawTexture( Texture2D texture, Vector2 position, Color color )
    {

        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Sprite sprite = new Sprite(texture.texture)
        {
            Position = new SFML.System.Vector2f(position.X, position.Y),
            Color = color.ToSFMLColor(),
        };

        GraphicsDevice.Draw( sprite, _states );
        sprite.Dispose();
    }

    /// <summary>
    /// Draws the texture at a position with rotation and scale, default color white.
    /// </summary>
    public void DrawTexture( Texture2D texture, Vector2 position, float rotation, Vector2 scale )
    {
        Rectangle fullRect = new Rectangle((int)position.X, (int)position.Y, (int)texture.texture.Size.X, (int)texture.texture.Size.Y);
        DrawTexture( texture, fullRect, new Vector2( 0, 0 ), Color.White, rotation, scale );
    }

    /// <summary>
    /// Draws the texture at a position with scale, default color white and rotation 0.
    /// </summary>
    public void DrawTexture( Texture2D texture, Vector2 position, Vector2 scale )
    {
        Rectangle fullRect = new Rectangle(0, 0, (int)texture.texture.Size.X, (int)texture.texture.Size.Y);
        DrawTexture( texture, fullRect, new Vector2( 0, 0 ), Color.White, 0f, scale );
    }

    /// <summary>
    /// Draws a portion of the texture (source rectangle) at a position, default scale 1, rotation 0, white color.
    /// </summary>
    public void DrawTexture( Texture2D texture, Rectangle srcRect, Vector2 position )
    {
        DrawTexture( texture, srcRect, new Vector2( 0, 0 ), Color.White, 0f, new Vector2( 1f, 1f ) );
    }

    /// <summary>
    /// Draws a portion of the texture at a position with color.
    /// </summary>
    public void DrawTexture( Texture2D texture, Rectangle srcRect, Vector2 position, Color color )
    {
        DrawTexture( texture, new Rectangle( ( int )position.X, ( int )position.Y, srcRect.Width, srcRect.Height ), new Vector2( 0, 0 ), color, 0f, new Vector2( 1f, 1f ) );
    }

    /// <summary>
    /// Draws a portion of the texture at a position with rotation and scale, default color white.
    /// </summary>
    public void DrawTexture( Texture2D texture, Rectangle srcRect, Vector2 position, float rotation, Vector2 scale )
    {
        DrawTexture( texture, new Rectangle( ( int )position.X, ( int )position.Y, srcRect.Width, srcRect.Height ), new Vector2( 0, 0 ), Color.White, rotation, scale );
    }

    /// <summary>
    /// Draws a rounded rectangle shape with the specified size and corner radius.
    /// </summary>
    /// <param name="position">The position of the shape.</param>
    /// <param name="size">The width and height of the rectangle.</param>
    /// <param name="radius">The radius of the rounded corners.</param>
    /// <param name="fillColor">The fill color of the shape.</param>
    /// <param name="outlineColor">The outline color of the shape.</param>
    /// <param name="outlineThickness">The outline thickness.</param>
    /// <param name="cornerPoints">Number of points used to smooth each rounded corner (default 8).</param>
    /// <returns>A <see cref="Shape"/> representing the rounded rectangle.</returns>
    public void DrawRoundedRectangle( Vector2 size, Vector2 position, float radius, Color fillColor, Color outlineColor, float outlineThickness = 0f, int cornerPoints = 8 )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        // Ensure radius doesn’t exceed half the smallest dimension
        radius = MathF.Min( radius, MathF.Min( size.X, size.Y ) / 2f );

        ConvexShape shape = new ConvexShape();
        int totalPoints = cornerPoints * 4;
        shape.SetPointCount( ( uint )totalPoints );

        // Generate points for each corner
        int pointIndex = 0;
        for ( int i = 0; i < cornerPoints; i++ )
        {
            float angle = MathF.PI / 2f * (i / (float)(cornerPoints - 1));
            // Top-left corner
            shape.SetPoint( ( uint )pointIndex++, new Vector2f(
                radius - MathF.Cos( angle ) * radius,
                radius - MathF.Sin( angle ) * radius ) );
        }
        for ( int i = 0; i < cornerPoints; i++ )
        {
            float angle = MathF.PI / 2f * (i / (float)(cornerPoints - 1));
            // Top-right corner
            shape.SetPoint( ( uint )pointIndex++, new Vector2f(
                size.X - radius + MathF.Sin( angle ) * radius,
                radius - MathF.Cos( angle ) * radius ) );
        }
        for ( int i = 0; i < cornerPoints; i++ )
        {
            float angle = MathF.PI / 2f * (i / (float)(cornerPoints - 1));
            // Bottom-right corner
            shape.SetPoint( ( uint )pointIndex++, new Vector2f(
                size.X - radius + MathF.Cos( angle ) * radius,
                size.Y - radius + MathF.Sin( angle ) * radius ) );
        }
        for ( int i = 0; i < cornerPoints; i++ )
        {
            float angle = MathF.PI / 2f * (i / (float)(cornerPoints - 1));
            // Bottom-left corner
            shape.SetPoint( ( uint )pointIndex++, new Vector2f(
                radius - MathF.Sin( angle ) * radius,
                size.Y - radius + MathF.Cos( angle ) * radius ) );
        }

        shape.FillColor = fillColor.ToSFMLColor();
        shape.OutlineColor = outlineColor.ToSFMLColor();
        shape.OutlineThickness = outlineThickness;
        shape.Position = Vector2.ToVector2f( position );
        GraphicsDevice.Draw( shape, _states );
        shape.Dispose();
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="rectangle"></param>
    /// <param name="color"></param>
    public void DrawRectangle( Rectangle rectangle, Color color )
    {
        DrawRoundedRectangle( new Vector2( rectangle.Width, rectangle.Height ), new Vector2( rectangle.X, rectangle.Y ), 0, color, Color.Transparent );
    }

    /// <summary>
    /// Draws a rectangle at a specified position and size.
    /// </summary>
    /// <param name="rect">The rectangle dimensions.</param>
    /// <param name="color">The fill or outline color.</param>
    /// <param name="filled">If true, fills the rectangle; otherwise draws an outline.</param>
    /// <param name="thickness">Outline thickness when not filled.</param>
    /// <param name="rotation">Rotation in radians (optional).</param>
    /// <param name="origin">Origin of rotation (defaults to rectangle center).</param>
    public void DrawRectangle( Rectangle rect, Color color, bool filled = true, float thickness = 2f, float rotation = 0f, Vector2? origin = null )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        RectangleShape shape = new RectangleShape(new Vector2f(rect.Width, rect.Height))
        {
            Position = new Vector2f(rect.X, rect.Y),
            Rotation = rotation * (180f / MathF.PI), // Convert radians → degrees
            FillColor = filled ? color.ToSFMLColor() : SFML.Graphics.Color.Transparent,
            OutlineColor = color.ToSFMLColor(),
            OutlineThickness = filled ? 0f : thickness,
        };

        if ( origin.HasValue )
        {
            shape.Origin = new Vector2f( origin.Value.X, origin.Value.Y );
        }
        else
        {
            shape.Origin = new Vector2f( rect.Width / 2f, rect.Height / 2f );
        }

        GraphicsDevice.Draw( shape, _states );
        shape.Dispose();
    }

    /// <summary>
    /// Draws a rectangle by position and size instead of Rectangle struct.
    /// </summary>
    public void DrawRectangle( Vector2 position, Vector2 size, Color color, bool filled = true, float thickness = 2f, float rotation = 0f )
    {
        DrawRectangle( new Rectangle( ( int )position.X, ( int )position.Y, ( int )size.X, ( int )size.Y ), color, filled, thickness, rotation );
    }
    /// <summary>
    /// Draws a line between two points with optional custom rotation.
    /// </summary>
    /// <param name="start">Start position of the line.</param>
    /// <param name="end">End position of the line.</param>
    /// <param name="color">Color of the line.</param>
    /// <param name="thickness">Thickness of the line in pixels.</param>
    /// <param name="angle">
    /// Optional rotation angle (in degrees). If set to <c>null</c>, the angle is calculated automatically from start and end.
    /// </param>
    public void DrawLine( Vector2 start, Vector2 end, Color color, float thickness = 1f, float? angle = null )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        // Calculate direction and length
        Vector2f diff = new Vector2f(end.X - start.X, end.Y - start.Y);
        float length = MathF.Sqrt(diff.X * diff.X + diff.Y * diff.Y);

        // If user didn’t specify angle, calculate from direction vector
        float rotation = angle ?? (MathF.Atan2(diff.Y, diff.X) * 180f / MathF.PI);

        // Create rectangle representing the line
        RectangleShape lineShape = new RectangleShape(new Vector2f(length, thickness))
        {
            FillColor = color.ToSFMLColor(),
            Position = new Vector2f(start.X, start.Y),
            Rotation = rotation
        };

        GraphicsDevice.Draw( lineShape, _states );
        lineShape.Dispose();
    }

    public void DrawGradient( GraphicsDrawer drawer, Gradient gradient, Vector2 position, Vector2 size )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( gradient.Colors.Count == 0 )
            return;

        // Create a vertex array (we’ll draw with quads)
        VertexArray vertices = new VertexArray(PrimitiveType.Quads);

        int colorCount = gradient.Colors.Count;
        float step = (gradient.Direction == GradientDirection.Horizontal)
                ? size.X / (colorCount - 1)
                : size.Y / (colorCount - 1);

        // Loop through gradient colors and build quads between them
        for ( int i = 0; i < colorCount - 1; i++ )
        {
            Color start = gradient.Colors[i].Color;
            Color end = gradient.Colors[i + 1].Color;

            if ( gradient.Direction == GradientDirection.Horizontal )
            {
                float x1 = position.X + (i * step);
                float x2 = position.X + ((i + 1) * step);

                vertices.Append( new Vertex( new Vector2f( x1, position.Y ), start.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( x2, position.Y ), end.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( x2, position.Y + size.Y ), end.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( x1, position.Y + size.Y ), start.ToSFMLColor() ) );
            }
            else // Vertical
            {
                float y1 = position.Y + (i * step);
                float y2 = position.Y + ((i + 1) * step);

                vertices.Append( new Vertex( new Vector2f( position.X, y1 ), start.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( position.X + size.X, y1 ), start.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( position.X + size.X, y2 ), end.ToSFMLColor() ) );
                vertices.Append( new Vertex( new Vector2f( position.X, y2 ), end.ToSFMLColor() ) );
            }
        }

        drawer.GraphicsDevice.Draw( vertices, _states );
    }

    /// <summary>
    /// Generates a texture containing a gradient defined by <see cref="Gradient"/>.
    /// </summary>
    /// <param name="gradient">The gradient definition.</param>
    /// <param name="width">The width of the output texture.</param>
    /// <param name="height">The height of the output texture.</param>
    /// <returns>A <see cref="Texture2D"/> containing the rendered gradient.</returns>
    public void DrawGradient( Gradient gradient, Rectangle rectangle)
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        int width = rectangle.Width;
        int height = rectangle.Height;

        Color[] pixels = new Color[width * height];

        for ( int y = 0; y < height; y++ )
        {
            for ( int x = 0; x < width; x++ )
            {
                float position = gradient.Direction == GradientDirection.Horizontal
                    ? (float)x / (width - 1)
                    : (float)y / (height - 1);

                Color color = InterpolateGradientColor(gradient, position);
                pixels[y * width + x] = color;
            }
        }

        var texture = new Texture2D( width, height, pixels );
        DrawTexture( texture, new Vector2( rectangle.X, rectangle.Y ), Color.White );
        texture.texture.Dispose();
    }

    /// <summary>
    /// Draws a polygon with the specified points and color.
    /// </summary>
    public void DrawPolygon( Vector2[] points, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( points.Length < 3 )
            return;

        if ( filled )
        {
            ConvexShape shape = new ConvexShape((uint)points.Length);
            for ( int i = 0; i < points.Length; i++ )
                shape.SetPoint( ( uint )i, new Vector2f( points[i].X, points[i].Y ) );

            shape.FillColor = color.ToSFMLColor();
            GraphicsDevice.Draw( shape, _states );
            shape.Dispose();
        }
        else
        {
            for ( int i = 0; i < points.Length; i++ )
            {
                Vector2 start = points[i];
                Vector2 end = points[(i + 1) % points.Length];
                DrawLine( start, end, color, thickness );
            }
        }
    }

    /// <summary>
    /// Draws an ellipse.
    /// </summary>
    public void DrawEllipse( Vector2 center, float radiusX, float radiusY, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        const int segments = 64;
        ConvexShape ellipse = new ConvexShape((uint)segments);

        for ( int i = 0; i < segments; i++ )
        {
            float angle = i * MathF.PI * 2f / segments;
            float x = center.X + MathF.Cos(angle) * radiusX;
            float y = center.Y + MathF.Sin(angle) * radiusY;
            ellipse.SetPoint( ( uint )i, new Vector2f( x, y ) );
        }

        ellipse.FillColor = filled ? color.ToSFMLColor() : SFML.Graphics.Color.Transparent;
        ellipse.OutlineColor = color.ToSFMLColor();
        ellipse.OutlineThickness = filled ? 0f : thickness;

        GraphicsDevice.Draw( ellipse, _states );
        ellipse.Dispose();
    }


    /// <summary>
    /// Draws a triangle from three points.
    /// </summary>
    public void DrawTriangle( Vector2 p1, Vector2 p2, Vector2 p3, Color color, bool filled = true, float thickness = 1f )
    {
        DrawPolygon( new Vector2[] { p1, p2, p3 }, color, filled, thickness );
    }

    /// <summary>
    /// Draws an arrow from start to end.
    /// </summary>
    public void DrawArrow( Vector2 start, Vector2 end, Color color, float thickness = 2f, float headLength = 10f, float headAngle = 25f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        DrawLine( start, end, color, thickness );

        Vector2 direction = end - start;
        float angle = MathF.Atan2(direction.Y, direction.X);

        Vector2 left = new Vector2(
            end.X - headLength * MathF.Cos(angle - MathF.PI * headAngle / 180f),
            end.Y - headLength * MathF.Sin(angle - MathF.PI * headAngle / 180f)
        );

        Vector2 right = new Vector2(
            end.X - headLength * MathF.Cos(angle + MathF.PI * headAngle / 180f),
            end.Y - headLength * MathF.Sin(angle + MathF.PI * headAngle / 180f)
        );

        DrawLine( end, left, color, thickness );
        DrawLine( end, right, color, thickness );
    }

    /// <summary>
    /// Draws a path made of connected lines.
    /// </summary>
    public void DrawPath( Vector2[] points, Color color, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( points.Length < 2 ) return;

        for ( int i = 1; i < points.Length; i++ )
            DrawLine( points[i - 1], points[i], color, thickness );
    }

    /// <summary>
    /// Draws a dashed line between two points.
    /// </summary>
    public void DrawDashedLine( Vector2 start, Vector2 end, Color color, float dashLength = 10f, float gapLength = 5f, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Vector2 diff = end - start;
        float length = diff.Length();
        Vector2 dir = diff / length;

        float drawn = 0f;
        while ( drawn < length )
        {
            float segment = MathF.Min(dashLength, length - drawn);
            Vector2 segStart = start + dir * drawn;
            Vector2 segEnd = start + dir * (drawn + segment);

            DrawLine( segStart, segEnd, color, thickness );
            drawn += dashLength + gapLength;
        }
    }

    // <summary>
    /// Draws a star shape.
    /// </summary>
    public void DrawStar( Vector2 center, int points, float innerRadius, float outerRadius, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( points < 2 ) return;

        ConvexShape star = new ConvexShape((uint)(points * 2));
        double step = Math.PI / points;

        for ( int i = 0; i < points * 2; i++ )
        {
            double r = (i % 2 == 0) ? outerRadius : innerRadius;
            double angle = i * step - Math.PI / 2;
            star.SetPoint( ( uint )i, new Vector2f(
                ( float )( center.X + r * Math.Cos( angle ) ),
                ( float )( center.Y + r * Math.Sin( angle ) )
            ) );
        }

        star.FillColor = filled ? color.ToSFMLColor() : SFML.Graphics.Color.Transparent;
        star.OutlineColor = color.ToSFMLColor();
        star.OutlineThickness = filled ? 0f : thickness;

        GraphicsDevice.Draw( star, _states );
        star.Dispose();
    }
    /// <summary>
    /// Draws a quadratic Bezier curve between three points.
    /// </summary>
    public void DrawBezierCurve( Vector2 start, Vector2 control, Vector2 end, Color color, int segments = 32, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Vector2 prev = start;

        for ( int i = 1; i <= segments; i++ )
        {
            float t = i / (float)segments;
            float u = 1 - t;
            Vector2 point = (u * u) * start + 2 * u * t * control + (t * t) * end;
            DrawLine( prev, point, color, thickness );
            prev = point;
        }
    }

    /// <summary>
    /// Draws a rounded rectangle.
    /// </summary>
    public void DrawRectangleRounded( Rectangle rect, float radius, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        // Draw main body
        DrawRectangle( new Rectangle(
            rect.X + ( int )radius, rect.Y + ( int )radius,
            rect.Width - ( int )( radius * 2 ), rect.Height - ( int )( radius * 2 )
        ), color, filled, thickness );

        // Draw edges
        DrawRectangle( new Rectangle( rect.X + ( int )radius, rect.Y, rect.Width - ( int )( radius * 2 ), ( int )radius ), color, filled, thickness );
        DrawRectangle( new Rectangle( rect.X + ( int )radius, rect.Y + rect.Height - ( int )radius, rect.Width - ( int )( radius * 2 ), ( int )radius ), color, filled, thickness );

        // Draw corners as circles
        DrawCircle( new Vector2( rect.X + radius, rect.Y + radius ), radius, color, filled, thickness );
        DrawCircle( new Vector2( rect.X + rect.Width - radius, rect.Y + radius ), radius, color, filled, thickness );
        DrawCircle( new Vector2( rect.X + radius, rect.Y + rect.Height - radius ), radius, color, filled, thickness );
        DrawCircle( new Vector2( rect.X + rect.Width - radius, rect.Y + rect.Height - radius ), radius, color, filled, thickness );
    }

    /// <summary>
    /// Draws an arc (part of a circle).
    /// </summary>
    public void DrawArc( Vector2 center, float radius, float startAngle, float endAngle, Color color, int segments = 64, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        float step = (endAngle - startAngle) / segments;
        Vector2 prev = new Vector2(
            center.X + MathF.Cos(startAngle) * radius,
            center.Y + MathF.Sin(startAngle) * radius
        );

        for ( int i = 1; i <= segments; i++ )
        {
            float angle = startAngle + step * i;
            Vector2 current = new Vector2(
                center.X + MathF.Cos(angle) * radius,
                center.Y + MathF.Sin(angle) * radius
            );

            DrawLine( prev, current, color, thickness );
            prev = current;
        }
    }

    /// <summary>
    /// Draws a spiral.
    /// </summary>
    public void DrawSpiral( Vector2 center, float startRadius, float endRadius, float turns, Color color, int segments = 100, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Vector2 prev = center;
        float radiusStep = (endRadius - startRadius) / segments;
        float angleStep = (MathF.PI * 2 * turns) / segments;

        for ( int i = 0; i <= segments; i++ )
        {
            float r = startRadius + i * radiusStep;
            float angle = i * angleStep;

            Vector2 current = new Vector2(
                center.X + MathF.Cos(angle) * r,
                center.Y + MathF.Sin(angle) * r
            );

            if ( i > 0 )
                DrawLine( prev, current, color, thickness );

            prev = current;
        }
    }

    /// <summary>
    /// Draws a gradient rectangle (two-color linear gradient).
    /// </summary>
    public void DrawGradientRect( Rectangle rect, Color colorTop, Color colorBottom )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        VertexArray quad = new VertexArray(PrimitiveType.Quads, 4);
        quad[0] = new Vertex( new Vector2f( rect.X, rect.Y ), colorTop.ToSFMLColor() );
        quad[1] = new Vertex( new Vector2f( rect.X + rect.Width, rect.Y ), colorTop.ToSFMLColor() );
        quad[2] = new Vertex( new Vector2f( rect.X + rect.Width, rect.Y + rect.Height ), colorBottom.ToSFMLColor() );
        quad[3] = new Vertex( new Vector2f( rect.X, rect.Y + rect.Height ), colorBottom.ToSFMLColor() );
        GraphicsDevice.Draw( quad );
        quad.Dispose();
    }

    /// <summary>
    /// Draws a regular polygon (e.g., pentagon, hexagon, octagon, etc.).
    /// </summary>
    public void DrawRegularPolygon( Vector2 center, int sides, float radius, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( sides < 3 ) return;

        Vector2[] points = new Vector2[sides];
        for ( int i = 0; i < sides; i++ )
        {
            float angle = (MathF.PI * 2f / sides) * i - MathF.PI / 2f;
            points[i] = new Vector2( center.X + MathF.Cos( angle ) * radius, center.Y + MathF.Sin( angle ) * radius );
        }

        DrawPolygon( points, color, filled, thickness );
    }

    /// <summary>
    /// Draws a cross symbol (+).
    /// </summary>
    public void DrawCross( Vector2 center, float size, Color color, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        float half = size / 2f;
        DrawLine( new Vector2( center.X - half, center.Y ), new Vector2( center.X + half, center.Y ), color, thickness );
        DrawLine( new Vector2( center.X, center.Y - half ), new Vector2( center.X, center.Y + half ), color, thickness );
    }

    /// <summary>
    /// Draws a ring (a circle outline with inner and outer radius).
    /// </summary>
    public void DrawRing( Vector2 center, float innerRadius, float outerRadius, Color color, int segments = 64 )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        VertexArray ring = new VertexArray(PrimitiveType.TriangleStrip);

        for ( int i = 0; i <= segments; i++ )
        {
            float angle = i * MathF.PI * 2f / segments;
            Vector2 outer = new Vector2(center.X + MathF.Cos(angle) * outerRadius, center.Y + MathF.Sin(angle) * outerRadius);
            Vector2 inner = new Vector2(center.X + MathF.Cos(angle) * innerRadius, center.Y + MathF.Sin(angle) * innerRadius);

            ring.Append( new Vertex( new Vector2f( outer.X, outer.Y ), color.ToSFMLColor() ) );
            ring.Append( new Vertex( new Vector2f( inner.X, inner.Y ), color.ToSFMLColor() ) );
        }
        GraphicsDevice.Draw( ring, _states );
        ring.Dispose();
    }
    /// <summary>
    /// Draws a sinusoidal wave.
    /// </summary>
    public void DrawWave( Vector2 start, float width, float amplitude, float frequency, Color color, int segments = 100, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Vector2 prev = start;
        for ( int i = 1; i <= segments; i++ )
        {
            float t = i / (float)segments;
            float x = start.X + t * width;
            float y = start.Y + MathF.Sin(t * frequency * MathF.PI * 2) * amplitude;
            Vector2 current = new Vector2(x, y);
            DrawLine( prev, current, color, thickness );
            prev = current;
        }
    }

    /// <summary>
    /// Draws a heart shape.
    /// </summary>
    public void DrawHeart( Vector2 center, float size, Color color, bool filled = true, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        ConvexShape heart = new ConvexShape(100);
        for ( int i = 0; i < 100; i++ )
        {
            float t = i / 100f * MathF.PI * 2f;
            float x = size * 16 * MathF.Pow(MathF.Sin(t), 3);
            float y = -size * (13 * MathF.Cos(t) - 5 * MathF.Cos(2 * t) - 2 * MathF.Cos(3 * t) - MathF.Cos(4 * t));
            heart.SetPoint( ( uint )i, new Vector2f( center.X + x, center.Y + y ) );
        }

        heart.FillColor = filled ? color.ToSFMLColor() : SFML.Graphics.Color.Transparent;
        heart.OutlineColor = color.ToSFMLColor();
        heart.OutlineThickness = filled ? 0f : thickness;
        GraphicsDevice.Draw( heart, _states );
        heart.Dispose();
    }

    /// <summary>
    /// Draws a circle.
    /// </summary>
    public void DrawCircle( Vector2 center, float radius, Color color, bool filled = true, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        CircleShape circle = new CircleShape(radius)
        {
            Position = new Vector2f(center.X - radius, center.Y - radius),
            FillColor = filled ? color.ToSFMLColor() : SFML.Graphics.Color.Transparent,
            OutlineColor = color.ToSFMLColor(),
            OutlineThickness = filled ? 0f : thickness
        };
        GraphicsDevice.Draw( circle, _states );
        circle.Dispose();
    }


    /// <summary>
    /// Draws a capsule (rounded rectangle ends).
    /// </summary>
    public void DrawCapsule( Vector2 position, float width, float height, Color color, bool filled = true, float thickness = 2f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        float radius = height / 2f;
        DrawRectangle( new Rectangle( ( int )( position.X + radius ), ( int )position.Y, ( int )( width - height ), ( int )height ), color, filled, thickness );
        DrawCircle( new Vector2( position.X + radius, position.Y + radius ), radius, color, filled, thickness );
        DrawCircle( new Vector2( position.X + width - radius, position.Y + radius ), radius, color, filled, thickness );
    }

    /// <summary>
    /// Draws a pie slice.
    /// </summary>
    public void DrawPieSlice( Vector2 center, float radius, float startAngle, float sweepAngle, Color color, bool filled = true, float thickness = 2f, int segments = 40 )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        VertexArray slice = new VertexArray(filled ? PrimitiveType.TriangleFan : PrimitiveType.LineStrip);
        slice.Append( new Vertex( new Vector2f( center.X, center.Y ), color.ToSFMLColor() ) );

        float angleStep = sweepAngle / segments;
        for ( int i = 0; i <= segments; i++ )
        {
            float angle = startAngle + i * angleStep;
            Vector2 point = new Vector2(
                center.X + MathF.Cos(angle) * radius,
                center.Y + MathF.Sin(angle) * radius
            );
            slice.Append( new Vertex( new Vector2f( point.X, point.Y ), color.ToSFMLColor() ) );
        }

        GraphicsDevice.Draw( slice, _states );
        slice.Dispose();
    }

    /// <summary>
    /// Draws a checkerboard pattern.
    /// </summary>
    public void DrawCheckerboard( Rectangle rect, int cellSize, Color colorA, Color colorB )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        for ( int y = rect.Y; y < rect.Y + rect.Height; y += cellSize )
        {
            for ( int x = rect.X; x < rect.X + rect.Width; x += cellSize )
            {
                bool isEven = ((x / cellSize) + (y / cellSize)) % 2 == 0;
                DrawRectangle( new Rectangle( x, y, cellSize, cellSize ), isEven ? colorA : colorB );
            }
        }
    }

    /// <summary>
    /// Draws a grid (useful for debugging layouts).
    /// </summary>
    public void DrawGrid( Rectangle rect, int cellWidth, int cellHeight, Color color, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        for ( int x = rect.X; x <= rect.X + rect.Width; x += cellWidth )
            DrawLine( new Vector2( x, rect.Y ), new Vector2( x, rect.Y + rect.Height ), color, thickness );

        for ( int y = rect.Y; y <= rect.Y + rect.Height; y += cellHeight )
            DrawLine( new Vector2( rect.X, y ), new Vector2( rect.X + rect.Width, y ), color, thickness );
    }

    /// <summary>
    /// Draws a noise texture for effects or debugging.
    /// </summary>
    public void DrawNoiseTexture( Rectangle rect, float density, Color color )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        Random rng = new Random();
        int count = (int)(rect.Width * rect.Height * density);

        for ( int i = 0; i < count; i++ )
        {
            float x = rect.X + (float)rng.NextDouble() * rect.Width;
            float y = rect.Y + (float)rng.NextDouble() * rect.Height;
            DrawLine( new Vector2( x, y ), new Vector2( x + 1, y + 1 ), color, 1f );
        }
    }

    /// <summary>
    /// Draws a polygon textured with a given texture.
    /// </summary>
    public void DrawTexturedPolygon( Vector2[] points, Texture2D texture, Color color )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( points.Length < 3 ) return;

        VertexArray polygon = new VertexArray(PrimitiveType.TriangleFan);
        for ( int i = 0; i < points.Length; i++ )
        {
            polygon.Append( new Vertex( new Vector2f( points[i].X, points[i].Y ), color.ToSFMLColor() ) );
        }

        SFML.Graphics.Texture sfmlTexture = texture.texture;
        RenderStates state = new RenderStates(sfmlTexture);
        state.BlendMode = _states.BlendMode;
        state.Shader = _states.Shader;
        state.Transform = _states.Transform;

        GraphicsDevice.Draw( polygon, state );
        polygon.Dispose();
    }

    /// <summary>
    /// Draws a strip line.
    /// </summary>
    /// <param name="points"></param>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    public void DrawLineStrip( Vector2[] points, Color color, float thickness = 1f )
    {
        if ( !_begin ) throw new Exception( "Please call begin(); before calling any draw methods." );

        if ( points == null || points.Length < 2 ) return;

        for ( int i = 1; i < points.Length; i++ )
            DrawLine( points[i - 1], points[i], color, thickness );
    }

    /// <summary>
    /// Draws a <see cref="AtlasSprite"/>
    /// </summary>
    /// <param name="sprite">The sprite.</param>
    /// <param name="position">The position.</param>
    /// <param name="scale">The scale.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="color">The color.</param>
    public void DrawAtlas(AtlasSprite sprite, Vector2 position, Vector2 scale, float rotation, Color color)
    {
        sprite.Draw( this, position, scale, rotation, color );
    }

#nullable disable

    /// <summary>
    /// Draws a <see cref="AtlasSprite"/>
    /// </summary>
    /// <param name="sprite">The sprite.</param>
    /// <param name="position">The position.</param>
    /// <param name="scale">The scale.</param>
    /// <param name="rotation">The rotation.</param>
    public void DrawAtlas( AtlasSprite sprite, Vector2 position, Vector2 scale, float rotation ) => DrawAtlas( sprite, position, scale, rotation, null );


    /// <summary>
    /// Draws a <see cref="AtlasSprite"/>
    /// </summary>
    /// <param name="sprite">The sprite.</param>
    /// <param name="position">The position.</param>
    /// <param name="scale">The scale.</param>
    public void DrawAtlas( AtlasSprite sprite, Vector2 position, Vector2 scale ) => DrawAtlas( sprite, position, scale, 0f, null );


    /// <summary>
    /// Draws a <see cref="AtlasSprite"/>
    /// </summary>
    /// <param name="sprite">The sprite.</param>
    /// <param name="position">The position.</param>
    public void DrawAtlas( AtlasSprite sprite, Vector2 position ) => DrawAtlas( sprite, position, Vector2.Zero, 0, null );

    #region private methods
    /// <summary>
    /// Interpolates the color at a given position based on the gradient color points.
    /// </summary>
    private Color InterpolateGradientColor( Gradient gradient, float position )
    {
        if ( gradient.Colors.Count == 0 )
            return Color.White;

        // Find the two color points that surround this position
        GradientColorPoint startPoint = gradient.Colors[0];
        GradientColorPoint endPoint = gradient.Colors[^1];

        foreach ( var cp in gradient.Colors )
        {
            if ( position >= cp.Start && position <= cp.End )
            {
                startPoint = cp;
                endPoint = cp;
                break;
            }

            if ( position > cp.End )
                startPoint = cp;
            else if ( position < cp.Start )
            {
                endPoint = cp;
                break;
            }
        }

        float t = (position - startPoint.Start) / Math.Max(0.0001f, endPoint.End - startPoint.Start);
        t = Math.Clamp( t, 0f, 1f );

        return LerpColor( startPoint.Color, endPoint.Color, t );
    }

    /// <summary>
    /// Linearly interpolates between two colors.
    /// </summary>
    private Color LerpColor( Color a, Color b, float t )
    {
        int r = (int)(a.R + (b.R - a.R) * t);
        int g = (int)(a.G + (b.G - a.G) * t);
        int bC = (int)(a.B + (b.B - a.B) * t);
        int aC = (int)(a.A + (b.A - a.A) * t);

        return new Color( r, g, bC, aC );
    }

    /// <summary>
    /// Draws a smooth line between two points with a given thickness and angle.
    /// </summary>
    /// <param name="start">Start position of the line.</param>
    /// <param name="end">End position of the line.</param>
    /// <param name="color">Line color.</param>
    /// <param name="thickness">Line thickness in pixels.</param>
    public void DrawLine( Vector2 start, Vector2 end, Color color, float thickness = 1f )
    {
        // Calculate direction vector
        float dx = end.X - start.X;
        float dy = end.Y - start.Y;
        float length = MathF.Sqrt(dx * dx + dy * dy);

        // Calculate rotation in degrees
        float angle = MathF.Atan2(dy, dx) * (180f / MathF.PI);

        // Create a rectangle representing the line
        RectangleShape lineShape = new RectangleShape(new SFML.System.Vector2f(length, thickness))
        {
            FillColor = color.ToSFMLColor(),
            Position = new SFML.System.Vector2f(start.X, start.Y),
            Rotation = angle
        };

        GraphicsDevice.Draw( lineShape );
        lineShape.Dispose();
    }
    /// <summary>
    /// Draws a filled circle with smooth edges using vertices.
    /// </summary>
    public void DrawCircle( Vector2 center, float radius, Color color, int segments = 100 )
    {
        Vertex[] vertices = new Vertex[segments + 2]; // center + points + repeat first
        vertices[0] = new Vertex( new SFML.System.Vector2f( center.X, center.Y ), color.ToSFMLColor() );

        for ( int i = 0; i <= segments; i++ )
        {
            float angle = i * 2 * MathF.PI / segments;
            float x = center.X + MathF.Cos(angle) * radius;
            float y = center.Y + MathF.Sin(angle) * radius;
            vertices[i + 1] = new Vertex( new SFML.System.Vector2f( x, y ), color.ToSFMLColor() );
        }
        Game.window.Draw( vertices, PrimitiveType.TriangleFan );
        vertices = null;
    }

    /// <summary>
    /// Draws a circle outline with smooth edges using vertices.
    /// </summary>
    public void DrawCircleOutline( Vector2 center, float radius, Color color, float thickness = 2f, int segments = 100 )
    {
        // Need 4 vertices per segment to form a quad
        Vertex[] vertices = new Vertex[segments * 4];

        for ( int i = 0; i < segments; i++ )
        {
            float angle1 = i * 2 * MathF.PI / segments;
            float angle2 = ((i + 1) % segments) * 2 * MathF.PI / segments;

            // Outer points
            float xOuter1 = center.X + MathF.Cos(angle1) * radius;
            float yOuter1 = center.Y + MathF.Sin(angle1) * radius;

            float xOuter2 = center.X + MathF.Cos(angle2) * radius;
            float yOuter2 = center.Y + MathF.Sin(angle2) * radius;

            // Inner points
            float xInner1 = center.X + MathF.Cos(angle1) * (radius - thickness);
            float yInner1 = center.Y + MathF.Sin(angle1) * (radius - thickness);

            float xInner2 = center.X + MathF.Cos(angle2) * (radius - thickness);
            float yInner2 = center.Y + MathF.Sin(angle2) * (radius - thickness);

            // Assign vertices for this quad (4 vertices per segment)
            int vIndex = i * 4;
            vertices[vIndex + 0] = new Vertex( new SFML.System.Vector2f( xInner1, yInner1 ), color.ToSFMLColor() );
            vertices[vIndex + 1] = new Vertex( new SFML.System.Vector2f( xOuter1, yOuter1 ), color.ToSFMLColor() );
            vertices[vIndex + 2] = new Vertex( new SFML.System.Vector2f( xOuter2, yOuter2 ), color.ToSFMLColor() );
            vertices[vIndex + 3] = new Vertex( new SFML.System.Vector2f( xInner2, yInner2 ), color.ToSFMLColor() );
        }

        Game.window.Draw( vertices, PrimitiveType.Quads );
    }

    public void Dispose()
    {
        if ( IsDisposed ) throw new ObjectDisposedException( "GraphicsDrawer is already disposed." );

        GraphicsDevice.Dispose();
        IsDisposed = true;
    }
    #endregion
}

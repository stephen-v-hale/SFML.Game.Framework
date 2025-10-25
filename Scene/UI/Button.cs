using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Scene.UI;

public class Button : ControlBase
{
    public override void Draw( GameTime frameTime, GraphicsDrawer graphicsBatch )
    {
        graphicsBatch.Begin();

        var fontSize = Font.MeasureString(this.Text);
        var position = Position.ToVector2();
        var padding = 10;
        var textPosition = new Vector2(position.X + (padding * 2) /2 - fontSize.X /2, position.Y + (padding *2) /2 - fontSize.Y /2);
        graphicsBatch.DrawRectangleRounded( new Rectangle( Position.X, Position.Y, padding + ( int )fontSize.X + padding, padding + ( int )fontSize.Y + padding ), 5, Background, true, 1 );
        graphicsBatch.DrawString( this.Text, this.Font, textPosition );
        graphicsBatch.End();
    }

    public override void Update( GameTime frameTime )
    {
    }
}

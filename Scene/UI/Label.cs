using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Scene.UI;

public class Label : ControlBase
{
    public override void Draw( GameTime frameTime, GraphicsDrawer graphicsBatch )
    {
        graphicsBatch.Begin();
        graphicsBatch.DrawString( this.Text, this.Font, this.Position.ToVector2() );
        graphicsBatch.End();
    }

    public override void Update( GameTime frameTime )
    {
    }
}

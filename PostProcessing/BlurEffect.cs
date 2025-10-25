using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.PostProcessing;

public class BlurEffect : PostProcessingEffect
{
    /// <summary>
    /// Initialize a new instance of <see cref="BlurEffect"/>
    /// </summary>
    /// <param name="radius">The radius of the blur.</param>
    public BlurEffect(float radius) : base( Encoding.UTF8.GetBytes( @"
        uniform sampler2D texture;
        uniform float radius;
        void main()
        {
            vec2 texSize = vec2(textureSize(texture, 0));
            vec2 uv = gl_TexCoord[0].xy;
            vec4 sum = vec4(0.0);
            int count = 0;

            for (int x = -4; x <= 4; x++)
            {
                for (int y = -4; y <= 4; y++)
                {
                    sum += texture2D(texture, uv + vec2(x, y) / texSize * radius);
                    count++;
                }
            }

            gl_FragColor = sum / float(count);
        }" ) )
    {
        shader.SetUniform( "radius", radius );
    }
}

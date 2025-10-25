using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SFML.Game.Framework.PostProcessing;

public class ColorGradingEffect : PostProcessingEffect
{
    /// <summary>
    /// Initialize a new instance of <see cref="ColorGradingEffect"/>
    /// </summary>
    /// <param name="lift">The lift.</param>
    /// <param name="gain">The gain.</param>
    public ColorGradingEffect(Vector3 lift, Vector3 gain) : base( Encoding.UTF8.GetBytes( @"
        uniform sampler2D texture;
        uniform vec3 lift;
        uniform vec3 gain;
        void main()
        {
            vec4 color = texture2D(texture, gl_TexCoord[0].xy);
            vec3 adjusted = (color.rgb + lift) * gain;
            gl_FragColor = vec4(adjusted, color.a);
        }" ) )
    {
        shader.SetUniform( "lift", new SFML.System.Vector3f( lift.X, lift.Y, lift.Z ) );
        shader.SetUniform( "gain", new SFML.System.Vector3f( gain.X, gain.Y, gain.Z ) );
    }
}

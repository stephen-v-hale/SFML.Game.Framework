using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Simplex noise helper.
/// </summary>
public class SimplexNoise
{
    private readonly Random random = new Random();
    private readonly int[] perm = new int[512];
    private const float F2 = 0.366025403f; // (sqrt(3)-1)/2
    private const float G2 = 0.211324865f; // (3-sqrt(3))/6

    public SimplexNoise( int seed = 0 )
    {
        if ( seed == 0 ) seed = Environment.TickCount;
        random = new Random( seed );
        int[] p = new int[256];
        for ( int i = 0; i < 256; i++ ) p[i] = i;
        for ( int i = 0; i < 256; i++ )
        {
            int j = random.Next(256);
            (p[i], p[j]) = (p[j], p[i]);
        }
        for ( int i = 0; i < 512; i++ )
            perm[i] = p[i & 255];
    }

    public float Noise( float xin, float yin )
    {
        float n0, n1, n2;
        float s = (xin + yin) * F2;
        int i = FastFloor(xin + s);
        int j = FastFloor(yin + s);
        float t = (i + j) * G2;
        float X0 = i - t;
        float Y0 = j - t;
        float x0 = xin - X0;
        float y0 = yin - Y0;

        int i1, j1;
        if ( x0 > y0 ) { i1 = 1; j1 = 0; }
        else { i1 = 0; j1 = 1; }

        float x1 = x0 - i1 + G2;
        float y1 = y0 - j1 + G2;
        float x2 = x0 - 1f + 2f * G2;
        float y2 = y0 - 1f + 2f * G2;

        int ii = i & 255;
        int jj = j & 255;
        float gi0 = perm[ii + perm[jj]] % 12;
        float gi1 = perm[ii + i1 + perm[jj + j1]] % 12;
        float gi2 = perm[ii + 1 + perm[jj + 1]] % 12;

        n0 = Calc( x0, y0, gi0 );
        n1 = Calc( x1, y1, gi1 );
        n2 = Calc( x2, y2, gi2 );

        return 70f * ( n0 + n1 + n2 );
    }

    private static float Calc( float x, float y, float g )
    {
        float t = 0.5f - x * x - y * y;
        if ( t < 0 ) return 0f;
        t *= t;
        var grads = new float[,] {
                {1,1},{-1,1},{1,-1},{-1,-1},{1,0},{-1,0},{1,0},{-1,0},{0,1},{0,-1},{0,1},{0,-1}
            };
        return t * t * ( grads[( int )g, 0] * x + grads[( int )g, 1] * y );
    }

    private static int FastFloor( float x ) => x > 0 ? ( int )x : ( int )x - 1;
}
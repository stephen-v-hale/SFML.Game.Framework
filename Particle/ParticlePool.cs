using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle;

#nullable disable

/// <summary>
/// Simple pool to reuse Particle objects and avoid allocations.
/// </summary>
public class ParticlePool
{
    private readonly Particle[] items;
    private readonly Stack<int> freeStack;

    public ParticlePool( int capacity )
    {
        items = new Particle[capacity];
        freeStack = new Stack<int>( capacity );
        for ( int i = 0; i < capacity; i++ )
        {
            items[i] = new Particle();
            freeStack.Push( i );
        }
    }

    public int Capacity => items.Length;

    public bool TryAcquire( out Particle p, out int index )
    {
        if ( freeStack.Count == 0 )
        {
            p = null;
            index = -1;
            return false;
        }

        index = freeStack.Pop();
        p = items[index];
        return true;
    }

    public void Release( int index )
    {
        if ( index < 0 || index >= items.Length ) return;
        items[index].Active = false;
        freeStack.Push( index );
    }

    public Particle this[int i] => items[i];
}
using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Components;

public delegate void GameComponentEventHandler<T>( T eventArgs );
public delegate void GameComponentEventHandler<T, T1>( T sender, T1 eventArgs );
public delegate void GameComponentEventHandler();
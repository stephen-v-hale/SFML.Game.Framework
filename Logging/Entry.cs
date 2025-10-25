using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Logging;

#nullable disable
public struct Entry
{
    public string Message;
    public EntryType Level;
    public Object Sender;
    public DateTime CreationDate = DateTime.Now;

    public Entry() { }
}

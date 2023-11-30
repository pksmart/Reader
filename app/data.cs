using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace TeleprompterC9onsole;

internal class TelePrompterConfig
{
    public int DelayInMillisecond { get; private set; } = 200;
    public void UpdateDelay(int increment)// negative to speed up
    {
        var newDelay = Min(DelayInMillisecond + increment, 1000);
        newDelay = Max(newDelay, 20);
        DelayInMillisecond = newDelay; 
    }
    public bool Done { get; private set; }
    public void SetDone() { Done = true; }
    
}


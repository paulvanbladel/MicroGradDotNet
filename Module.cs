using System;
using System.Collections.Generic;

public abstract class Module
{
    public virtual void ZeroGrad()
    {
        foreach (var p in Parameters())
        {
            p.Grad = 0;
        }
    }

    public virtual IEnumerable<Value> Parameters()
    {
        return new Value[0];
    }
}



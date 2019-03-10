using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Data.Interface
{
    public interface IContextFactory
    {
        DataContext Create();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics.Interfaces
{
    public interface IUI
    {
        void Run(IUI ui, string msg);
    }
}
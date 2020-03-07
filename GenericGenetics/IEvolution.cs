using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics
{
    public interface IEvolution<T>
    {
        public void SetParameters(Parameters parameters);
        public void Run(Action<DNA<T>, int> displayPhenotype);
    }
}

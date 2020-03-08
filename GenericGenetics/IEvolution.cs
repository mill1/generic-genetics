﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics
{
    public interface IEvolution<T>
    {
        public int DnaSize { get; set; }
        public void SetParameters(Parameters parameters);
        public void Run(int dnaSize, Action<DNA<T>, int> displayPhenotype);
    }
}

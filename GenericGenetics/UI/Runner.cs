using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics.UI
{
    // This class implements the console user interface of the application.
    public class Runner
    {
        private readonly Interfaces.IUICircleEvolution ui;
        public Runner(Interfaces.IUICircleEvolution ui)
        {
            this.ui = ui;
        }

        public void Run()
        {
            ui.Run(ui);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics.UI
{
    // This class implements the console user interface of the application.
    // It compiles stuff here that will be used in the runner consumer.
    public class Runner
    {
        private readonly Interfaces.IUI ui;

        public Runner(Interfaces.IUI ui)
        {
            this.ui = ui;
        }

        public void Run()
        {
            ui.Run(ui, "Runner");
        }
    }
}

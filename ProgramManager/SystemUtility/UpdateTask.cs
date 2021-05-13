using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace ProgramManager.SystemUtility
{
    /// <summary>
    /// Task wykonywany celem aktualizacji stanu listy programow
    /// </summary>
    public class UpdateTask
    {
        InstalledSoftwareHandler installedSoftwareHandler;

        public UpdateTask() {
            installedSoftwareHandler = new InstalledSoftwareHandler();
        }
        public UpdateTask(InstalledSoftwareHandler installedSoftwareHandler)
        {
            this.installedSoftwareHandler = installedSoftwareHandler;
        }
        /// <summary>
        /// Utworzenie nowego <see cref="DispatcherTimer"/> i dodanie metody <see cref="Tick"/> do eventu <see cref="DispatcherTimer.Tick"/>.
        /// </summary>
        /// <param name="interval">Interwal czasowy liczony w sekundach</param>
        public void Run(int interval)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Tick;
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Start();
        }

        /// <summary>
        /// Wywolanie metody <see cref="InstalledSoftwareHandler.Update"/> do aktualizacji stanu listy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, EventArgs e)
        {
            installedSoftwareHandler.Update();
        }

    }
}

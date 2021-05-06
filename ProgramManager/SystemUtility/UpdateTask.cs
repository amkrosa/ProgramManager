using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace ProgramManager.SystemUtility
{
    class UpdateTask
    {
        InstalledSoftware installedSoftware;
        public UpdateTask() {
            installedSoftware = InstalledSoftware.GetInstance();
        }

        public void Run(int interval)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Tick;
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            installedSoftware.Update();
        }

    }
}

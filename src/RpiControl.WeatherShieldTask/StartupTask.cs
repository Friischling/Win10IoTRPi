using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace RpiControl.WeatherShieldTask
{
    public class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        private bool cancelled = false;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnCanceled;
            

            while (!cancelled)
            {
                await AzureIoTHub.SendDeviceToCloudMessageAsync();
                await Task.Delay(10000);
            }
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            cancelled = true;
            deferral.Complete();
        }
    }
}
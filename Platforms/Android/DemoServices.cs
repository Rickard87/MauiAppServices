using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    [Service(ForegroundServiceType = Android.Content.PM.ForegroundService.TypeDataSync)]
    public class DemoServices : Service, IServiceTest
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action == "START_SERVICE")
            {
                System.Diagnostics.Debug.WriteLine("!! SERVICE HAS STARTED !!");
                RegisterNotification();
            }
            else if(intent.Action == "STOP_SERVICE")
            {
                System.Diagnostics.Debug.WriteLine("!! SERVICE HAS STOPPED !!");
                StopForeground(true);
                StopSelfResult(startId);
            }
            return StartCommandResult.NotSticky; //Does this make the notification stick?
        }
        public async void Start()
        {
            CheckAndRequestLocationPermission();
            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(DemoServices));
            startService.SetAction("START_SERVICE");
            MainActivity.ActivityCurrent.StartService(startService);
        }
        public void Stop()
        {
            Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
            stopIntent.SetAction("STOP_SERVICE");
            MainActivity.ActivityCurrent.StartService(stopIntent);
        }
        private void RegisterNotification()
        {
            NotificationChannel channel = new NotificationChannel("ServiceChannel", "Service demo", NotificationImportance.Max);
            NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
            Notification notification = new Notification.Builder(this, "ServiceChannel")
                .SetContentTitle("The Service is working")
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
                .SetOngoing(true)
                .Build();

            StartForeground(100, notification);
        }
        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.PostNotifications>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.PostNotifications>();

            System.Diagnostics.Debug.WriteLine("?? " + status + " ??");

            return status;
        }
    }
}

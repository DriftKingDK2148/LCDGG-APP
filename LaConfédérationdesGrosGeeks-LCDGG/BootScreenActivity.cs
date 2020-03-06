
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LaConfédérationdesGrosGeeks_LCDGG
{
    [Activity(Label = "LCDGG App", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen", MainLauncher = true, NoHistory = true)]
    public class BootScreenActivity : Activity
    {



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BootScreen);
        }

            
           

            

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        async void SimulateStartup()
        {
            await Task.Delay(2000); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
        public override void OnBackPressed() { }

    }
}

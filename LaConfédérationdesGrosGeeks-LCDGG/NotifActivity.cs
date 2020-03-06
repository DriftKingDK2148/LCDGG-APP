
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace LaConfédérationdesGrosGeeks_LCDGG
{
    [Activity(Label = "Derniers articles")]
    public class NotifActivity : Activity
    {
        

        
           
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NotifActivity);

            var buttonVAlidation1 = FindViewById<Button>(Resource.Id.buttonValidation1);
            var buttonVAlidation2 = FindViewById<Button>(Resource.Id.buttonValidation2);
            var buttonVAlidation3 = FindViewById<Button>(Resource.Id.buttonValidation3);
            var buttonVAlidation4 = FindViewById<Button>(Resource.Id.buttonValidation4);




            // Create your application here


            buttonVAlidation1.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Article1FullScreen));
                StartActivity(nextActivity);

            };

            //Ouverture nouvelle activité : Lastarticles
            buttonVAlidation2.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Article2FullScreen));
                StartActivity(nextActivity);
            };

            buttonVAlidation3.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Article3FullScreen));
                StartActivity(nextActivity);
            };

            buttonVAlidation4.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Article4FullScreen));
                StartActivity(nextActivity);
            };


        }
    }
}

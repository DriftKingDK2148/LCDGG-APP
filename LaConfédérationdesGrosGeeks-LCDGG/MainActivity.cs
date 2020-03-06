using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Android.Views;
using Java.Interop;
using Xamarin.Essentials;

namespace LaConfédérationdesGrosGeeks_LCDGG
{

    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon")]
    

    public class MainActivity : Activity
    {
        public static string url1 = "https://lcdgg.thomascyrix.com/2020/03/03/28-millions-dimages-rendues-libres-de-droits-par-un-musee-de-washington";
        public static string url2 = "https://lcdgg.thomascyrix.com/2020/02/27/gardez-un-oeil-sur-votre-connexion-internet-avec-networker/";
        public static string url3 = "https://lcdgg.thomascyrix.com/2020/02/20/witeboard-tableau-blanc-partage/";
        public static string url4 = "https://lcdgg.thomascyrix.com/2020/02/20/witeboard-tableau-blanc-partage/";




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        //ActivityNotification redirection articles


        //retour téléphone = retour page web
        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back && web_view.CanGoBack())
            {
                web_view.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
        
        WebView web_view;

        public class HelloWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return false;
            }
        }




        //#####################################################################
        // ONCREATE METHOD###################
        //#####################################################################

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Connexion Internet Requise");
                alertDialog.SetMessage("Votre appareil ne semble pas être connecté à Internet.\n\nVous devez être conecté pour accéder aux articles !");
                alertDialog.SetNeutralButton("OK", delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            }


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var contactUsButton = FindViewById<Button>(Resource.Id.contactUsButton1);
            var fullScreenBUTTON = FindViewById<Button>(Resource.Id.newButton1);
            var lastArticlesBUTTON = FindViewById<Button>(Resource.Id.lastArticlesButton);
            var helpUsBUTTON = FindViewById<Button>(Resource.Id.helpUsButton1);

            //Ouverture nouvelle activité : Fullscreenmode
            fullScreenBUTTON.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(FullScreenActivity));
                StartActivity(nextActivity);

            };

            //Ouverture nouvelle activité : Lastarticles
            lastArticlesBUTTON.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(NotifActivity));
                StartActivity(nextActivity);
            };

            helpUsBUTTON.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(HelpUsActivity));
                StartActivity(nextActivity);
            };



            //Message alerte afficher coordonées developpeur
            contactUsButton.Click += ContactUsButtonClick;
            

            async void ContactUsButtonClick(object sender, EventArgs e)
            {
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Coordonnées du développeur");
                alertDialog.SetMessage("\ndriftking.dk.moderation@gmail.com\n\nCette adresse a été copiée dans le presse papier.\nMerci de votre aide !\n\nLa ThomatoTeam :)");
                alertDialog.SetNeutralButton("OK", delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
                var hasText = Clipboard.HasText;
                await Clipboard.SetTextAsync("driftking.dk.moderation@gmail.com");

            }



            web_view = FindViewById<WebView>(Resource.Id.webview);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetWebViewClient(new HelloWebViewClient());
            web_view.LoadUrl("https://lcdgg.thomascyrix.com/");

            
        }

        private class HybridWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView webView, string url)
            {

                // If the URL is not our own custom scheme, just let the webView load the URL as usual
                var scheme = "hybrid:";

                if (!url.StartsWith(scheme))
                    return false;

                // This handler will treat everything between the protocol and "?"
                // as the method name.  The querystring has all of the parameters.
                var resources = url.Substring(scheme.Length).Split('?');
                var method = resources[0];
                var parameters = System.Web.HttpUtility.ParseQueryString(resources[1]);

                if (method == "UpdateLabel")
                {
                    var textbox = parameters["textbox"];

                    // Add some text to our string here so that we know something
                    // happened on the native part of the round trip.
                    var prepended = $"C# says: {textbox}";

                    // Build some javascript using the C#-modified result
                    var js = $"SetLabelText('{prepended}');";

                    webView.LoadUrl("javascript:" + js);
                }

                return true;
            }
        }


    }
}

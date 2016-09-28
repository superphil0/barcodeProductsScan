using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using AT.Nineyards.Anyline.Modules.Barcode;
using AT.Nineyards.Anyline.Models;
using System;
using System.Collections.Generic;
using Android.Content;
using App1.Resources.layout;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IBarcodeResultListener 
    {
        BarcodeScanView scanView;

        public void OnResult(string result, BarcodeScanView.BarcodeFormat barcodeFormat, AnylineImage anylineImage)
        {

            Toast.MakeText(this, result, ToastLength.Short).Show();
            var intent = new Intent(this, typeof(ProductActivity));
            List<string> phoneNumbers = new List<string>();
            phoneNumbers.Add(result);
            intent.PutStringArrayListExtra("barcode", phoneNumbers);
            StartActivity(intent);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // allocate the element we defined earlier in our Main.axml
            scanView = FindViewById<BarcodeScanView>(Resource.Id.AnylineScanView);

            // Load config from the .json file
            // We don't need this, if we define our configuration in the XML code
            scanView.SetConfigFromAsset("scanConfig.json");
            // Initialize with our license key and our result listener
            // Important: use the license key for your app package
            scanView.InitAnyline("eyJzY29wZSI6WyJBTEwiXSwicGxhdGZvcm0iOlsiaU9TIiwiQW5kcm9pZCIsIldpbmRvd3MiXSwidmFsaWQiOiIyMDE3LTA5LTI3IiwibWFqb3JWZXJzaW9uIjoiMyIsImlzQ29tbWVyY2lhbCI6ZmFsc2UsInRvbGVyYW5jZURheXMiOjYwLCJpb3NJZGVudGlmaWVyIjpbIkFULk5pbmV5YXJkcy5BbnlsaW5lWGFtYXJpbkV4YW1wbGUiXSwiYW5kcm9pZElkZW50aWZpZXIiOlsiQVQuTmluZXlhcmRzLkFueWxpbmVYYW1hcmluRXhhbXBsZSJdLCJ3aW5kb3dzSWRlbnRpZmllciI6WyJBVC5OaW5leWFyZHMuQW55bGluZVhhbWFyaW5FeGFtcGxlIl19CmlVNitKa2J5SkVnK25pOTBGcWJYaEFMVDIrbkRMN3JGWmxYakdUY3AybzFIMjd6L0FHbmNwOTNZWjNlMEpGMlJESHRXcTUvNjV6cVk0eXNTMDlzbEpPVlJsejBWZ1NSSHFqZTcrMWVFeng0ZjBlbnNxQ0dRNXFYckhZUXBSd2dQTllHTXlndm1iTzIwTUhjbHVBdkdKZEczbTh2ZUtHMm5qVEZ5MVNQaWw1eU0xK2hmbC8xRElHY3Exa1R1anl1TStPeUVuajdJLzhrQ3RaZkVhbjFvblo0YXlxdjF1WkZQbXZGb3o5SVQ1TjBRN0lsdXBydlBXM1MveHNzenVlRHpDWmNhTVlDWmY4cTVsaHZDakpHUDVhRTJLcHQ0Zk9pVE1pSkV0a2JkN3JBWWpmOEN1UGFoR0ZnWHJ5TnlTN3BCUlNzUnN6ZXEyV2Voakl4QkxycTN3Zz09", this);

            // Don't stop scanning when a result is found
            scanView.SetCancelOnResult(false);

            // Register event that shows if the camera is initialized
            scanView.CameraOpened += (s, e) =>
            {
                Log.Debug("Camera", "Camera opened successfully. Frame resolution " + e.Width + " x " + e.Height);
            };

            // Register event that shows if the camera returns an error
            scanView.CameraError += (s, e) =>
            {
                Log.Error("Camera", "OnCameraError: " + e.Event.Message);
            };

        }
        protected override void OnResume()
        {
            base.OnResume();
            scanView.StartScanning();
        }
    }

}


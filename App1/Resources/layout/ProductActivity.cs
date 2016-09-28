using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json.Linq;
namespace App1.Resources.layout
{
    [Activity(Label = "ProdcutActivity")]
    public class ProductActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var phoneNumbers = Intent.Extras.GetStringArrayList("barcode") ?? new string[0];
            string url = " https://api.outpan.com/v2/products/" + phoneNumbers[0] + "?apikey=[INSERT API KEY]";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            JObject bob = null;
            using (WebResponse response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    var content = stream.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }
                     bob = JObject.Parse (content);
                }
            }
            LinearLayout linearLayout = new LinearLayout(this);
            SetContentView(linearLayout);
            TextView textView = new TextView(this);
            textView.SetText(bob["name"].ToString(),TextView.BufferType.Normal);
            linearLayout.AddView(textView);
        }
    }
}
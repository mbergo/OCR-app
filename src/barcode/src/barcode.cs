using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Gms.Vision.Barcodes;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using ZXing.Mobile;

namespace BarcodeApp
{
    [Activity(Label = "BarcodeApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static readonly string TAG = "BARCODE";
        static readonly int PHOTO_REQUEST = 1;

        private TextView _textView;
        private Button _button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _textView = FindViewById<TextView>(Resource.Id.text_view);
            _button = FindViewById<Button>(Resource.Id.button);

            _button.Click += async delegate
            {
                // Initialize the barcode reader
                MobileBarcodeScanner scanner = new MobileBarcodeScanner(this);

                // Scan the barcode
                ZXing.Result result = await scanner.Scan();

                // Process the barcode result
                if (result != null)
                {
                    _textView.Text = result.Text;

                    // Send barcode text to API at example.com
                    string url = "http://example.com/api";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    string postData = "text=" + result.Text;
                    byte[] dataBytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = dataBytes.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(dataBytes, 0, dataBytes.Length);
                    }

                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (Stream responseStream = response.GetResponseStream())
                            {
                                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                                string responseString = reader.ReadToEnd();
                                // Process response from API
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        // Handle error
                    }
                }
            };
        }
    }
}

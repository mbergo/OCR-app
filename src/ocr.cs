using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Gms.Vision.Faces;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Tesseract;

namespace OcrApp
{
    [Activity(Label = "OcrApp", MainLauncher = true)]
    public class MainActivity : Activity, ISurfaceHolderCallback, Android.Hardware.Camera.IPictureCallback
    {
        static readonly string TAG = "OCR";
        static readonly int PHOTO_REQUEST = 1;

        private Android.Hardware.Camera _camera;
        private SurfaceView _surfaceView;
        private ISurfaceHolder _surfaceHolder;
        private Bitmap _bitmap;
        private TextView _textView;
        private TesseractApi _api;
        private Button _button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _surfaceView = FindViewById<SurfaceView>(Resource.Id.surface_view);
            _textView = FindViewById<TextView>(Resource.Id.text_view);
            _button = FindViewById<Button>(Resource.Id.button);

            _surfaceHolder = _surfaceView.Holder;
            _surfaceHolder.AddCallback(this);

            _button.Click += delegate
            {
                _camera.TakePicture(null, null, this);
            };

            _api = new TesseractApi(this, AssetsDeployment.OncePerInitialization);
        }

        public void OnPictureTaken(byte[] data, Android.Hardware.Camera camera)
        {
            _bitmap = BitmapFactory.DecodeByteArray(data, 0, data.Length);
            _api.SetImage(_bitmap);
            _textView.Text = _api.Text;
            _api.End();
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {
            _camera.StartPreview();
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            _camera = Android.Hardware.Camera.Open();
            _camera.SetPreviewDisplay(_surfaceHolder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            _camera.StopPreview();
            _camera.Release();
        }
    }
}

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
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Graphics;
using Android.Graphics.Drawables;
using OmnicTabs.Core.ViewModels;
using System.IO;

namespace OmnicTabs.Droid.Views
{
    [Activity(Label = "View for GrandChildViewModel")]
    public class GrandChildView : MvxActivity
    {
        Button _buttonSave;
        Button _buttonRemove;
        MvxImageView _imageView;
        Bitmap _image;
        private string _imagePath;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ZoomedImageView);
            _buttonSave = FindViewById<Button>(Resource.Id.button1);
            _buttonSave.Click += DownloadAsync;
            _buttonRemove = FindViewById<Button>(Resource.Id.button2);
            _buttonRemove.Click += (sender, args) => Finish();
            _imageView = FindViewById<MvxImageView>(Resource.Id.big_image_view);
            var bitmapDrawable = _imageView.Drawable as BitmapDrawable;
            if (bitmapDrawable != null) _image = bitmapDrawable.Bitmap;

            var viewModel = (GrandChildViewModel)ViewModel;
            _imagePath = viewModel.ImageUrl;
        }

        private async void DownloadAsync(object sender, EventArgs e)
        {
            var documentsPath =
                (new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim), "Camera"));
            if (!documentsPath.Exists())
            {
                documentsPath.Mkdir();
            }
            var localFilename = _imagePath.GetHashCode() + ".jpg"; // TODO use Random
            var localPath = System.IO.Path.Combine(documentsPath.AbsolutePath, localFilename);

            using (var stream = new FileStream(localPath, FileMode.OpenOrCreate))
            {
                _image.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                await stream.FlushAsync();
                stream.Close();
            }
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(new Java.IO.File(localPath));
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            Bitmap bitmap;
            BitmapFactory.Options options;
            using (options = new BitmapFactory.Options())
            {
                options.InJustDecodeBounds = true;
                BitmapFactory.DecodeFile(localPath, options);
                options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / _imageView.Height : options.OutWidth / _imageView.Width;
                options.InJustDecodeBounds = false;
                bitmap = BitmapFactory.DecodeFile(localPath, options);
            }
            _imageView.SetImageBitmap(bitmap);
            Finish();
            if (new Java.IO.File(localPath).Exists())
                Toast.MakeText(this, "Image saved to gallery", ToastLength.Long).Show();
        }
    }
}
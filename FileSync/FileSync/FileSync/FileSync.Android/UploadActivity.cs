using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace FileSync.Droid
{
    [Activity(Label = "FileSync-Upload", Icon = "@mipmap/ic_fs_main", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionSend, Intent.ActionSendMultiple },
        Categories = new[] { Intent.CategoryDefault },
        DataMimeTypes = new string[] { @"application/octet-stream", @"text/*", @"application/pdf", @"image/*", "application/epub+zip", "audio/*", "video/*" })]
    public class UploadActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental", "SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            if (Intent.Action == Intent.ActionSend || Intent.Action == Intent.ActionSendMultiple)
            {
                LoadApplication(new App(true));
                MessagingCenter.Send((object)this, ViewModels.UploadViewModel.UploadFileMessageName, (object)Intent);
            }
            else
            {
                FinishAffinity();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
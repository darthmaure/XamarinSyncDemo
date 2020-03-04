using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Net;
using Android.Provider;
using FileSync.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSync.Droid.Services.FilesLocationService))]
namespace FileSync.Droid.Services
{
    public class FilesLocationService : IFilesLocationService
    {
        public IList<string> GetActualPathForFiles(object intent, object context)
        {
            var activity = context as UploadActivity;
            var resolver = activity.ContentResolver;

            return ProcessFiles(resolver, intent as Intent);
        }

        public IList<string> ProcessFiles(ContentResolver resolver, Intent intent)
        {
            var results = new List<string>();
            ClipData clip = intent.ClipData;
            for (int i = 0; i < clip.ItemCount; i++)
            {
                var uri = clip.GetItemAt(i).Uri;
                var file = GetFileName(resolver, uri);

                // Open a stream from the URI 
                using var fileStream = resolver.OpenInputStream(uri);
                using var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(docsPath, file);
                System.IO.File.WriteAllBytes(filePath, memoryStream.ToArray());
                results.Add(filePath);
            }
            return results;
        }

        private string GetFileName(ContentResolver resolver, Uri uri)
        {
            var filename = string.Empty;
            using (var cursor = resolver.Query(uri, null, null, null, null))
            {
                int nameIndex = cursor.GetColumnIndex(OpenableColumns.DisplayName);

                cursor.MoveToFirst();

                filename = cursor.GetString(nameIndex);
            }
            return filename;
        }
    }
}
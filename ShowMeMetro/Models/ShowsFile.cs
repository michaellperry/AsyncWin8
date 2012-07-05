using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using System;

namespace ShowMeMetro.Models
{
    public class ShowsFile
    {
        private const string FileName = "shows.xml";
        private static readonly XmlSerializer ShowsSerializer = new XmlSerializer(typeof(ShowsDocument));

        public async Task<bool> GetFileExistsAsync()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return storage.FileExists(FileName);
        }

        public async Task<ShowsDocument> ReadDocumentAsync()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
            using (var randomAccessStream = await file.OpenReadAsync())
            {
                return await Task.Run(delegate()
                {
                    using (var stream = randomAccessStream.AsStreamForRead())
                    {
                        return (ShowsDocument)ShowsSerializer.Deserialize(stream);
                    }
                });
            }
            //lock (this)
            //{
            //    using (IsolatedStorageFileStream stream = storage.OpenFile(FileName, FileMode.Open, FileAccess.Read))
            //    {
            //        return (ShowsDocument)ShowsSerializer.Deserialize(stream);
            //    }
            //}
        }

        public async Task WriteDocumentAsync(ShowsDocument document)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            using (var randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await Task.Run(delegate
                {
                    using (Stream stream = randomAccessStream.AsStreamForWrite())
                    {
                        ShowsSerializer.Serialize(stream, document);
                    }
                });
            }
            //lock (this)
            //{
            //    using (IsolatedStorageFileStream stream = storage.CreateFile(FileName))
            //    {
            //        ShowsSerializer.Serialize(stream, document);
            //    }
            //}
        }
    }
}

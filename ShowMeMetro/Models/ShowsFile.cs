using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ShowMeMetro.Models
{
    public class ShowsFile
    {
        private const string FileName = "shows.xml";
        private static readonly XmlSerializer ShowsSerializer = new XmlSerializer(typeof(ShowsDocument));

        public async Task<bool> FileExists()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
                return true;
            }
            catch (IOException fnf)
            {
                return false;
            }
        }

        public async Task<ShowsDocument> ReadDocument()
        {
            //lock (this)
            //{
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
            IAsyncOperation<IRandomAccessStream> operation = file.OpenAsync(FileAccessMode.Read);
            try
            {
                IRandomAccessStream randomAccessStream = await operation;
                return await Task.Run(() =>
                {
                    using (Stream stream = randomAccessStream.AsStreamForRead())
                    {
                        return (ShowsDocument)ShowsSerializer.Deserialize(stream);
                    }
                });
            }
            finally
            {
                operation.Close();
            }
            //}
        }

        public async Task WriteDocument(ShowsDocument document)
        {
            //lock (this)
            //{
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                ShowsSerializer.Serialize(stream, document);
            }
            //}
        }
    }
}

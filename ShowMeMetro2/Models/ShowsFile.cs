using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using QEDCode;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ShowMeMetro2.Models
{
    public class ShowsFile
    {
        private const string FileName = "shows.xml";
        private static readonly XmlSerializer ShowsSerializer = new XmlSerializer(typeof(ShowsDocument));

        private AwaitableCriticalSection _lock = new AwaitableCriticalSection();

        public async Task<bool> GetFileExistsAsync()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
                return true;
            }
            catch (FileNotFoundException fnf)
            {
                return false;
            }
        }

        public async Task<ShowsDocument> ReadDocumentAsync()
        {
            using (var cs = await _lock.EnterAsync())
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
                using (IRandomAccessStream randomAccessStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    return await Task.Run(() =>
                    {
                        using (Stream stream = randomAccessStream.AsStreamForRead())
                        {
                            return (ShowsDocument)ShowsSerializer.Deserialize(stream);
                        }
                    });
                }
            }
        }

        public async Task WriteDocumentAsync(ShowsDocument document)
        {
            using (var cs = await _lock.EnterAsync())
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
                using (IRandomAccessStream randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await Task.Run(() =>
                    {
                        using (Stream stream = randomAccessStream.AsStreamForWrite())
                        {
                            ShowsSerializer.Serialize(stream, document);
                        }
                    });
                }
            }
        }
    }
}

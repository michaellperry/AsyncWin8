using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

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
            using (Stream stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(FileName))
            {
                return (ShowsDocument)ShowsSerializer.Deserialize(stream);
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

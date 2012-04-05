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

        public bool FileExists
        {
            get
            {
                try
                {
                    StorageFile file = ApplicationData.Current.LocalFolder.GetFileAsync(FileName).GetResults();
                    return true;
                }
                catch (IOException fnf)
                {
                    return false;
                }
            }
        }

        public ShowsDocument ReadDocument()
        {
            //lock (this)
            //{
            //    using (IsolatedStorageFileStream stream = storage.OpenFile(FileName, FileMode.Open, FileAccess.Read))
            //    {
            //        return (ShowsDocument)ShowsSerializer.Deserialize(stream);
            //    }
            //}
            return null;
        }

        public void WriteDocument(ShowsDocument document)
        {
            lock (this)
            {
                StorageFile file = ApplicationData.Current.LocalFolder.CreateFileAsync(FileName).GetResults();
                using (Stream stream = file.OpenStreamForWriteAsync().Result)
                {
                    ShowsSerializer.Serialize(stream, document);
                }
            }
        }
    }
}

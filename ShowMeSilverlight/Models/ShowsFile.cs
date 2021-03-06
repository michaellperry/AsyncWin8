﻿using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace ShowMeSilverlight.Models
{
    public class ShowsFile
    {
        private const string FileName = "shows.xml";
        private static readonly XmlSerializer ShowsSerializer = new XmlSerializer(typeof(ShowsDocument));

        private IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();

        public bool FileExists
        {
            get
            {
                return storage.FileExists(FileName);
            }
        }

        public ShowsDocument ReadDocument()
        {
            lock (this)
            {
                using (IsolatedStorageFileStream stream = storage.OpenFile(FileName, FileMode.Open, FileAccess.Read))
                {
                    return (ShowsDocument)ShowsSerializer.Deserialize(stream);
                }
            }
        }

        public void WriteDocument(ShowsDocument document)
        {
            lock (this)
            {
                using (IsolatedStorageFileStream stream = storage.CreateFile(FileName))
                {
                    ShowsSerializer.Serialize(stream, document);
                }
            }
        }
    }
}

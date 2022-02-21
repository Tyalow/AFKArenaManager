using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class ContentList
    {
        public List<ContentType> ContentTypes { get; private set; }

        public ContentList(string filePath)
        {
            ContentTypes = new List<ContentType>();
            LoadContent(filePath);
        }

        public void AddContent(ContentType content)
        {
            bool duplicateFound = false;
            foreach (ContentType contentType in ContentTypes)
            {
                if(contentType.ContentName == content.ContentName)
                {
                    duplicateFound = true;
                }
            }
            if(!duplicateFound)
            {
                ContentTypes.Add(content);
            }
        }
        public void LoadContent(string filePath)
        {
            string[] contentArray = File.ReadAllLines(filePath);
            foreach (string content in contentArray)
            {
                string[] parsedContent = content.Split(',');
                ContentTypes.Add(new ContentType(parsedContent[0], Int32.Parse(parsedContent[1])));
            }
        }
        public void SaveContent(string filePath)
        {
            string[] contentString = new string[ContentTypes.Count];

            for (int i = 0; i < ContentTypes.Count; i++)
            {
                contentString[i] = ContentTypes.ElementAt(i).ToString();
            }
            File.WriteAllLines(filePath, contentString);
        }

    }
}

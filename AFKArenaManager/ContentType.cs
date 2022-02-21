using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class ContentType
    {
        public string ContentName { get; set; }
        //Value of content 1-10 Scale
        public int ContentValue { get; set; }

        public ContentType(string contentName, int contentValue)
        {
            ContentName = contentName;
            ContentValue = contentValue;
        }
        public override string ToString()
        {
            return $"{ContentName},{ContentValue}";
        }
    }
}

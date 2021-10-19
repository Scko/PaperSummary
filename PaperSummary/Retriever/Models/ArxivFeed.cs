using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaperSummary.Retriever.Models
{
    [XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class ArxivFeed
    {
        [XmlElement("entry")]
        public List<Entry> Entries;
    }

    [XmlType("entry")]
    public class Entry
    {
        [XmlElement("title")]
        public string Title;
        [XmlElement("summary")]
        public string Summary;
        [XmlElement("published")]
        public DateTime Published;
        [XmlElement("arxiv:doi")]
        public string DOI;
        [XmlElement("id")]
        public string Id;
        [XmlElement("author")]
        public List<Author> Authors;
    }

    public class Author
    {
        [XmlElement("name")]
        public string Name;
    }

}

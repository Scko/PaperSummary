using PaperSummary.Retriever.Interfaces;
using PaperSummary.Retriever.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PaperSummary
{
    public class ArxivRetriever : IRetriever
    {
        public async Task<List<Article>> GetArticles(string query, int maxResults)
        {
            var queryResults = await CallArxiv(query, maxResults);

            var feed = DeserializeXmlString<ArxivFeed>(queryResults);

            return ToArticles(feed);
        }

        private static T DeserializeXmlString<T>(string queryResults)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T results;

            using (TextReader reader = new StringReader(queryResults))
            {
                results = (T)serializer.Deserialize(reader);
            }

            return results;
        }

        private List<Article> ToArticles(ArxivFeed feed)
        {
            return feed.Entries.Select(e => new Article() { Abstract = e.Summary, Published = e.Published, Title = e.Title, Authors = string.Join(", ", e.Authors.Select(a => a.Name)) }).ToList();
        }

        private async Task<string> CallArxiv(string query, int maxResults = 10) 
        {
            using HttpClient client = new HttpClient();
            var uri = $"http://export.arxiv.org/api/query?search_query={query}&start=0&max_results={maxResults}&sortBy=submittedDate&sortOrder=descending";
            string responseBody = await client.GetStringAsync(uri);

            return responseBody;
        }
    }
}

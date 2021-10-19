using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaperSummary.Retriever.Interfaces
{
    public interface IRetriever
    {
        Task<List<Article>> GetArticles(string query, int maxResults);
    }
}

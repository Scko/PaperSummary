using System.Threading.Tasks;

namespace PaperSummary
{
    public interface ISummarizer
    {
        Task ProcessSummaries(string query, int maxArticles, string outputDirectory, int numDays);
    }
}

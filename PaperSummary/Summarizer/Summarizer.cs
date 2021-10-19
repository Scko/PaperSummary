using PaperSummary.Retriever.Interfaces;
using PaperSummary.TTS.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaperSummary
{
    public class Summarizer : ISummarizer
    {
        private readonly IRetriever _retriever;
        private readonly ITTSProcessor _ttsProcessor;
        public Summarizer(IRetriever retriever, ITTSProcessor ttsProcessor) 
        {
            _retriever = retriever;
            _ttsProcessor = ttsProcessor;
        }

        public async Task ProcessSummaries(string query, int maxArticles, string outputDirectory, int numDays)
        {
            var articles = await _retriever.GetArticles(query, maxArticles);
            articles = articles.Where(searchResults => searchResults.Published > DateTime.Now.AddDays(-1*numDays)).ToList();
            if (articles.Any()) 
            {
                Console.WriteLine("Synthesizing...");
                _ttsProcessor.SynthesizeAndSave(articles, outputDirectory);
            } 
            else 
            {
                Console.WriteLine($"Nothing to synthesize.");
            }
        }
    }
}
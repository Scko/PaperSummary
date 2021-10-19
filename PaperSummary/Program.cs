using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaperSummary.Retriever.Interfaces;
using PaperSummary.TTS.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PaperSummary
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = SetupDependencyInjection();

            var settings = GetSettings();

            Console.WriteLine("Setting Google Application Credentials...");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", settings.CredentialPath);

            Console.WriteLine("Paper Summaries started...");
            var summarizer = serviceProvider.GetService<ISummarizer>();
            await summarizer.ProcessSummaries(settings.Query, settings.MaxArticles, settings.OutputFile, settings.NumberOfDays);
            Console.WriteLine("Paper Summaries ending...");
        }

        private static Settings GetSettings()
        {
            IConfiguration config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();

            Settings settings = new Settings();
            config.GetSection("Settings").Bind(settings);

            return settings;
        }

        private static ServiceProvider SetupDependencyInjection()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRetriever, ArxivRetriever>()
                .AddSingleton<ITTSProcessor, TTSProcessor>()
                .AddSingleton<ISpeechSynthesizer, GoogleSpeechSynthesizer>()
                .AddSingleton<ISummarizer, Summarizer>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
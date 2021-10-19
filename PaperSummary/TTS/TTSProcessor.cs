using PaperSummary.TTS.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace PaperSummary
{
    public class TTSProcessor: ITTSProcessor
    {
        private readonly ISpeechSynthesizer _speechSynthesizer;
        public TTSProcessor(ISpeechSynthesizer speechSynthesizer) 
        {
            _speechSynthesizer = speechSynthesizer;
        }

        public void SynthesizeAndSave(List<Article> articles, string outputDirectory) 
        {
            int i = 0;
            foreach (Article article in articles) 
            {
                var audioContent = _speechSynthesizer.Synthesize($"Title: {article.Title}. Abstract: {article.Abstract}");

                Console.WriteLine($"Article '{article.Title}' converted to speech");

                var saveLocation = $"{outputDirectory}/{i}.mp3";
                File.WriteAllBytes(saveLocation, audioContent);
                Console.WriteLine($"Speech file saved at '{saveLocation}'");

                i++;
            }
        }
    }
}

using System.Collections.Generic;

namespace PaperSummary.TTS.Interfaces
{
    public interface ITTSProcessor
    {
        void SynthesizeAndSave(List<Article> articles, string outputDirectory);
    }
}

namespace PaperSummary.TTS.Interfaces
{
    public interface ISpeechSynthesizer
    {
        byte[] Synthesize(string description);
    }
}

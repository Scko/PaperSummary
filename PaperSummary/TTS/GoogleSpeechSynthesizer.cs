using Google.Cloud.TextToSpeech.V1;
using PaperSummary.TTS.Interfaces;

namespace PaperSummary
{
    public class GoogleSpeechSynthesizer : ISpeechSynthesizer
    {
        private readonly TextToSpeechClient _client;
        private readonly VoiceSelectionParams _voice;
        private readonly AudioConfig _audioConfig;
        public GoogleSpeechSynthesizer() 
        {
            _client = TextToSpeechClient.Create();
            _voice = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                SsmlGender = SsmlVoiceGender.Female
            };
            _audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3
            };
        }

        public byte[] Synthesize(string description) 
        {
            var input = new SynthesisInput
            {
                Text = description
            };
            var response = _client.SynthesizeSpeech(input, _voice, _audioConfig);

            return response.AudioContent.ToByteArray();
        }
    }
}

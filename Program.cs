using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Text_to_speech
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"settings.txt";
            
            string[] lines = File.ReadAllLines(fileName);

            SynthesizeAudioAsync(lines[0], lines[1], lines[2]);
        }

        static async Task SynthesizeAudioAsync(string YourSubscriptionKey, string YourServiceRegion, string textToSay)
        {
            var config = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);

            using var synthesizer = new SpeechSynthesizer(config, null);
            var result = await synthesizer.SpeakTextAsync(textToSay);

            using var stream = AudioDataStream.FromResult(result);
            await stream.SaveToWaveFileAsync("./file.wav");
        }


    }
}

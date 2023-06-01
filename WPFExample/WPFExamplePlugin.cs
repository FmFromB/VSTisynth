using System;
using System.Windows.Controls;
using AudioPlugSharp;
using AudioPlugSharpWPF;

namespace WPFExample
{
    public class WPFExamplePlugin : AudioPluginWPF
    {
        AudioIOPort stereoOutput;

        AudioPluginParameter gainParameter = null;
        AudioPluginParameter panParameter = null;

        public WPFExamplePlugin()
        {
            Company = "ISOTROP | WINRAR";
            Website = "https://www.youtube.com/channel/UCV21xKGQKI7gDV4cNNvUrZw";
            Contact = "isotropmusic@gmail.com";
            PluginName = "Korneplod";
            PluginCategory = "Instrument|Synth";
            PluginVersion = "0.0.1";

            // Unique 64bit ID for the plugin
            PluginID = 0x1E92758E710B4947;

            HasUserInterface = true;
            EditorWidth = 200;
            EditorHeight = 100;
        }

        public override void Initialize()
        {
            base.Initialize();

            OutputPorts = new AudioIOPort[] { stereoOutput = new AudioIOPort("Stereo Output", EAudioChannelConfiguration.Stereo) };

            AddParameter(gainParameter = new AudioPluginParameter
            {
                ID = "gain",
                Name = "Gain",
                Type = EAudioPluginParameterType.Float,
                MinValue = -20,
                MaxValue = 6,
                DefaultValue = 0,
                ValueFormat = "{0:0.0}dB"
            });
            AddParameter(panParameter = new AudioPluginParameter
            {
                ID = "pan",
                Name = "Pan",
                Type = EAudioPluginParameterType.Float,
                MinValue = -1,
                MaxValue = 1,
                DefaultValue = 0,
                ValueFormat = "{0:0.0}"
            });
        }

        public override void HandleNoteOn(int noteNumber, float velocity, int sampleOffset)
        {
            Logger.Log("Note on: " + noteNumber + " offset: " + sampleOffset);

            freq = Math.Pow(2, (noteNumber - 69) / 12.0) * 440;
            desiredNoteVolume = velocity * 0.5f;

        }

        public override void HandleNoteOff(int noteNumber, float velocity, int sampleOffset)
        {
            Logger.Log("Note off: " + noteNumber + " offset: " + sampleOffset);

            desiredNoteVolume = 0;
        }

        public override void HandleParameterChange(AudioPluginParameter parameter, double newNormalizedValue, int sampleOffset)
        {
            Logger.Log("Param change: " + parameter.Name + " val: " + parameter.ProcessValue + " offset: " + sampleOffset);

            base.HandleParameterChange(parameter, newNormalizedValue, sampleOffset);
        }

        double Lerp(double value1, double value2, double amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        double noteVolume = 0;
        double desiredNoteVolume = 0;
        long samplesSoFar = 0;
        double freq = 0;
        double linearGain = 1.0f;


        public override void Process()
        {
            base.Process();

            Span<double> outLeftSamples = stereoOutput.GetAudioBuffers()[0];
            Span<double> outRightSamples = stereoOutput.GetAudioBuffers()[1];

            linearGain = Math.Pow(10.0, 0.05 * gainParameter.ProcessValue);
            double pan = panParameter.ProcessValue;

            int currentSample = 0;
            int nextSample = 0;

            do
            {
                // Trigger any events at our current sample, and find out how many samples we have until there are more events
                nextSample = Host.ProcessEvents();

                Logger.Log("Process from " + currentSample + " to " + (nextSample - 1));

                bool needGainUpdate = gainParameter.NeedInterpolationUpdate;
                bool needPanUpdate = panParameter.NeedInterpolationUpdate;

                // Loop over the samples we have remaining until we need to trigger more events
                for (int i = currentSample; i < nextSample; i++)
                {
                    noteVolume = Lerp(noteVolume, desiredNoteVolume, 0.001);

                    // If we need to update our gain paramter, get the sample-accurate interpolated value
                    if (needGainUpdate)
                    {
                        linearGain = Math.Pow(10.0, 0.05 * gainParameter.GetInterpolatedProcessValue(i));
                    }

                    if (needPanUpdate)
                    {
                        pan = panParameter.GetInterpolatedProcessValue(i);
                    }

                    outLeftSamples[i] = Math.Sin(((double)samplesSoFar * 2 * Math.PI * freq) / Host.SampleRate) * noteVolume * linearGain * (1 - pan);
                    outRightSamples[i] = Math.Sin(((double)samplesSoFar * 2 * Math.PI * freq) / Host.SampleRate) * noteVolume * linearGain * (1 + pan);

                    samplesSoFar++;
                }

                currentSample = nextSample;
            }
            while (nextSample < outLeftSamples.Length & nextSample < outRightSamples.Length); // Continue looping until we hit the end of the buffer

            // Write out our managed audio data
            stereoOutput.WriteData();
        }
    }
}

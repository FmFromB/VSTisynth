using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class TremoloSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _sourse;
        private SignalGenerator _lfo;

        public int LfoFrequency { get; set; }
        public float LfoGain { get; set; }

        public WaveFormat WaveFormat => _sourse.WaveFormat;

        public TremoloSampleProvider(ISampleProvider sourse, int lfoFrequency = 5, float lfoAmplitude = 1.0f)
        {
            _sourse = sourse;
            LfoFrequency = lfoFrequency;
            LfoGain = LfoGain;
            UpdateLowFrequencyOscillator();
        }

        public void UpdateLowFrequencyOscillator()
        {
            _lfo = new SignalGenerator(_sourse.WaveFormat.SampleRate, _sourse.WaveFormat.Channels)
            {
                Type = SignalGeneratorType.Sin,
                Frequency = LfoFrequency,
                Gain = LfoGain,
            };
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _sourse.Read(buffer, offset, count);
            var lfoBuffer = new float[count];
            _lfo.Read(lfoBuffer, offset, count);
           
            for(int i = 0; i < samples; i++)
            {
                if (_lfo.Gain > 0.0f)
                {
                    buffer[offset + i] += (buffer[offset + i] * lfoBuffer[offset + i]);
                }
            }
            return samples;
        }
    }
}

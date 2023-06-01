using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class SynthWaveProvider : ISampleProvider
    {
        private int _sampleRate;
        private readonly int _note;
        private readonly double _twelfthRootOfTwo = Math.Pow(2, 1.0 / 12.0);
        private readonly SynthSignalGenerator _sourse;
        private readonly EnvelopeGenerator _adsr;
        private double baseFrequency;
        private bool enableSubOsc;
        public WaveFormat WaveFormat { get; }
        public bool EnableSubOsc 
        {
            get
            {
                return enableSubOsc;
            }
            set
            {
                enableSubOsc = value;
                if(_sourse != null)
                {
                    _sourse.EnableSubOsc = value;
                }
            }
        }

        // ADSR переменные

        private float attackSeconds;
        private float decaySeconds;
        private float sustainLevel;
        private float releaseSeconds;
      
        public float AttackSeconds
        {
            get
            {
                return AttackSeconds;
            }
            set
            {
                attackSeconds = value;
                _adsr.AttackRate = attackSeconds * WaveFormat.SampleRate;
            }
        }

        public float DecaySeconds
        {
            get
            {
                return DecaySeconds;
            }
            set
            {
                decaySeconds = value;
                _adsr.DecayRate = decaySeconds * WaveFormat.SampleRate;
            }
        }

        public float SustainLevel
        {
            get
            {
                return SustainLevel;
            }
            set
            {
                sustainLevel = value;
                _adsr.SustainLevel = sustainLevel;
            }
        }

        public float ReleaseSeconds
        {
            get
            {
                return ReleaseSeconds;
            }
            set
            {
                releaseSeconds = value;
                _adsr.ReleaseRate = releaseSeconds * WaveFormat.SampleRate;
            }
        }

        public double BaseFrequency 
        {
            get 
            {
                return baseFrequency;
            }
            set
            {
                baseFrequency = value;
                if(_sourse != null)
                {
                    _sourse.Frequency = Frequency;
                }
            } 
        }

        public double Frequency => BaseFrequency * Math.Pow(_twelfthRootOfTwo, _note);
        private double lfoFrequency;
        public double LfoFrequency 
        { 
            get
            {
                return lfoFrequency;
            }
            set
            {
                lfoFrequency = value;
                _sourse.LfoFrequency = value;
            }
        }
        private double lfoGain;
        public double LfoGain 
        {
            get
            {
                return lfoGain;
            }
            set
            {
                lfoGain = value;
                _sourse.LfoGain = value;
            }
        }

        public SynthWaveProvider(SignalGeneratorType waveType = SignalGeneratorType.Sin, 
            int sampleRate = 44100, int note = 0, float level = 1.0f)
        {
            _note = note;
            _sampleRate = sampleRate;
            var channels = 1; // Моно звук
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, channels);
            _adsr = new EnvelopeGenerator();

            //ADSR По умолчанию
            AttackSeconds = 0.0f;
            DecaySeconds = 0.0f;
            SustainLevel = 1.0f;
            ReleaseSeconds = 0.0f;


            EnableSubOsc = false;

            _sourse = new SynthSignalGenerator(_sampleRate, channels, LfoGain, LfoFrequency)
            {
                Frequency = Frequency,
                Type = waveType,
                Gain = level,
            };

            _adsr.Gate(true);
        }

        public void Stop()
        {
            _adsr.Gate(false);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_adsr.State == EnvelopeGenerator.EnvelopeState.Idle)
            {
                return 0;
            }
            var samples = _sourse.Read(buffer, offset, count);
            for (int i = 0; i < samples; i++)
            {
                buffer[offset++] *= _adsr.Process();  
            }
            return samples;        
        }

        public static implicit operator double(SynthWaveProvider v)
        {
            throw new NotImplementedException();
        }
    }
}
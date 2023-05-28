﻿using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace synthesizer
{
    public class NotchFilterSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider _sourse;
        private readonly BiQuadFilter _filter;

        public WaveFormat WaveFormat => _sourse.WaveFormat;

        public NotchFilterSampleProvider(ISampleProvider sourse, int cutOffFrequency = 1500, float q = 0.7f)
        {
            _sourse = sourse;
            _filter = BiQuadFilter.NotchFilter(_sourse.WaveFormat.SampleRate, cutOffFrequency, q);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var samples = _sourse.Read(buffer, offset, count);

            for (int i = 0; i < samples; i++)
            {
                buffer[offset + i] = _filter.Transform(buffer[offset + i]);
            }

            return samples;
        }
    }
}

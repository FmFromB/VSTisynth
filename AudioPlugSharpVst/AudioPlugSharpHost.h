#pragma once

#include "AudioPlugSharpController.h"
#include "public.sdk/source/vst/vstaudioprocessoralgo.h"
#include "public.sdk/source/vst/vsteditcontroller.h"
#include "public.sdk/source/vst/vstaudioeffect.h"

using namespace Steinberg;
using namespace Steinberg::Vst;

using namespace AudioPlugSharp;

public ref class AudioPlugSharpHost : public IAudioHost
{
public:
	// Inherited via IAudioHost
	virtual property double SampleRate;
	virtual property unsigned int MaxAudioBufferSize;
	virtual property AudioPlugSharp::EAudioBitsPerSample BitsPerSample;
	virtual property double BPM;

	virtual void AudioPlugSharpHost::ProcessAllEvents()
	{
		int nextSample = 0;

		do
		{
			nextSample = ProcessEvents();
		}
		while (nextSample < processData->numSamples);
	}

	virtual int AudioPlugSharpHost::ProcessEvents()
	{
		int minSample = processData->numSamples;

		// Handle parameter changes
		if (processData->inputParameterChanges != nullptr)
		{
			int32 numParamsChanged = processData->inputParameterChanges->getParameterCount();

			for (int32 i = 0; i < numParamsChanged; i++)
			{
				IParamValueQueue* paramQueue = processData->inputParameterChanges->getParameterData(i);

				if (paramQueue != nullptr)
				{
					ParamValue value;
					int32 sampleOffset;
					int32 numPoints = paramQueue->getPointCount();

					ParamID paramID = paramQueue->getParameterId();
					
					AudioPlugSharp::AudioPluginParameter^ param = plugin->Processor->Parameters[paramID - PLUGIN_PARAMETER_USER_START];

					for (int32 i = startEvent; i < numPoints; i++)
					{
						if (paramQueue->getPoint(i, sampleOffset, value) == kResultTrue)
						{
							if (sampleOffset == 0)
							{
								plugin->Processor->HandleParameterChange(param, value, sampleOffset);
							}
							else if (sampleOffset > eventSampleOffset)
							{
								plugin->Processor->HandleParameterChange(param, value, sampleOffset);
								minSample = sampleOffset + 1;

								startEvent = i + 1;

								break;
							}
						}
					}
				}
			}
		}
			
		// Handle MIDI events
		if (processData->inputEvents != nullptr)
		{
			int32 numEvent = processData->inputEvents->getEventCount();

			for (int32 i = 0; i < numEvent; i++)
			{
				Event event;

				if (processData->inputEvents->getEvent(i, event) == kResultOk)
				{
					if (event.sampleOffset == eventSampleOffset)
					{
						switch (event.type)
						{
							case Event::kNoteOnEvent:
							{
								plugin->Processor->HandleNoteOn(event.noteOn.pitch, event.noteOn.velocity, event.sampleOffset);

								break;
							}
							case Event::kNoteOffEvent:
							{
								plugin->Processor->HandleNoteOff(event.noteOff.pitch, event.noteOff.velocity, event.sampleOffset);

								break;
							}
							case Event::kPolyPressureEvent:
								plugin->Processor->HandlePolyPressure(event.polyPressure.pitch, event.polyPressure.pressure, event.sampleOffset);

								break;
							}
					}
					else if (event.sampleOffset > eventSampleOffset)
					{
						if (event.sampleOffset < minSample)
							minSample = event.sampleOffset;

						break;
					}
				}
			}
		}

		eventSampleOffset = minSample;

		return eventSampleOffset;
	}

	virtual void AudioPlugSharpHost::SendNoteOn(int noteNumber, float velocity, int sampleOffset)
	{
		Event event;

		event.type = Event::kNoteOnEvent;
		event.noteOn.channel = 1;
		event.noteOn.pitch = noteNumber;
		event.noteOn.velocity = velocity;

		processData->outputEvents->addEvent(event);
	}

	virtual void AudioPlugSharpHost::SendNoteOff(int noteNumber, float velocity, int sampleOffset)
	{
		Event event;

		event.type = Event::kNoteOffEvent;
		event.noteOff.channel = 1;
		event.noteOff.pitch = noteNumber;
		event.noteOff.velocity = velocity;

		processData->outputEvents->addEvent(event);
	}

	virtual void AudioPlugSharpHost::SendPolyPressure(int noteNumber, float pressure, int sampleOffset)
	{
		Event event;

		event.type = Event::kNoteOnEvent;
		event.polyPressure.channel = 1;
		event.polyPressure.pitch = noteNumber;
		event.polyPressure.pressure = pressure;

		processData->outputEvents->addEvent(event);
	}

	virtual void AudioPlugSharpHost::BeginEdit(int parameter)
	{
		controller->beginEdit(parameter + PLUGIN_PARAMETER_USER_START);
	}

	virtual void AudioPlugSharpHost::PerformEdit(int parameter, double normalizedValue)
	{
		controller->performEdit(parameter + PLUGIN_PARAMETER_USER_START, (float)normalizedValue);
	}

	virtual void AudioPlugSharpHost::EndEdit(int parameter)
	{
		controller->endEdit(parameter + PLUGIN_PARAMETER_USER_START);
	}

internal:
	ProcessData* processData = nullptr;
	property AudioPlugSharp::IAudioPlugin^ plugin;
	AudioPlugSharpController* controller = nullptr;

	void SetProcessData(ProcessData* data)
	{
		eventSampleOffset = 0;
		startEvent = 0;
		processData = data;
	}

private:
	int eventSampleOffset = 0;
	int startEvent = 0;
		
};


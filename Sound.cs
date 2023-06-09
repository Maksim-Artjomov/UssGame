using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace UssGame
{
    class Sound
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private float outputVolume;

        public Sound(string fail)
        {
            audioFile = new AudioFileReader(fail);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputVolume = 1.0f;
        }
        public void SetVolume(float volume)
        {
            outputVolume = volume;
            outputDevice.Volume = outputVolume;
        }
        public void Play()
        {
            outputDevice.Play();
        }

        public void Stop()
        {
            outputDevice.Stop();
            audioFile.Position = 0;
        }
        public bool IsPlaying()
        {
            return outputDevice.PlaybackState == PlaybackState.Playing;
        }
    }
}

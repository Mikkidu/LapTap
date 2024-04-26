using UnityEngine;

namespace AlexDev.CatchMe.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip clip;

        [Range(0, 1f)]
        public float volume;

    }
}

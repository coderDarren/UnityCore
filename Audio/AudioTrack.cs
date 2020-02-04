using UnityEngine;

namespace UnityCore {

    namespace Audio {

        [System.Serializable]
        public class AudioObject {
            public AudioType type;
            public AudioClip clip;
        }

        [System.Serializable]
        public class AudioTrack
        {
            public AudioSource source;
            public AudioObject[] audio;
        }
    }
}

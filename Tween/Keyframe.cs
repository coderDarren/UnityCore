
using UnityEngine;

namespace Tween {

    [System.Serializable]
    public class Keyframe<T>
    {
        public T value;
        [Range(0,1)]
        public float nTime;
    }
}

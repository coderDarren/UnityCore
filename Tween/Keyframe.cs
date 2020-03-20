
using UnityEngine;

namespace Tween {

    public class Keyframe<T>
    {
        public T value;
        [Range(0,1)]
        public float nTime;

        public Keyframe(T _val, float _n) {
            this.value = _val;
            this.nTime = _n;
        }
    }

    [System.Serializable]
    public class Vec3Keyframe
    {
        public Vector3 value;
        [Range(0,1)]
        public float nTime;
    }

    [System.Serializable]
    public class FloatKeyframe
    {
        public float value;
        [Range(0,1)]
        public float nTime;
    }

    [System.Serializable]
    public class ColorKeyframe
    {
        public Color value;
        [Range(0,1)]
        public float nTime;
    }
}

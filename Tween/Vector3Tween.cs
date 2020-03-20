using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween {

    /// <summary>
    /// Vector3Tween is a MonoBehaviour wrapper class for Vector3 tween objects
    /// It is responsible for responding to events from the tween process
    /// </summary>
    public abstract class Vector3Tween : MonoBehaviour
    {
        //public Keyframe<Vector3>[] keys;
        public Vec3Keyframe[] keys;
        public float duration;
        public float delay;
        public bool wrap;

        protected Tweener<Vector3> m_Tween;

#region Unity Functions
        private void OnEnable() {
            Init();
        }

        private void OnDisable() {
            Dispose();
        }
#endregion

#region Private Functions
    private Keyframe<Vector3>[] GenericKeys(Vec3Keyframe[] _keys) {
        var _ret = new List<Keyframe<Vector3>>();
        foreach (Vec3Keyframe _frame in _keys) {
            _ret.Add(new Keyframe<Vector3>(_frame.value, _frame.nTime));
        }
        return _ret.ToArray();
    }
#endregion

#region Override Functions
        protected virtual void Init() {
            m_Tween = new Tweener<Vector3>(GenericKeys(keys), duration, delay, wrap);
            if (m_Tween.Loop != null) {
                m_Tween.OnSetValue += OnSetValue;
                m_Tween.OnMoveValue += OnMoveValue;
                StartCoroutine(m_Tween.Loop);
            }
        }

        protected virtual void Dispose() {
            if (m_Tween.Loop != null) {
                m_Tween.OnSetValue -= OnSetValue;
                m_Tween.OnMoveValue -= OnMoveValue;
                StopCoroutine(m_Tween.Loop);
            }
        }

        protected abstract void OnSetValue(Vector3 _val);
        protected abstract void OnMoveValue(Vector3 _curr, Vector3 _target, float _nTime);
#endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween {

    /// <summary>
    /// FloatTween is a MonoBehaviour wrapper class for float tween objects
    /// It is responsible for responding to events from the tween process
    /// </summary>
    public abstract class FloatTween : MonoBehaviour
    {

        //public Keyframe<float>[] keys;
        public FloatKeyframe[] keys;
        public float duration;
        public float delay;
        public bool wrap;

        protected Tweener<float> m_Tween;

#region Unity Functions
        private void OnEnable() {
            Init();
        }

        private void OnDisable() {
            Dispose();
        }
#endregion

#region Private Functions
    private Keyframe<float>[] GenericKeys(FloatKeyframe[] _keys) {
        var _ret = new List<Keyframe<float>>();
        foreach (FloatKeyframe _frame in _keys) {
            _ret.Add(new Keyframe<float>(_frame.value, _frame.nTime));
        }
        return _ret.ToArray();
    }
#endregion

#region Override Functions
        protected virtual void Init() {
            m_Tween = new Tweener<float>(GenericKeys(keys), duration, delay, wrap);
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

        protected abstract void OnSetValue(float _val);
        protected abstract void OnMoveValue(float _curr, float _target, float _nTime);
#endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween {

    /// <summary>
    /// ColorTween is a MonoBehaviour wrapper class for Color tween objects
    /// It is responsible for responding to events from the tween process
    /// </summary>
    public abstract class ColorTween : MonoBehaviour
    {

        public Keyframe<Color>[] keys;
        public float duration;
        public float delay;
        public bool wrap;
        public bool runOnEnable=true;

        protected Tweener<Color> m_Tween;

#region Unity Functions
        private void OnEnable() {
            if (runOnEnable)
                StartTween();
        }

        private void OnDisable() {
            StopTween();
        }
#endregion

#region Public Functions
        public void StartTween() {
            Init();
        }

        public void StopTween() {
            Dispose();
        }
#endregion

#region Override Functions
        protected virtual void Init() {
            m_Tween = new Tweener<Color>(keys, duration, delay, wrap);
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

        protected abstract void OnSetValue(Color _val);
        protected abstract void OnMoveValue(Color _curr, Color _target, float _nTime);
#endregion
    }
}

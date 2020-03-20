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

        //public Keyframe<Color>[] keys;
        public ColorKeyframe[] keys;
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

#region Private Functions
    private Keyframe<Color>[] GenericKeys(ColorKeyframe[] _keys) {
        var _ret = new List<Keyframe<Color>>();
        foreach (ColorKeyframe _frame in _keys) {
            _ret.Add(new Keyframe<Color>(_frame.value, _frame.nTime));
        }
        return _ret.ToArray();
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
            m_Tween = new Tweener<Color>(GenericKeys(keys), duration, delay, wrap);
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

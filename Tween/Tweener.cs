using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween {

    public class Tweener<T>
    {

        public delegate void SetValueDelegate(T _val);
        public delegate void MoveValueDelegate(T _curr, T _target, float _nTime);
        public event SetValueDelegate OnSetValue;
        public event MoveValueDelegate OnMoveValue;
        
        private Keyframe<T>[] m_Keys;
        private float m_Duration;
        private float m_Delay;
        private IEnumerator m_Loop;
        private int m_Index = -1;
        private bool m_Wrap;

        public IEnumerator Loop {
            get {
                return m_Loop;
            }
        }

#region Constructors
        public Tweener(Keyframe<T>[] _keys, float _duration, float _delay, bool _wrap) {
            m_Keys = _keys;
            m_Duration = _duration;
            m_Delay = _delay;
            m_Wrap = _wrap;

            if (m_Keys.Length > 0) {
                m_Loop = RunTween();
            }
        }
#endregion

#region Private Functions
        private IEnumerator RunTween() {
            yield return new WaitForSeconds(m_Delay);
            
            var _key = GetNextKey();
            var _nextKey = GetNextKey();
            float _timer = 0;
            float _time = Mathf.Abs(_nextKey.nTime - _key.nTime) * m_Duration;

            SetValue(_key.value);
            
            // run until coroutine is stopped
            while (true) {
                _timer += Time.deltaTime;

                MoveValue(_key.value, _nextKey.value, _timer / _time);

                if (_timer > _time) {
                    if (m_Wrap) {
                        _key = _nextKey;
                    } else {
                        _key = GetNextKey();
                    }
                    _nextKey = GetNextKey();
                    _time = Mathf.Abs(_nextKey.nTime - _key.nTime) * m_Duration;
                    _timer = 0;
                }

                yield return null;
            }
        }

        private Keyframe<T> GetNextKey() {
            m_Index ++;
            if (m_Index > m_Keys.Length - 1) {
                m_Index = 0;
            }
            return m_Keys[m_Index];
        }
        
        private void SetValue(T _val) {
            try {
                OnSetValue(_val);
            } catch (System.Exception) {}
        }

        private void MoveValue(T _current, T _target, float _nTime) {
            try {
                OnMoveValue(_current, _target, _nTime);
            } catch (System.Exception) {}
        }
#endregion

    }
}

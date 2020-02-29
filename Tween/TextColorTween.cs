
using UnityEngine;
using UnityEngine.UI;

namespace Tween {

    [RequireComponent(typeof(Text))]
    public class TextColorTween : ColorTween
    {
        private Text m_Text;

#region Override Functions
        protected override void Init() {
            m_Text = GetComponent<Text>();
            base.Init();
        }

        protected override void OnSetValue(Color _val) {
            m_Text.color = _val;
        }

        protected override void OnMoveValue(Color _curr, Color _target, float _nTime) {
            m_Text.color = Color.Lerp(_curr, _target, _nTime);
        }
#endregion

    }

}

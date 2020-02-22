
using UnityEngine;
using UnityEngine.UI;

namespace Tween {

    [RequireComponent(typeof(Image))]
    public class ImageColorTween : ColorTween
    {
        private Image m_Image;

#region Override Functions
        protected override void Init() {
            m_Image = GetComponent<Image>();
            base.Init();
        }

        protected override void OnSetValue(Color _val) {
            m_Image.color = _val;
        }

        protected override void OnMoveValue(Color _curr, Color _target, float _nTime) {
            m_Image.color = Color.Lerp(_curr, _target, _nTime);
        }
#endregion

    }

}

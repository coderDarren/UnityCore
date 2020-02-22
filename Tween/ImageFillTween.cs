
using UnityEngine;
using UnityEngine.UI;

namespace Tween {

    [RequireComponent(typeof(Image))]
    public class ImageFillTween : FloatTween
    {
        private Image m_Image;

#region Override Functions
        protected override void Init() {
            m_Image = GetComponent<Image>();
            base.Init();
        }

        protected override void OnSetValue(float _val) {
            m_Image.fillAmount = _val;
        }

        protected override void OnMoveValue(float _curr, float _target, float _nTime) {
            m_Image.fillAmount = Mathf.Lerp(_curr, _target, _nTime);
        }
#endregion

    }

}

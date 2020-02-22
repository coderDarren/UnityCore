
using UnityEngine;

namespace Tween {

    public class RotationTween : Vector3Tween
    {
#region Override Functions
        protected override void OnSetValue(Vector3 _val) {
            transform.localRotation = Quaternion.Euler(_val);
        }

        protected override void OnMoveValue(Vector3 _curr, Vector3 _target, float _nTime) {
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(_curr, _target, _nTime));
        }
#endregion

    }

}

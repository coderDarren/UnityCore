using System.Threading.Tasks;
using UnityEngine;

namespace UnityCore {
    
    namespace Menu {

        public class Page : MonoBehaviour
        {
            public PageType type;
            public bool useAnimation;
            public bool isAnimating {get;private set;}

            /*
             * Animaton Requirements...
             *  - This class uses certain controls to determine page state
             *  - Pages have three core states:
             *      1. Resting
             *      2. Turning On
             *      3. Turning Off
             *  - The animator must have a control boolean called 'on'. Otherwise a flag will be thrown.
             */
            private Animator m_Animator;

#region Unity Functions
            private void OnEnable() {
                CheckAnimatorIntegrity();
            }
#endregion

#region Public Functions
            /// <summary>
            /// Call this to turn the page on or off by setting the control '_on'
            /// </summary>
            public async Task<bool> Animate(bool _on) {
                if (useAnimation) {
                    m_Animator.SetBool("on", _on);
                    
                    // wait for animation to finish
                    isAnimating = true;
                    while (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
                        await Task.Delay(50);
                    }
                    isAnimating = false;

                    Log("Page ["+type+"] finished transitioning to "+(_on ? "<color=#0f0>on</color>." : "<color=#f00>off</color>."));
                }

                return true;
            }
#endregion

#region Private Functions
            private void CheckAnimatorIntegrity() {
                if (useAnimation) {
                    // try to get animator
                    m_Animator = GetComponent<Animator>();
                    if (!m_Animator) {
                        LogWarning("You opted to animate page ["+type+"], but no Animator component exists on the object.");
                    }
                }
            }

            private void Log(string _msg) {
                Debug.Log("[Page]: "+_msg);
            }

            private void LogWarning(string _msg) {
                Debug.LogWarning("[Page]: "+_msg);
            }
#endregion
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {

    namespace Menu {

        public class TestMenu : MonoBehaviour
        {
            public PageController pageController;

 #if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.F)) {
                    pageController.TurnPageOn(PageType.Loading);
                }
                if (Input.GetKeyUp(KeyCode.G)) {
                    pageController.TurnPageOff(PageType.Loading);
                }
                if (Input.GetKeyUp(KeyCode.H)) {
                    pageController.TurnPageOff(PageType.Loading, PageType.Menu);
                }
                if (Input.GetKeyUp(KeyCode.J)) {
                    pageController.TurnPageOff(PageType.Loading, PageType.Menu, true);
                }
            }
#endif           
        }
    }
}

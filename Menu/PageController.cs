using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityCore {
    
    namespace Menu {

        public class PageController : MonoBehaviour
        {
            public static PageController instance;

            public bool debug;
            public PageType entryPage;
            public Page[] pages;

            private Hashtable m_Pages;
            private List<Page> m_OnList;
            private List<Page> m_OffList;

#region Unity Functions
            private void Awake() {
                if (!instance) {
                    instance = this;
                    m_Pages = new Hashtable();
                    m_OnList = new List<Page>();
                    m_OffList = new List<Page>();
                    RegisterAllPages();

                    if (entryPage != PageType.None) {
                        TurnPageOn(entryPage);
                    }
                    
                    DontDestroyOnLoad(gameObject);
                } else {
                    Destroy(gameObject);
                }
            }
#endregion

#region Public Functions
            /// <summary>
            /// Turn on page with type '_type'
            /// </summary>
            public void TurnPageOn(PageType _type) {
                if (_type == PageType.None) return;
                if (!PageExists(_type)) {
                    LogWarning("You are trying to turn a page on ["+_type+"] that has not been registered.");
                    return;
                }

                Page _page = GetPage(_type);
                _page.gameObject.SetActive(true);
                _page.Animate(true);
            }

            /// <summary>
            /// Turn off page with type '_off'
            /// Optionally turn page with type '_on' on
            /// Optionally wait for page to exit before turning on optional page
            /// </summary>
            public void TurnPageOff(PageType _off, PageType _on=PageType.None, bool _waitForExit=false) {
                if (_off == PageType.None) return;
                if (!PageExists(_off)) {
                    LogWarning("You are trying to turn a page off ["+_on+"] that has not been registered.");
                    return;
                }

                Page _offPage = GetPage(_off);
                if (_offPage.gameObject.activeSelf) {
                    _offPage.Animate(false);
                }

                if (_waitForExit && _offPage.useAnimation) {
                    Page _onPage = GetPage(_on);
                    StopCoroutine("WaitForPageExit");
                    StartCoroutine(WaitForPageExit(_onPage, _offPage));
                } else {
                    TurnPageOn(_on);
                }
            }

            public bool PageIsOn(PageType _type) {
                if (!PageExists(_type)) {
                    LogWarning("You are trying to detect if a page is on ["+_type+"], but it has not been registered.");
                    return false;
                }
                
                return GetPage(_type).isOn;
            }
#endregion

#region Private Functions
            private IEnumerator WaitForPageExit(Page _on, Page _off) {
                while (_off.targetState != Page.FLAG_NONE) {
                    yield return null;
                }

                TurnPageOn(_on.type);
            }

            private void RegisterAllPages() {
                foreach(Page _page in pages) {
                    RegisterPage(_page);
                }
            }

            private void RegisterPage(Page _page) {
                if (PageExists(_page.type)) {
                    LogWarning("You are trying to register a page ["+_page.type+"] that has already been registered: <color=#f00>"+_page.gameObject.name+"</color>.");
                    return;
                }
                
                m_Pages.Add(_page.type, _page);
                Log("Registered new page ["+_page.type+"].");
            }

            private Page GetPage(PageType _type) {
                if (!PageExists(_type)) {
                    LogWarning("You are trying to get a page ["+_type+"] that has not been registered.");
                    return null;
                }

                return (Page)m_Pages[_type];
            }

            private bool PageExists(PageType _type) {
                return m_Pages.ContainsKey(_type);
            }

            private void Log(string _msg) {
                if (!debug) return;
                Debug.Log("[Page Controller]: "+_msg);
            }

            private void LogWarning(string _msg) {
                if (!debug) return;
                Debug.LogWarning("[Page Controller]: "+_msg);
            }
#endregion
        }
    }
}
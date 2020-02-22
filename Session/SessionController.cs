
using UnityEngine;
using UnityCore.Menu;

namespace UnityCore {

    namespace Session {

        public class SessionController : MonoBehaviour
        {
            public static SessionController instance;

            private long m_SessionStartTime;
            private bool m_IsPaused;
            private GameController m_Game;
            private float m_FPS;

            public long sessionStartTime {
                get {
                    return m_SessionStartTime;
                }
            }

            public float fps {
                get {
                    return m_FPS;
                }
            }
            
#region Unity Functions
            private void Awake() {
                Configure();
            }

            private void OnApplicationFocus(bool _focus) {
                if (_focus) {
                    // Open a window to unpause the game
                    PageController.instance.TurnPageOn(PageType.PausePopup);
                } else {
                    // Flag the game paused
                    m_IsPaused = true;
                }
            }

            private void Update() {
                if (m_IsPaused) return;
                m_Game.OnUpdate();
                m_FPS = Time.frameCount / Time.time;
            }
#endregion

#region Public Functions
            public void InitializeGame(GameController _game) {
                m_Game = _game;
                m_Game.OnInit();
            }

            public void UnPause() {
                m_IsPaused = false;
            }
#endregion

#region Private Functions
            /// <summary>
            /// Initialize the singleton pattern!
            /// </summary>
            private void Configure() {
                if (!instance) {
                    instance = this;
                    StartSession();
                    DontDestroyOnLoad(gameObject);
                } else {
                    Destroy(gameObject);
                }
            }

            private void StartSession() {
                m_SessionStartTime = EpochSeconds();
            }

            private long EpochSeconds() {
                var _epoch = new System.DateTimeOffset(System.DateTime.UtcNow);
                return _epoch.ToUnixTimeSeconds();
            }
#endregion
        }
    }
}

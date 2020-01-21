
using UnityEngine;

namespace UnityCore {
    
    namespace Data {

        public class DataController : MonoBehaviour
        {
            private static readonly string DATA_SCORE = "score";
            private static readonly string DATA_HIGHSCORE = "highscore";
            private static readonly int DEFAULT_INT = 0;

            public bool debug;

#region Properties
            public int Highscore {
                get {
                    return GetInt(DATA_HIGHSCORE);
                }
                private set {
                    SaveInt(DATA_HIGHSCORE, value);
                }
            }

            public int Score {
                get {
                    return GetInt(DATA_SCORE);
                }
                set {
                    SaveInt(DATA_SCORE, value);
                    int _score = this.Score;
                    if (_score > this.Highscore) {
                        this.Highscore = _score;
                    }
                }
            }
#endregion

#region Unity Functions
#if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.R)) {
                    TestAddScore(1);
                    Log("[Test] Score = "+this.Score+" | Highscore = "+this.Highscore);
                }

                if (Input.GetKeyUp(KeyCode.T)) {
                    TestAddScore(-1);
                    Log("[Test] Score = "+this.Score+" | Highscore = "+this.Highscore);
                }

                if (Input.GetKeyUp(KeyCode.Space)) {
                    TestResetHighscore();
                    TestResetScore();
                    Log("[Test] Score = "+this.Score+" | Highscore = "+this.Highscore);
                }
            }
#endif
#endregion

#region Private Functions
            private void SaveInt(string _data, int _value) {
                PlayerPrefs.SetInt(_data, _value);
            }

            private int GetInt(string _data) {
                return PlayerPrefs.GetInt(_data, DEFAULT_INT);
            }

            private void Log(string _msg) {
                if (!debug) return;
                Debug.Log("[DataController]: "+_msg);
            }

            private void LogWarning(string _msg) {
                if (!debug) return;
                Debug.LogWarning("[DataController]: "+_msg);
            }
#endregion

#region Tests
            private void TestAddScore(int _delta) {
                this.Score += _delta;
            }

            private void TestResetScore() {
                this.Score = 0;
            }
            
            private void TestResetHighscore() {
                this.Highscore = 0;
            }
#endregion
        }
    }
}
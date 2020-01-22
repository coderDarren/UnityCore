using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {

    namespace Data {

        public class TestData : MonoBehaviour
        {
            public DataController data;

#region Unity Functions
#if UNITY_EDITOR
            private void Update() {
                if (Input.GetKeyUp(KeyCode.R)) {
                    TestAddScore(1);
                    Debug.Log("[Test] Score = "+data.Score+" | Highscore = "+data.Highscore);
                }

                if (Input.GetKeyUp(KeyCode.T)) {
                    TestAddScore(-1);
                    Debug.Log("[Test] Score = "+data.Score+" | Highscore = "+data.Highscore);
                }

                if (Input.GetKeyUp(KeyCode.Space)) {
                    TestResetScore();
                    Debug.Log("[Test] Score = "+data.Score+" | Highscore = "+data.Highscore);
                }
            }
#endif
#endregion

            private void TestAddScore(int _delta) {
                data.Score += _delta;
            }

            private void TestResetScore() {
                data.Score = 0;
            }
        }
    }
}

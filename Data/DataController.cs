
using UnityEngine;

namespace UnityCore {
    
    namespace Data {

        public class DataController : MonoBehaviour
        {
            public static readonly string DATA_SCORE = "score";
            public static readonly string DATA_HIGHSCORE = "highscore";

            private static readonly int DEFAULT_INT = 0;
            private static readonly float DEFAULT_FLOAT = 0.0F;
            private static readonly string DEFAULT_STRING = "";

            public bool debug;

#region Public Functions
            public T GetData<T>(string _data, bool _createIfNull=true) {
                if (!DataExists(_data)) {
                    string _warning = "Data ["+_data+"] does not exist.";
                    if (_createIfNull) {
                        _warning += "Creating..";
                        LogWarning(_warning);
                    } else {
                        _warning += "Returning default..";
                        LogWarning(_warning);
                        return default(T);
                    }
                }

                if (typeof(T) == typeof(int)) {
                    return PlayerPrefs.GetInt(_data, DEFAULT_INT);
                } else if (typeof(T) == typeof(float)) {
                    return PlayerPrefs.GetFloat(_data, DEFAULT_FLOAT);
                } else if (typeof(T) == typeof(string)) {
                    return PlayerPrefs.GetString(_data, DEFAULT_STRING);
                }
                
                return default(T);
            }

            public void SaveData<T>(string _data, T _val) {
                if (typeof(T) == typeof(int)) {
                    return PlayerPrefs.SetInt(_data, _val);
                } else if (typeof(T) == typeof(float)) {
                    return PlayerPrefs.SetFloat(_data, _val);
                } else if (typeof(T) == typeof(string)) {
                    return PlayerPrefs.SetString(_data, _val);
                }
            }
#endif

#region Private Functions
            private bool DataExists(string _data) {
                return PlayerPrefs.HasKey(_data);
            }

            private void LogWarning(string _msg) {
                if (!debug) return;
                Debug.LogWarning("[DataController]: "+_msg);
            }
#endif

#region Tests

#endregion
        }
    }
}
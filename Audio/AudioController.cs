using System.Collections;
using UnityEngine;

namespace UnityCore {
    
    namespace Audio {

        public class AudioController : MonoBehaviour
        {
            public static AudioController instance;

            public bool debug;
            public AudioTrack[] tracks;

            private Hashtable m_AudioTable; // relationship of audio types (key) and tracks (value)
            private Hashtable m_JobTable;   // relationship between audio types (key) and jobs (value)

            private enum AudioAction {
                START,
                STOP,
                RESTART
            }

            [System.Serializable]
            public class AudioObject {
                public AudioType type;
                public AudioClip clip;
            }

            [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;
            }

            private class AudioJob {
                public AudioAction action;
                public AudioType type;
                public bool fade;
                public float delay;

                public AudioJob(AudioAction _action, AudioType _type, bool _fade, float _delay) {
                    action = _action;
                    type = _type;
                    fade = _fade;
                    delay = _delay;
                }
            }

#region Unity Functions
            private void Awake() {
                if (!instance) {
                    Configure();
                }
            }

            private void OnDisable() {
                Dispose();
            }
#endregion

#region Public Functions
            public void PlayAudio(AudioType _type, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.START, _type, _fade, _delay));
            }

            public void StopAudio(AudioType _type, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.STOP, _type, _fade, _delay));
            }

            public void RestartAudio(AudioType _type, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.RESTART, _type, _fade, _delay));
            }
#endregion

#region Private Functions
            private void Configure() {
                instance = this;
                m_AudioTable = new Hashtable();
                m_JobTable = new Hashtable();
                GenerateAudioTable();
            }

            private void Dispose() {
                // cancel all jobs in progress
                foreach(DictionaryEntry _kvp in m_JobTable) {
                    IEnumerator _job = (IEnumerator)_kvp.Value;
                    StopCoroutine(_job);
                }
            }

            private void AddJob(AudioJob _job) {
                // cancel any job that might be using this job's audio source
                RemoveConflictingJobs(_job.type);

                IEnumerator _jobRunner = RunAudioJob(_job);
                m_JobTable.Add(_job.type, _jobRunner);
                StartCoroutine(_jobRunner);
                Log("Starting job on ["+_job.type+"] with operation: "+_job.action);
            }

            private void RemoveJob(AudioType _type) {
                if (!m_JobTable.ContainsKey(_type)) {
                    Log("Trying to stop a job ["+_type+"] that is not running.");
                    return;
                }
                IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
                StopCoroutine(_runningJob);
                m_JobTable.Remove(_type);
            }

            private void RemoveConflictingJobs(AudioType _type) {
                // cancel the job if one exists with the same type
                if (m_JobTable.ContainsKey(_type)) {
                    RemoveJob(_type);
                }

                // cancel jobs that share the same audio track
                AudioType _conflictAudio = AudioType.None;
                foreach (DictionaryEntry _entry in m_JobTable) {
                    AudioType _audioType = (AudioType)_entry.Key;
                    AudioTrack _audioTrackInUse = GetAudioTrack(_audioType, "Get Audio Track In Use");
                    AudioTrack _audioTrackNeeded = GetAudioTrack(_type, "Get Audio Track Needed");
                    if (_audioTrackInUse.source == _audioTrackNeeded.source) {
                        _conflictAudio = _audioType;
                    }
                }
                if (_conflictAudio != AudioType.None) {
                    RemoveJob(_conflictAudio);
                }
            }

            private IEnumerator RunAudioJob(AudioJob _job) {
                yield return new WaitForSeconds(_job.delay);

                AudioTrack _track = GetAudioTrack(_job.type); // track existence should be verified by now
                _track.source.clip = GetAudioClipFromAudioTrack(_job.type, _track);

                switch (_job.action) {
                    case AudioAction.START:
                        _track.source.Play();
                    break;
                    case AudioAction.STOP:
                        if (!_job.fade) {
                            _track.source.Stop();
                        }
                    break;
                    case AudioAction.RESTART:
                        _track.source.Stop();
                        _track.source.Play();
                    break;
                }

                // fade volume
                if (_job.fade) {
                    float _initial = _job.action == AudioAction.START || _job.action == AudioAction.RESTART ? 0 : 1;
                    float _target = _initial == 0 ? 1 : 0;
                    float _duration = 1.0f;
                    float _timer = 0.0f;

                    while (_timer < _duration) {
                        _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                        _timer += Time.deltaTime;
                        yield return null;
                    }

                    if (_job.action == AudioAction.STOP) {
                        _track.source.Stop();
                    }
                }

                m_JobTable.Remove(_job.type);
                Log("Job count: "+m_JobTable.Count);
            }

            private void GenerateAudioTable() {
                foreach(AudioTrack _track in tracks) {
                    foreach(AudioObject _obj in _track.audio) {
                        // do not duplicate keys
                        if (m_AudioTable.ContainsKey(_obj.type)) {
                            LogWarning("You are trying to register audio ["+_obj.type+"] that has already been registered.");
                        } else {
                            m_AudioTable.Add(_obj.type, _track);
                            Log("Registering audio ["+_obj.type+"]");
                        }
                    }
                }
            }

            private AudioTrack GetAudioTrack(AudioType _type, string _job="") {
                if (!m_AudioTable.ContainsKey(_type)) {
                    LogWarning("You are trying to <color=#fff>"+_job+"</color> for ["+_type+"] but no track was found supporting this audio type.");
                    return null;
                }
                return (AudioTrack)m_AudioTable[_type];
            }

            private AudioClip GetAudioClipFromAudioTrack(AudioType _type, AudioTrack _track) {
                foreach (AudioObject _obj in _track.audio) {
                    if (_obj.type == _type) {
                        return _obj.clip;
                    }
                }
                return null;
            }

            private void Log(string _msg) {
                if (!debug) return;
                Debug.Log("[Audio Controller]: "+_msg);
            }
            
            private void LogWarning(string _msg) {
                if (!debug) return;
                Debug.LogWarning("[Audio Controller]: "+_msg);
            }
#endregion
        }
    }
}
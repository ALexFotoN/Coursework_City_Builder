using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AudioService : MonoBehaviour
{
    #region Singleton
    private static AudioService _instance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _configs = Resources.LoadAll<AudioConfigSO>("Audio");
    }
    #endregion

    [SerializeField]
    private AudioData _audioPrefab;

    private AudioConfigSO[] _configs;
    private List<AudioData> _audioObjects = new();

    public static void PlayAudio(string id)
    {
        _instance.Play(id);
    }

    private void Play(string id)
    {
        var data = _configs.FirstOrDefault(x => x.Id == id);
        if (data)
        {
            var audioObject = Instantiate(_audioPrefab, transform);
            audioObject.Init(data);
            if (data.Loop)
            {
                _audioObjects.Add(audioObject);
            }
            else
            {
                Destroy(audioObject.gameObject, data.Time);
            }
        }
    }

    public static void StopAudio(string id)
    {
        _instance.Stop(id);
    }

    private void Stop(string id)
    {
        var obj = _audioObjects.FirstOrDefault(x => x.Id == id);
        if (obj)
        {
            _audioObjects.Remove(obj);
            Destroy(obj.gameObject);
        }
    }
}

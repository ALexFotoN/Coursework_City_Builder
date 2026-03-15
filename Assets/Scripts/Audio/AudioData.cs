using UnityEngine;

public class AudioData : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private string _id;
    public string Id => _id;

    public void Init(AudioConfigSO data)
    {
        _id = data.Id;
        _audioSource.clip = data.Clip;
        _audioSource.volume = data.Volume;
        _audioSource.pitch = data.Pitch;
        _audioSource.loop = data.Loop;
        _audioSource.Play();
    }
}

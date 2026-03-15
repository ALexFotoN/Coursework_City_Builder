using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/AudioConfig")]
public class AudioConfigSO : ScriptableObject
{
    public string Id;
    public AudioClip Clip;
    public bool Loop;
    public float Time;
    [Range(0, 1)]
    public float Volume = 1;
    [Range(-3, 3)]
    public float Pitch = 1;
}

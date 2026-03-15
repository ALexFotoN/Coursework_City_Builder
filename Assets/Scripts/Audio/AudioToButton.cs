using UnityEngine;
using UnityEngine.UI;

public class AudioToButton : MonoBehaviour
{
    [SerializeField]
    private string _audioId = "click";

    public void Click()
    {
        AudioService.PlayAudio(_audioId);
    }
}

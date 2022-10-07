using UnityEngine;

namespace AudioControl
{
    public class AudioSourceControllerTest : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        private async void Start()
        {
            await AudioSourceController.PlayOneShot(audioClip, gameObject);
        }
    }
}
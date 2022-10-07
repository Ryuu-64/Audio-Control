using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace AudioControl
{
    public class AudioSourceController : SingletonMonoBehaviour<AudioSourceController>
    {
        private static readonly ObjectPool<AudioSource> AudioSourcePool =
            new(
                () => new GameObject(nameof(AudioSource)).AddComponent<AudioSource>(),
                audioSource => audioSource.gameObject.SetActive(true),
                audioSource => audioSource.gameObject.SetActive(false),
                Destroy
            );

        public static async Task PlayOneShot(AudioClip audioClip, GameObject gameObject)
        {
            AudioSource audioSource = AudioSourcePool.Get();
            audioSource.clip = audioClip;
            var transform = audioSource.transform;
            transform.parent = gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.gameObject.name = audioClip.name;
            audioSource.PlayOneShot(audioClip);
            await UniTask.WaitUntil(() => !audioSource.isPlaying);
            transform.parent = Instance.transform;
            AudioSourcePool.Release(audioSource);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class Speaker : MonoBehaviour, IInteractable
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private bool isPlaying = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }
    public void Interact()
    {
        if (PlayerInteract.Instance.GetIsCarryCd())
        {
            Song song = PlayerInteract.Instance.GetPlayerCd();
            Debug.Log(song.song_url);
            StartCoroutine(PlayAudioFromURL(song.song_url));
        }
        else
        {
            // Dung nhac
            isPlaying = !isPlaying;
            if(isPlaying)
            {
                audio.UnPause();
                particle.Play();
            }
            else
            {
                audio.Pause();
                particle.Pause();
            }
        }
    }

    IEnumerator PlayAudioFromURL(string url)
    {
        using (UnityWebRequest response = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return response.SendWebRequest();


            if (response.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(response);
                audio.clip = clip;
                audio.Play();
                Debug.Log("Now Playing: " + clip.name);
                PlayerInteract.Instance.HideCd();
                particle.Play();

                isPlaying = true;
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

}

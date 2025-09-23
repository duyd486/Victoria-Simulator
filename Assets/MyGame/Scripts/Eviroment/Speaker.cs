using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class Speaker : MonoBehaviour, IInteractable
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private AudioClip clip;
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
            StartCoroutine(ApiManager.Instance.PlayAudioFromURL(song.song_url, PlayAudio));
        }
        else
        {
            if (clip == null) return;
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

    public void PlayAudio(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();

        this.clip = clip;

        PlayerInteract.Instance.HideCd();
        particle.Play();
        isPlaying = true;
    }
}

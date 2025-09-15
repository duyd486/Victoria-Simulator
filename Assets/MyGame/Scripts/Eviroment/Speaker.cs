using UnityEngine;

public class Speaker : MonoBehaviour, IInteractable
{
    [SerializeField] private new AudioSource audio;
    [SerializeField] private ParticleSystem particle;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }
    public void Interact()
    {
        string songName = PlayerInteract.Instance.GetSongName();
        if (songName.Length > 0)
        {
            AudioClip clip = Resources.Load<AudioClip>(songName);
            if (clip != null)
            {
                audio.clip = clip;
                audio.Play();
                Debug.Log("Now Playing: " + clip.name);
                PlayerInteract.Instance.HideCd();
                particle.Play();
            }
            else
            {
                Debug.LogError("Không tìm thấy nhạc trong Resources!");
            }
        }
    }
}

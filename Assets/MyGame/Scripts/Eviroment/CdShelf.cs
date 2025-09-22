using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class CdShelf : MonoBehaviour, IInteractable
{
    [SerializeField] private Artist shelfArtist;
    

    public void Interact()
    {
        if(shelfArtist.name.Length > 0)
        {
            StartCoroutine(ApiManager.Instance.GetSongRequest(shelfArtist, () =>
            {
                CdPickerUI.Instance.Show(shelfArtist);
            }));

        }
        else
        {
            Debug.Log("This shelf doesnt have cd");
        }
    }


    public void SetArtist(Artist artist)
    {
        shelfArtist = artist;
    }
}

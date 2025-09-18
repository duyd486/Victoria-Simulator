using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class CdShelf : MonoBehaviour, IInteractable
{
    [SerializeField] private Artist shelfArtist;
    

    public void Interact()
    {
        if(shelfArtist.artistName.Length > 0)
        {
            CdPickerUI.Instance.Show(shelfArtist);
        }
        else
        {
            Debug.Log("This shelf doesnt have cd");
        }
    }

}

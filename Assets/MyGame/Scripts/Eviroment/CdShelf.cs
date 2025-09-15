using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class CdShelf : MonoBehaviour, IInteractable
{

    [SerializeField] private string artistName;
    [SerializeField] private string[] songNameArr;
    

    public void Interact()
    {
        if(artistName.Length > 0)
        {
            Debug.Log(artistName);
            foreach(string name in songNameArr)
            {
                Debug.Log(name);
                PlayerInteract.Instance.SetSongName(name);
                PlayerInteract.Instance.SetCd(Resources.Load<Texture2D>(name));
            }
        }
        else
        {
            Debug.Log("This shelf doesnt have cd");
        }
    }

}

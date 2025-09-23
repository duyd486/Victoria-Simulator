using UnityEngine;

public class SlideDoor : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        isOpen = !isOpen;
        if ( isOpen )
        {
            transform.position -= new Vector3(4.4f, 0, 0);
        }
        else
        {
            transform.position += new Vector3(4.4f, 0, 0);
        }
    }
}

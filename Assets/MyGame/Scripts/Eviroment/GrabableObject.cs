using UnityEngine;

public class GrabableObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (!PlayerInteract.Instance.isCarrySmt)
        {
            PlayerInteract.Instance.GrabObject(this.gameObject);
        }
        PlayerInteract.Instance.isCarrySmt = true;
    }
}
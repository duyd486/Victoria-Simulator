using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    [SerializeField] private float maxDistance = 6f;
    [SerializeField] private bool canInteractObject = false;

    [SerializeField] public bool canInteract = true;

    [SerializeField] private Song song;

    [SerializeField] private Renderer cd;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideCd();
    }

    private void Update()
    {
        if(canInteract)
        {
            HandleInteract();
        }
    }

    private void HandleInteract()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) && hit.transform.GetComponentInParent<IInteractable>() != null)
        {
            canInteractObject = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.GetComponentInParent<IInteractable>().Interact();
            }
        } else
        {
            canInteractObject = false;
        }
    }

    public string GetSongName()
    {
        return song.songName;
    }

    public void SetCd(Song song)
    {
        this.song = song;
        if (song.thumbnail != null)
        {
            ShowCd();
            cd.material.mainTexture = song.thumbnail;
            CdPickerUI.Instance.Hide();
            Debug.Log("Loaded cover from Resources!");
        }
        else
        {
            Debug.LogError("Không tìm thấy ảnh cover trong Resources/");
        }
    }

    public void SetInteractable(bool canInter)
    {
        canInteract = canInter; 
    }

    public void HideCd()
    {
        cd.gameObject.SetActive(false);
    }
    public void ShowCd()
    {
        cd.gameObject.SetActive(true);
    }

    public bool GetCanInteract()
    {
        return canInteractObject;
    }
}

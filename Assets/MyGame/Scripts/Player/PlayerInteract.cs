using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    [SerializeField] private float maxDistance = 6f;
    private bool canInteract = false;

    [SerializeField] private string songName;

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
        HandleInteract();
    }

    private void HandleInteract()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) && hit.transform.GetComponentInParent<IInteractable>() != null)
        {
            canInteract = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.GetComponentInParent<IInteractable>().Interact();
            }
        } else
        {
            canInteract = false;
        }
    }

    public string GetSongName()
    {
        return songName;
    }

    public void SetSongName(string name)
    {
        songName = name;
    }

    public void SetCd(Texture2D tex)
    {
        if (tex != null)
        {
            ShowCd();
            cd.material.mainTexture = tex;
            Debug.Log("Loaded cover from Resources!");
        }
        else
        {
            Debug.LogError("Không tìm thấy ảnh cover trong Resources/");
        }
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
        return canInteract;
    }
}

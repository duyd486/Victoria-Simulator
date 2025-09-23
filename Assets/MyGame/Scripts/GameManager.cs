using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CdShelf[] cdShelves;
    [SerializeField] private List<Artist> artistList;


    [SerializeField] private bool lightRoom = true;
    [SerializeField] private GameObject roomLightOb;
    [SerializeField] private Material ledMaterial;
    [SerializeField] private float itensity = 1.0f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(ApiManager.Instance.GetArtistsRequest());

        for(int i = 0; i < cdShelves.Length; i++)
        {
            cdShelves[i].SetArtist(artistList[i]);
        }
    }

    public void TurnLightSwitch()
    {
        lightRoom = !lightRoom;
        roomLightOb.SetActive(lightRoom);
        ledMaterial.EnableKeyword("_EMISSION");
        ledMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void SetArtistList(List<Artist> artistList)
    {
        this.artistList = artistList;
    }
    public List<Artist> GetArtistList()
    {
        return this.artistList;
    }
}

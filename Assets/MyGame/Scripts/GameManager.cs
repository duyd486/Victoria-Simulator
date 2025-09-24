using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CdShelf[] cdShelves;
    [SerializeField] private List<Artist> artistList;


    [SerializeField] private GameObject roomLightOb;
    [SerializeField] private Material ledMaterial;
    public bool lightRoom = true;
    public bool isCellingFanRotate = false;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(ApiManager.Instance.GetArtistsRequest());

        for(int i = 0; i < cdShelves.Length; i++)
        {
            cdShelves[i].SetArtist(artistList?[i]);
        }
    }


    public void TurnLightSwitch()
    {
        lightRoom = !lightRoom;
        roomLightOb.SetActive(lightRoom);
        ledMaterial.EnableKeyword("_EMISSION");
        ledMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void TurnCellingFan()
    {
        isCellingFanRotate = !isCellingFanRotate;
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

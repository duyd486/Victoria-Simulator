using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CdShelf[] cdShelves;
    [SerializeField] private List<Artist> artistList;

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

    public void SetArtistList(List<Artist> artistList)
    {
        this.artistList = artistList;
    }
    public List<Artist> GetArtistList()
    {
        return this.artistList;
    }
}

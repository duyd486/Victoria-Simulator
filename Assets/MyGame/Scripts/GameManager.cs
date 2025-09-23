using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CdShelf[] cdShelves;


    private void Start()
    {
        StartCoroutine(ApiManager.Instance.GetArtistsRequest());

        for(int i = 0; i < cdShelves.Length; i++)
        {
            cdShelves[i].SetArtist(ApiManager.Instance.artistList[i]);
        }
    }
}

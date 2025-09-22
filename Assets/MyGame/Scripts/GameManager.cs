using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CdShelf[] cdShelves;


    private void Start()
    {
        for(int i = 0; i < cdShelves.Length; i++)
        {
            //Debug.Log("Im a cd shelf number " + i);
            cdShelves[i].SetArtist(ApiManager.Instance.artistList[i]);
        }


        //Debug.Log(ApiManager.Instance.artistList.Count);
        //foreach(Artist artist in ApiManager.Instance.artistList)
        //{
        //    Debug.Log(artist.name);
        //}

    }
}

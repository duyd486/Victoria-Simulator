using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class ArtistData
{
    public List<Artist> artists;
    public int total;
}

[System.Serializable]
public class RootArtist
{
    public bool success;
    public string message;
    public ArtistData data;
}


[System.Serializable]
public class SongData
{
    public List<Artist> artists;
    public int total;
}

[System.Serializable]
public class RootSong
{
    public bool success;
    public string message;
    public SongData data;
}

public class ApiManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetArtistsRequest());
    }

    IEnumerator GetArtistsRequest()
    {
        using (UnityWebRequest response = UnityWebRequest.Get("http://localhost:8000/api/artists"))
        {
            yield return response.SendWebRequest();

            if(response.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(response.downloadHandler.text);

                string json = response.downloadHandler.text;

                RootArtist artistResponse = JsonUtility.FromJson<RootArtist>(json);

                foreach(Artist artist in artistResponse.data.artists)
                {
                    Debug.Log(artist.name);
                }
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }
}

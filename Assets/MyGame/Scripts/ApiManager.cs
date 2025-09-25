using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    public static ApiManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        StartCoroutine(GetArtistsRequest());
    }

    void Start()
    {

    }

    public IEnumerator PostPayment()
    {
        using (UnityWebRequest response = UnityWebRequest.Get("http://localhost:8000/api/payment"))
        {
            yield return response.SendWebRequest();

            if (response.result == UnityWebRequest.Result.Success)
            {
                string json = response.downloadHandler.text;
                Debug.Log(json);
                RootArtist artistResponse = JsonUtility.FromJson<RootArtist>(json);

                GameManager.Instance.SetArtistList(artistResponse.data.artists);
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

    public IEnumerator GetArtistsRequest()
    {
        using (UnityWebRequest response = UnityWebRequest.Get("http://localhost:8000/api/artists"))
        {
            yield return response.SendWebRequest();

            if(response.result == UnityWebRequest.Result.Success)
            {
                string json = response.downloadHandler.text;
                Debug.Log(json);
                RootArtist artistResponse = JsonUtility.FromJson<RootArtist>(json);

                GameManager.Instance.SetArtistList(artistResponse.data.artists);
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }


    public IEnumerator GetSongRequest(Artist artist, Action ShowCd)
    {
        using (UnityWebRequest response = UnityWebRequest.Get("http://localhost:8000/api/artists/" + artist.id))
        {
            yield return response.SendWebRequest();

            if (response.result == UnityWebRequest.Result.Success)
            {
                string json = response.downloadHandler.text;

                RootSong songResponse = JsonUtility.FromJson<RootSong>(json);

                foreach(Artist artist1 in GameManager.Instance.GetArtistList())
                {
                    if(artist1.id == artist.id)
                    {
                        artist1.songs = songResponse.data.user.songs;
                        artist.songs = songResponse.data.user.songs;
                    }
                }

                foreach(Song song in songResponse.data.user.songs)
                {
                    Debug.Log(song.name);
                }

                ShowCd();
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

    public IEnumerator PlayAudioFromURL(string url, Action<AudioClip> PlayAudio)
    {
        using (UnityWebRequest response = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return response.SendWebRequest();


            if (response.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(response);
                PlayAudio(clip);
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

    public IEnumerator GetTextureFromURL(string url, Action<Texture2D> SetTexture)
    {
        using (UnityWebRequest response = UnityWebRequestTexture.GetTexture(url))
        {
            yield return response.SendWebRequest();
            Debug.Log(response.result);

            if (response.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(response);
                SetTexture(texture);
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

}

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
    public Artist user;
    public int total;
}

[System.Serializable]
public class RootSong
{
    public bool success;
    public string message;
    public SongData data;
}

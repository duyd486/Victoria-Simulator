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

    public IEnumerator PostPayment(Song song, Action<RootPayment> ShowQr)
    {
        using (UnityWebRequest response = new UnityWebRequest("http://localhost:8000/api/payment?song_id=" + song.id, "POST"))
        {
            response.downloadHandler = new DownloadHandlerBuffer();
            yield return response.SendWebRequest();

            if (response.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(response.downloadHandler?.text);
                string json = response.downloadHandler.text;
                RootPayment rootPayment = JsonUtility.FromJson<RootPayment>(json);
                ShowQr(rootPayment);
            }
            else
            {
                Debug.LogError(response.error);
            }
        }
    }

    public IEnumerator HandlePaymentHeartBeat(string code, Action PaymentPaid)
    {
        using (UnityWebRequest response = UnityWebRequest.Get("http://localhost:8000/api/payment/" + code))
        {
            yield return response.SendWebRequest();

            if (response.result == UnityWebRequest.Result.Success)
            {
                string json = response.downloadHandler.text;
                PaymentHeartBeatRoot heartBeatRoot = JsonUtility.FromJson<PaymentHeartBeatRoot>(json);
                if(heartBeatRoot.data == "PAID")
                {
                    Debug.Log("Đã trả tiền");
                    PaymentPaid();
                }
            }
            else
            {
                Debug.LogError("Call Api thất bại");
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
                RootArtist artistResponse = JsonUtility.FromJson<RootArtist>(json);
                GameManager.Instance.SetArtistList(artistResponse.data.artists);
                Debug.Log("Api nghệ sĩ được lấy thành công");
            }
            else
            {
                Debug.LogError("Call Api thất bại");
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
public class PaymentHeartBeatRoot
{
    public bool success;
    public string message;
    public string data;
}

[System.Serializable]
public class PaymentData
{
    public string song_id;
    public int order_code;
    public int price;
    public int status;
    public string updated_at;
    public string created_at;
    public int id;
    public string checkout_url;
    public string code_url;
}

[System.Serializable]
public class RootPayment
{
    public bool success;
    public string message;
    public PaymentData data;
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

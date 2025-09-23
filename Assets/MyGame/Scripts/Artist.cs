using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Artist
{
    public int id;
    public string name;
    public int songs_count;
    public List<Song> songs;    
}

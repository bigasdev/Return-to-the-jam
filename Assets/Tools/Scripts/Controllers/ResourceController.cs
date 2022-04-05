using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ResourceController
{
    public const string audiosDirectory = "Audios";
    public const string iconsDirectory = "Icons";
    private static AudioClip[] soundClips;
    private static Sprite[] icons;
    public static void StartSets(){
        soundClips = Resources.LoadAll<AudioClip>(audiosDirectory);
        icons = Resources.LoadAll<Sprite>(iconsDirectory);
    }
    public static AudioClip GetAudio(string name){
        foreach(var a in soundClips){
            if(a.name == name){
                return a;
            }
        }
        Debug.Log("ERROR 001 : NO AUDIO FOUND WITH THIS NAME!");
        return null;
    }
    public static Sprite GetSprite(string name){
        foreach(var a in icons){
            if(a.name == name){
                return a;
            }
        }
        Debug.Log("ERROR 001 : NO ICON FOUND WITH THIS NAME!");
        return null;
    }
}

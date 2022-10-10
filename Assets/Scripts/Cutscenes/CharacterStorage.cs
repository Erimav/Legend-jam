using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterStorage
{
    //no touch this plz XD
    private static CharacterBlueprintHolder CharBPHolder;
    private static bool HasSetup = false;

    public static System.IO.FileInfo[] GetFilesFromDirectory(string Directory, string Extension)
    {
        System.IO.DirectoryInfo tmpD = new(Directory);

        if (!tmpD.Exists)
        {
            Debug.LogWarning("Creating Directory at: " + Directory);
            tmpD = System.IO.Directory.CreateDirectory(Directory);
        }
        return tmpD.GetFiles("*." + Extension); //Basic Info Blueprint  
    }

    public static void Setup()
    {
        HasSetup = true;
        System.IO.FileInfo[] AllFiles = GetFilesFromDirectory("Assets/Json", "CBs");
        for(int i = 0; i < AllFiles.Length; i++)
        {
            CharacterBlueprintHolder tmpHolder = JsonUtility.FromJson<CharacterBlueprintHolder>(AllFiles[i].OpenText().ReadToEnd());
            foreach (CharacterBlueprint tmpCB in tmpHolder.CharacterBlueprints)
            {
                tmpCB.UpdateSprite(LoadImageFromPath(tmpCB.Path_Sprite));
            }
            CharBPHolder = tmpHolder;
        }
    }

    private static Texture2D LoadImageFromPath(string path = "")
    {
        try
        {
            try
            {
                byte[] tmpBytes = System.IO.File.ReadAllBytes(path);
                Texture2D tex = new(512, 512, TextureFormat.DXT5, true);
                tex.LoadRawTextureData(tmpBytes);
                tex.Apply(true, false);
                return tex;
            }
            catch
            {
                try
                {
                    byte[] tmpBytes = System.IO.File.ReadAllBytes(path);
                    Texture2D tex = new(512, 512, TextureFormat.DXT1, true);
                    tex.LoadRawTextureData(tmpBytes);
                    tex.Apply(true, false);
                    return tex;
                }
                catch
                {
                    Debug.LogWarning("Texture File at Path '" + path + "' is not saved in correct format! Must be DDS with compression DXT5, DXT1 and with mipmaps.  Defaulting to Error.");
                    return new(4, 4, TextureFormat.DXT1, true);
                }
            }
        }
        catch
        {
            Debug.LogWarning("Texture File at Path '" + path + "' not found! Defaulting to Error.");
            return new(4, 4, TextureFormat.DXT1, true);
        }
    }

    public static CharacterBlueprint GetCharacter(int Pos)
    {
        if (!HasSetup) { Setup(); }
        return CharBPHolder.CharacterBlueprints[Pos];
    }
}

[System.Serializable]
public class CharacterBlueprintHolder
{
    public CharacterBlueprint[] CharacterBlueprints;
}

[System.Serializable]
public class CharacterBlueprint
{
    public string Name;
    public string Path_Sprite;
    public Sprite selfSprite;
    public float Speech_Pitch;

    public void UpdateSprite(Texture2D texture)
    {
        selfSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}

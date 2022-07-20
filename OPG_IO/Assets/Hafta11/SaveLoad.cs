using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad 
{
    public static void SaveData(PlayerData PlayerSaveData)
    {
        Debug.Log(Application.persistentDataPath);

        BinaryFormatter Bformatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameSaveData.save";

        FileStream fstream = new FileStream(path, FileMode.Create);

        PlayerData PxData = new PlayerData(PlayerSaveData);

        Bformatter.Serialize(fstream, PxData);
        fstream.Close();

        Debug.Log("Player Data Saved Successfully!");
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/GameSaveData.save";

        if(File.Exists(path))
        {
            BinaryFormatter Bformatter = new BinaryFormatter();
            FileStream fstream = new FileStream(path, FileMode.Open);

            PlayerData Pxdata = Bformatter.Deserialize(fstream) as PlayerData;

            fstream.Close();
            Debug.Log("Player Data Loaded Succesfully!");

            return Pxdata;
        }
        else
        {
            Debug.LogError("Error: Save file not found in path : " + path);
            return null;
        }

        return null;
    }
}

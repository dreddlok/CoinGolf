using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                                           // Allows us to add second class into the script. System contains "serialisable" attribute.
using System.Runtime.Serialization.Formatters.Binary;   // This allows us to use the binary formatter.
using System.IO;                                        // Allows us to manipulate files.

public static class SaveGameManager {

    public static void SavePlayer(PlayerSave save)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Create);

        PlayerData data = new PlayerData(save);

        binaryFormatter.Serialize(stream, data);
        stream.Close();

    }

    public static bool[] LoadPlayer(out float volume)
    {
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();
            volume = data.volume;
            return data.levelsUnlocked;
        } else
        {
            Debug.Log("File does not exist");
            volume = 1;
            return new bool[9];
        }
        
    }


    [Serializable]
    public class PlayerData
    {
        public bool[] levelsUnlocked;
        public float volume;

        public PlayerData(PlayerSave playerSave )
        {
            levelsUnlocked = playerSave.level;
            Debug.Log(playerSave.level.ToString());
            volume = playerSave.volume;
            Debug.Log(playerSave.volume.ToString());
        }
    }

}

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

    public static bool[] LoadPlayer(out float volume, out float sfxVolume, out int [] levelCoins, out PlayerSave.LevelGrade [] levelGrade)
    {
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();
            volume = data.volume;
            sfxVolume = data.sfxVolume;
            levelCoins = data.levelCoins;
            levelGrade = data.levelGrade;
            return data.levelsUnlocked;
        } else
        {
            Debug.Log("File does not exist");
            volume = 1;
            sfxVolume = 1;
            levelCoins = new int[19];
            levelGrade = new PlayerSave.LevelGrade[19];
            return new bool[19];
        }
        
    }


    [Serializable]
    public class PlayerData
    {
        public bool[] levelsUnlocked;
        public float volume;
        public bool[] level;
        public PlayerSave.LevelGrade[] levelGrade;
        public int[] levelCoins; // the number of coins collected in the level
        public float sfxVolume;

        public PlayerData(PlayerSave playerSave )
        {
            levelsUnlocked = playerSave.level;
            volume = playerSave.volume;
            sfxVolume = playerSave.sfxVolume;
            levelCoins = playerSave.levelCoins;
            levelGrade = playerSave.levelGrade;
        }
    }

}

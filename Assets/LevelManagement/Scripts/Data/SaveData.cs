using System.Collections;
using System.Collections.Generic;
using System;

namespace LevelManagement.Data
{

    [Serializable]
    public class SaveData
    {
        public string playerName;
        //private readonly string defaultPlayerName = "Player";

        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;
        public int[] numberOfStarsInEachLevel; //TODO bardziej sensowna nazwa

        public string hashValue;

        public SaveData()
        {
            //playerName = defaultPlayerName;
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            hashValue = String.Empty;
            numberOfStarsInEachLevel = new int[LevelLoader.numberOfScenes];
        }
    } 
}

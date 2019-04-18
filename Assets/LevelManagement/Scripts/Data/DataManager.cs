using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private SaveData saveData;
        private JsonSaver jsonSaver;

        public static  DataManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                saveData = new SaveData();
                jsonSaver = new JsonSaver();
            }
        }

        private void Start()
        {
            LoadDefault();
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public float MasterVolume
        {
            get { return saveData.masterVolume; }
            set { saveData.masterVolume = value; }
        }

        public float SFXVolume
        {
            get { return saveData.sfxVolume; }
            set { saveData.sfxVolume = value; }
        }

        public float MusicVolume
        {
            get { return saveData.musicVolume; }
            set { saveData.musicVolume = value; }
        }

        public int[] numberOfStarsInEachLevel
        {
            get { return saveData.numberOfStarsInEachLevel; }
            set { saveData.numberOfStarsInEachLevel = value; }
        }

        //public string PlayerName
        //{
        //    get { return saveData.playerName; }
        //    set { saveData.playerName = value; }
        //}

        public void Save()
        {
            jsonSaver.Save(saveData);
        }

        public void Load()
        {
            jsonSaver.Load(saveData);
        }

        public void Delete()
        {
            jsonSaver.Delete();
        }

        public void LoadDefault()
        {
            for(int i = 0; i<saveData.numberOfStarsInEachLevel.Length; i++)
            {
                numberOfStarsInEachLevel[i] = -1;
                if(i == LevelLoader.level1Index)
                {
                    numberOfStarsInEachLevel[i] = 0;
                }
            }
        }

        public void CompareDataAfterLevel()
        {
            int currentLevelStars = LevelScoreManager.Instance.stars;
            int nextLevelStars = 0;
            int oldCurrentLevelStars = numberOfStarsInEachLevel[SceneManager.GetActiveScene().buildIndex];
            if (currentLevelStars > oldCurrentLevelStars)
            {
                numberOfStarsInEachLevel[SceneManager.GetActiveScene().buildIndex] = currentLevelStars;
            }

            if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1) // last level case
            {
                int oldNextLevelStars = numberOfStarsInEachLevel[SceneManager.GetActiveScene().buildIndex + 1];
                if (nextLevelStars > oldNextLevelStars)
                {
                    numberOfStarsInEachLevel[SceneManager.GetActiveScene().buildIndex + 1] = nextLevelStars;
                }
            }
            Save();
        }
    }
}



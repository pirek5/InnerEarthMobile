using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                OnResumePressed();
            }
        }

        public void OnResumePressed()
        {
            Time.timeScale = 1;
            base.OnBackPressed();
        }

        public void OnRestartPressed()
        {
            if (GameManager.Instance != null)
            {
                Time.timeScale = 1;
                LevelLoader.instance.ReloadLevel();
                base.OnBackPressed();
            }
        }

        public void OnMainMenuPressed()
        {
            Time.timeScale = 1;
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}



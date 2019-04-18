using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LevelManagement
{

    public class GameMenu : Menu<GameMenu>
    {
        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                OnPausePressed();
            }
        }

        public void OnPausePressed()
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            PauseMenu.Open();
        }
    }
}




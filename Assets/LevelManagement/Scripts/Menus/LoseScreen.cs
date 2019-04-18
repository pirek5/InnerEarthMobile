using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class LoseScreen : Menu<LoseScreen>
    {
        public void OnRestartPressed()
        {
            Cursor.visible = false;
            base.OnBackPressed();
            LevelLoader.instance.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }
    }
}

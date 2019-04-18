using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;
using System.Text.RegularExpressions;
using System;

namespace LevelManagement
{
    public class ChooseLevelMenu : Menu<ChooseLevelMenu>
    {

        public IconPanel[] soretedIconPanels = new IconPanel[LevelLoader.numberOfScenes];
        private IconPanel[] unsosrtedIconPanels;

        protected override void Awake()
        {
            base.Awake();
            unsosrtedIconPanels = FindObjectsOfType<IconPanel>();
            SortingIconPanels();
        }

        public void Init()
        {
            DataManager.instance.Load();
            for (int i = LevelLoader.level1Index; i< soretedIconPanels.Length; i++)
            {
                bool isUnlocked = DataManager.instance.numberOfStarsInEachLevel[i] >= 0; // ujemna liczba gwiazd oznacza zablokowany poziom
                soretedIconPanels[i].transform.parent.gameObject.SetActive(isUnlocked);  //przycisk jest aktywny w zależności od tego czy poziom jest odblokowany
                if (isUnlocked)
                {
                    soretedIconPanels[i].IconEnable(4); 
                    soretedIconPanels[i].IconDisable(4 - DataManager.instance.numberOfStarsInEachLevel[i]); //wyświetla odpowiednią liczbę gwiazdek dla danego poziomu
                }
            }
        }

        public void OnLevelPressed(int level) // odwołanie ustawione w edytorze
        {
            LevelLoader.instance.LoadLevel(level);
            Cursor.visible = false;
            GameMenu.Open();
        }

        public void OnTutorialPressed()
        {
            Tutorial.Open();
            Cursor.visible = false;
            FindObjectOfType<Tutorial>().Init(); //TODO
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private void SortingIconPanels() // przyporządkowanie paneli do poszczegolnych poziomów
        {
            foreach(IconPanel iconPanel in unsosrtedIconPanels)
            {
                string index = Regex.Match(iconPanel.gameObject.name, @"\d+").Value; // znajduje liczbę w nazwie uzytej w edytorze
                int i;
                Int32.TryParse(index, out i); // zamiana string na int
                int offset = LevelLoader.level1Index - 1; // w zalzenosci od tego ktory numer jest przypisaney do pierwszego poziomu, sortuje panele z ikonami biąrąd pod uwage tę poprawkę
                soretedIconPanels[i+offset] = iconPanel;
            }
        }
    }
}

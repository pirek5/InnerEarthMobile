using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System;

namespace LevelManagement
{
    public class Tutorial : Menu<Tutorial>
    {
        // references set in editor
        [SerializeField] private GameObject[] pages;
        [SerializeField] private GameObject confirmationWindow;
        [SerializeField] private Image ladderRimImage; 
        [SerializeField] private Image crateRimImage;

        //config
        [SerializeField] private float blinkingPeriod;
        [SerializeField] private Vector2 playerStartPosition;
        [SerializeField] private Vector2 crateStartPosition;


        //cached
        private Crate crateToFollow;
        int currentPage;

        public void Init()
        {
            PositioningObjects(); // Player and crate
            StartCoroutine(BlinkingObjectivesRim());
            currentPage = 0;  // reset first to first page
            pages[0].SetActive(true);
            foreach(TutorialObjective tutorialObj in FindObjectsOfType<TutorialObjective>()) // init all tutorial objectives
            {
                tutorialObj.Init();
            }
        }

        private void PositioningObjects()
        {
            crateToFollow = FindObjectOfType<Crate>();
            crateToFollow.isMoveable = true; // crate script prevents from moving except while pushing/pulling by player
            crateToFollow.transform.position = crateStartPosition;
            crateToFollow.constantPositionX = crateToFollow.transform.position.x;
            crateToFollow.isMoveable = false;
            FindObjectOfType<Player>().transform.position = playerStartPosition;
        }

        private void Update()
        {
            crateRimImage.gameObject.transform.position = Camera.main.WorldToScreenPoint(crateToFollow.transform.position);
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                BackToMainMenuConfirmationWindow();
            }
        }

        public void NextPage()
        {
            currentPage++;
            for(int i = 0; i< pages.Length;i++)
            {
                pages[i].SetActive(false);
                if(i == currentPage)
                {
                    pages[i].SetActive(true);
                    if(currentPage == 4)
                    {
                        Cursor.visible = true;
                    }
                }
            }
        }

        IEnumerator BlinkingObjectivesRim()
        {
            while (true)
            {
                ladderRimImage.enabled = !ladderRimImage.enabled;
                crateRimImage.enabled = !crateRimImage.enabled;
                yield return new WaitForSeconds(blinkingPeriod);
            }
        }

        public override void OnBackPressed()
        {
            foreach (GameObject page in pages)
            {
                page.SetActive(false);
            }
            base.OnBackPressed();
            base.OnBackPressed();
            Cursor.visible = true;
        }

        private void BackToMainMenuConfirmationWindow()
        {
            Time.timeScale = Convert.ToInt32(confirmationWindow.activeSelf);
            confirmationWindow.SetActive(!confirmationWindow.activeSelf);
            Cursor.visible = !Cursor.visible;
        }

        public void OnYesPressed()
        {
            Cursor.visible = true;
            Time.timeScale = 1;
            confirmationWindow.SetActive(false);
            OnBackPressed();
        }

        public void OnNoPressed()
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            confirmationWindow.SetActive(false);
        }
    }
}



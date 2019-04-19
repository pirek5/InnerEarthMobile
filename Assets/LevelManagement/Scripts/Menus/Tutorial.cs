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
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject controlls;

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
            currentPage = 0;  // reset page to first
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
            controlls.SetActive(true);
            player.transform.position = playerStartPosition;
            player.SetActive(true);
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
        }

        private void BackToMainMenuConfirmationWindow()
        {
            Time.timeScale = Convert.ToInt32(confirmationWindow.activeSelf);
            confirmationWindow.SetActive(!confirmationWindow.activeSelf);
        }

        public void OnYesPressed()
        {
            Time.timeScale = 1;
            confirmationWindow.SetActive(false);
            OnBackPressed();
        }

        public void OnNoPressed()
        {
            Time.timeScale = 1;
            confirmationWindow.SetActive(false);
        }

        public void OnDisable()
        {
            player.SetActive(false);
            controlls.SetActive(false);
        }
    }
}



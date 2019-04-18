using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LoadingIcon : MonoBehaviour { //TODO refactor

    public static LoadingIcon instance;

    [SerializeField] private Text loadingText;
    [SerializeField] private Text pressAnyKeyText;

    [SerializeField] private float period = 1;

    private string[] possibleTexts = { "LOADING","LOADING.", "LOADING..", "LOADING..." };

    private void Awake() 
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            gameObject.SetActive(false);
        }
        
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
        pressAnyKeyText.gameObject.SetActive(false);
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        int i = 0;
        while (!LevelLoader.levelIsReady)  // level loading
        {
            loadingText.text = possibleTexts[i];
            i++;
            if (i >= possibleTexts.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(period);
        }

        //if (LevelLoader.instance.CheckIfMainMenu())
        //{
        //    gameObject.SetActive(false);
        //    StopAllCoroutines();
        //    LevelLoader.levelIsReady = false;
        //}

        loadingText.gameObject.SetActive(false);
        pressAnyKeyText.gameObject.SetActive(true);
        bool isStartPressed = false;
        while (!isStartPressed)  // waiting to press any key
        {
            isStartPressed = Input.anyKeyDown || LevelLoader.instance.CheckIfMainMenu();
            yield return null;
        }
        LevelLoader.levelIsReady = false;
        loadingText.gameObject.SetActive(true);
        isStartPressed = false;
        gameObject.SetActive(false);
        GameManager.Instance.StartLava();
    }






}

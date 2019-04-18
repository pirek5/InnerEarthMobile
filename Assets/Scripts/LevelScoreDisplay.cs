using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScoreDisplay : MonoBehaviour {


    //References in editor
    [SerializeField] private IconPanel starsPanel;
    [SerializeField] private Text heartsText;
    [SerializeField] private Text coinsText;

    private static LevelScoreDisplay instance;
    public static LevelScoreDisplay Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateScore(int maxCoins, int coins, int hearts, int numberOfStars)
    {
        coinsText.text = coins + "/" + maxCoins;
        heartsText.text = hearts + "/3";
        starsPanel.IconEnable(4);
        starsPanel.IconDisable(4 - numberOfStars);
    }

}

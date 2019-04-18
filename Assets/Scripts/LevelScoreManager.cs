using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScoreManager : MonoBehaviour {

    private int maxCoins;
    private int coins = 0;
    private int hearts = 3;
    public int stars = 0; //TODO make private

    private static LevelScoreManager instance;
    public static LevelScoreManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            maxCoins = FindObjectsOfType<Coin>().Length;
            instance = this;
        }
    }

    public void UpdateHeartsAmount(int updatedHearts)
    {
        this.hearts = updatedHearts;
    }

    public void UpdateLevelScoreDisplay()
    {
        stars = CalculateScore();
        LevelScoreDisplay.Instance.UpdateScore(maxCoins, coins, hearts, stars);
    }

    private int CalculateScore() // TODO usunąć magiczne liczby
    {
        int maxScore = maxCoins + 3; // 3 - max lives
        coins = maxCoins - FindObjectsOfType<Coin>().Length;
        int score = coins + hearts;
        float ratio = (float)score / maxScore;


        if (ratio >= 1) // max score - 4 stars
        {
            return 4;
        }
        else if (ratio < 1 && ratio >= 0.7f) // 3 stars score
        {
            return 3;
        }
        else if (ratio < 0.7f && ratio >= 0.3f) // 2 stars score
        {
            return 2;
        }
        else return 1; // 1 star score
    }
}

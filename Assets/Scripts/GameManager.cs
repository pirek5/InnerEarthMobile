using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
using LevelManagement.Data;

public class GameManager : MonoBehaviour // TODO zmienic z magicznych liczba serca i liczby gwiazdek
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void LevelWin()
    {
        if (Player.IsActive)
        {
            Cursor.visible = true;
            WinScreen.Open();
            LevelScoreManager.Instance.UpdateLevelScoreDisplay();
            StopPlayerMovement();
            StopEnemiesMovement();
            StopLava();
            DataManager.instance.CompareDataAfterLevel();
        }
    }

    public void LevelLose()
    {
        StopEnemiesMovement();
        Cursor.visible = true;
        LoseScreen.Open();
    }

    private void StopPlayerMovement()
    {
        Player player = FindObjectOfType<Player>();
        player.GetComponent<Animator>().SetTrigger("Win");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.IsActive = false;
    }

    private void StopEnemiesMovement()
    {
        EnemyMovement[] enenemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enenemies)
        {
            enemy.GetComponent<Animator>().SetTrigger("End");
            enemy.Stop();
        }
    }

    private void StopLava()
    {
        var lava = FindObjectOfType<Lava>();
        if (lava)
        {
            lava.isFloating = false;
        }
    }

    public void StartLava()
    {
        var lava = FindObjectOfType<Lava>();
        if (lava)
        {
            lava.isFloating = true;
        }
    }
}
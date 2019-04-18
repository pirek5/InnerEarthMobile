using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //reference set in editor //TODO bez konieczności użycia edytora
    [SerializeField] private IconPanel healthPanel;

    //config
    [SerializeField] private float verticalAfterHitBodyThrow = 1f;
    [SerializeField] private float throwPeriod = 1f;
    [SerializeField] private float endGameDelay = 1f;
    [SerializeField] private float drowningTime = 1f;
    [SerializeField] private float immunityPeriod = 1f;
    [SerializeField] private float blinkingFrequency = 1f;

    // state
    public int lives = 3;
    private bool immunity = false;

    //dependencies
    Player player;
    PlayerAnimations playerAnimations;
    Rigidbody2D myRigidbody;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerAnimations = GetComponent<PlayerAnimations>();
        myRigidbody = GetComponent<Rigidbody2D>();
    
    }

    private void LateUpdate()
    {
        if (Player.IsActive)
        {
            if (player.isTouchingWater) Drowning();
            if (player.isHeadTouchingLava) InstantDeath();
            if (player.isTouchingLava && !immunity) LavaTouched();
            if (player.isTouchingEnemy && !immunity) EnemyTouched();
        }
    }

    private void EnemyTouched()
    {
        LoseHealth(1);
        StartCoroutine(AfterHitBodyThrow());
        CheckIfDead();
    }

    private void LoseHealth(int healthLose)
    {
        lives -= healthLose;
        if (lives < 0)
        {
            lives = 0;
        }
        LevelScoreManager.Instance.UpdateHeartsAmount(lives);
        healthPanel.IconDisable(healthLose);
    }

    IEnumerator AfterHitBodyThrow()
    {
        Player.IsActive = false;
        myRigidbody.velocity = new Vector2(0, verticalAfterHitBodyThrow);
        yield return new WaitForSeconds(throwPeriod);

        if(lives > 0) // activate player after hit only if this wasnt last life
        {
            Player.IsActive = true;
        }
    }

    IEnumerator TemporaryImmunity()
    {
        immunity = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float t1 = Time.time;
        float t2 = t1;
        while (t2 - t1 < immunityPeriod + blinkingFrequency)
        {
            t2 = Time.time;
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkingFrequency);
        }
        spriteRenderer.enabled = true;
        immunity = false;
    }

    private void Drowning()
    {
        LoseHealth(lives);
        player.zeroVelocity = true;
        Player.IsActive = false;
        myRigidbody.gravityScale = 0.3f;
        playerAnimations.Drowning();
        StartCoroutine(DelayedDeath(drowningTime));
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.zeroVelocity = true;
        DeathSequence();
    }

    private void DeathSequence()
    {
        transform.localScale = new Vector2(1f, 1f);
        playerAnimations.Grave();
        GameManager.Instance.LevelLose();
    }

    private void LavaTouched()
    {
        LoseHealth(1);
        CheckIfDead();
    }

    private void InstantDeath()
    {
        LoseHealth(lives);
        Player.IsActive = false;
        player.zeroVelocity = true;
        DeathSequence();
    }

    private void CheckIfDead()
    {
        if (lives <= 0)
        {
            Player.IsActive = false;
            playerAnimations.Dead();
            StartCoroutine(DelayedDeath(endGameDelay));
        }
        else
        {
            StartCoroutine(TemporaryImmunity());
        }
    }
}

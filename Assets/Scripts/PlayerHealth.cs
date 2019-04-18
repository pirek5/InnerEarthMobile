using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player
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
    private bool zeroVelocity = false;
    private bool immunity = false;

    protected override void Update()
    {
        if (isActive)
        {
            base.Update();
            if (isTouchingWater) Drowning();
            if (isHeadTouchingLava) InstantDeath();
            if (isTouchingLava && !immunity) LavaTouched();
            if (isTouchingEnemy && !immunity) EnemyTouched();
        }

        if (zeroVelocity) // prevents weird behavior after death or during drowning
        {
            myRigidbody.velocity = Vector2.zero;
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
        isActive = false;
        myRigidbody.velocity = new Vector2(0, verticalAfterHitBodyThrow);
        yield return new WaitForSeconds(throwPeriod);

        if(lives > 0) // activate player after hit only if this wasnt last life
        {
            IsActive = true;
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
        zeroVelocity = true;
        isActive = false;
        myRigidbody.gravityScale = 0.3f;
        animator.SetTrigger("Drowning");
        StartCoroutine(DelayedDeath(drowningTime));
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        zeroVelocity = true;
        DeathSequence();
    }

    private void DeathSequence()
    {
        transform.localScale = new Vector2(1f, 1f);
        animator.SetTrigger("Grave");
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
        isActive = false;
        zeroVelocity = true;
        DeathSequence();
    }

    private void CheckIfDead()
    {
        if (lives <= 0)
        {
            isActive = false;
            animator.SetTrigger("Dead");
            StartCoroutine(DelayedDeath(endGameDelay));
        }
        else
        {
            StartCoroutine(TemporaryImmunity());
        }
    }
}

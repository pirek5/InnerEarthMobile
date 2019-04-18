using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LavaDetector : MonoBehaviour {

    //set in editor
    [SerializeField] private Animator steamAnimator, fireAnimator;
    [SerializeField] private SpriteRenderer steamSpriteRenderer, fireSpriteRenderer;

    //config
    [SerializeField] Color steamForegroundColor, steamBackgroundColor, fireForegroundColor, fireBackgroundColor;
    [SerializeField] private string[] tags;
    [SerializeField] string foregroundSortingName, backgroundSortingName;

    // cached
    private Collider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        steamAnimator.speed = Random.Range(0.8f, 1.2f);
        fireAnimator.speed = Random.Range(0.8f, 1.2f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tags.Any(collision.gameObject.tag.Contains))
        {
            CheckWhichEffect(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (tags.Any(collider.gameObject.tag.Contains))
        {
            if (collider.CompareTag("SteamFG"))
            {
                steamAnimator.SetBool("Steam", false);
            }
            else if (collider.CompareTag("SteamBG"))
            {
                steamAnimator.SetBool("Steam", false);
            }
            else if (collider.CompareTag("FireFG"))
            {
                fireAnimator.SetBool("Fire", false);
            }
            else if (collider.CompareTag("FireBG"))
            {
                fireAnimator.SetBool("Fire", false);
            }
        }
    }

    private void CheckWhichEffect(GameObject collider)
    {
        if (collider.CompareTag("SteamFG"))
        {
            steamSpriteRenderer.color = steamForegroundColor;
            steamSpriteRenderer.sortingLayerName = foregroundSortingName;
            steamAnimator.SetBool("Steam", true);
        }
        else if (collider.CompareTag("SteamBG"))
        {
            steamSpriteRenderer.color = steamBackgroundColor;
            steamSpriteRenderer.sortingLayerName = backgroundSortingName;
            steamAnimator.SetBool("Steam", true);
        }
        else if (collider.CompareTag("FireFG"))
        {
            fireSpriteRenderer.color = fireForegroundColor;
            fireSpriteRenderer.sortingLayerName = foregroundSortingName;
            fireAnimator.SetBool("Fire", true);
        }
        else if (collider.CompareTag("FireBG"))
        {
            fireSpriteRenderer.color = fireBackgroundColor;
            fireSpriteRenderer.sortingLayerName = backgroundSortingName;
            fireAnimator.SetBool("Fire", true);
        }
    }
}

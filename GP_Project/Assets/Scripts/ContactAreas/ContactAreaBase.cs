using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAreaBase : MonoBehaviour
{
    [Header("Contact Area Settings")]
    [SerializeField]
    protected Color playerColorChange=Color.black;
    [SerializeField]
    private float timeBetweenTick;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite pressedSprite;

    protected Color originalColor;
    protected SpriteRenderer playerSprite;
    private SpriteRenderer myRenderer;

    private bool canDoEffect = false;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        playerSprite= GameManager.Instance?.Player.GetComponent<SpriteRenderer>();
        if (playerSprite)
        {
            originalColor = playerSprite.color;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerSprite)
            {
                playerSprite.color = playerColorChange;

            }
            myRenderer.sprite = pressedSprite;
            canDoEffect = true;
            StartCoroutine(Effect()); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myRenderer.sprite = defaultSprite;
            playerSprite.color = originalColor;
            canDoEffect = false;
            StopCoroutine(Effect());
            Debug.Log("sTOPPED");
        }
    }

    private IEnumerator Effect()
    {
        while (canDoEffect)
        {
            DoEffect();
            yield return new WaitForSeconds(timeBetweenTick);
        }
        
    }
    protected virtual void DoEffect()
    {
        Debug.Log("Effect on: "+gameObject);
    }
}

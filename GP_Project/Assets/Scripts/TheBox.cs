using System.Collections.Generic;
using UnityEngine;

public class TheBox : MonoBehaviour
{
    [SerializeField]
    private Sprite normalSprite;
    [SerializeField]
    private Sprite activatedSprite;

    [SerializeField]
    private float effectTime=10;

    private SpriteRenderer mySprite=null;

    private List<BoxReferenced> myReferences = new List<BoxReferenced>();

    bool isActive = false;

    public void AddReference(BoxReferenced newVal)
    {
        myReferences.Add(newVal);
    }


    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) return;
        if (collision.CompareTag("Player"))
        {
            mySprite.sprite = activatedSprite;
            isActive = true;
            foreach (BoxReferenced myref in myReferences)
            {
                myref.ActivateBox();
            }
            Invoke(nameof(ResetStuff), effectTime);
        }

    }

    private void ResetStuff()
    {
        isActive = false;
        mySprite.sprite = normalSprite;
        foreach(BoxReferenced myref in myReferences)
        {
            myref.DeActivateBox();
        }
    }
}

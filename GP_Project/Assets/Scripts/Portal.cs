using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour,BoxReferenced
{

    [Header("Portal Settings")]
    [SerializeField]
    private Portal[] myDestinations;
    [SerializeField]
    private Transform myTeleportPoint;
    [SerializeField]
    private Color colorDeactivated;


    public Transform TeleportPoint => myTeleportPoint;
    public bool isWorking => isActive;

    private bool isActive = true;

    private Color defaultColor;
    private SpriteRenderer mySpriteRenderer;
    private void Awake()
    {
        SettupDestinations();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = mySpriteRenderer.color;
    }

    private void Start()
    {
        AddReference();
    }

    private void SettupDestinations()
    {
        myDestinations = myDestinations.Where(val => val != this).ToArray();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Teleport(collision.transform);
        }
    }

    private void Teleport(Transform victim)  //Remake when home
    {
        bool willtp = false;
        int index=0;
        while (!willtp)
        {
            index = Random.Range(0, myDestinations.Length);
            willtp = myDestinations[index].isWorking;
        }

        victim.position = myDestinations[index].TeleportPoint.position;
    }


    private void Deactivate()
    {
        mySpriteRenderer.color = colorDeactivated;
    }

    private void Activate()
    {
        mySpriteRenderer.color = defaultColor;
    }


    //interface stuff

    public void DeActivateBox()
    {
        Deactivate();
    }

    public void ActivateBox()
    {
        Activate();
    }

    public void AddReference()
    {
        FindObjectOfType<TheBox>().AddReference(this);
    }
}

using System.Linq;
using UnityEngine;
using System.Collections.Generic;

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
    public bool PortalIsActive => portalActive;

    [SerializeField]
    private bool portalActive = true;

    private bool used=false;

    public bool PortalUsed{ get => used; set => used=value; }

    private Color defaultColor;
    private SpriteRenderer mySpriteRenderer;
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = mySpriteRenderer.color;

        if (portalActive == false)
        {
            DisablePortal();
        }

        SettupDestinations();
    }

    private void Start()
    {
        AddReference();
    }

    private void SettupDestinations()
    {
        myDestinations = myDestinations.Where(val => val != this).ToArray();
        if (myDestinations.Length == 0)
        {
            DisablePortal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Teleport(collision.transform);
        }
    }
    
    private void Teleport(Transform victim)
    {
        if (portalActive)
        {

            List<Portal> toUse = new List<Portal>();
            foreach (Portal _portal in myDestinations)
            {
                if (_portal.PortalIsActive)
                {
                    toUse.Add(_portal);
                }
            }

            victim.position = toUse[Random.Range(0, toUse.Count - 1)].TeleportPoint.position;

            used = true;
            GameManager.Instance.OnPortalUsed();
        } 
    }


    private void DisablePortal()
    {
        portalActive = false;
        mySpriteRenderer.color = colorDeactivated;
    }

    private void EnablePortal()
    {
        portalActive = true;
        mySpriteRenderer.color = defaultColor;
    }


    //interface stuff

    public void DeActivateBox()
    {
        EnablePortal();
        
    }

    public void ActivateBox()
    {
        DisablePortal();
    }

    public void AddReference()
    {
        TheBox[] boxes = FindObjectsOfType<TheBox>();
        foreach (TheBox box in boxes)
        {
            box.AddReference(this);
        }
    }
}

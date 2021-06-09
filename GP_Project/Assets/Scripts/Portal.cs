using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour,BoxReferenced
{
    [SerializeField]
    private Portal[] myDestinations;


    private void Awake()
    {
        SettupDestinations();
    }

    private void Start()
    {
        AddReference();
    }

    private void SettupDestinations()
    {
        myDestinations = myDestinations.Where(val => val != this).ToArray();


        /*foreach (Portal por in myDestinations)
        {
            if (por == this)
            {
                myDestinations.
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void Deactivate()
    {

    }

    public void Activate()
    {

    }

    public void DeActivateBox()
    {
        throw new System.NotImplementedException();
    }

    public void ActivateBox()
    {
        throw new System.NotImplementedException();
    }

    public void AddReference()
    {
        FindObjectOfType<TheBox>().AddReference(this);
    }
}

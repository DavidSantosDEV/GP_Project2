using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TheBox : MonoBehaviour
{
    [SerializeField]
    private float effectTime=10;

    private List<BoxReferenced> myReferences = new List<BoxReferenced>();

    public void AddReference(BoxReferenced newVal)
    {
        myReferences.Add(newVal);
    }


    bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) return;
        if (collision.CompareTag("Player"))
        {
            isActive = true;
            foreach (BoxReferenced myref in myReferences)
            {
                myref.DeActivateBox();
            }
            Invoke(nameof(ResetStuff), effectTime);
        }

    }

    private void ResetStuff()
    {
        isActive = false;
        foreach(BoxReferenced myref in myReferences)
        {
            myref.ActivateBox();
        }
    }
}

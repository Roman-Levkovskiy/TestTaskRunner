using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArchPas : MonoBehaviour
{
    bool thisIsCorrectSide;
    HumanArch parentArch;
    public void setStartingValues(HumanArch parentArch, bool thisIsCorrectSide)
    {
        this.thisIsCorrectSide = thisIsCorrectSide;
        this.parentArch = parentArch;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HumanController>())   //its a bit faster then tag comparing and dont use strings
        {
            parentArch.passWasCrossed(thisIsCorrectSide);

            gameObject.SetActive(false);
        }
    }

}

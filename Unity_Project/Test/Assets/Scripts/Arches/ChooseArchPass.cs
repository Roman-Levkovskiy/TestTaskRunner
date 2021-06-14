using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseArchPass : MonoBehaviour
{
    bool thisIsCorrectSide;
    ChooseArch parentArch;
    Arch.shoes currentShoesType;
    public void setStartingValues(ChooseArch parentArch, bool thisIsCorrectSide, Arch.shoes currentShoesType)
    {
        this.thisIsCorrectSide = thisIsCorrectSide;
        this.parentArch = parentArch;
        this.currentShoesType = currentShoesType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HumanController>())   //its a bit faster then tag comparing and dont use strings
        {
            other.GetComponent<HumanController>().setShoes(currentShoesType.ToString());
            if (!thisIsCorrectSide)
            {
                if (other.gameObject.GetComponent<HumanController>().getThisHumanFirst())
                {
                    parentArch.destroyFirstHuman();
                }
            }
            else
            {
                other.GetComponent<HumanController>().passTheArch();
            }
        }
    }
}

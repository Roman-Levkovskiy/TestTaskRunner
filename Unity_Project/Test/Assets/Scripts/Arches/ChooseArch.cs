using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseArch : MonoBehaviour
{
    bool leftIsCorrectSide;
    Arch.shoes correctVariant;
    Arch.shoes wrongVariant;
    GameObject leftPass;
    GameObject rightPass;
    GameObject dangerZone;
    HumanCrowd humanCrowd;
    public void setStartingValues(bool leftIsCorrectSide, GameObject leftPass, GameObject rightPass, GameObject dangerZone, Arch.shoes correctVariant, Arch.shoes wrongVariant, Material correctShoesMaterial)
    {
        this.leftIsCorrectSide = leftIsCorrectSide;
        this.leftPass = leftPass;
        this.rightPass = rightPass;
        leftPass.AddComponent<ChooseArchPass>().setStartingValues(GetComponent<ChooseArch>(), leftIsCorrectSide, leftIsCorrectSide ? correctVariant : wrongVariant);
        rightPass.AddComponent<ChooseArchPass>().setStartingValues(GetComponent<ChooseArch>(), !leftIsCorrectSide, !leftIsCorrectSide ? correctVariant : wrongVariant);
        this.dangerZone = dangerZone;
        dangerZone.AddComponent<ArchBottomZone>().setCurrentShoesType(correctVariant, correctShoesMaterial);
    }
    public void setStartingValues(HumanCrowd humanCrowd)
    {
        this.humanCrowd = humanCrowd;
    }
    public void destroyFirstHuman()
    {
        humanCrowd.removeFirstHuman(false);
    }
}

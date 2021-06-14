using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanArch : MonoBehaviour
{
    GameObject humanPrefab;
    HumanCrowd humanCrowd;
    bool leftIsCorrectSide;
    GameObject leftPass;
    GameObject rightPass;
    public void setStartingValues(GameObject humanPrefab, HumanCrowd humanCrowd)
    {
        this.humanPrefab = humanPrefab;
        this.humanCrowd = humanCrowd;
    }
    public void setStartingValues(bool leftIsCorrectSide, GameObject leftPass, GameObject rightPass)
    {
        this.leftIsCorrectSide = leftIsCorrectSide;
        this.leftPass = leftPass;
        this.rightPass = rightPass;
        leftPass.AddComponent<HumanArchPas>().setStartingValues(GetComponent<HumanArch>(), leftIsCorrectSide);
        rightPass.AddComponent<HumanArchPas>().setStartingValues(GetComponent<HumanArch>(), !leftIsCorrectSide);
    }

    public void passWasCrossed(bool wasPickedCorrectSide)
    {
        if(wasPickedCorrectSide)
        {
            Vector3 currentLastHumanPosition = humanCrowd.getLastHuman().transform.position;
            humanCrowd.addLastHuman(Instantiate(humanPrefab, currentLastHumanPosition, new Quaternion()));
        }
        else
        {
            bool needToDestroyHuman = true;
            humanCrowd.removeFirstHuman(needToDestroyHuman);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    CameraFollows cameraFollows;
    HumanCrowd crowd;
    bool gameWasWon;
    public void setValues(CameraFollows cameraFollows, HumanCrowd crowd)
    {
        this.cameraFollows = cameraFollows;
        this.crowd = crowd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HumanController>())   //its a bit faster then tag comparing and dont use strings
        {
            if (!gameWasWon)
            {
                gameWasWon = true;
                crowd.winTheGame();
            }
            //Do winning animation
            if (other.gameObject.GetComponent<HumanController>().getThisHumanFirst())
            {
                //win the game
                other.gameObject.GetComponent<HumanController>().gameWasWon();
            }
            cameraFollows.setFirstHuman(null);
        }
    }
}

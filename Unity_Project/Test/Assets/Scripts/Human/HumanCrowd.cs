using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCrowd : MonoBehaviour
{
    [SerializeField] GameObject humanPrefab;
    public List<GameObject> crowd; //I dont use queue or stack because I need first human in case if he'll die and last to add someone in crowd
    [SerializeField] CameraFollows cameraFollows;
    [SerializeField] UIController uiController;
    private void Start()
    {
        cameraFollows = Camera.main.GetComponent<CameraFollows>();
        crowd = new List<GameObject>();
    }
    public void addLastHuman(GameObject human)
    {
        HumanController newLastHuman;
        newLastHuman = human.GetComponent<HumanController>();
        if (crowd.Count > 0)
        {
            HumanController lastHuman;
            GameObject lastHumanObject = crowd[crowd.Count - 1];
            lastHuman = lastHumanObject.GetComponent<HumanController>();

            lastHuman.setPrevHuman(human);
            newLastHuman.setNextHuman(lastHumanObject);
        }
        else
        {
            newLastHuman.setThisHumanFirst();
            cameraFollows.setFirstHuman(human.transform);
        }
        newLastHuman.setCrowd(GetComponent<HumanCrowd>());
        human.transform.parent = gameObject.transform;
        crowd.Add(human);

        uiController.changeScore(crowd.Count);
    }
    public  void removeFirstHuman(bool needToDestroyHuman)
    {
        if (needToDestroyHuman)
        {
            Destroy(crowd[0]);
        }

        if (crowd.Count > 1)
        {
            cameraFollows.setFirstHuman(crowd[1].transform);
            crowd[1].GetComponent<HumanController>().setNextHuman(null);
            crowd[0].GetComponent<HumanController>().setPrevHuman(null);
            for (int i = 0; i < crowd.Count - 1; ++i)
            {
                crowd[i] = crowd[i + 1];
            }
        }
        else
        {
            uiController.looseTheGame();
            crowd[0].GetComponent<HumanController>().setNextHuman(null);
            cameraFollows.setFirstHuman(null);
        }

        crowd[0].GetComponent<HumanController>().setThisHumanFirst();
        crowd.RemoveAt(crowd.Count - 1);

        uiController.unpause();
        uiController.changeScore(crowd.Count);
    }
    public void winTheGame()//Maybe I'll need this to change crowd logic on win
    {
        uiController.winTheGame();
    }
    public void noTochesRegistered()    //same
    {
        uiController.pause();
    }
    public void tochesRegistered()
    {
        uiController.unpause();
    }
    public GameObject getLastHuman()
    {
        return crowd[crowd.Count - 1];
    }
}

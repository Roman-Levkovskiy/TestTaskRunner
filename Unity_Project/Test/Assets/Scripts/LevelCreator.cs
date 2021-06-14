using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] CameraFollows cameraFollows;
    [SerializeField] GameObject trassaPrefab;
    [SerializeField] GameObject archPrefab;
    [SerializeField] GameObject humanPrefab;
    [SerializeField] HumanCrowd humanCrowd;
    [SerializeField] GameObject finish;
    int roadLength = 20;
    void Start()
    {
        humanCrowd.addLastHuman(Instantiate(humanPrefab));
        createTrassa(roadLength);
        createHumanArch(new Vector3(0, 0, 32));
        createHumanArch(new Vector3(0, 0, 64));
        createHumanArch(new Vector3(0, 0, 96));
        createHumanArch(new Vector3(0, 0, 128));
        createHumanArch(new Vector3(0, 0, 160));
        createChooseArch(new Vector3(0, 0, 192), Arch.shoes.cylinder);
        createChooseArch(new Vector3(0, 0, 224), Arch.shoes.cube);
        createChooseArch(new Vector3(0, 0, 256), Arch.shoes.cylinder);
        createChooseArch(new Vector3(0, 0, 288), Arch.shoes.cube);
        createFinish();
    }

    void createFinish()
    {
        Instantiate(finish, new Vector3(0, -0.4f, roadLength*16), new Quaternion()).GetComponent<Finish>().setValues(cameraFollows, humanCrowd);
    }
    void createTrassa(int roadLength)
    {
        for(int i = 0; i < roadLength; ++i)
        {
            Instantiate(trassaPrefab, new Vector3(0, 0, i * 16), new Quaternion()).transform.eulerAngles =  new Vector3(-90, 0, 0);
        }
    }
    void createHumanArch(Vector3 coordinates)
    {
        GameObject humanArch = Instantiate(archPrefab, coordinates, new Quaternion());
        humanArch.GetComponent<Arch>().setArchType();
        humanArch.GetComponent<HumanArch>().setStartingValues(humanPrefab, humanCrowd);
    }
    void createChooseArch(Vector3 coordinates, Arch.shoes shoes)
    {
        GameObject chooseArch = Instantiate(archPrefab, coordinates, new Quaternion());
        chooseArch.GetComponent<Arch>().setArchType(shoes);
        chooseArch.GetComponent<ChooseArch>().setStartingValues(humanCrowd);
    }
}

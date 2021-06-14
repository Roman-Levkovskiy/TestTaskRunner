using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Im not proud of this script, but there is nothing critical for now. I think I'll rewrite it later
public class Arch : MonoBehaviour
{
    [SerializeField] GameObject leftFrameObject;
    [SerializeField] GameObject leftPass;
    [SerializeField] GameObject leftFrame;
    
    [SerializeField] GameObject rightFrameObject;
    [SerializeField] GameObject rightPass;
    [SerializeField] GameObject rightFrame;

    [SerializeField] GameObject dangerZone;

    [SerializeField] Material correctChoiceMaterial;
    [SerializeField] Material wrongChoiceMaterial;

    [SerializeField] GameObject rootShoesObject;
    public Dictionary<string, GameObject> listOfShoes;

    archType typeOfArch;
    struct setOfSideObjects
    {
        public GameObject frameObject;
        public GameObject pass;
        public GameObject frame;

        public setOfSideObjects(GameObject frameObject, GameObject pass, GameObject frame)
        {
            this.frameObject = frameObject;
            this.pass = pass;
            this.frame = frame;
        }
    }
    public enum shoes
    {
        cube,
        cylinder,
        sphere
    }
    public enum archType
    {
        human,
        chose
    }

    //I'm not shure about this variant. It will work only if different arches require different set of input
    //But I can easily rewrite this part of code on curent stage
    public void setArchType()
    {
        this.typeOfArch = archType.human;
        createHumanArch();
    }
    public void setArchType(shoes correctVariant)
    {
        this.typeOfArch = archType.chose;
        createChooseArch(correctVariant);
    }
    void createChooseArch(shoes correctVariant)
    {
        listOfShoes = new Dictionary<string, GameObject>();
        foreach (Transform child in rootShoesObject.transform)
        {
            listOfShoes.Add(child.name, child.gameObject);
        }
        
        setOfSideObjects leftSide, rightSide;       //I need this just to hold those objects in left/right category if I need to use them separatly

        leftSide = new setOfSideObjects(leftFrameObject, leftPass, leftFrame);
        rightSide = new setOfSideObjects(rightFrameObject, rightPass, rightFrame);

        shoes randomWrongShoes = correctVariant;
        int shoesTypesCount = Enum.GetNames(typeof(shoes)).Length;
        while (randomWrongShoes == correctVariant)
        {
            randomWrongShoes = (shoes)UnityEngine.Random.Range(0, shoesTypesCount);
        }

        Material correctShoesMaterial = listOfShoes[correctVariant.ToString()].GetComponent<Shoes>().getThisMaterial();
        Material wrongShoesMaterial = listOfShoes[randomWrongShoes.ToString()].GetComponent<Shoes>().getThisMaterial();

        bool isLeftIsCorrect = UnityEngine.Random.Range(0, 2) == 0;

        gameObject.AddComponent<ChooseArch>().setStartingValues(isLeftIsCorrect, leftPass, rightPass, dangerZone, correctVariant, randomWrongShoes, correctShoesMaterial);

        setSide(leftSide, isLeftIsCorrect ? correctVariant : randomWrongShoes, isLeftIsCorrect, isLeftIsCorrect ? correctShoesMaterial : wrongShoesMaterial);
        setSide(rightSide, !isLeftIsCorrect ? correctVariant : randomWrongShoes, !isLeftIsCorrect, !isLeftIsCorrect ? correctShoesMaterial : wrongShoesMaterial);
    }
    void createHumanArch()
    {
        HumanArch thisHumanArch = gameObject.AddComponent<HumanArch>();

        bool isLeftIsCorrect = UnityEngine.Random.Range(0, 2) == 0;

        thisHumanArch.setStartingValues(isLeftIsCorrect, leftPass, rightPass);

        setOfSideObjects leftSide, rightSide;

        leftSide = new setOfSideObjects(leftFrameObject, leftPass, leftFrame);
        rightSide = new setOfSideObjects(rightFrameObject, rightPass, rightFrame);

        dangerZone.SetActive(false);

        if (isLeftIsCorrect)
        {
            leftFrame.GetComponent<Renderer>().material = correctChoiceMaterial;
            rightFrame.GetComponent<Renderer>().material = wrongChoiceMaterial;
        }
        else
        {
            leftFrame.GetComponent<Renderer>().material = wrongChoiceMaterial;
            rightFrame.GetComponent<Renderer>().material = correctChoiceMaterial;
        }
    }
    void setSide(setOfSideObjects setOfObjects, shoes variant, bool isCorrect, Material shoesMaterial)      //that method COULD contain more code, so I'll keep it for now
    {
        GameObject shoesInFrame = Instantiate(listOfShoes[variant.ToString()], setOfObjects.frameObject.transform.position, new Quaternion());
        shoesInFrame.transform.parent = gameObject.transform.parent;
        shoesInFrame.GetComponent<Renderer>().material = shoesMaterial;
    }
}

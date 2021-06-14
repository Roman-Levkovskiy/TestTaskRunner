using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    HumanAnimationController animationController;
    HumanCrowd crowd;
    [SerializeField] GameObject nextHuman, prevHuman;
    [SerializeField] GameObject particles;
    Vector3 targetPosition;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool isFirst;
    Dictionary<string, pairOfShoes> listOfShoes;
    [SerializeField] GameObject rootShoesObjectLeft;
    [SerializeField] GameObject rootShoesObjectRight;
    [SerializeField] pairOfShoes currentShoes;
    bool isPushed;
    float pushCoordinate;
    bool isGameWon;
    struct pairOfShoes
    {
        GameObject leftShoe;
        GameObject rightShoe;

        public pairOfShoes(GameObject leftShoe, GameObject rightShoe)
        {
            this.leftShoe = leftShoe;
            this.rightShoe = rightShoe;
        }
        public void clearCurrentShoes()
        {
            if (leftShoe)
            {
                leftShoe.SetActive(false);
                leftShoe = null;
                rightShoe.SetActive(false);
                rightShoe = null;
            }
        }
        public void activateCurrentShoes()
        {
            leftShoe.SetActive(true);
            rightShoe.SetActive(true);
        }
        public string getShoe()
        {
            return leftShoe.name;
        }
    }

    void Start()
    {
        animationController = GetComponent<HumanAnimationController>();
        rb = GetComponent<Rigidbody>();

        listOfShoes = new Dictionary<string, pairOfShoes>();

        for(int i = 0; i<rootShoesObjectLeft.transform.childCount; ++i)
        {

            listOfShoes.Add
            (
                rootShoesObjectLeft.transform.GetChild(i).name,
                new pairOfShoes
                (
                    rootShoesObjectLeft.transform.GetChild(i).gameObject, 
                    rootShoesObjectRight.transform.GetChild(i).gameObject
                )
            );

        }
    }
    public void setCrowd(HumanCrowd crowd)
    {
        this.crowd = crowd;
    }
    public void setShoes(string nameOfShoes)
    {
        currentShoes.clearCurrentShoes();
        currentShoes = listOfShoes[nameOfShoes];
        currentShoes.activateCurrentShoes();
    }
    public string getShoes()//I dont like idea of using strings in here, but time is almost over. I can rewrite this code later
    {
        return currentShoes.getShoe();
    }
    public void setNextHuman(GameObject human)
    {
        nextHuman = human;
        if(isFirst)
        {
            isFirst = false;
        }
    }
    public void setPrevHuman(GameObject human)
    {
        prevHuman = human;
    }
    public void setThisHumanFirst()
    {
        isFirst = true;
    }
    public bool getThisHumanFirst()
    {
        return isFirst;
    }
    public void push(float pushCoordinate)
    {
        isPushed = true;
        this.pushCoordinate = pushCoordinate;
    }
    public void gameWasWon()
    {
        isGameWon = true;
    }
    public void passTheArch()
    {
        GameObject newParticles = Instantiate(particles);
        newParticles.SetActive(true);
        newParticles.GetComponent<ParticleSystem>().Play();
        newParticles.transform.position = transform.position;
        Destroy(newParticles, 2);
        animationController.jump();
    }
    private void FixedUpdate()
    {
        if (!isGameWon)
        {
            if (nextHuman)
            {
                Vector3 nextHumanPos = nextHuman.transform.position;
                targetPosition.z = nextHumanPos.z - 2;
                targetPosition.x = nextHumanPos.x;
            }
            else
            {
                if (isFirst)
                {
                    if (Input.touchCount > 0 || Input.GetMouseButton(0))
                    {
                        //float tapPosition = (Input.mousePosition.x - Camera.main.pixelWidth / 2) / Camera.main.pixelWidth;               //for mouse tests
                        float tapPosition = (Input.GetTouch(0).position.x - Camera.main.pixelWidth / 2) / Camera.main.pixelWidth;               //for mouse tests
                        if (transform.position.x > -4 && transform.position.x < 4)
                        {
                            targetPosition.x = transform.position.x + tapPosition;
                        }
                        crowd.tochesRegistered();
                    }
                    else
                    {
                        if (!isGameWon)
                        {
                            crowd.noTochesRegistered();
                        }
                    }
                    targetPosition.z = transform.position.z + 2;
                }
                else//if person is not first and doesnt have hext human, then it will die in the near future, so it should just go forward
                {
                    targetPosition.x = 0;
                    targetPosition.z = transform.position.z + 2;
                }
            }
            if (isPushed)
            {
                targetPosition.x = targetPosition.x < pushCoordinate ? targetPosition.x - 2 : targetPosition.x + 2;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        rb.velocity = (targetPosition - transform.position) * 4.5f;
        isPushed = false;
    }
}

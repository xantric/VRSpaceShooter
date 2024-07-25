using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateDeath : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MainGameObject;
    public GameObject RaysObject;
    public GameObject Gun1;
    public GameObject Gun2;
    // public int Health;

    void Start()
    {
        Invoke("Exit", 10);
    }

    // void HealthSystem(){

    // }
    void Exit(){
        Menu.SetActive(true);
        RaysObject.SetActive(true);
        MainGameObject.SetActive(false);
        Gun1.SetActive(false);
        Gun2.SetActive(false);
    }

}

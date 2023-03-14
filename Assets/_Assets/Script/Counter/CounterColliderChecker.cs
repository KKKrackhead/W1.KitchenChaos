using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterColliderChecker : MonoBehaviour
{
    public int ItemIndex;
    int TempIndex;
    bool CounterChecked;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collider's working");
        CounterChecked = true;
        TempIndex = GetComponent<CounterColliderChecker>().ItemIndex;
        Debug.Log("Index = " +TempIndex);
    }

    void OnTriggerExit(Collider other)
    {
        TempIndex = 5;
    }

    private void Update()
    {
        if (CounterChecked && Input.GetKeyDown(KeyCode.E))
        {
            switch (TempIndex)
            {
                case 0:
                    GameObject.Find("CheeseStorage").GetComponent<Animator>().SetTrigger("OpenClose");
                    break;

                case 1:
                    GameObject.Find("BunsStorage").GetComponent<Animator>().SetTrigger("OpenClose");
                    break;

                case 2:
                    GameObject.Find("MeatStorage").GetComponent<Animator>().SetTrigger("OpenClose");
                    break;

                case 3:
                    GameObject.Find("CabbageStorage").GetComponent<Animator>().SetTrigger("OpenClose");
                    break;

                case 4:
                    GameObject.Find("TomatoStorage").GetComponent<Animator>().SetTrigger("OpenClose");
                    break;

                default:
                    break;
            }
        }
    }
}

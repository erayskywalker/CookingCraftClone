using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryIn : MonoBehaviour
{
    public int current;
    [SerializeField] int max;

    [SerializeField] BakeryOut bakeryOut;

    private void Start()
    {
        bakeryOut = transform.parent.GetComponentInChildren<BakeryOut>();
    }


    float timer = 0.6f;

    bool isCollecting;

    private void Update()
    {
        if (isCollecting && current < max && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            AddObjToBakery();
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (GameManager.Instance.PlayerStack > 0 && current < max)
    //    {
    //        isCollecting = true;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.PlayerStack > 0 && current < max)
        {
            isCollecting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCollecting)
        {
            isCollecting = false;
        }
    }
    private void AddObjToBakery()
    {
        if (timer < 0f)
        {
            current++;
            PlayerStacks.StackInstance.RemoveStack();
            bakeryOut.meal++;
            timer = 0.6f;
        }

        if (current == max)
        {
            isCollecting = false;
        }
    }
}

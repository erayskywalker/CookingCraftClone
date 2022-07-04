using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableOrder : MonoBehaviour
{
    TableController table;

    public int tableNumber;

    [SerializeField] Transform stacks;

    public List<GameObject> customers = new List<GameObject>();

    public int burgerNeeded;
    public int burgerGived;

    bool isCustomerArrived;

    float timer;

    bool canPuttingBurger;

    Coroutine customer;

    private void Awake()
    {
        table = GetComponentInParent<TableController>();
    }

    private void Update()
    {
        if (burgerNeeded == 0 && table.burgerOrder[tableNumber] != 0)
        {
            burgerNeeded = table.burgerOrder[tableNumber];
        }

        if (canPuttingBurger && burgerGived < burgerNeeded && GameManager.Instance.PlayerStack > 0)
        {
            timer -= Time.deltaTime;
            PutBurgerToTable();
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
            isCustomerArrived = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (burgerGived < burgerNeeded)
            {
                canPuttingBurger = GameManager.Instance.PlayerStack > 0 && stacks.GetChild(GameManager.Instance.PlayerStack).gameObject.CompareTag("Burger") && isCustomerArrived;
            }
        }

        #region Siparisler tamamlandiginda

        if (burgerNeeded == burgerGived && burgerGived != 0)
        {
            burgerGived = 0;
            customer = StartCoroutine(Completed());

        }
        #endregion
    }

    private void OnTriggerExit(Collider other)
    {
        if (canPuttingBurger)
        {
            canPuttingBurger = false;
        }

        if (other.CompareTag("Customer"))
        {
            isCustomerArrived = false;
        }
    }


    void PutBurgerToTable()
    {
        if (timer < 0f)
        {
            burgerGived++;
            PlayerStacks.StackInstance.RemoveStack();
            timer = GameManager.Instance.puttingSpeed;
        }

        if (burgerGived == burgerNeeded)
        {
            canPuttingBurger = false;
        }
    }

    IEnumerator Completed()
    {
        yield return new WaitForSeconds(5f);

        foreach (GameObject go in customers)
            go.GetComponent<CustomerController>().LeaveRestourant();

        customers.Clear();

        table.ClearTable(tableNumber);
        yield return null;
    }
}

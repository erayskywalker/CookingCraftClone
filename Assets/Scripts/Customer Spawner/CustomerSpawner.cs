using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] CustomerPrefabs;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] TableController table;

    [SerializeField] int level;

    [SerializeField] float min, max;

    [SerializeField] float distance;

    int groupSize;
    float timer;
    int j;


    private void Start()
    {
        //StartCoroutine(FirstSpawn());
    }

    private void Update()
    {
        //if (isFirstCustomerArrived)
        Spawner();
    }

    private void Spawner()
    {
        if (table.tableCount != 0)
            timer += Time.deltaTime;

        if (timer > Random.Range(min, max) && table.tableCount != 0)
        {
            timer = 0f;
            groupSize = Random.Range(1, 4);

            for (j = 0; j < 3; j++)
            {
                if (table.isEmpty[j])
                {
                    table.isEmpty[j] = false;
                    table.tableCount--;
                    break;
                }
            }


            for (int i = 0; i < groupSize; i++)
            {
                GameObject go = Instantiate(CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)], SpawnPoint.position + new Vector3(0f, 0f, i * distance), new Quaternion(0f, 180f, 0f, 0f), this.transform);
                go.GetComponent<CustomerController>().WalkToTable(table.tables[j].GetChild(i).transform);
                go.GetComponent<CustomerController>().settedTable = j;
                table.transform.GetChild(j).GetComponent<TableOrder>().customers.Add(go);

                switch (level)
                {
                    case 1:
                        table.burgerOrder[j] += go.GetComponent<CustomerController>().BurgerOrder();
                        break;

                    case 2:
                        table.hotdogOrder[j] += go.GetComponent<CustomerController>().HotDogOrder();
                        table.pizzaOrder[j] += go.GetComponent<CustomerController>().PizzaOrder();
                        break;

                }


            }
        }
    }

}

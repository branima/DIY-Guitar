using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{

    public GameObject[] customerPrefabs;

    public Transform customerStartingPosition;
    public Transform customerOrderingPosition;

    int currCustomerIdx;

    // Start is called before the first frame update
    void Awake()
    {
        currCustomerIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnNext()
    {
        GameObject newCustomer = Instantiate(customerPrefabs[currCustomerIdx], customerStartingPosition.position, customerPrefabs[currCustomerIdx].transform.rotation, null);
        newCustomer.GetComponent<MoveForOrder>().targetPosition = customerOrderingPosition;
        currCustomerIdx++;
        currCustomerIdx = currCustomerIdx % customerPrefabs.Length;
        return newCustomer;
    }
}

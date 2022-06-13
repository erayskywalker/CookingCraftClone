using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacks : MonoBehaviour
{
    [SerializeField] GameObject[] stackPrefabs;
    [SerializeField] Transform objTransform;
    [SerializeField] float distance;


    int current, max;

    public static PlayerStacks StackInstance { get; private set; }

    private void Awake()
    {
        if (StackInstance == null)
        {
            StackInstance = this;
        }
    }


    public IEnumerator AddStack(int stackIndex)
    {
        while (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
        {
            yield return new WaitForSeconds(GameManager.Instance.collectingSpeed);
            Instantiate(stackPrefabs[stackIndex], new Vector3(objTransform.transform.position.x, objTransform.transform.position.y, objTransform.transform.position.z), objTransform.rotation, this.transform);
            objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y + distance, objTransform.transform.position.z);
            GameManager.Instance.PlayerStack++;
            yield return null;
        }
    }

    public void RemoveStack()
    {
        Destroy(gameObject.transform.GetChild(GameManager.Instance.PlayerStack).gameObject);
        GameManager.Instance.PlayerStack--;
        objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y - distance, objTransform.transform.position.z);
    }


}

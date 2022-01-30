using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvoreManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;

    public List<GameObject> listOfTrees;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player1.isDead || player2.isDead)
        {
            int tmp = UnityEngine.Random.Range(0, listOfTrees.Count);
            listOfTrees[tmp].gameObject.SetActive(false);
            listOfTrees.Remove(listOfTrees[tmp]);
        }
    }
}

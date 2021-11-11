using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    
    [SerializeField]private GameObject generateMonster;
    [SerializeField]private Transform generatePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateOneMonster(){
        Instantiate(generateMonster,generatePoint);
        Debug.Log("born one monster!");
    }

}

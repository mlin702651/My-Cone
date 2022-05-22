using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [System.Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake(){
        if(Instance==null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    #endregion


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools){    //開好每個池子
            Queue<GameObject> objectPool = new Queue<GameObject>(); //池子裡的物件queue

            for(int i = 0; i<pool.size; i++){ //把每個物件加到queue裡
                GameObject obj = Instantiate(pool.prefab);
                DontDestroyOnLoad(obj);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);  //把這個池子加入Dictionary
        }
    }

    //生物件
    public GameObject SpawnFromPool (string tag, Vector3 position, Vector3 rotation){
        
        if(!poolDictionary.ContainsKey(tag)){
            Debug.LogWarning("Pool with tag" + tag + "doesn't exsist.");
            return null;
        }
        //Debug.Log("try");
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();//傳回第一個物件 並移除

        //設定初始值
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.eulerAngles = rotation;

        //給物件上面的那些值
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        //重生
        if(pooledObj != null){
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

    public void DisableObject(string tag){
        foreach (GameObject item in poolDictionary[tag])
        {
            item.SetActive(false);
        }
    }

    public void MakeChild(string tag, ref GameObject parent){
        
        foreach (GameObject item in poolDictionary[tag])
        {
            item.transform.parent = parent.transform;
        }
    }

    

}
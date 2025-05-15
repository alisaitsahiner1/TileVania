using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()     //starttan daha önce çalışır
    {
        int numberScenePersist=FindObjectsOfType<ScenePersist>().Length;
        if(numberScenePersist>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}

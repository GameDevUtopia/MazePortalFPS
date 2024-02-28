using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetValue : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > 10)
        {
            StaticData.t = timer;
            SceneManager.LoadScene("Exit");
        }
    }
   
}

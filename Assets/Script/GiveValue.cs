using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveValue : MonoBehaviour
{
    [SerializeField] Text text; 
    // Start is called before the first frame update
    void Start()
    {
        float newText = StaticData.t;
        text.text = newText.ToString();
    }
}

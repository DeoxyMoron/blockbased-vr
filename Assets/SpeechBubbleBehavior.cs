using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleBehavior : MonoBehaviour
{
    public int foobar;
    public TMPro.TextMeshPro textObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textObj.text = "hey";
    }
}

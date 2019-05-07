using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penguin_behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent!= null)
        {
            if (gameObject.transform.parent.tag == "ally")
            {
                gameObject.transform.position = gameObject.transform.parent.position;

            }
        }

    }
}

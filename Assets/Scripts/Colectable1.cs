using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable1 : MonoBehaviour
{
    public GameObject item1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            item1.SetActive(true);
        }
    }
}

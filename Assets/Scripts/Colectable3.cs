using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable3 : MonoBehaviour
{
    public GameObject item3;
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
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            item3.SetActive(true);
        }
    }
}

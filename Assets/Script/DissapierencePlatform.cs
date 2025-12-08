using System.Collections.Generic;
using UnityEngine;

public class DissapierencePlatform : MonoBehaviour
{
    public float DissapierenceSecond = 1.5f;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("HidePlatform", DissapierenceSecond);
        }
    }

    void HidePlatform()
    {
        this.gameObject.SetActive(false);
    }

}

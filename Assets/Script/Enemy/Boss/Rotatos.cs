using UnityEngine;

public class Rotatos : MonoBehaviour {

    public float speed;

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0,0, speed * Time.deltaTime);
    }
}

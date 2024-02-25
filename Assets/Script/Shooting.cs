using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public Rigidbody Bullet;
    public float bulletSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
    
            Rigidbody bulletInstance = Instantiate(Bullet, shootPoint.position, transform.rotation);
            bulletInstance.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            Destroy(bulletInstance.gameObject, 3f);
        }
    }
}

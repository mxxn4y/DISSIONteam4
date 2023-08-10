using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTracker : MonoBehaviour
{

    public float aS = 5f;
    public float aR = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, aR);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Object"))
                {
                    Vector3 forceDirection = transform.position - hitCollider.transform.position;
                    hitCollider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * aS);
                }
            }

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aR);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objects")
            Destroy(other.gameObject);
    }
}

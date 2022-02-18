using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    MeshFilter meshFilter;

    [SerializeField] private Mesh capsuleMesh = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontal * 10, 0, vertical * 10);
        rb.velocity = transform.TransformDirection(rb.velocity);

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(new Vector3(0, mouseX * 2, 0));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (Input.GetKey(KeyCode.G))
            {
                MeshFilter otherMeshFilter = other.gameObject.GetComponent<MeshFilter>();
                meshFilter.mesh = otherMeshFilter.mesh;
                rb.velocity = Vector3.zero;
            }
            else
            {
                meshFilter.mesh = capsuleMesh;
            }
        }
        else if (other.gameObject.layer == 7)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Destroy(other.gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            meshFilter.mesh = capsuleMesh;
        }
    }
}

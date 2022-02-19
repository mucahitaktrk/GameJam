using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    SkinnedMeshRenderer meshFilter;
    Animator playerAnimator;
    [SerializeField] private Mesh capsuleMesh = null;
    public static bool isHide = false;

    private float speed = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = transform.GetChild(1)
            .GetComponent<SkinnedMeshRenderer>();

        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontal * 10, 0, vertical * 10).normalized;
        rb.velocity = transform.TransformDirection(rb.velocity);

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.rotation = Quaternion.Euler(0,90,vertical);

        float i = rb.velocity.z + rb.velocity.x;

        playerAnimator.SetFloat("speedZ", Mathf.Abs(i));
       // playerAnimator.SetFloat("speedZ", Mathf.Abs(vertical));

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (Input.GetKey(KeyCode.G))
            {
                MeshFilter otherMeshFilter = other.gameObject.GetComponent<MeshFilter>();
                meshFilter.sharedMesh = otherMeshFilter.mesh;
                rb.velocity = Vector3.zero;
                isHide = true;
            }
            else
            {
                meshFilter.sharedMesh = capsuleMesh;
                isHide = false;
            }
        }
        else if (other.gameObject.layer == 7)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Destroy(other.gameObject);
                isHide = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            meshFilter.sharedMesh = capsuleMesh;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    SkinnedMeshRenderer meshFilter;
    Animator playerAnimator;
    [SerializeField] private Mesh playerMesh = null;
    [SerializeField] private Material playerMaterial = null;
    public static bool isHide = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = transform.GetChild(1)
            .GetComponent<SkinnedMeshRenderer>();

        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isHide)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(horizontal * 2, 0, vertical * 2);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector3(horizontal * 5, 0, vertical * 5);
            }
            if (rb.velocity != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
            }

            float i = Mathf.Abs(rb.velocity.z) + Mathf.Abs(rb.velocity.x);

            playerAnimator.SetFloat("speedZ", Mathf.Abs(i));

        }

    }



private void OnCollisionStay(Collision other)
{
        if (other.gameObject.layer == 6)
        {
            if (Input.GetKey(KeyCode.G))
            {
                MeshFilter otherMeshFilter = other.gameObject.GetComponent<MeshFilter>();
                MeshRenderer otherMeshRenderer = other.gameObject.GetComponent<MeshRenderer>();
                meshFilter.material = otherMeshRenderer.material;
                meshFilter.sharedMesh = otherMeshFilter.mesh;
                rb.velocity = Vector3.zero;
                isHide = true;
            }
            else
            {
                meshFilter.sharedMesh = playerMesh;
                meshFilter.material = playerMaterial;
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

    private void OnCollisionExit(Collision other)
{
        if (other.gameObject.layer == 6)
        {
            meshFilter.sharedMesh = playerMesh;
            meshFilter.material = playerMaterial;
        }
    }
}

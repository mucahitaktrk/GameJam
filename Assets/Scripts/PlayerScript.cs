using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    SkinnedMeshRenderer meshFilter;
    Animator playerAnimator;
    [SerializeField] private Mesh playerMesh = null;
    [SerializeField] private Material playerMaterial = null;
    public static bool isHide = false;

    [SerializeField] private GameObject phone = null;

    [SerializeField] private GameObject[] tik;

    Vector3 direction;

    public CharacterController playerCharacterController;
    [SerializeField] private Transform cameraTransform;
    AudioSource audioSourch;

    public AudioClip[] ac;

    float playerSpeed;
    private float characterTurnSmoothTime = 0.1f;
    private float characterTurnSmoothVelocity = 0.0f;

    public bool[] urun1;

    public Slider timeBar;

    float time = 0;


    public GameObject win;
    public GameObject lose;

    public GameObject image;
    void Start()
    {
        image.SetActive(false);
        Time.timeScale = 1f;
        win.SetActive(false);
        lose.SetActive(false);
        urun1 = new bool[8];
        audioSourch = GetComponent<AudioSource>();
        for (int i = 0; i < tik.Length; i++)
        {
            tik[i].SetActive(false);
        }
        meshFilter = transform.GetChild(1)
            .GetComponent<SkinnedMeshRenderer>();
        phone.SetActive(false);
        playerAnimator = GetComponent<Animator>();

        StartCoroutine(Ses());
    }

    void Update()
    {

        if (urun1[0] && urun1[1] && urun1[2] && urun1[3] && urun1[4] && urun1[5] && urun1[6] && urun1[7])
        {
            win.SetActive(true);
            Time.timeScale = 0f;
        }
        time += Time.deltaTime;
        timeBar.value = time;
        if (time <= 180)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            if (!isHide)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 100 * Time.fixedDeltaTime, 0);


                direction = new Vector3(horizontal * playerSpeed, 0.0f, vertical * playerSpeed);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSpeed = 5f;
                }
                else
                {
                    playerSpeed = 2f;
                }

                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref characterTurnSmoothVelocity, characterTurnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0);

                    Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
                    playerCharacterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
                }



                float i = Mathf.Abs(direction.z) + Mathf.Abs(direction.x);

                playerAnimator.SetFloat("speedZ", Mathf.Abs(i));

            }
            if (Input.GetKey(KeyCode.Tab))
            {
                phone.SetActive(true);
            }
            else
            {
                phone.SetActive(false);
            }
        }
        else
        {
            lose.SetActive(true);
        }
    }



    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            if (Input.GetKey(KeyCode.Q))
            {

                    MeshFilter otherMeshFilter = other.gameObject.GetComponent<MeshFilter>();
                    MeshRenderer otherMeshRenderer = other.gameObject.GetComponent<MeshRenderer>();
                    meshFilter.material = otherMeshRenderer.material;
                    meshFilter.sharedMesh = otherMeshFilter.mesh;
                    playerSpeed = 0;
                    isHide = true;
                image.SetActive(true);

            }
            else
            {
                meshFilter.sharedMesh = playerMesh;
                meshFilter.material = playerMaterial;
                isHide = false;
                image.SetActive(false);
            }
        }
            if (other.gameObject.tag == "urun1")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(other.gameObject);
                    tik[0].SetActive(true);
                    audioSourch.clip = ac[1];
                    audioSourch.Play();
                urun1[0] = true;
                }
            }
        else if (other.gameObject.tag == "urun2")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[1].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[1] = true;
            }
        }
        else if (other.gameObject.tag == "urun3")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[2].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[2] = true;
            }
        }
        else if (other.gameObject.tag == "urun4")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[3].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[3] = true;
            }
        }
        else if (other.gameObject.tag == "urun5")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[4].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[4] = true;
            }
        }
        else if (other.gameObject.tag == "urun6")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[5].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[5] = true;
            }
        }
        else if (other.gameObject.tag == "urun7")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[6].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[6] = true;
            }
        }
        else if (other.gameObject.tag == "urun8")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(other.gameObject);
                tik[7].SetActive(true);
                audioSourch.clip = ac[1];
                audioSourch.Play();
                urun1[7] = true;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            isHide = false;
            meshFilter.sharedMesh = playerMesh;
            meshFilter.material = playerMaterial;
        }
    }

    IEnumerator Ses()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (urun1[0] && urun1[1] && urun1[2] && urun1[3] && urun1[4] && urun1[5] && urun1[6] && urun1[7])
            {
                audioSourch.clip = ac[0];
                audioSourch.Play();
            }
        }
        
    }

    public void Win()
    {
        SceneManager.LoadScene(0);
    }
}


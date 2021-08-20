using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float mouseSensitivity = 1f;
    public float moveSensitivity = 1f;

    Vector3 dir;
    int xDir, yDir;
    float rot = 0;

    Rigidbody rigid;
    Animator anim;
    PlayerHealth health;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused && !health.isDead)
        {
            //rotate camera
            rot += Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.rotation = Quaternion.Euler(0, rot, 0);

            if (Input.GetKey(KeyCode.D))
            {
                xDir = 1;
            } else if (Input.GetKey(KeyCode.A))
            {
                xDir = -1;
            } else
            {
                xDir = 0;
            }

            if (Input.GetKey(KeyCode.W))
            {
                yDir = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                yDir = -1;
            }
            else
            {
                yDir = 0;
            }

            rigid.velocity = Vector3.ClampMagnitude(transform.right * xDir + transform.forward * yDir, 1f) * moveSensitivity;
        }
    }
}

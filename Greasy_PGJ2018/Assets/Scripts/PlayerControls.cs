using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {

    public AudioClip jump;
    AudioSource AudioSRX;

	Rigidbody2D playerRigidBody;
	public float speed;
	public float jumpForce;
    int canJump;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    public SpriteRenderer[]  bodyParts;
    Animator playerAnimator;

    public GameObject smokePrefab;
    public GameObject wrench;
    GameObject wrenchInstance;
    bool facingRight = true;

    void Awake () {
		playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        AudioSRX = GetComponent<AudioSource>();
    }

    private void Start()
    {
        canJump = 2;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].flipX = true;
            }
            playerAnimator.SetBool("Walk", true);
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 15.796f));
            facingRight = false;
            if (wrenchInstance != null)
            {
                wrenchInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 120.0f);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].flipX = false;
            }
            playerAnimator.SetBool("Walk", true);
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -15.796f));
            facingRight = true;
            if (wrenchInstance != null)
            {
                wrenchInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 75.0f);
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("Duck", true);
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.0273627f, 0.2736068f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(2.225312f, 2.409235f);
        }

        if (playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump > 0)
        {
            AudioSRX.clip = jump;

            if(!AudioSRX.isPlaying)
            {
                AudioSRX.Play();
            }

            playerAnimator.SetBool("Jump", true);
            playerRigidBody.velocity = Vector2.up * jumpForce;
            canJump--;
            if (canJump >= 1)
            {
                Destroy(Instantiate(smokePrefab, transform.Find("Bottom").position, Quaternion.identity), 0.2f);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("Walk", false);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("Duck", false);
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.0273627f, -0.5083277f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(2.225312f, 4.173103f);
            transform.Translate(Vector2.up * 2);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            if (!GameObject.Find("Wrench(Clone)"))
            {
                wrenchInstance = facingRight ? Instantiate(wrench, transform.Find("RightArm").transform) : Instantiate(wrench, transform.Find("LeftArm").transform);
                wrenchInstance.GetComponent<Animator>().enabled = false;
            }
            else
            {
                Destroy(wrenchInstance);
            }
        }
        if (wrenchInstance != null)
        {
            wrenchInstance.transform.position = facingRight ? new Vector2(transform.Find("RightArm").transform.position.x + 0.1f, transform.Find("RightArm").transform.position.y - 0.1f) :
                                                              new Vector2(transform.Find("LeftArm").transform.position.x - 0.2f, transform.Find("LeftArm").transform.position.y - 0.2f);
            wrenchInstance.transform.rotation = facingRight ? transform.Find("RightArm").transform.rotation : transform.Find("LeftArm").transform.rotation;
            if(facingRight)
            {
                wrenchInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 75.0f);
            }
            else
            {
                wrenchInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 120.0f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                wrenchInstance.GetComponent<Animator>().enabled = true;
                if(facingRight)
                {
                    wrenchInstance.GetComponent<Animator>().Play("WrenchAnimation", 0, 0f);
                }
                else
                {
                    wrenchInstance.GetComponent<Animator>().Play("WrenchAnimationLEft", 0, 0f);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            canJump = 2;
            playerAnimator.SetBool("Jump", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour {
  public CharacterController controller;
  public float speed = 12f;
  public float gravity = -9.81f;

  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public float jumpHeight = 3f;
  public LayerMask groundMask;

  private int health = 100;
  [SerializeField]
  private TMP_Text healthText;
  [SerializeField]
  private AudioSource footSteps;

  public HealthLimit healthSlider;

  [SerializeField]
  private MouseMovment cameraMouse;

  Vector3 velocity;
  bool isGrounded;
  // Start is called before the first frame update
  void Start() { healthText.text = health.ToString();
    healthSlider.SetMaxHealth(health);
   }

  public int GetHealth() { return health; }

  public void damage(int dam) {
    health -= dam;
    healthSlider.SetHealth(health);
    StartCoroutine(cameraMouse.Shake(0.2f, 0.3f));

    if (health <= 0) {
      health = 0;
      GameManager.instance.GameDone(false);
      // You Lost
      Destroy(gameObject);
    }

    healthText.text = health.ToString();
  }

  // Update is called once per frame
  void Update() {
    isGrounded =
        Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    if (isGrounded && velocity.y < 0) {
      velocity.y = -2f;
    }
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Vector3 moment = transform.right * x + transform.forward * z;
    if (moment.magnitude > 0.1) {
      footSteps.volume = 0.5f;
    } else
      footSteps.volume = 0f;

    controller.Move(moment * speed * Time.deltaTime);

    if (Input.GetButtonDown("Jump") && isGrounded) {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    velocity.y += gravity * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);

    if(transform.position.y <= -40){
      transform.position = new Vector3(45, 0, -5);
    }
  }

  void OnCollisionEnter(Collision other) {
    if (other.transform.CompareTag("Enemy")) {
      damage(10);
    }
  }

  void OnTriggerEnter(Collider other) {
    if (other.transform.CompareTag("Portal")) {
      AudioManager.instance.Play("whoosh");
    }
    if (other.transform.CompareTag("Finish")) {
      GameManager.instance.GameDone(true);
    }
  }
}

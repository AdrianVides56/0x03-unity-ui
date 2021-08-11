using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f;
    public int health = 5;
    private int score = 0;
    public Text scoreText;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, 0, zMove) * speed * Time.deltaTime;
    }

    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            speed = 0f;
            Thread.Sleep(1500);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Pickup")
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.GetComponent<Collider>().tag == "Trap")
        {
            health--;
            Debug.Log($"Health: {health}");
        }
        if (other.GetComponent<Collider>().tag == "Goal")
        {
            Debug.Log("You win!");
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
}

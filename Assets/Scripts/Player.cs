using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;

    int hp = 10;

    [SerializeField] GameObject hpBar;

    int score;

    float scoreTime;

    [SerializeField] Text scoreText;

    GameObject currentFloor;

    [SerializeField] FloorManager floorManager;

    [SerializeField] GameObject startButton;

    [SerializeField] GameObject restartButton;

    private void Start()
    {
        init();

        gameObject.SetActive(false);
    }

    void init()
    {
        floorManager.InitFloor();

        transform.position = new Vector3(0f, -3f, 0f);

        hp = 10;
        score = 0;
        scoreTime = 0;
    }

    public void startGame()
    {
        gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;

        init();

        startButton.SetActive(false);

        Time.timeScale = 1;
    }

    public void restartGame()
    {
        init();

        restartButton.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            restartGame();
        }
        else
        {
            GetComponent<Animator>().SetBool("run", false);
        }

        updateHpBar();

        updateScore();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Normal")
        {
            Debug.Log("Normal");
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = other.gameObject;
                modifyHP(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "Nails")
        {
            Debug.Log("Nails");
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = other.gameObject;
                modifyHP(-3);
                GetComponent<Animator>().SetTrigger("hurt");
                if (hp > 0) other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            modifyHP(-3);
            GetComponent<Animator>().SetTrigger("hurt");
            if (hp > 0) other.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            Debug.Log("DeathLine");
            modifyHP(-10);
            GetComponent<AudioSource>().Play();
        }
    }

    private void modifyHP(int modify)
    {
        hp += modify;
        if (hp <= 0)
        {
            hp = 0;
            GetComponent<AudioSource>().Play();
            restartButton.SetActive(true);

            Time.timeScale = 0;
        }
        else if (hp > 10)
        {
            hp = 10;
        }
    }

    private void updateHpBar()
    {
        for (int i = 0; i < hpBar.transform.childCount; i++)
        {
            if (hp > i)
            {
                hpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                hpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void updateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime >= 1f)
        {
            score++;
            scoreTime = 0;
            scoreText.text = "地下" + (score < 100 ? score < 10 ? "00" : "0" : "") + score.ToString() + "階";
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KulibinSpace.MessageBus;

public class Player : MonoBehaviour {
    public GameMessage stopGameMessage;
    [SerializeField] private GameObject _player;
    [SerializeField] public float _speed;
    [SerializeField] public PlayerHealth PlayerHealth;
    public float _maxYCoordinate;
    public float _minYCoordinate;
    public Destroyer Destroyer;
    public TextMeshProUGUI AmmoView;

    public TextMeshProUGUI ResultScore;
    public TextMeshProUGUI BestResult;
    public TextMeshProUGUI ScoreView;
    public HealthUI HealthUI;
    public Score Score;
    public GameObject Buttons;
    public Image HPImage;
    public Image AmmoImage;
    public GameObject Game;
    public AudioSource HitSound;
    public int AmmoValue;
    public int MyProperty { get { return AmmoValue; } set { AmmoValue = value; } }

    void Start () {
        PlayerHealth = gameObject.GetComponent<PlayerHealth>();
        AmmoView.text = AmmoValue.ToString();
        //transform.localPosition = new Vector3(-6f, -1.5f, 0);
    }

    void Update () {
        PlayerMovePC();
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _minYCoordinate, _maxYCoordinate));
    }

    public void PlayerMovePC () {
        transform.Translate(0, Input.GetAxis("Vertical") * _speed * Time.deltaTime, 0);
    }

    public void PlayerMoveUp () {
        transform.Translate(0, _speed * Time.deltaTime, 0);
    }

    public void MoveDown () {
        transform.Translate(0, -_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            HitSound.Play();
            PlayerHealth.TakeDamage(1);
            HealthUI.HealthView.text = PlayerHealth.Health.ToString();
        }
    }

    // вызывается из анимации гибели игрока
    public void PlayerDieNow () {
        stopGameMessage?.Invoke();
        Score.CheckBestResult(Score.ScoreValue);
        //ResultScore.text = "Score: " + Score.ScoreValue;
        ResultScore.text = string.Format(LanguageManager.Get("score"), Score.ScoreValue);
        //BestResult.text = "Best Result: " + PlayerPrefs.GetInt("Best").ToString();
        BestResult.text = string.Format(LanguageManager.Get("highscore"), GamePrefs.best);
        ResultScore.enabled = true;
        //Buttons.SetActive(true); // теперь включение кнопки РЕСТАРТ через объект Реклама YG
        ScoreView.enabled = false;
        Game.SetActive(false);
        gameObject.SetActive(false);
    }
}

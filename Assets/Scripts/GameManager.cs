using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject balloonPrefab; // Prefab del globo
    public Transform balloonParent; // Lugar donde se generan los globos
    public Transform dropZone; // Zona donde se ordenan las etiquetas
    
    public List<string> currentTags = new List<string> { "<a>", "</a>", "<p>", "</p>" };
    private List<string> playerOrder = new List<string>();

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private float timeLeft = 60f;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnBalloons();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    void SpawnBalloons()
    {
        foreach (string tag in currentTags)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent);
            newBalloon.GetComponent<Balloon>().htmlTag = tag;
        }
    }

    public void CatchBalloon(string tag)
    {
        playerOrder.Add(tag);

        if (playerOrder.Count == currentTags.Count)
        {
            CheckOrder();
        }
    }

    void CheckOrder()
    {
        if (playerOrder.SequenceEqual(currentTags))
        {
            score++;
            scoreText.text = "Puntos: " + score;
            NextLevel();
        }
        else
        {
            playerOrder.Clear(); // Resetear si el orden está mal
        }
    }

    void NextLevel()
    {
        currentTags = new List<string> { "<div>", "</div>", "<h1>", "</h1>" }; // Nueva ronda
        playerOrder.Clear();
        SpawnBalloons();
    }

    void GameOver()
    {
        Debug.Log("Fin del juego. Puntuación: " + score);
    }
}

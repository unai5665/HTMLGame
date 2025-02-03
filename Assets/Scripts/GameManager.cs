using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject balloonPrefab; // Prefab del globo
    public Transform balloonParent;  // Lugar donde se generan los globos
    public Transform dropZone; // Zona donde se ordenan las etiquetas

    public List<string> currentTags = new List<string> { "<a>", "</a>", "<p>", "</p>" };
    private List<string> playerOrder = new List<string>();

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TagContainer tagContainer;

    private float timeLeft = 60f;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnBalloons();  // Solo se llama aquí
    }

    void Update()
{
    // Solo actualizamos el contador si el tiempo no ha llegado a 0
    if (timeLeft > 0)
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft); // Actualiza el texto
    }
    else
    {
        timerText.text = "Tiempo: 0"; // Asegura que el contador se quede en 0 cuando termine
    }

    // Verifica si el tiempo ha llegado a 0 para finalizar el juego
    if (timeLeft <= 0)
    {
        GameOver();
    }
}

    // Función para crear los globos
    void SpawnBalloons()
    {
        // Asegurarse de que no se generen globos extras
        foreach (Transform child in balloonParent)  // Limpiar el BalloonParent antes de generar nuevos globos
        {
            Destroy(child.gameObject);
        }

        foreach (string tag in currentTags)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent); // Crear el globo bajo el BalloonParent
            newBalloon.GetComponent<Balloon>().htmlTag = tag; // Asignar la etiqueta al globo

            newBalloon.transform.position = new Vector3(Random.Range(-5f, 3f), Random.Range( -3f, -6f), -5.7f); // Posición aleatoria
        }
    }

    // Esta función se llama cuando el jugador captura un globo
    public void CatchBalloon(string tag)
    {
        playerOrder.Add(tag);

        if (playerOrder.Count == currentTags.Count)
        {
            CheckOrder();
        }
    }

    // Verifica si el orden de las etiquetas es correcto
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

    // Avanza al siguiente nivel
    public void NextLevel()
    {
        currentTags = new List<string> { "<div>", "</div>", "<h1>", "</h1>" }; // Nueva ronda
        playerOrder.Clear();
        SpawnBalloons();  // Nuevos globos para la siguiente ronda
    }

    // Fin del juego
    void GameOver()
    {
        Debug.Log("Fin del juego. Puntuación: " + score);
    }
}

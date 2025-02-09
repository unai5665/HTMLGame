using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TagContainer tagContainer;

    public AudioSource backgroundMusic;
    public AudioSource ClicBalloon; 
    public AudioSource levelUp; 




    // Prefabs y referencias a objetos en la escena
    public GameObject balloonPrefab; // Prefab del globo
    public Transform balloonParent;  // Lugar donde se generan los globos
    public Transform dropZone; // Zona donde se ordenan las etiquetas

    // Variables del juego
    public List<string> currentTags = new List<string>();
    private List<string> playerOrder = new List<string>();

    private List<List<string>> levels = new List<List<string>>(){
        new List<string> { "<a>", "<p>", "</p>", "</a>" },         // Nivel 1
        new List<string> { "<div>", "<b>", "</b>", "</div>" },     // Nivel 2
        new List<string> { "<table>", "<tr>", "</tr>", "</table>" },// Nivel 3
        new List<string> { "<h1>", "<p>", "</p>", "</h1>" },// Nivel 4
        new List<string> { "<ul>", "<li>", "</li>", "</ul>" },// Nivel 5
        new List<string> { "<header>", "<nav>", "</nav>", "</header>" } // Nivel 6


    };




    private float timeLeft = 60f;
    private int score = 0;

    private int currentLevel = 0;
    

    void Awake()
    {
        Instance = this;
    }

    // Se llama cuando el jugador hace clic en "Iniciar Juego"
    public void StartGame()
    {  
        StopAllCoroutines();

        timeLeft = 60f;

        score = 0;

        currentLevel = 0;

        scoreText.text = " " + score;

        playerOrder.Clear();

        currentTags = levels[currentLevel];

        tagContainer.UpdateExpectedOrder(currentTags);

        tagContainer.ClearSlots();

        tagContainer.InitializeSlots();

        CleanDraggableTags();

        SpawnBalloons();  // Solo se llama aquí

        StartTimer();  // Comienza el contador de tiempo

        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

    }

    // Aquí es donde el temporizador empieza a descontar
    void StartTimer()
    {
        // Inicia el tiempo solo cuando realmente se empieza el juego
        StartCoroutine(TimerCountdown());
    }

    IEnumerator TimerCountdown()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft); // Actualiza el texto

            yield return null; // Espera un frame
        }

        timerText.text = "Tiempo: 0"; // Asegura que el contador se quede en 0 cuando termine

        GameOver(); // Llama al GameOver cuando termine el tiempo
    }

    // Función para generar los globos
    void SpawnBalloons()
    {
        // Limpiar globos anteriores antes de crear nuevos

        foreach (Transform child in balloonParent)
        {
            Destroy(child.gameObject);
        }

        // Generar nuevos globos
        foreach (string tag in currentTags)
        {
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent);

            newBalloon.GetComponent<Balloon>().htmlTag = tag;

            newBalloon.transform.position = new Vector3(Random.Range(-2.5f, 2f), Random.Range(-3f, -6f), -5f);

            newBalloon.transform.localScale = Vector3.one; // Asegúrate de que la escala sea normal

        }
    }

    // Función para que el jugador capture un globo
    public void CatchBalloon(string tag)
    {
        playerOrder.Add(tag);
    }


    // Avanza al siguiente nivel
    public void NextLevel()
{
    if (currentLevel < levels.Count - 1)
    {
        currentLevel++;
        score++;
        scoreText.text = " " + score;
        currentTags = levels[currentLevel];

        // Verificar que currentTags tiene el orden correcto
        Debug.Log("Nivel " + currentLevel + " - Orden de etiquetas actual: " + string.Join(", ", currentTags));

        if (tagContainer != null)
        {
            // Asegurarse de que se llama a UpdateExpectedOrder correctamente
            Debug.Log("Llamando a UpdateExpectedOrder con el nuevo orden.");
            tagContainer.UpdateExpectedOrder(currentTags);  // Aquí se llama a la actualización
        }

        if (levelUp != null)
        {
            levelUp.Play();  // Reproduce el clip asignado en sfxSource.clip
        }


        Debug.Log("Avanzando al siguiente nivel.");
        playerOrder.Clear();
        SpawnBalloons();
    }
    else
    {
        score++;
        Debug.Log("No hay más niveles. Fin del juego.");
        GameOver();

    }
}



    // Fin del juego, se muestra la pantalla de Game Over
    void GameOver()
    {
        

        UIManager.Instance.ShowGameOverScreen(score); // Mostrar pantalla de Game Over

        // Asegurar que la pantalla de Game Over está activa antes de limpiar los tags
        if (UIManager.Instance.gameOverScreen.activeSelf)
        {
            CleanDraggableTags();
        }

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

    }


    // Reinicia el juego (se puede usar tanto al inicio como al volver al inicio)
    public void ResetGame()
{
    // Detener todas las corrutinas activas
    StopAllCoroutines();

    // Destruir todos los globos existentes
    foreach (Transform child in balloonParent)
    {
        Destroy(child.gameObject);
    }

    // Reiniciar variables del juego
    timeLeft = 60f;
    score = 0;
    currentLevel = 0;
    scoreText.text = " " + score;
    timerText.text = "Tiempo: 60";
    playerOrder.Clear();

    // Reiniciar la información de etiquetas
    currentTags = levels[currentLevel];
    tagContainer.UpdateExpectedOrder(currentTags);
    tagContainer.ClearSlots();
    tagContainer.InitializeSlots();

    // Limpiar etiquetas arrastrables
    CleanDraggableTags();

    // Detener la música de fondo si está activa
    if (backgroundMusic != null)
    {
        backgroundMusic.Stop();
    }
}



// Función para limpiar todos los DraggableTags en el contenedor
    void CleanDraggableTags()
    {
        if (tagContainer != null)
        {
            foreach (Transform child in tagContainer.tagContainerPanel)
            {
                Destroy(child.gameObject); // Eliminar todos los DraggableTags previos
            }
        }
    }
}
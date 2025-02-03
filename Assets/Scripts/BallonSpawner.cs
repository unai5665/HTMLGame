using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;  // Prefab del globo
    public Transform spawnPoint;      // Punto donde los globos aparecerán
    public float spawnInterval = 2f;  // Intervalo de tiempo entre cada aparición de globo
    public float spawnHeight = -3f;    // Altura en la que aparecerán los globos
    public int maxBalloons = 4;       // Máximo número de globos en la pantalla
    private int currentBalloonCount = 0;
    private List<GameObject> activeBalloons = new List<GameObject>();

    void Start()
    {
        // Comienza a generar globos al iniciar el juego
        StartCoroutine(SpawnBalloons());
    }

    // Coroutine para generar los globos en intervalos regulares
    IEnumerator SpawnBalloons()
    {
        while (true) // Bucle infinito para mantener el spawn activo
        {
            // Revisa cuántos globos hay en pantalla
            if (activeBalloons.Count < maxBalloons)
            {
                SpawnBalloon();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Función que genera un globo
    void SpawnBalloon()
    {
        // Determina una posición aleatoria en el eje X y Z para el globo
        float randomX = Random.Range(-0.5f, 1.5f); // Puedes ajustar estos valores
        float fixedZ = -6f;

        // Crea una nueva instancia del globo en la posición determinada
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, fixedZ);

        
        GameObject newBalloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity,GameManager.Instance.balloonParent);
        activeBalloons.Add(newBalloon);
    }

     public void RemoveBalloon(GameObject balloon)
    {
        activeBalloons.Remove(balloon);
        Destroy(balloon);
    }
}

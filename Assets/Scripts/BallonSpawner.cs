using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;  // Prefab del globo
    public Transform spawnPoint;      // Punto donde los globos aparecerán
    public float spawnInterval = 2f;  // Intervalo de tiempo entre cada aparición de globo
    public float spawnHeight = -3f;    // Altura en la que aparecerán los globos

    void Start()
    {
        // Comienza a generar globos al iniciar el juego
        StartCoroutine(SpawnBalloons());
    }

    // Coroutine para generar los globos en intervalos regulares
    IEnumerator SpawnBalloons()
    {
        while (true)
        {
            SpawnBalloon();  // Llama a la función que genera un globo

            // Espera el tiempo especificado antes de generar el siguiente globo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Función que genera un globo
    void SpawnBalloon()
    {
        // Determina una posición aleatoria en el eje X y Z para el globo
        float randomX = Random.Range(-0.5f, 1.5f); // Puedes ajustar estos valores
        float fixedZ = 0f;
        

        // Crea una nueva instancia del globo en la posición determinada
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, fixedZ);

        // Instancia el globo
        Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
    }
}

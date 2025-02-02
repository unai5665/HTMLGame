using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    public string htmlTag; // La etiqueta HTML que contiene este globo
    public TMP_Text textMeshPro;

    private float speed = 0.5f; // Velocidad de subida
    private BalloonSpawner spawner;
    

     void Start()
    {   
        spawner = FindObjectOfType<BalloonSpawner>();
        textMeshPro = GetComponentInChildren<TMP_Text>();

        if (textMeshPro != null)
        {
            textMeshPro.text = htmlTag; // Asigna la etiqueta al texto del globo
        }
        else
        {
            Debug.LogError("TextMeshPro no encontrado en el prefab HTMLTag.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

    
         // Reaparece si se sale de la pantalla
        if (transform.position.y > 5f)
        {
            Respawn();
        }
    }

    // Detecta clic en el globo
    private void OnMouseDown()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CatchBalloon(htmlTag);
            spawner.RemoveBalloon(gameObject); // Elimina el globo correctamente
        }
    }

    void Respawn()
    {
        transform.position = new Vector3(Random.Range(-2f, 2f), -3f, 0);
    }
}
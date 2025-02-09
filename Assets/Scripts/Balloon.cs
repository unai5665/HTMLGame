using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    public string htmlTag; // La etiqueta HTML que contiene este globo
    public TMP_Text textMeshPro;

    private float speed = 1f; // Velocidad de subida

    public AudioClip popSound; // Sonido de explosión
    private AudioSource audioSource;

void Start()
{
    textMeshPro = GetComponentInChildren<TMP_Text>();
    if (textMeshPro != null)
    {
        // Desactiva la interpretación de rich text para que se muestren los caracteres "<" y ">" literalmente.
        textMeshPro.richText = false;
        if (!string.IsNullOrEmpty(htmlTag))
        {
            textMeshPro.text = htmlTag;
        }
    }
    audioSource = gameObject.AddComponent<AudioSource>();
}





    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);  // El globo sube

        // Reaparece si se sale de la pantalla
        if (transform.position.y > 5f)
        {
            Respawn();
        }
    }

   

    void Respawn()
    {
        // Cuando el globo sale de la pantalla, regresa a la parte inferior con una posición aleatoria en el eje X
        transform.position = new Vector3(Random.Range(-2.5f, 2f), Random.Range( -3f, -6f), -5f);
    }

    // Detecta clic en el globo
    private void OnMouseDown()
    {
        if (GameManager.Instance != null)
        {

            GameManager.Instance.CatchBalloon(htmlTag);

           if (GameManager.Instance.ClicBalloon != null && popSound != null)
            {
                GameManager.Instance.ClicBalloon.PlayOneShot(popSound, 2.0f); // Volumen más alto
            }

            Destroy(gameObject); // Espera a que el sonido termine antes de destruir
    
            

            TagContainer tagContainer = FindObjectOfType<TagContainer>();
            if (tagContainer != null)
            {
                tagContainer.AddTagToUI(htmlTag);
            }
        }
    }
}


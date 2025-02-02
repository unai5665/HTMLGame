using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    public string htmlTag; // La etiqueta HTML que contiene este globo
    public TMP_Text textMeshPro;


    private float speed = 2f; // Velocidad de subida

     void Start()
    {
        // Buscar el TextMeshPro dentro del objeto hijo
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

        // Si el globo se sale de la pantalla, reaparece abajo
        if (transform.position.y > Screen.height)
        {
            ResetPosition();
        }
    }

    public void OnClick()
    {
        GameManager.Instance.CatchBalloon(htmlTag); // Enviar la etiqueta al rectángulo
        Destroy(gameObject); // Explota el globo
    }

    void ResetPosition()
    {
        float randomX = Random.Range(100, Screen.width - 100);
        transform.position = new Vector3(randomX, -50, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{

    public float ballForce = 1.0f;

    public float resetHeight = 0.6f;

    public Vector3 resetPosition;

    public Vector3 finishPosition;
    public float finishDistance = 0.25f;

    private bool isFinished;

    public GameObject finishText; 

    // Start is called before the first frame update
    void Start()
    {
        isFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            //El juego ha acabado. Esperamos a que se pulse una tecla.
            if (Input.anyKeyDown) SceneManager.LoadScene(0);
        }
        else
        {
            //Controlamos si hay que resetear
            ResetControl();

            //Movemos la bola
            MovementControl();

            //Comprobamos si hemos llegado a la meta
            FinishControl();
        }
    }

    void FinishControl()
    {
        //Comprobar si estamos cerca de la meta
        if( Vector3.Distance(transform.position, finishPosition)<finishDistance )
        {
            //Hemos llegado a la meta
            finishText.SetActive(true);
            isFinished = true;
        }
    }

    void ResetControl()
    {
        //Comprobar si la altura de la bola es menor que 0.6
        if(transform.position.y < resetHeight)
        {
            //Nos hemos caido. Volver a la posición inicial
            transform.position = resetPosition;

            //Reseteamos tambien la velocidad
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

    }

    void MovementControl()
    {
        //Vamos a empujar la bola en la dirección que pulse el usuario
        GetComponent<Rigidbody>().AddForce((transform.right * ballForce * Input.GetAxis("Horizontal")) + (transform.forward * ballForce * Input.GetAxis("Vertical")));
    }
}

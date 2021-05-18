using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    Camera cam;

    public float distance = 10.0f;

    public GameObject pointer;

    Vector3 pointerScale;

    //Chave
    public GameObject Key;

    public GameObject Door;

    //Time
    float currentGazeTime = 0;

    public float waitingTime = 3.0f;

    //Animações
    Animator anim;

    //Globus
    public GameObject Globus;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        pointerScale = pointer.transform.localScale;

        //Animações
        anim = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pointer.GetComponent<Image>().enabled = cam.enabled;

        Ray raio = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit alvo;

        Debug.DrawRay(raio.origin, raio.direction * distance, Color.red, 0);

        if (Physics.Raycast(raio, out alvo, distance))
        {
            print("I'm looking at " + alvo.transform.name);
            if (alvo.transform.gameObject.CompareTag("Interactable"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                currentGazeTime += Time.deltaTime;
                print("passou " + Mathf.Round(currentGazeTime) + "s");
                if (currentGazeTime > waitingTime)
                {
                    SceneManager.LoadScene(alvo.transform.gameObject.name);
                }
            }
            else //Relógio
            if (alvo.transform.gameObject.CompareTag("Clock"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);
            }
            else //Porta
            if (alvo.transform.gameObject.CompareTag("Door"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);

                anim.SetBool("doorOpen", true);
            }
            else //Chave
            if (alvo.transform.gameObject.CompareTag("Key"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);

                Key.SetActive(false);
            }

            //Globo
            if (alvo.transform.gameObject.CompareTag("Globus"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);
                Globus.transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            }
            else
            {
                pointer.transform.localScale = pointerScale;
            }
        }
        else
        {
            print("I'm looking at nothing!");

            currentGazeTime = 0;
        }
    }
}

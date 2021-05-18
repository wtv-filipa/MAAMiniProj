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

    //Cookies
    public GameObject Cookie1;

    public GameObject Cookie2;

    public GameObject Cookie3;

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
            } //Relógio
            else if (alvo.transform.gameObject.CompareTag("Clock"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);
            } //Porta
            else if (alvo.transform.gameObject.CompareTag("Door"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);

                anim.SetBool("doorOpen", true);
            } //Chave
            else if (alvo.transform.gameObject.CompareTag("Key"))
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
                Globus
                    .transform
                    .Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
            }

            //Cookies
            if (alvo.transform.gameObject.CompareTag("EatCookie"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                if (Cookie1)
                {
                    Cookie1.SetActive(false);
                }
                if (Cookie2)
                {
                    Cookie2.SetActive(false);
                }
                if (Cookie3)
                {
                    Cookie3.SetActive(false);
                }
                print("I'm looking at " + alvo.transform.name);
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

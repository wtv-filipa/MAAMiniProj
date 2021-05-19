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
    public GameObject HatterId;

    //Time
    float currentGazeTime = 0;

    public float waitingTime = 3.0f;

    float KeyWaiting = 1.0f;

    //Animações
    Animator anim;
    Animator keyAnim;
    Animator hatterAnim;

    //Globus
    public GameObject Globus;

    //Cookies
    public GameObject Cookie1;

    //Fumo Bule
    public GameObject SmokeTeaPot;

    //Skybox
    public Material Dia;
    public Material Anoitecer;
    public GameObject DayLight;

    //Sons
    public AudioSource PortaRanger;
    public AudioSource AssobioBule;

    // Start is called before the first frame update
    void Start()
    {
        //RenderSettings.skybox = Dia;

        cam = this.GetComponent<Camera>();
        pointerScale = pointer.transform.localScale;

        //Animações
        anim = Door.GetComponent<Animator>();
        keyAnim = Key.GetComponent<Animator>();
        hatterAnim = HatterId.GetComponent<Animator>();
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
            }

            //Chave
            if (alvo.transform.gameObject.CompareTag("Key"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);

                keyAnim.SetBool("PlayAnim", true);
                currentGazeTime += Time.deltaTime;
                if (currentGazeTime > KeyWaiting)
                {
                    Key.SetActive(false);
                    anim.SetBool("doorOpen", true);
                    PortaRanger.Play();
                }
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
                print("I'm looking at " + alvo.transform.name);

                Cookie1.SetActive(false);
            }
            else
            {
                pointer.transform.localScale = pointerScale;
            }

            //Teapot
            if (alvo.transform.gameObject.CompareTag("Teapot"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);
                print("I'm looking at " + alvo.transform.name);

                AssobioBule.Play();
            }
            else
            {
                pointer.transform.localScale = pointerScale;
                AssobioBule.Stop();
            }

            //HatterId
            if (alvo.transform.gameObject.CompareTag("HatterId"))
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);
                print("I'm looking at " + alvo.transform.name);

                hatterAnim.SetBool("CloseUp", true);
                anim.SetBool("doorOpen", false);
                Key.SetActive(true);

                RenderSettings.skybox = Anoitecer;

                Destroy(DayLight);
            }
            else
            {
                pointer.transform.localScale = pointerScale;
                hatterAnim.SetBool("CloseUp", false);

            }
        }
        else
        {
            print("I'm looking at nothing!");

            currentGazeTime = 0;
        }
    }
}

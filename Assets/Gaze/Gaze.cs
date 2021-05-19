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

    //Variáveis
    public GameObject Key;

    public GameObject Key2;

    public GameObject Door;

    public GameObject HatterId;

    public GameObject FadeScreen;

    public GameObject PlacaFim;

    public GameObject FumoChaleira;

    public GameObject GoBackLobby;

    //Time
    float currentGazeTime = 0;

    public float waitingTime = 3.0f;

    float KeyWaiting = 1.0f;

    //Animações
    Animator anim;

    Animator keyAnim;

    Animator hatterAnim;

    Animator fadeAnim;

    //Animaçoes de brilhos
    public GameObject ParticulaChave;

    public GameObject LuzChave;

    //Globus
    public GameObject Globus;

    //Cookies
    public GameObject Cookie1;

    //Skybox
    public Material Dia;

    public Material Anoitecer;

    public GameObject DayLight;

    public GameObject Festa;

    public GameObject Personagens;

    //Sons
    public AudioSource PortaRanger;

    public AudioSource AssobioBule;

    public AudioSource ComerCookie;

    public AudioSource Sussurro;

    public AudioSource SomAmbiente;

    public AudioSource Vento;

    float som = 0.021f;

    bool hasPlayedDoor = false;

    public float speed = 2;

    public Transform target;

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
        fadeAnim = FadeScreen.GetComponent<Animator>();
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
            if (
                alvo.transform.gameObject.CompareTag("Interactable") ||
                alvo.transform.gameObject.CompareTag("DrinkMe") ||
                alvo.transform.gameObject.CompareTag("Clock") ||
                alvo.transform.gameObject.CompareTag("Key") ||
                alvo.transform.gameObject.CompareTag("Key2") ||
                alvo.transform.gameObject.CompareTag("Globus") ||
                alvo.transform.gameObject.CompareTag("EatCookie") ||
                alvo.transform.gameObject.CompareTag("Teapot") ||
                alvo.transform.gameObject.CompareTag("HatterId")

                )
            {
                pointer.transform.localScale =
                    Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                currentGazeTime += Time.deltaTime;

                //CENA INICIAL
                if (
                    currentGazeTime > waitingTime &&
                    alvo.transform.gameObject.CompareTag("DrinkMe")
                )
                {
                    currentGazeTime = 0 + Time.deltaTime;
                    //fadeAnim.SetBool("FadeOut", false);
                    SceneManager.LoadScene(alvo.transform.gameObject.name);
                }

                //PORTAL
                if (
                    currentGazeTime > waitingTime &&
                    alvo.transform.gameObject.CompareTag("Interactable")
                )
                {
                    currentGazeTime = 0 + Time.deltaTime;
                    SceneManager.LoadScene(alvo.transform.gameObject.name);
                }

                //CLOCK
                if (alvo.transform.gameObject.CompareTag("Clock"))
                {
                    currentGazeTime = 0 + Time.deltaTime;
                }

                //KEY
                if (alvo.transform.gameObject.CompareTag("Key"))
                {
                    keyAnim.SetBool("PlayAnim", true);
                }
                if (
                    currentGazeTime > KeyWaiting &&
                    alvo.transform.gameObject.CompareTag("Key")
                )
                {
                    currentGazeTime = 0 + Time.deltaTime;
                    Key.SetActive(false);
                    ParticulaChave.SetActive(false);
                    anim.SetBool("doorOpen", true);
                    PortaRanger.Play();
                }

                //KEY2
                if (
                    currentGazeTime > KeyWaiting &&
                    alvo.transform.gameObject.CompareTag("Key2")
                )
                {
                    currentGazeTime = 0 + Time.deltaTime;
                    Key2.SetActive(false);
                    anim.SetBool("doorOpen", true);
                    PortaRanger.Play();
                }

                //GLOBUS
                if (alvo.transform.gameObject.CompareTag("Globus"))
                {
                    Globus
                        .transform
                        .Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
                }

                //COOKIES
                if (alvo.transform.gameObject.CompareTag("EatCookie"))
                {
                    Cookie1.SetActive(false);
                    ComerCookie.Play();
                }

                //TEAPOT
                if (
                    currentGazeTime > waitingTime &&
                    alvo.transform.gameObject.CompareTag("Teapot")
                )
                {
                    currentGazeTime = 0 + Time.deltaTime;

                    FumoChaleira.SetActive(true);
                    AssobioBule.Play();
                }

                //HATERID
                if (alvo.transform.gameObject.CompareTag("HatterId"))
                {
                    if (!hasPlayedDoor)
                    {
                        Sussurro.Play();
                        hasPlayedDoor = true;
                    }

                    anim.SetBool("doorOpen", false);
                    Key2.SetActive(true);

                    PlacaFim.SetActive(true);
                    GoBackLobby.SetActive(true);
                    RenderSettings.skybox = Anoitecer;
                    Destroy(DayLight);
                    Destroy(Festa);
                    Destroy(Personagens);

                    //Som ambiente mais baixo ao olhar para a carta
                    SomAmbiente.volume = som;
                    Vento.volume = som;
                }

                if (alvo.transform.gameObject.CompareTag("HatterId")){

                    hatterAnim.SetBool("CloseUp", true);
                }

                if(!alvo.transform.gameObject.CompareTag("HatterId"))
                {
                    hatterAnim.SetBool("IntoPos", true);
                    hatterAnim.SetBool("CloseUp", false);
                }
            }
        }
        else
        {
            print("I'm looking at nothing!");
            pointer.transform.localScale = pointerScale;
            currentGazeTime = 0;
        }
    }
}

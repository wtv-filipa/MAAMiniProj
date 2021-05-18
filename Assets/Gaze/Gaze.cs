using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gaze : MonoBehaviour
{
    Camera cam;
    public float distance = 10.0f; 
    public GameObject pointer;
    Vector3 pointerScale;

    //Time
    float currentGazeTime = 0;
    public float waitingTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        pointerScale = pointer.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        pointer.GetComponent<Image>().enabled = cam.enabled;

        Ray raio = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit alvo;

        Debug.DrawRay(raio.origin, raio.direction * distance, Color.red, 0);

        if (Physics.Raycast(raio, out alvo, distance)) { 
            print("I'm looking at " + alvo.transform.name);
            if (alvo.transform.gameObject.CompareTag("Interactable")) {
                pointer.transform.localScale = Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                currentGazeTime += Time.deltaTime;
                print("passou " + Mathf.Round(currentGazeTime) + "s"); if (currentGazeTime > waitingTime)
                { SceneManager.LoadScene(alvo.transform.gameObject.name); }
            }
            else if (alvo.transform.gameObject.CompareTag("Clock"))
            {
                pointer.transform.localScale = Vector3.Lerp(pointerScale, pointerScale * 2, 1);

                print("I'm looking at " + alvo.transform.name);
               
            }

            else
            {
                pointer.transform.localScale = pointerScale;
            }
        }
        else { 
            print("I'm looking at nothing!");

            currentGazeTime = 0;
        }
    }
}

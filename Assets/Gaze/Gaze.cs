using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    Camera cam;
    public float distance = 10.0f; 
    public GameObject pointer;
    Vector3 pointerScale;

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
            }
            else
            {
                pointer.transform.localScale = pointerScale;
            }
        }
        else { 
            print("I'm looking at nothing!");
        }
    }
}

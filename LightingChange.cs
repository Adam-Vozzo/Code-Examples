using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingChange : MonoBehaviour {

    public GameObject LightGreen;
    public GameObject LightYellow;
    public GameObject LightBlue;

    public GameObject areaTrigger1; //tag Trigger1
    public GameObject areaTrigger2; //tag Trigger2

    public Color color1;
    public Color color2;
    public Color color3;

    Color yellowFog = new Color(0.204f, 0.204f, 0.0f, 0.1f);
    Color blueFog = new Color(0.0f, 0.204f, 0.204f, 0.1f);

    public Camera cam;

    // Use this for initialization
    void Start () {
        //cam = GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {


    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("LightingChangeTrigger");

        if (col.gameObject.name == "Player")
        {
            //swaplights
            LightGreen.SetActive(false);
            LightBlue.SetActive(false);
            LightYellow.SetActive(true);

            //SWAP FOG COLOUR
            //RenderSettings.fogColor = yellowFog;

            //swapcameracolour
            //cam.backgroundColor = Color.Lerp(color1, color2, Mathf.Lerp(0, 1, 1)); //TESTING WITHOUT BACKGROUND COLOUR CLEAR CALLS AND FOG
            //HAVE TO SEE HOW THIS INTERACTS WITH VR CAMERAS, CURRENTLY USING CENTRE EYE CAM //NOT USING THIS CURRENTLY (looks like background is still changing though)
        }
    }
}

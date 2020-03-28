using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assign a random texture from an array of textures when a critter spawns
public class RandomCatTexture : MonoBehaviour
{
    public Texture[] KittenTex;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.mainTexture = (KittenTex[Random.Range(0, 5)]);
    }
}

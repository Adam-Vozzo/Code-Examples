using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Interaction system for Leap Motion hologram particle system and audio [Student Project - Adam Vozzo]
public class FireInteraction : MonoBehaviour
{
    public ParticleSystem fire;
    public float modifier;
    public GameObject user;
    public GameObject user2;

    public Light fireLight;
    public GameObject fire_Light;

    //audio
    public AudioSource fireSFX;
    public AudioSource music;
    public AudioSource interactionSFX;
    public AudioClip interactionSFXClip;

    //for lerping modifier
    public float min = 0f;
    public float max = 50f;
    public float t = 0.0f;

    //for lerping velocity
    public float vmin = 0f;
    public float vmax = 0.5f;
    static float vt = 0.0f;

    static float lightLerp = 0.0f;

    public bool alreadyPlayed = false;

    void Start()
    {
        //For testing purposes, disable for final
        //fire_Light.gameObject.SetActive(false);
    }

    void Update()
    {
        var sizeOverLifetime = fire.sizeOverLifetime;
        var main = fire.main;
        var trails = fire.trails;
        var velocity = fire.velocityOverLifetime;

        //Light Intensity
         float lightMin = 0F;
         float lightMax = 5.0F;
        
        

        //modifier math, lerping at the rate of t
        modifier = Mathf.Lerp(min, max, t);

        //if gameobject hand is active, increase modifier, clamped to a max value
        //Particle system adjustments controlled by 'modifier' - size, amount, colour, music and sfx volume, y velocity over lifetime, ribbon trails.
        if (user.activeInHierarchy || user2.activeInHierarchy || Input.GetButton("Jump"))
        {
            //Turn on light over logs
            fire_Light.gameObject.SetActive(true);
            //increase Intensity over time
            lightLerp += 0.4f * Time.deltaTime;
            //change intensity, brightness
            fireLight.intensity = Mathf.Lerp(lightMin, lightMax, lightLerp);

            //Play audio once when a user is detected
            if (!alreadyPlayed)
            {
                interactionSFX.PlayOneShot(interactionSFXClip, 0.7F); 
                alreadyPlayed = true;
            }

            //Variable controlling the rate of change of the modifier, essentially sensitivity
            t += 0.03f * Time.deltaTime;

            //velocity lerp modifier
            vt += 0.06f * Time.deltaTime;

            //trails at a threshold
            if (modifier > 50)
            {
                trails.enabled = true;
                trails.ribbonCount = 1;
                modifier = 50;

            } else
            {
                trails.enabled = false;
            }

            //Adjust fire colour based on modifier rate. May not be used depending on effectiveness in user research.
            /*
            if (t > 0.3)
            {
                ChangeFireColourBlue();
                if (t > 0.5)
                {
                    ChangeFireColourGreen();
                }
            } else
            {
                ChangeFireColourRed();
            }
            */


            //If users leave the leap motion detection range
        } else
        {
            //LightAdjustment
            fire_Light.gameObject.SetActive(false);

            lightLerp = 0.0f;



            t -= 0.1f * Time.deltaTime;
            if (t < 0.1f)
            {
                t = 0.1f;
            }

            //velocity lerp modifier
            vt -= 0.5f * Time.deltaTime;
            if (vt < 0.1f)
            {
                vt = 0.1f;
            }
            alreadyPlayed = false;
        }

        //Limit the modifier to prevent infinite growth of the particle system which could lag the system
        if (t > 50.0f)
        {
            t -= 0.1f * Time.deltaTime;
        }

        if (vt > 1f)
        {
            vt = 1f;
        }

        //particle adjustments
        sizeOverLifetime.xMultiplier = 0.1f + (modifier * 0.05f); //1
        main.maxParticles = (int)(modifier*5f); //100
        velocity.y = Mathf.Lerp(vmin, vmax, vt); //0.6


        //audio adjustments
        fireSFX.volume = (modifier * 0.1f);
        music.volume = (modifier * 0.1f);

        //Bugfixing logs
        Debug.Log("t=: "+ t + "modifier= : " + modifier + "vt=: " + vt);

    }

    //If fire colour adjustment is implemented, these methods are used
    public void ChangeFireColourBlue()
    {
        var col = fire.colorOverLifetime;

        Gradient gradBlue = new Gradient();
        gradBlue.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.blue, 0.2f), new GradientColorKey(Color.cyan, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
        col.color = gradBlue;
        fireLight.color = Color.Lerp(Color.red, Color.blue, 0.8f);

        Debug.Log("Fire Blue");
    }
    public void ChangeFireColourRed()
    {
        var col = fire.colorOverLifetime;

        Gradient gradRed = new Gradient();
        gradRed.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.red, 0.2f), new GradientColorKey(Color.yellow, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
        col.color = gradRed;

        Debug.Log("Fire Red");

    }
    public void ChangeFireColourGreen()
    {
        var col = fire.colorOverLifetime;

        Gradient gradGreen = new Gradient();
        gradGreen.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.green, 0.2f), new GradientColorKey(Color.yellow, 0.7f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
        col.color = gradGreen;
        fireLight.color = Color.Lerp(Color.blue, Color.green, 0.8f);

        Debug.Log("Fire Green");


    }
}

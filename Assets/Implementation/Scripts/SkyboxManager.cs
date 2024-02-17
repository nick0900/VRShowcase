using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] Material baseSkybox = null;
    [SerializeField] Material displaySkybox = null;
    public static SkyboxManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (baseSkybox == null)
        {
            baseSkybox = RenderSettings.skybox;
        }
        else
        {
            RenderSettings.skybox = baseSkybox;
        }

        if (displaySkybox == null)
        {
            displaySkybox = new Material(Shader.Find("Skybox/Panoramic"));
        }
    }

    private BallToSkyboxAction latestCaller = null;
    
    public bool HasSkybox(BallToSkyboxAction caller)
    {
        bool ret = latestCaller == caller;
        latestCaller = caller;
        return ret;
    }

    public void SetSkybox(BallToSkyboxAction caller, Texture skybox)
    {
        if (caller == latestCaller)
        {
            displaySkybox.mainTexture = skybox;
            RenderSettings.skybox = displaySkybox;
        }
    }

    public void ResetSkybox(BallToSkyboxAction caller)
    {
        if (caller == latestCaller)
        {
            RenderSettings.skybox = baseSkybox;
        }
        latestCaller = null;
    }
}

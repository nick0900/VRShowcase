using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallToSkyboxAction : MonoBehaviour
{
    [SerializeField] GameObject SkyboxEffectPrefab;

    private Transform imageBall;
    private Renderer imageRenderer;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        if (grabbable != null) 
        {
            grabbable.activated.AddListener(ToggleBallToSky);
        }
        imageBall = transform.GetChild(0);
        imageRenderer = imageBall.GetComponent<Renderer>();
    }

    bool active = false;

    public void ToggleBallToSky(ActivateEventArgs args)
    {
        if (!active)
        if (SkyboxManager.Instance.HasSkybox(this))
        {
            SkyboxManager.Instance.ResetSkybox(this);
        }
        else
        {
            active = true;
            StartCoroutine(EffectTiming());
        }
    }

    IEnumerator EffectTiming()
    {
        GameObject effectInstance = Instantiate(SkyboxEffectPrefab, null);
        effectInstance.transform.position = imageBall.transform.position;
        effectInstance.transform.rotation = Quaternion.identity;
        effectInstance.transform.localScale = imageBall.transform.localScale * this.transform.localScale.x;
        effectInstance.GetComponent<Renderer>().material = imageRenderer.material;
        
        yield return new WaitForSeconds(0.5f);

        Destroy(effectInstance);
        SkyboxManager.Instance.SetSkybox(this, imageRenderer.material.GetTexture("_EmissionMap"));
        active = false;
    }
}

using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private Camera sceneCamera;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {

            if(sceneCamera != null)
            {
                sceneCamera = Camera.main;
                sceneCamera.gameObject.SetActive(false);
            }

            Camera.main.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
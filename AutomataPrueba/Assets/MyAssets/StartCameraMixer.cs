using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMixer))]
public class StartCameraMixer : MonoBehaviour
{
    [SerializeField]
    Camera[] cameras;
    CameraMixer mixer;

    [SerializeField]
    MouseLook player_look;
    [SerializeField]
    CharacterMove player_move;

    [SerializeField]
    Camera player_camera;
    [SerializeField]
    Camera r_camara;
    bool first_time = true;
    int cur_camera = 0;



    // Start is called before the first frame update
    void Start()
    {
        mixer = GetComponent<CameraMixer>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeToChangeCamera();

        if (Input.GetKeyDown(KeyCode.R))
            returnCameraPlayer();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (first_time)
        {
            setPlayerStates(false);
            StartAnim();
        }
    }

    private bool TimeToChangeCamera()
    {
        if (cur_camera == 4 && player_camera.transform.position == cameras[4].transform.position)
            returnCameraPlayer();

        if (player_camera.transform.position == cameras[cur_camera].transform.position)
        {
            if (cur_camera >= 4)
                return false;

            cur_camera++;
            StartAnim();
            return true;
        }

        return false;
    }

    private void StartAnim() 
    {
        mixer.blendCamera(cameras[cur_camera], 5.0f, Interpolators.linear);
    }

    private void returnCameraPlayer()
    {
        mixer.blendCamera(r_camara, 5.0f, Interpolators.linear);

        setPlayerStates(true);
    }


    private void setPlayerStates(bool state)
    {
        first_time = false;
        
        player_look.enabled = state;
        player_move.enabled = state;
    }
}

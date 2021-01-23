using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEffects : MonoBehaviour
{
    public static CamEffects instance;

    private float shake_pow_basic = 0.05f;
    private float shake_dur_basic = 0.1f;

    private float fov_amount = 0.3f;
    private float fov_buildup = 0.3f;
    private float fov_hangtime = 0.4f;

    private float FOV_base;


    void Awake()
    {
        VerifySingleInstance();
        FOV_base = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Backspace)) CameraShake(shake_pow_basic, shake_dur_basic);
        //if (Input.GetKeyDown(KeyCode.Backspace)) CameraFOVDistort(fov_amount, fov_buildup, fov_hangtime);
    }


    void VerifySingleInstance()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    #region CoroutineStarterFunctions

    public void CameraShake(float amount, float duration)
    {
        StartCoroutine(CamShake(amount, duration));
    }

    public void CameraFOVDistort(float amount, float buildip_time, float hangtime)
    {
        StartCoroutine(CameFOVDistort(amount, buildip_time, hangtime));
    }

    public void CameraShake()
    {
        StartCoroutine(CamShake(shake_pow_basic, shake_dur_basic));
    }

    public void CameraFOVDistort()
    {
        StartCoroutine(CameFOVDistort(fov_amount, fov_buildup, fov_hangtime));
    }


    #endregion



    #region actual coroutines
    private IEnumerator CamShake(float amount, float duration)
    {
        //save the original position
        Vector3 original_position = new Vector3(0, 0, -10);

        //loop until set duration expires
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;

            //shake the camera about randomally
            transform.localPosition = new Vector3(original_position.x + Random.Range(-amount, amount), original_position.y + Random.Range(-amount, amount), original_position.z);

        }
        transform.localPosition = original_position;

    }

    private IEnumerator CameFOVDistort(float amount, float buildip_time, float hangtime)
    {
        //FOV_base
        yield return null;
        float init_time = Time.time;

        //zoom out by [amount]
        while (Time.time < init_time + buildip_time)
        {
            GetComponent<Camera>().orthographicSize += amount / 20;
            yield return new WaitForEndOfFrame();
        }

        //maintain the zoom-out for [hangtime]
        init_time = Time.time;
        while (Time.time < init_time + hangtime)
        {
            yield return new WaitForEndOfFrame();
        }

        //return to normal FoV
        yield return new WaitForEndOfFrame();
        GetComponent<Camera>().orthographicSize = FOV_base;

    }
    #endregion




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamEffects : MonoBehaviour
{
    public static CamEffects instance;
    private Animator anim;

    private float shake_pow_basic = 0.05f;
    private float shake_dur_basic = 0.1f;

    private float fov_amount = 0.3f;
    private float fov_buildup = 0.3f;
    private float fov_hangtime = 0.4f;

    private float FOV_base;

    
    public Text defeat_text;
    public Text win_text;

    void Awake()
    {
        VerifySingleInstance();
        FOV_base = GetComponent<Camera>().orthographicSize;
        anim = GetComponent<Animator>();
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


    public void Defeat()
    {
        StopAllCoroutines();
        Vector2 position = Player.instance.transform.position;
        Player.instance.transform.position=new Vector2(999, 999);
        transform.SetParent(null);
        transform.position = position;
        Debug.LogWarning("Game Over!");

        defeat_text.text = new string[]
        {
            "Well, that didn't go as planned.",
            "F",
            "[*]",
            "Try again.",
            "Your persistence is admirable. Unlike your ability.",
            "Try harder.",
            "Next time - try dodging.",
            //"Next time you have a chance to kill someone - don't hesitate",
            "Goodnight mr. Player",
            "No mr. Player, I expect you to die.",
            "Do you win as gracefully as you loose?",
            "Shocking, not at all shocking.",
            "I like the way you die, boy.",
            "Screaming won't help, but it is worth a try.",
            "You now have time to bleed.",
            "Muda! Muda! Muda! Muda! Muda! Muda! Muda! Muda! Muda! Muda! Muda! Muda... MUDA!",
            "Not quite there yet."

        }[Random.Range(0, 16)];
        anim.SetTrigger("Defeat");
        StartCoroutine(Utils.DelayedReturnToMenu(6));
    }

    public void Victory()
    {
        win_text.text = new string[]
        {
            "Victory!",
            "Look mom, I made it!",
            "A congratulations, it's a celebration!",
            "You win!",
            "Nice.",
            "Wicked!",
            "You get one awesome token!"
        }[Random.Range(0, 7)];
        anim.SetTrigger("Victory");
        StartCoroutine(Utils.DelayedReturnToMenu(15));
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

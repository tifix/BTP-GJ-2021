using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectObjPulsate : MonoBehaviour
{
    private RectTransform RT;
    private Player p;
    public float resizing_frequency=0.5f;
    public float resizing_power =0.2f;

    private void Start()
    {
        p = Player.instance;
        RT = GetComponent<RectTransform>();
        StartCoroutine(Effects());
    }

    IEnumerator Effects()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            float index = p.wc.comboIndex;

            if (index < 5) { resizing_power = 0.05f; resizing_frequency = 1f; }
            else if (index < 10) {resizing_power = 0.1f; resizing_frequency = 2f; }
            else if (index < 20) {resizing_power = 0.2f; resizing_frequency = 3f;}
            else if (index < 30) {resizing_power = 0.2f; resizing_frequency = 3.2f;}
            else if (index < 40) {resizing_power = 0.2f; resizing_frequency = 3.4f;}
            else {resizing_power = 0.25f;resizing_frequency = 4f; }

            float scale = Mathf.Sin(Time.time * resizing_frequency);
            scale *= resizing_power;
            RT.localScale = new Vector3(1 + scale, 1 + scale, 1 + scale);

            if (index > 2) GetComponent<Text>().text= 'x' + index.ToString();
            else GetComponent<Text>().text = "";

            index = Mathf.InverseLerp(2, 20, index);
            index = Mathf.Clamp(index, 0, 1);

            GetComponent<Text>().color = Color.Lerp(Color.yellow, Color.red, index);
        }
    }
}

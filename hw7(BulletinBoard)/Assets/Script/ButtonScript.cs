using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class ButtonScript : MonoBehaviour {
 
    public Text text;
    private int frame = 20;
 
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        text.gameObject.SetActive(false);
    }
 
    IEnumerator Fold()
    {
        float rx = 0;
        float xy = 120;
        for (int i = 0; i < frame; i++)
        {
            rx -= 90f / frame;
            xy -= 120f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            if (i == frame - 1)
            {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
 
    IEnumerator Unfold()
    {
        float rx = -90;
        float xy = 0;
        for (int i = 0; i < frame; i++)
        {
            rx += 90f / frame;
            xy += 120f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);
            if (i == 0)
            {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }
 
 
    void TaskOnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(Fold());
        }
        else
        {
            StartCoroutine(Unfold());
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private TooltipOptions options;
    private Vector3 offset = new Vector3(-50, -50, 0);

    public Text Title;
    public Text Text;

    public void Init(TooltipOptions options)
    {
        this.options = options;

        this.Title.text = options.Title;
        this.Text.text = options.Text;
        Debug.Log(this.gameObject.GetComponent<RectTransform>().sizeDelta);
    }

    public void Update()
    {
        this.gameObject.transform.localPosition = Input.mousePosition + this.offset;
    }
}

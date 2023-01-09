using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;

    private Vector2 offset;

    private RectTransform Screen;
    private Vector2 ScreenSize;

    private RectTransform Rect;
    private Vector2 Size;

    public Text Title;
    public Text Text;

    private void Awake()
    {
        Instance = this;
        this.Screen = this.transform.parent.GetComponent<RectTransform>();
        this.ScreenSize = this.Screen.sizeDelta;

        this.Rect = gameObject.GetComponent<RectTransform>();
        this.Size = this.Rect.sizeDelta;

        this.offset = this.Size / 2;
        this.Disable();
    }

    public void Init(TooltipOptions options)
    {
        this.Title.text = options.Title;
        this.Text.text = options.Text;

        this.SetToPosition();

        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void Update()
    {
        this.SetToPosition();
    }

    private void SetToPosition()
    {
        Vector2 directPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.Screen, Input.mousePosition, null, out directPos);

        var posX = directPos.x;
        var posY = directPos.y;

        posY = (posY + this.Size.y > this.ScreenSize.y / 2) ? posY - offset.y : posY + offset.y; 
        posX = (posX + this.Size.x > this.ScreenSize.x / 2) ? posX - offset.x : posX + offset.x;

        Vector2 offsetPos = new Vector2(posX, posY);

        this.gameObject.transform.localPosition = offsetPos;
    }

    public static void Show(TooltipOptions options)
    {
        Instance.Init(options);
    }

    public static void Hide()
    {
        Instance.Disable();
    }
}

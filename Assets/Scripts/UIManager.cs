using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TileManager tileManager;

    //Tools
    [SerializeField]
    private Button pencil;
    [SerializeField]
    private Button picker;
    [SerializeField]
    private Button eraser;

    //Colors
    [SerializeField]
    private Image primaryColorImage;
    [SerializeField]
    private Image secondaryColorImage;
    [SerializeField]
    public Button primaryColorButton;
    [SerializeField]
    public Button secondaryColorButton;

    private Color primaryColor = Color.black;
    private Color secondaryColor = Color.white;

    //Sliders
    [SerializeField]
    private Slider redSlider;
    [SerializeField]
    private Slider greenSlider;
    [SerializeField]
    private Slider blueSlider;
    private SliderManager redScrubber;
    private SliderManager blueScrubber;
    private SliderManager greenScrubber;

    [SerializeField]
    public List<SliderManager> scrubs = new List<SliderManager>();
    bool isScrubberSelectedPrev;
    [SerializeField]
    bool isPrimaryColor = true;

    public Image ColorImage {
        get {
            if (isPrimaryColor) {
                return primaryColorImage;
            }
            else {
                return secondaryColorImage;
            }
        }
        set {
            if (isPrimaryColor) {
                primaryColorImage = value;
            }
            else {
                secondaryColorImage = value;
            }
        }
    }

    public Color CurrentColor {
        get {
            if (isPrimaryColor) {
                return primaryColor;
            }
            else {
                return secondaryColor;
            }
        }
        set {
            if (isPrimaryColor) {
                primaryColor = value;
            }
            else {
                secondaryColor = value;
            }
            ColorPreview();
            if(CurrentTool == Tool.Picker) {
                UpdateSliders();
            }
        }
    }

    [SerializeField]
    private Tool currentTool = Tool.Pencil;
    public Tool CurrentTool {
        get { return currentTool; }
        set {
            currentTool = value;
            ToolToggle();
        }
    }

    public enum Tool {
        Pencil,
        Picker,
        Eraser
    }

    public void Start() {
        redScrubber = redSlider.GetComponentInChildren<SliderManager>();
        blueScrubber = blueSlider.GetComponentInChildren<SliderManager>();
        greenScrubber = greenSlider.GetComponentInChildren<SliderManager>();
        scrubs.AddRange(new List<SliderManager>() { redScrubber, blueScrubber, greenScrubber });
        pencil.onClick.AddListener(delegate { ColorPencil(); });
        picker.onClick.AddListener(delegate { ColorPicker(); });
        eraser.onClick.AddListener(delegate { ColorEraser(); });
        redSlider.onValueChanged.AddListener(delegate { SlideColor(); });
        greenSlider.onValueChanged.AddListener(delegate { SlideColor(); });
        blueSlider.onValueChanged.AddListener(delegate { SlideColor(); });
        primaryColorButton.onClick.AddListener(delegate { SetPrimaryColor(); });
        secondaryColorButton.onClick.AddListener(delegate { SetSecondaryColor(); });
        CurrentColor = Color.black;
    }

    public void ToolToggle() {
        pencil.image.color = Color.white;
        picker.image.color = Color.white;
        eraser.image.color = Color.white;
        switch (CurrentTool) {
            case UIManager.Tool.Pencil:
                pencil.image.color = Color.gray;
                break;
            case UIManager.Tool.Picker:
                picker.image.color = Color.gray;
                break;
            case UIManager.Tool.Eraser:
                eraser.image.color = Color.gray;
                break;
        }
    }

    public void ColorToggler() {
        GameObject primaryToggle = primaryColorButton.transform.Find("PrimaryToggle").gameObject;
        GameObject secondaryToggle = secondaryColorButton.transform.Find("SecondaryToggle").gameObject;
        if (isPrimaryColor) {
            primaryToggle.SetActive(true);
            secondaryToggle.SetActive(false);
        }
        else {
            primaryToggle.SetActive(false);
            secondaryToggle.SetActive(true);
        }
    }

    public void SetPrimaryColor() {
        isPrimaryColor = true;
        UpdateSliders();
        ColorToggler();
    }

    public void SetSecondaryColor() {
        isPrimaryColor = false;
        UpdateSliders();
        ColorToggler();
    }

    public void ColorPreview() {
        Debug.Log("Color Preview!");
        ColorImage.color = CurrentColor;
    }

    public void ColorPencil() {
        CurrentTool = Tool.Pencil;
    }

    public void ColorPicker() {
        CurrentTool = Tool.Picker;
    }

    public void ColorEraser() {
        CurrentTool = Tool.Eraser;
    }

    public void GetColor(GameObject box) {
        //currentColor = box.GetComponent<SpriteRenderer>().color; COLORPICKER
    }

    public void SlideColor() {
        foreach(SliderManager scrub in scrubs) {
            if (scrub.IsSelected) {
                UpdateRGBValues();
            }
            isScrubberSelectedPrev = scrub.IsSelected;
        }
    }

    public void UpdateRGBValues() {
        CurrentColor = new Color(r: redScrubber.GetComponent<Slider>().value, g: greenScrubber.GetComponent<Slider>().value, b: blueScrubber.GetComponent<Slider>().value);
    }

    public void UpdateSliders() {
        redSlider.value = CurrentColor.r;
        greenSlider.value = CurrentColor.g;
        blueSlider.value = CurrentColor.b;
    }


    public void Play() {
        Debug.Log("play");
        List<GameObject> tiles = tileManager.tiles;
        foreach(GameObject tile in tiles.ToArray()) {
            if(tile.GetComponent<SpriteRenderer>().color == Color.white) {
                tiles.Remove(tile);
                Destroy(tile);
            }
            else { 
}
        }
        tileManager.tiles = tiles;
    }
    // Start is called before the first frame update

}

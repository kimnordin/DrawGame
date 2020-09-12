using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TileManager tileManager;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Button pencil;
    public Button picker;
    public Button eraser;
    public Image selectedColor;

    SliderManager redScrubber;
    SliderManager blueScrubber;
    SliderManager greenScrubber;
    public List<SliderManager> scrubs = new List<SliderManager>();
    bool isScrubberSelectedPrev;

    [SerializeField]
    private Color currentColor = Color.white;
    public Color CurrentColor {
        get { return currentColor; }
        set {
            currentColor = value;
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

    public void ColorPreview() {
        Debug.Log("Color Preview!");
        selectedColor.color = CurrentColor;
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

    // Start is called before the first frame update

}

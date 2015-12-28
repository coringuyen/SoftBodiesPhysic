using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClothGUI : MonoBehaviour {

    public Slider SpringConstant;
    public Slider DampingFactor;
    public Slider RestLength;
    public Slider AirBlow;
    public Button Instruction;
    public Button CreateCloth;
    public Button DestroyCloth;
    public InputField Row;
    public InputField Column;
    public InputField Width;
    public InputField Height;
    public Toggle anchor2;
    public Toggle anchor4;
    public RawImage InstructionBG;
    public Text instructions;
    public Button BackToMenu;
    public Button CameraInstruction;
    public RawImage CameraInstructionBG;
    public Text camerainstructions;
    public Button BackToCloth;

    float springcontant, dampingfactor, restlength, air;

    void Start ()
    {
        // set position of all the GUI depend on the screen position
        DestroyCloth.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.85f, Screen.height * 0.9f, 0);
        CreateCloth.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f, Screen.height * 0.45f, 0);
        Instruction.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f, Screen.height * 0.3f, 0);
        RestLength.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.18f, Screen.height * 0.9f, 0);
        DampingFactor.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.18f, Screen.height * 0.8f, 0);
        SpringConstant.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.18f, Screen.height * 0.7f, 0);
        AirBlow.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.18f, Screen.height * 0.6f, 0);

        Row.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.3f, Screen.height * 0.72f, 0);
        Column.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.42f, Screen.height * 0.72f, 0);
        Width.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.58f, Screen.height * 0.72f, 0);
        Height.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.7f, Screen.height * 0.72f, 0);

        anchor2.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.37f, Screen.height * 0.86f, 0);
        anchor4.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.66f, Screen.height * 0.86f, 0);

        InstructionBG.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        instructions.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        BackToMenu.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.1f, Screen.height * 0.93f, 0);

        CameraInstruction.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.85f, Screen.height * 0.1f, 0);
        CameraInstructionBG.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        camerainstructions.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        BackToCloth.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.1f, Screen.height * 0.93f, 0);

        springcontant = SpringConstant.value;
        dampingfactor = DampingFactor.value;
        restlength = RestLength.value;
        air = AirBlow.value;

        CreateCloth.gameObject.SetActive(true);
        Instruction.gameObject.SetActive(true);
        anchor2.gameObject.SetActive(true);
        anchor4.gameObject.SetActive(true);
        Row.gameObject.SetActive(true);
        Column.gameObject.SetActive(true);
        Width.gameObject.SetActive(true);
        Height.gameObject.SetActive(true);
        anchor2.isOn = true;
    }

    void Update()
    {
        // checking if user give in the input for all the inputfield
        if (Row.text != "" && Column.text != "" && Width.text != "" && Height.text != "")
        {
            CreateCloth.interactable = true;
        }
        else
            CreateCloth.interactable = false;
    }

    public void turnOffInput()
    {
        Row.gameObject.SetActive(false);
        Column.gameObject.SetActive(false);
        Width.gameObject.SetActive(false);
        Height.gameObject.SetActive(false);
    }

    public void turnOnInput()
    {
        Row.gameObject.SetActive(true);
        Column.gameObject.SetActive(true);
        Width.gameObject.SetActive(true);
        Height.gameObject.SetActive(true);
    }

    public void turnOffSlider()
    {
        SpringConstant.gameObject.SetActive(false);
        DampingFactor.gameObject.SetActive(false);
        RestLength.gameObject.SetActive(false);
        AirBlow.gameObject.SetActive(false);
    }

    public void turnOnSlider()
    {
        SpringConstant.gameObject.SetActive(true);
        DampingFactor.gameObject.SetActive(true);
        RestLength.gameObject.SetActive(true);
        AirBlow.gameObject.SetActive(true);
    }

    public void resetSliders()
    {
        SpringConstant.value = springcontant;
        DampingFactor.value = dampingfactor;
        RestLength.value = restlength;
        AirBlow.value = air;
    }
    //public void moveExitToTheConner()
    //{
    //    // move exit button to bottom right
    //    Instruction.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.85f, Screen.height * 0.1f, 0);
    //}

    //public void moveExitBack()
    //{
    //    // move exit button back to the origin position
    //    Instruction.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f, Screen.height * 0.44f, 0);
    //}
}

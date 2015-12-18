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

    void Start ()
    {
        // set position of all the GUI depend on the screen position
        DestroyCloth.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.85f, Screen.height * 0.9f, 0);
        CreateCloth.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f, Screen.height * 0.54f, 0);
        Instruction.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.5f, Screen.height * 0.44f, 0);
        RestLength.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.15f, Screen.height * 0.9f, 0);
        DampingFactor.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.15f, Screen.height * 0.8f, 0);
        SpringConstant.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.15f, Screen.height * 0.7f, 0);
        AirBlow.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.15f, Screen.height * 0.6f, 0);

        Row.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.3f, Screen.height * 0.8f, 0);
        Column.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.42f, Screen.height * 0.8f, 0);
        Width.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.58f, Screen.height * 0.8f, 0);
        Height.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.7f, Screen.height * 0.8f, 0);

        CreateCloth.gameObject.SetActive(true);
        Instruction.gameObject.SetActive(true);
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

    void turnOffInput()
    {
        Row.gameObject.SetActive(false);
        Column.gameObject.SetActive(false);
        Width.gameObject.SetActive(false);
        Height.gameObject.SetActive(false);
    }

    void turnOnInput()
    {
        Row.gameObject.SetActive(true);
        Column.gameObject.SetActive(true);
        Width.gameObject.SetActive(true);
        Height.gameObject.SetActive(true);
    }

    void turnOffSlider()
    {

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

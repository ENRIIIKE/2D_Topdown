using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using enrike.utils.text;
public class PlayerInventory : MonoBehaviour
{
    //Singleton
    #region Singleton
    private static PlayerInventory _instance;
    public static PlayerInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInventory>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<PlayerInventory>();
                }

            }
            return _instance;
        }
    }
    #endregion

    //=====MATERIALS=====
    //Material Number = 1
    [SerializeField] private int wood;
    public int Wood
    {
        get { return wood; }
        private set { wood = value; }
    }
    //Material Number = 2
    [SerializeField] private int stone;
    public int Stone
    {
        get { return stone; }
        private set { stone = value; }
    }

    //Material Number = 3
    [SerializeField] private int iron;
    public int Iron
    {
        get { return iron; }
        private set { iron = value; }
    }

    //=====TEXT=====
    [Header("Text")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI ironText;

    [Header("Pop Up Text")]
    public LeanTweenType easeType;
    public string notEnoughMaterialsText;
    public float fontSize;

    private void Awake()
    {
        if (_instance != null) Destroy(this);
    }
    private void Start()
    {
        /*Wood = 0;
        Stone = 0;
        Iron = 0;*/
    }

    public void GiveMaterial(int amount, int materialNumber)
    {
        switch (materialNumber)
        {
            case 1:
                Wood += amount;
                break;
            case 2:
                Stone += amount;
                break;
            case 3:
                Iron += amount;
                break;
        }
    }
    public void TakeMaterial (int amount, int materialNumber)
    {
        switch (materialNumber)
        {
            case 1:
                Wood -= amount;
                break;
            case 2:
                Stone -= amount;
                break;
            case 3:
                Iron -= amount;
                break;
        }
    }
    public bool CheckMaterial (int needWood, int needStone, int needIron)
    {
        bool haveWood;
        bool haveStone;
        bool haveIron;

        if (needWood > Wood)
        {
            haveWood = false;
        }
        else haveWood = true;

        if (needStone > Stone)
        {
            haveStone = false;
        }
        else haveStone = true;

        if (needIron > Iron)
        {
            haveIron = false;
        }
        else haveIron = true;


        if (haveWood == true && haveStone == true && haveIron == true)
        {
            return true;
        }
        else
        {
            //Instantiate popup text message indicating that the player does not have materials
            //And returns false value for bool method

            if (GameObject.Find("Low Materials") != null)
            {
                return false;
            }

            GameObject textInstance = UniversalUtilities.InstantiateText(transform.position, notEnoughMaterialsText, fontSize, Color.red);
            textInstance.name = "Low Materials";
            

            return false;
        }
    }

    private void Update()
    {
        woodText.text = Wood.ToString();
        stoneText.text = Stone.ToString();
        ironText.text = Iron.ToString();
    }
}

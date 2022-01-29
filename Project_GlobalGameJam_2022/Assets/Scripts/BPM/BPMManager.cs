using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPMManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private List<Keyboard> keyboard;

    private Keyboard currentKeyboard;

    private Key currentKey;

    private PlayerType currentPlayer = PlayerType.Man;

    [SerializeField]
    private Color keyPressed;

    [SerializeField]
    private Color releasedKey;

    #endregion

    #region Events
    public delegate void Action(float songBeat);
    public static event Action OnKeyPressed;

    #endregion

    void Start()
    {
        //pegar o currentPlayer de algum lugar , game manager? variavel global?
    }

    private void Update()
    {
        this.InputKey();
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("input Q");
            ChangeKeyColor(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("input W");
            ChangeKeyColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("input E");
            ChangeKeyColor(2);
        }
    }

    private void getKeyboard()
    {
        this.currentKeyboard = keyboard.Find(k => k.player.ToString() == this.currentPlayer.ToString());
    }

    private void ChangeKeyColor(int indice)
    {
        if(this.currentKey != null)
        {
            this.currentKey.GetComponent<Image>().color = this.releasedKey;
        }     

        Debug.Log("entrou corotina");
        this.getKeyboard();
        this.currentKey = this.currentKeyboard.keys[indice];

        var keycolor = currentKey.GetComponent<Image>();
        keycolor.color = this.keyPressed;

        if(OnKeyPressed != null)
        {
            Debug.Log("event send");
            OnKeyPressed(currentKey.songBeat);
        }

        Debug.Log("saiu corotina");
    }
}

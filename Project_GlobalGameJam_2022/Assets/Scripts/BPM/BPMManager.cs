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

    private PlayerType currentPlayer = PlayerType.Indian;

    [SerializeField]
    private Color keyPressed;

    [SerializeField]
    private Color releasedKey;

    [SerializeField]
    private GameObject indianBPMColor;

    [SerializeField]
    private GameObject moderIndianBPMColor;

    #endregion

    #region Events
    public delegate void Action(float songBeat);
    public static event Action OnKeyPressed;

    #endregion

    void Start()
    {
        BPMSwitchNative.OnChangeBPM += changePlayerKeyboard;

        this.indianBPMColor.SetActive(true);
        this.moderIndianBPMColor.SetActive(false);
    }

    private void Update()
    {
        this.InputKey();
    }

    public void OnDisable()
    {
        BPMSwitchNative.OnChangeBPM -= changePlayerKeyboard;
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("input 1");
            ChangeKeyColor(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("input 2");
            ChangeKeyColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("input 3");
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

    private void changePlayerKeyboard(PlayerType newPlayer)
    {
        this.currentPlayer = newPlayer;

        switch(newPlayer)
        {
            case PlayerType.Indian:
                this.indianBPMColor.SetActive(true);
                this.moderIndianBPMColor.SetActive(false);     
                
                //mudar musica para musica do indio floresta
                break;
            case PlayerType.ModernIndian:
                this.moderIndianBPMColor.SetActive(true);
                this.indianBPMColor.SetActive(false);

                //mudar musica para musica do indio funai
                break;
        }      
    }
}

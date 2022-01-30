using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMSwitchNative : MonoBehaviour
{
    public delegate void BPMAction(PlayerType currentPlayer);
    public static event BPMAction OnChangeBPM;

    public delegate void ChangePlayerAction(PlayerType currentPlayer);
    public static event ChangePlayerAction OnLockPlayer;


    [SerializeField]
    private PlayerType changePlayerTo;

    private PlayerType currentPlayer;

    void Start()
    {
        Debug.Log("start switch bpm");
        if (changePlayerTo == PlayerType.ModernIndian)
        {
            Debug.Log("indian");
            this.currentPlayer = PlayerType.Indian;
        }
        else
        {
            Debug.Log("modern");
            this.currentPlayer = PlayerType.ModernIndian;
        }

        if (OnLockPlayer != null)
        {
            Debug.Log("lock player");
            OnLockPlayer(this.changePlayerTo);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ColectStar());       
        }
    }

    private IEnumerator ColectStar()
    {
        Debug.Log("colecstar Indian");
        //som do coletavel

        Debug.Log("Player to change:" + this.changePlayerTo);
        Debug.Log("Current Player:" + this.currentPlayer);

        if (OnChangeBPM != null)
        {
            //mandar mensagem ao game manager para trocar o player atual
            Debug.Log("change to" + this.changePlayerTo);
            OnChangeBPM(changePlayerTo);
        }

        //mandar mensagem ao player que for indian para desabilitar o input
        if (OnLockPlayer != null)
        {
            Debug.Log("lock player");
            OnLockPlayer(this.currentPlayer);
        }
        
        //aqui seria legal uma particula

        //mandar mensagem ao background para add um arvore dentra as que estao em um array

        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.6f);

        this.gameObject.SetActive(true);
    }
}

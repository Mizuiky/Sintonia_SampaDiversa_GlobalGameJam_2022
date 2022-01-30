using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMSwitchNative : MonoBehaviour
{
    public delegate void Action(PlayerType currentPlayer);
    public static event Action OnChangeBPM;

    [SerializeField]
    private PlayerType changePlayerTo;

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

        if (OnChangeBPM != null)
        {
            //mandar mensagem ao game manager para trocar o player atual
            Debug.Log("chama modern");
            OnChangeBPM(changePlayerTo);
        }

        //mandar mensagem ao player que for indian para desabilitar o input

        //aqui seria legal uma particula

        //mandar mensagem ao background para add um arvore dentra as que estao em um array

        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMSwitchNative : MonoBehaviour
{
    public delegate void Action(PlayerType currentPlayer);
    public static Action OnChangeBPM;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ColectStar());       
        }
    }

    private IEnumerator ColectStar()
    {
        //som do coletavel

        if (OnChangeBPM != null)
        {
            //mandar mensagem ao game manager para trocar o player atual
            OnChangeBPM(PlayerType.Man);
        }

        //aqui seria legal uma particula

        //mandar mensagem ao background para add um arvore dentra as que estao em um array

        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);
    }
}

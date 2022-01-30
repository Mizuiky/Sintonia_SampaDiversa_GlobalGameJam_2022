using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingBlocks2 : MonoBehaviour
{
    public float timerBlink = 2f;
    
    //public BoxCollider2D colliderBlink;

    // Start is called before the first frame update
    void Start()
    {
        BPMManager.OnKeyPressed += setBlick;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Color colorBlock = this.GetComponent<SpriteRenderer>().color;
        colorBlock.a = 0.3f;
        this.GetComponent<SpriteRenderer>().color = colorBlock;

        StartCoroutine(Blink());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void setBlick(float blicksongBeat)
    {
        this.timerBlink = blicksongBeat;
    }

    public void OnEnable()
    {
        BPMManager.OnKeyPressed -= setBlick;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerBlink);

            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            Color colorBlock = this.GetComponent<SpriteRenderer>().color;
            colorBlock.a = 1f;
            this.GetComponent<SpriteRenderer>().color = colorBlock;

            yield return new WaitForSeconds(timerBlink);

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            colorBlock = this.GetComponent<SpriteRenderer>().color;
            colorBlock.a = 0.3f;
            this.GetComponent<SpriteRenderer>().color = colorBlock;
    

        }
    }
}

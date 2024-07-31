using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPointerArrow : MonoBehaviour
{
    private Coroutine BobCoro;
    [SerializeField] float bobHeight = 0.5f;
    [SerializeField] float bobDuration = 0.5f;



    private void OnEnable()
    {
        BobCoro = StartCoroutine(GetBobbing());
    }

    private void OnDisable()
    {
        StopCoroutine(BobCoro);
    }



    private IEnumerator GetBobbing()
    {
        bool up = true;
        
        
        while(true)
        {
            if(up)
            {
                transform.position = transform.position + new Vector3(0, bobHeight, 0);
            }
            else
            {
                transform.position = transform.position + new Vector3(0, -bobHeight, 0);
            }
            
            up = !up;
            yield return new WaitForSeconds(bobDuration);
        }


    }
}

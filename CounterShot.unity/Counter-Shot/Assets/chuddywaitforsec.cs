using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuddywaitforsec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator waiting(System.Action onComplete)
    {
        yield return new WaitForSeconds(2);
        onComplete?.Invoke();
    }
   
}

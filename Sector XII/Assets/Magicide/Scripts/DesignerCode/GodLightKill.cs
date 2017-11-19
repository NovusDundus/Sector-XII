using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodLightKill : MonoBehaviour {

    public float timeTillKill = 5.0f;

    public Light lt;
    public float timeForEffect = 2f;
    public float fadeIntensity = .8f;

    // Use this for initialization
    void Start() {
            lt = GetComponent<Light>();
            StartCoroutine(FadeLightOut());
    }

    // Update is called once per frame
    void Update() {

        KillCommand();
        FadeLightOut();
    }

    void KillCommand(){

        Destroy(this.gameObject, timeTillKill);
       // gameObject.SetActive(false); // To lazy to add in timer. 
       
}

   IEnumerator FadeLightOut() {
       Color startColor = lt.color;
       Color endColor = Color.clear;
   
       float elapsed = 0f;
    
   
       while (elapsed <= timeForEffect) {
           lt.color = Color.Lerp(startColor, endColor, elapsed / timeForEffect * fadeIntensity);
           elapsed += Time.deltaTime;
           yield return null;
       }
   
       lt.color = endColor;
   }
}

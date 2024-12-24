using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] Text hpText;   
    
    public void Oninit(float damage)
    {  
        if (damage < 0)
        {
            hpText.color = Color.green;
        }
        else
        {
            hpText.color = Color.red;
        }
        hpText.text = (Mathf.Abs(damage)).ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}

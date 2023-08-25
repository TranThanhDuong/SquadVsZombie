using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSlotDeployControl : MonoBehaviour
{
    private bool isHasUnit = false;
    private SpriteRenderer spriteRenderer;
    private Transform trans;
    private bool isActive = false;
    private void Awake()
    {
        isHasUnit = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        trans = transform;
    }
    // Start is called before the first frame update
    public bool SetActive(bool isActive, bool isHasUnit_ = false)
    {
       if(!isHasUnit)
        {
          if(this.isActive!=isActive)
            {
                if (isActive)
                {
                    spriteRenderer.color = Color.yellow;
                    trans.localScale = Vector3.one + Vector3.one * 0.2f;
                }
                else
                {
                    spriteRenderer.color = Color.white;
                    trans.localScale = Vector3.one;
                }
            }
            
            this.isActive = isActive;
        }

       if(isHasUnit_ != isHasUnit)
        {
            isHasUnit = isHasUnit_;
        }
        return !isHasUnit;
    }
   
}

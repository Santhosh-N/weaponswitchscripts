using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
	public GameObject swordAttackButton,fireButton,gunSprite,swordSprite,emptyButton,empHand;
	public static int weaponLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        //emptyButton.gameObject.SetActive(true);
		//empHand.gameObject.SetActive(true);
		//fireButton.gameObject.SetActive(true);
		//swordAttackButton.gameObject.SetActive(true);
		//swordSprite.gameObject.SetActive(true);
		//gunSprite.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch(weaponLevel)
		{
			case 0: 
			{
				emptyButton.gameObject.SetActive(true);
				empHand.gameObject.SetActive(true);
				fireButton.gameObject.SetActive(false);
				swordAttackButton.gameObject.SetActive(false);
				swordSprite.gameObject.SetActive(false);
				gunSprite.gameObject.SetActive(false);
				break;
			}
			case 1:
			{
				empHand.gameObject.SetActive(false);
				emptyButton.gameObject.SetActive(false);
				gunSprite.gameObject.SetActive(true);	
				fireButton.gameObject.SetActive(true);
				break;
			}
			case 2:
			{
				WeaponSwitch.weaponLevel += 1;
				gunSprite.gameObject.SetActive(false);
				empHand.gameObject.SetActive(false);
				emptyButton.gameObject.SetActive(false);
				fireButton.gameObject.SetActive(false);
				swordAttackButton.gameObject.SetActive(true);
				swordSprite.gameObject.SetActive(true);
				break;
			}
			default:
			{
				break;
			}
								
					
		}
    }
	
	public void WeaponSwitchFun()
	{
		if(WeaponSwitch.weaponLevel > 2)
		{
			WeaponSwitch.weaponLevel = 0;
		}
		else{
		WeaponSwitch.weaponLevel += 1;
		}
	}
}

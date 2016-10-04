using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class DailyBonusController : MonoBehaviour
{
    public BonusItem[] BonusItems;
    public RectTransform Arrow;
    public AudioSource SpineSound;
    public AudioSource ChoiceEndedSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChoiceRandom()
    {
        var choice = ProbabilityController.ChoiceRandom(BonusItems);
        StartCoroutine(
            RotateToAngle(new Vector3(0,0,1),
            Arrow.rotation.eulerAngles.z,
            Arrow.rotation.eulerAngles.z + (360 - Arrow.rotation.eulerAngles.z) + BonusItems[choice].Angle,
            10, EndRandomChoice));
    }

    public void EndRandomChoice()
    {
        ChoiceEndedSound.Play();
    }

    IEnumerator RotateToAngle(Vector3 rotateAxis,float currentAngle, float targetAngleValue, 
        float speed = 10, Action endFired = null)
    {
        var itemSoundAngle = currentAngle + (360/BonusItems.Length);
        while (true)
        {
            var step = ((targetAngleValue - currentAngle) + speed) * Time.deltaTime;
            if (currentAngle + step > targetAngleValue)
            {
                step = targetAngleValue - currentAngle;
                Arrow.Rotate(rotateAxis, step);
                if (endFired != null)
                    endFired();

                break;
            }
            currentAngle += step;
            if (currentAngle >= itemSoundAngle)
            {
                SpineSound.Play();
                itemSoundAngle = currentAngle + (360 / BonusItems.Length);
            }
            Arrow.Rotate(rotateAxis, step);

            yield return null;
        }
    }
}
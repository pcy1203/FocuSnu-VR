using UnityEngine;
using System.Collections;

public class ColliderScript : MonoBehaviour
{
    public string disappearedObjectName;
    public ObjectManager objectManager;

    private GameObject currentObjectInCollider;
    private Coroutine holdCoroutine;
    private bool isHolding = false;
    private float holdTimeRequired = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == disappearedObjectName)
        {
            if (currentObjectInCollider == null)
            {
                currentObjectInCollider = other.gameObject;

                holdCoroutine = StartCoroutine(HoldTimer());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObjectInCollider)
        {
            
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }
            currentObjectInCollider = null;
            isHolding = false;
        }
    }

    private IEnumerator HoldTimer()
    {
        isHolding = false;
        float timer = 0f;

        while (timer < holdTimeRequired)
        {
            if (currentObjectInCollider == null)
            {
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        isHolding = true;
        HandleSuccessfulPlacement();
    }

    private void HandleSuccessfulPlacement()
    {
        if (currentObjectInCollider != null)
        {

            objectManager.score += 1;
            Debug.Log("Correct! Points: " + objectManager.score);
            objectManager.UpdateScoreUI();

            int index = objectManager.objectsToDisappear.IndexOf(currentObjectInCollider);
            if (index != -1)
            {
                objectManager.ResetObject(currentObjectInCollider, index);
            }

            this.gameObject.SetActive(false);
        }

        currentObjectInCollider = null;
        holdCoroutine = null;
    }

    void Update()
    {
        if (isHolding && currentObjectInCollider != null)
        {
        }
    }
}

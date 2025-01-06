using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public GameManager gameManager;
    public Material cardBack;
    public Material cardFront;
    private bool isFaceUp = false;
    private bool isAnimating = false;
    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private float flipSpeed = 300f;
    private bool materialUpdated = false;

    public UnityEvent<Card> OnFlipComplete = new UnityEvent<Card>();

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (isAnimating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, flipSpeed * Time.deltaTime);

            float angle = Quaternion.Angle(transform.rotation, initialRotation);
            if (!materialUpdated && angle >= 90f)
            {
                UpdateMaterial();
                materialUpdated = true;
            }

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isAnimating = false;
                materialUpdated = false;
                OnFlipComplete.Invoke(this);
            }
        }
        
    }

    public void Flip()
    {
        if (isAnimating || isFaceUp) return;
        gameManager.CardClicked(this);
    }

    public void Animate()
    {
        isAnimating = true;
        targetRotation = initialRotation * Quaternion.Euler(0, 0, isFaceUp ? 0 : 180);
        isFaceUp = !isFaceUp;
    }

    public void FlipBack()
    {
        isAnimating = true;
        targetRotation = initialRotation;
        isFaceUp = !isFaceUp;
    }

    private void UpdateMaterial()
    {
        SetMaterial(isFaceUp ? cardFront : cardBack);
    }

    private void SetMaterial(Material mat)
    {
        GetComponentInChildren<Renderer>().material = mat;
    }

    public bool IsFaceUp()
    {
        return isFaceUp;
    }

    public bool IsAnimating()
    {
        return isAnimating;
    }

    public void SetFaceUp(bool faceUp)
    {
        isFaceUp = faceUp;
        UpdateMaterial();
    }
}

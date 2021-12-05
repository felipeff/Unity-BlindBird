using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  
/// </summary>
public class Flower : MonoBehaviour
{

    [Tooltip("The color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, .3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowerColor = new Color(.5f, 0f, 1f);

    /// <summary>
    /// The trigger collider representing the nectar
    /// </summary>
    [HideInInspector]
    public Collider nectarCollider;

    // The solid collider representing the flower petals
    private Collider flowerCollider;

    // the flower's material
    private Material flowerMaterial;
    
    /// <summary>
    ///  A vector pointing straight out of the flower
    /// </summary>
    public Vector3 FlowerUpVector
    {
        get
        {
            return nectarCollider.transform.up;
        }
    }

    /// <summary>
    /// The center position of the nectar collider
    /// </summary>
    public Vector3 FlowerCenterPosition
    {
        get
        {
            return nectarCollider.transform.position;
        }
    }


    /// <summary>
    ///  The amount of nectar remaining in the flower
    /// </summary>
    public float NectarAmount { get; private set; }


    /// <summary>
    /// Whether the flower has any nectar remaining
    /// </summary>
    public bool HasNectar
    {
        get
        {
            return NectarAmount > 0f;
        }
    }


    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">Amount of nectar to try to remove</param>
    /// <returns>Amount of nectar it actually removed</returns>
    public float Feed(float amount)
    {
        // Track how much nectar was successfully taken (cannot take more than is available)
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);

        // Subtract the nectar
        NectarAmount -= amount;

        if(NectarAmount <= 0)
        {
            // No nectar remaining
            NectarAmount = 0;

            // disable the flower and nectar colliders
            nectarCollider.gameObject.SetActive(false);
            flowerCollider.gameObject.SetActive(false);

            // Change the flower color to indicate that it is empty
            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);
        }

        // return the amount of nectar that was taken
        return nectarTaken;
    }

    /// <summary>
    ///  Resets the flower
    /// </summary>
    public void ResetFlower()
    {
        // refill the nectar
        NectarAmount = 1f;

        // enable colliders
        nectarCollider.gameObject.SetActive(true);
        flowerCollider.gameObject.SetActive(true);

        // change the flower color to indicate that it is full
        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);
    }

    /// <summary>
    /// Called when the flower wakes up
    /// </summary>
    private void Awake()
    {
        // find the flower's mesh renderer and get the main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        // find the flower and nectar colliders
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();

    }

}

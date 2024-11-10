using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player => GetComponent<Player>();

    [Header("Shoot Parameters")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject fireballPrefab;

    [Header("Helper Dots")]
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private int numberOfDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    private void Start()
    {
        GenerateDots();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
            SetDotsPos();
    }

    #region Dots
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, attackPoint.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private void SetDotsPos()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].transform.position = (Vector2)attackPoint.position + MouseAim() * (i * spaceBetweenDots) * 5f;
        }
    }

    public void ChangeDotVisibility(bool x)
    {
        foreach (GameObject d in dots)
        {
            d.SetActive(x);
        }
    }

    #endregion
  
    private Vector2 MouseAim()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - playerPos;

        return direction.normalized;
    }

    public void ShootFireball()
    {
        Instantiate(fireballPrefab, attackPoint.position, Quaternion.identity).GetComponent<Projectile>().SetupDirection(MouseAim());
    }
}

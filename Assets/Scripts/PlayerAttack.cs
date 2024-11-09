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
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = SetDotsPos(i * spaceBetweenDots);
            }
        }
    }

    private Vector2 SetDotsPos(float p)
    {
        return (Vector2)player.transform.position + MouseAim() * p * 5f;
    }

    private Vector2 MouseAim()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - playerPos;

        return direction.normalized;
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    public void ChangeDotVisibility(bool x)
    {
        foreach(GameObject d in dots)
        {
            d.SetActive(x);
        }
    }

    public void ShootFireball()
    {
        Instantiate(fireballPrefab, attackPoint.position, Quaternion.identity).GetComponent<Projectile>().SetupDirection(MouseAim());
    }
}

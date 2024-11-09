using UnityEngine;

public class Test : MonoBehaviour
{
    //Basic character movement test
    float horMove;
    float verMove;

    private void Update()
    {
        horMove = Input.GetAxis("Horizontal");
        verMove = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(horMove, verMove, 0);

        transform.Translate(moveVector * Time.deltaTime);
    }
}

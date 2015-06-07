using UnityEngine;
using System.Collections;

public class SpaceEndlessWrapper : MonoBehaviour
{

    Rect rectCamLocal = new Rect();
    Vector3 camRecBottomLeft = new Vector3();
    Vector3 camRecTopRight = new Vector3();
    Vector3 position = new Vector3();
    float offset = .1f;

    void Start()
    {        
        rectCamLocal = Camera.main.pixelRect;
        camRecBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(rectCamLocal.x, rectCamLocal.y, Camera.main.nearClipPlane));
        camRecTopRight = Camera.main.ScreenToWorldPoint(new Vector3(rectCamLocal.x + rectCamLocal.width, rectCamLocal.y + rectCamLocal.height, Camera.main.nearClipPlane));
    }

    void LateUpdate()
    {
        position = transform.position;
        if (transform.position.x > camRecTopRight.x)
        {
            position.x = camRecBottomLeft.x + offset;
        }

        if (transform.position.x < camRecBottomLeft.x)
        {
            position.x = camRecTopRight.x - offset;
        }

        if (transform.position.y > camRecTopRight.y)
        {
            position.y = camRecBottomLeft.y + offset;
        }

        if (transform.position.y < camRecBottomLeft.y)
        {
            position.y = camRecTopRight.y - offset;
        }
        transform.position = position;
    }

}

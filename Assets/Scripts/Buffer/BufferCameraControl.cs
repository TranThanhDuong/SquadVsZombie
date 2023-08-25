using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BufferCameraControl : MonoBehaviour
{
    public Vector3 deltaMove;
    private Vector3 ogrinPos;
    private Transform trans;
    [Range(0.5f,5f)]
    public float sensitive = 1f;
    [Range(0.1f, 5f)]
    public float speedMove = 1f;
    public Transform limit_Left;
    public Transform limit_Right;
    public static int sideMove;
    public Vector3 ogrinPosMove;
    public static bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        deltaMove = Vector3.zero;
        ogrinPosMove = trans.position;

    }
    public void SetSnapCamera(Transform target)
    {
        trans.DOMoveX(target.position.x, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        // xu ly deltamove
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            ogrinPos = Input.mousePosition;
            deltaMove = Vector3.zero;
            isMove = true;
        }
        else if (Input.GetMouseButton(0))
        {
            deltaMove = Input.mousePosition - ogrinPos;
            ogrinPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ogrinPos =Vector3.zero;
            deltaMove = Vector3.zero;
            isMove = false;
        }
        else
        {
            deltaMove = Vector3.Lerp(deltaMove, Vector3.zero, Time.deltaTime * 0.5f);
        }

        deltaMove = new Vector3(deltaMove.x, 0, 0);
        // xu ly di chuyen camera

        Vector3 newPos = trans.position - deltaMove * sensitive;
        float x = newPos.x;
        x = Mathf.Clamp(x, limit_Left.position.x, limit_Right.position.x);
        newPos= new Vector3(x, newPos.y, newPos.z);
        trans.position = Vector3.Lerp(trans.position, newPos, Time.deltaTime * speedMove);
        float dis = trans.position.x - ogrinPosMove.x;

        if (dis > 0)
        {
            sideMove = 1;
        }
        else if (dis < 0)
        {
            sideMove = -1;
        }
        else
            sideMove = 0;
        ogrinPosMove = trans.position;
    }
    private void FixedUpdate()
    {


    }
}

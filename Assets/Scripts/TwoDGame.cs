using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDGame : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private TwoDMovement player2d;
    [SerializeField] private float panningSpeed;
    [SerializeField] private Transform tablet;
    [SerializeField] private GameObject exitButton;
    private bool toTablet;
    private bool fromTablet;
    private bool inTablet;
    private Vector3 orgPoint;
    private Quaternion orgRot;
    private float time = 0;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !inTablet)
        {
            Debug.Log("Shoot");
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Tablet")
            {
                Debug.Log("Hit");
                toTablet = true;
                player.enabled = false;
                player2d.enabled = true;
                inTablet = true;

                orgPoint = cam.transform.position;
                orgRot = cam.transform.rotation;
            }
        }

        if (toTablet)
            LerpToTablet();

        if (fromTablet)
            LerpFromTablet();
    }

    private void LerpToTablet()
    {
        time += Time.deltaTime * panningSpeed;
        cam.transform.position = Vector3.Lerp(orgPoint, targetPoint.position, time);
        cam.transform.rotation = Quaternion.Slerp(orgRot, tablet.rotation, time);

        if (time >= 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            exitButton.SetActive(true);
            toTablet = false;
            time = 0;
        }
    }

    private void LerpFromTablet()
    {
        time += Time.deltaTime * panningSpeed;
        cam.transform.position = Vector3.Lerp(targetPoint.position, orgPoint, time);
        cam.transform.rotation = Quaternion.Slerp(tablet.rotation, orgRot, time);

        if (time >= 1)
        {
            fromTablet = false;
            time = 0;
            player.enabled = true;
            inTablet = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ExitGame()
    {
        exitButton.SetActive(false);
        fromTablet = true;
        player2d.enabled = false;
    }
}

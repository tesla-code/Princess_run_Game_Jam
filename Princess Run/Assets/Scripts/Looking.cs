using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Looking : MonoBehaviourPunCallbacks
{
    #region Variables

    public static bool cursorLocked = true;

    public Transform player;
    public Transform cams;
   

    public float maxAngle;

    public float xSensitivity;
    public float ySensitivity;

    private Quaternion camCenter;

    #endregion

    #region MonoBehaviour Callbacks

    void Start()
    {
        camCenter = cams.localRotation;
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        SetX();
        SetY();

        UpdateCursorLock();
    }

    #endregion

    #region Private Methods
    void SetX()
    {
        float tempInput = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion tempAdjustment = Quaternion.AngleAxis(tempInput, Vector3.up);
        Quaternion tempDelta = player.localRotation * tempAdjustment;

        player.localRotation = tempDelta;
    }
    void SetY()
    {
        float tempInput = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion tempAdjustment = Quaternion.AngleAxis(tempInput, -Vector3.right);
        Quaternion tempDelta = cams.localRotation * tempAdjustment;

        if (Quaternion.Angle(camCenter, tempDelta) < maxAngle)
        {
            cams.localRotation = tempDelta;
        }

        
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    #endregion
}


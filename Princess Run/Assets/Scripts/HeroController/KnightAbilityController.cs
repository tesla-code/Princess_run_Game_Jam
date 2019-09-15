using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightAbilityController : MonoBehaviour
{
    #region Variables
    private Image cooldown;

    private KnightController knight;

    [Range(30f, 60f)]
    [SerializeField]
    protected float cooldownTime;

    [Range(1, 9)]
    [SerializeField]
    protected int number;

    [SerializeField]
    private KnightAbility _ability;

    private float timePassed;
    private bool onCooldown;
    private KeyCode usage;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        knight = GetComponentInParent<KnightController>();
        cooldown = GetComponent<Image>();
        cooldown.fillAmount = 1;
        onCooldown = false;

        cooldown.transform.position = new Vector3(Screen.width - (Screen.width - 50 * number), Screen.height - (Screen.height - 50));

        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = number.ToString();

        switch (number)
        {
            case 1: usage = KeyCode.Alpha1; break;
            case 2: usage = KeyCode.Alpha2; break;
            case 3: usage = KeyCode.Alpha3; break;
            case 4: usage = KeyCode.Alpha4; break;
            case 5: usage = KeyCode.Alpha5; break;
            case 6: usage = KeyCode.Alpha6; break;
            case 7: usage = KeyCode.Alpha7; break;
            case 8: usage = KeyCode.Alpha8; break;
            case 9: usage = KeyCode.Alpha9; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_ability.onGoingEvent)
        {
            _ability.Execute(knight);
        }

        if (Input.GetKeyDown(usage) && !onCooldown)
        {
            onCooldown = true;
            timePassed = 0;

            _ability.StartAbility(knight);
        }

        else if (onCooldown)
        {
            timePassed += Time.deltaTime;

            cooldown.fillAmount = timePassed / cooldownTime;

            if (timePassed >= cooldownTime)
            {
                onCooldown = false;
            }
        }
    }
    #endregion
}


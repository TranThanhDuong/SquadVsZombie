using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public enum DeployType
{
    DeploySlot,
    DeploySkill,
}
public enum CardType
{
    UnitType,
    SkillType,
}

public class CardUIItemControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public DeployType deployType;
    public CardType cardType;
    public Image iconCard;
    public Image iconDeploy;
    public Text nameLB;
    public Text costLB;
    public Text levelLB;
    public Text timeLB;
    public GameObject lockObject;
    public Image bgCountDown;
    public Text countDownLB;
    public UnitData dataCard;
    public RectTransform parentUI;
    public RectTransform iconImage;
    public Color activeColor;
    public Color disableColor;

    private Camera cam;
    public LayerMask markUnitSlot;
    public LayerMask markSkillSlot;
    private bool validDeploy = false;
    private UnitSlotDeployControl unitSlotDeployControl;
    private ConfigUnitRecord configUnit;
    private ConfigUnitLevelRecord configUnitLevel;
    private bool isActive = false;
    private bool enoughtGold = false;
    private bool enoughtTime = false;

    private bool isTouching = false;
    #region Card Drag handle
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive || !enoughtGold || !enoughtTime)
            return;

        validDeploy = false;
        MoveObject(eventData.position, eventData.pressEventCamera);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isActive || !enoughtGold || !enoughtTime)
            return;

        MoveObject(eventData.position, eventData.pressEventCamera);
        CheckUnitSlot(eventData.position);
    }
    private void MoveObject(Vector2 point, Camera camera)
    {
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentUI, point, camera, out localPoint);
        iconImage.anchoredPosition = localPoint;

    }
    private void CheckUnitSlot(Vector3 point)
    {
        // Ray ray = cam.ScreenPointToRay(point);
        // Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        Vector3 pos= cam.ScreenToWorldPoint(point);
        pos = new Vector3(pos.x, pos.y, 0);
        Collider2D collider2= Physics2D.OverlapCircle(pos, 0.2f, markUnitSlot);
        if(collider2!=null)
        {
             unitSlotDeployControl = collider2.GetComponent<UnitSlotDeployControl>();
            if (unitSlotDeployControl != null)
            {
                validDeploy = unitSlotDeployControl.SetActive(true);
            }
        }
        else
        {
            if (unitSlotDeployControl != null)
            {
                validDeploy = false;
                unitSlotDeployControl.SetActive(false);
            }
        }

    }

    private void CheckSkillPos(Vector3 point)
    {
        Vector3 pos = cam.ScreenToWorldPoint(point);
        pos = new Vector3(pos.x, pos.y, 0);
        Collider2D collider2 = Physics2D.OverlapCircle(pos, 0.2f, markSkillSlot);
        if (collider2 != null)
        {
            validDeploy = true;
        }
        else
        {
            validDeploy = false;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActive || !enoughtGold || !enoughtTime)
            return;
        isTouching = true;
        iconImage.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isTouching)
            return;
        isTouching = false;
        iconImage.gameObject.SetActive(false);
        iconImage.anchoredPosition = Vector2.zero;
        if(validDeploy)
        {
            // deployInit
            GameObject go = Instantiate(Resources.Load("Units/" + configUnit.prefab, typeof(GameObject))) as GameObject;
            go.transform.SetParent(null);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.transform.position = unitSlotDeployControl.transform.position;
            go.GetComponent<UnitControl>().Setup(new UnitDataDeploy { cfUnit = configUnit, cfUnitLevel = configUnitLevel, control = unitSlotDeployControl });
            unitSlotDeployControl.SetActive(false, true);
            MissionControl.instance.GoldChange(-configUnit.cost);
            MissionControl.instance.TimeChange(-configUnit.timeDeploy);
            StartCountDown();
        }
    }
    #endregion

    #region card Data
    public void Setup(UnitData data, Camera cam)
    {
        this.cam = cam;
        dataCard = data;
        configUnit = ConfigManager.instance.configUnit.GetRecordByKeySearch(dataCard.id);
        ConfigUnitlevelKey key = new ConfigUnitlevelKey();
        key.id_Unit = dataCard.id;
        key.level = dataCard.level;
        configUnitLevel = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(key);

        if (costLB != null)
            costLB.text = configUnit.cost.ToString();
        if (nameLB != null)
            nameLB.text = configUnit.name;
        if (levelLB != null)
            levelLB.text = "LV: " + data.level.ToString();
        if (timeLB != null)
            timeLB.text = "TIME: " + configUnit.timeDeploy;

        if (lockObject != null)
            lockObject.SetActive(false);
        StartCountDown();
        MissionControl.instance.OnCostChange += OnCostChange;
        MissionControl.instance.OnGoldChange += OnGoldChange;
        iconCard.overrideSprite = Resources.Load("Icon/" + configUnit.prefab + "/Icon", typeof(Sprite)) as Sprite;
        iconDeploy.overrideSprite = Resources.Load("Icon/" + configUnit.prefab + "/Deploy", typeof(Sprite)) as Sprite;
    }
    private void StartCountDown()
    {
        bgCountDown.gameObject.SetActive(true);
        bgCountDown.fillAmount = 1;
        float timeCountDown = 0f;
        isActive = false;
        bgCountDown.DOFillAmount(0, configUnit.countDown).OnComplete(() =>
        {
            bgCountDown.gameObject.SetActive(false);
            isActive = true;

        }).OnUpdate(()=> {
            timeCountDown += Time.deltaTime;
            if (timeCountDown > configUnit.countDown)
                timeCountDown = configUnit.countDown;
            int timeTemp = Mathf.RoundToInt(timeCountDown);
            countDownLB.text = (configUnit.countDown - timeTemp).ToString();

        });

    }
    void OnGoldChange(int gold)
    {
        if(configUnit.cost > gold)
        {
            enoughtGold = false;
            costLB.color = disableColor;
        }
        else
        {
            enoughtGold = true;
            costLB.color = activeColor;
        }
    }


    void OnCostChange(int time)
    {
        if (configUnit.timeDeploy > time)
        {
            enoughtTime = false;
            timeLB.color = disableColor;
        }
        else
        {
            enoughtTime = true;
            timeLB.color = activeColor;
        }
    }
    #endregion
    }

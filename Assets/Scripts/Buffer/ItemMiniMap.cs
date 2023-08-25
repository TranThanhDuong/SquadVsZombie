using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int index_map;
    private bool isCall = false;
    private Transform transCam;
    private Transform trans;
    private float width;
    private bool isUnlock;

    private ConfigMissionRecord record;
    private MissionData data;

    public GameObject lockObj;
    public GameObject unLockObj;

    public List<GameObject> stars;
    private void Awake()
    {
        width = 8.8f;
        transCam = MiniMapSetting.instance.transCam;
        trans = transform;
    }
    public void Setup(int index)
    {
        this.index_map = index;
        this.isUnlock = DataAPIControler.instance.GetCurrentMission() >= index + 1;
        if (isUnlock)
        {
            lockObj.SetActive(false);
            unLockObj.SetActive(true);

            record = ConfigManager.instance.configMission.GetRecordByKeySearch(index + 1);
            data = DataAPIControler.instance.GetMissionDataByID(index + 1);
            int totalDone = 0;

            if(data != null)
            {
                totalDone++;
                for(int i = 0; i < record.lsMissionNeed.Count; i++)
                {
                    if (data.goals[i] < record.lsMissionNeed[i])
                       totalDone++;
                }    
            }

            SetAchives(totalDone);
        }
        else
        {
            lockObj.SetActive(true);
            unLockObj.SetActive(false);
            SetAchives(0);
        } 
        
            
    }

    private void SetAchives(int total)
    {
       for(int i = 0; i < stars.Count; i++)
       {
            if (i == total)
                stars[i].SetActive(true);
            else
                stars[i].SetActive(false);
       }    
    }
        

    private void LateUpdate ()
    {
        if (!BufferCameraControl.isMove)
            return;

        Vector3 pos = transCam.InverseTransformPoint(trans.position);
       if (pos.x + width<=0)
        {
            // nam ngoai ria  trai , 
            if(BufferCameraControl.sideMove>0)
            {
                // an di
                OnInvisibleitem();
            }
        }
       else if(pos.x - width > 0)
        {
            // nam ngoai ria  phai , 
            if (BufferCameraControl.sideMove < 0)
            {
                // an di
                OnInvisibleitem();
            }
        }

    }
    private void OnInvisibleitem()
    {
       
        MiniMapSetting.instance.ItemInvisible(this);
       
    }

    public void OnMouseDown()
    {
        if (!isUnlock)
            return;

        if(record == null)
        {
            Debug.LogError("Hien tang dang trong qua trinh them mission");
            return;
        }
        DialogCreateMissionParam param = new DialogCreateMissionParam();
        param.cf = record;
        param.data = data;
        param.isPause = false;
        DialogManager.instance.ShowDialog(DialogIndex.DialogCreateMission,param);
        
    }
}

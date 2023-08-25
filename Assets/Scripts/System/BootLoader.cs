using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : Singleton<BootLoader>
{
    // Start is called before the first frame update
    void Start()
    {
        ConfigManager.instance.InitStart(() =>
        {

            InitConfigDone();


        });
    }

    private void InitConfigDone()
    {
        DataAPIControler.instance.OnInit(() =>
        {
            LoadSceneManager.instance.LoadSceneByIndex(1, () => {

                ViewManager.instance.OnSwitchView(ViewIndex.HomeView);
            });
        });
     
    }
}

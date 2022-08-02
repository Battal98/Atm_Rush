using Keys;
using UnityEngine;

namespace Controllers
{
    public class LoadDataController:MonoBehaviour
    {
        #region Self Veriables

        #region Privete Veriables
        public LoadGameDataParams LoadDataParams;
        
        #endregion

        #endregion

        public void LoadData()
        {
            if (ES3.FileExists())
            {
                LoadDataParams.NewLevel = ES3.Load<int>("Level");
                LoadDataParams.NewInstantiateLevel = ES3.Load<int>("InstantiateLevel");

                LoadDataParams.NewTotalWealth = ES3.Load<float>("TotalWealth");

                LoadDataParams.NewStackLevel = ES3.Load<int>("StackLevel");
                LoadDataParams.NewStackLevelPrice = ES3.Load<int>("StackPrice");
                LoadDataParams.NewInCome = ES3.Load<int>("InCome");
                LoadDataParams.NewInComePrice = ES3.Load<int>("InComePrice");

                /*if (LoadDataParams.NewCharButtonName == null)
                {
                    LoadDataParams.NewCharButtonName = "Black";
                }
                else
                {
                    LoadDataParams.NewCharButtonName = ES3.Load<string>("CharButtonName");
                }*/

            }
            








            //if (saveDataParams.SFX != null) ES3.Save("SFX", saveDataParams.SFX);
            //if (saveDataParams.VFX != null) ES3.Save("VFX", saveDataParams.VFX);

        }
    } 
}

using Keys;
using UnityEngine;


namespace Controllers
{
    public class SaveDataController:MonoBehaviour
    {
        #region Self Variables

        public SaveGameDataParams SaveDataParams;

        #region Serialized Variables

        //[SerializeField] private List<GameObject> SaveObject;

        #endregion

        #endregion
        public void SaveData()
        {
            if (SaveDataParams.Level >= 0) ES3.Save("Level", SaveDataParams.Level);
            if (SaveDataParams.TotalWealth >= 0) ES3.Save("TotalWealth", SaveDataParams.TotalWealth);
            if (SaveDataParams.StackLevel >= 0) ES3.Save("StackLevel", SaveDataParams.StackLevel);
            if (SaveDataParams.StackLevelPrice >= 0) ES3.Save("StackPrice", SaveDataParams.StackLevelPrice);
            if (SaveDataParams.InComeLevel >= 0) ES3.Save("InCome", SaveDataParams.InComeLevel);
            if (SaveDataParams.InComePrice >= 0) ES3.Save("InComePrice", SaveDataParams.InComePrice);
            if (SaveDataParams.InstantiateLevel >= 0) ES3.Save("InstantiateLevel", SaveDataParams.InstantiateLevel);
            /*if (SaveDataParams.CharButtonName != null) ES3.Save("CharButtonName", SaveDataParams.CharButtonName);*/
            //if (saveDataParams.SFX != null) ES3.Save("SFX", saveDataParams.SFX);
            //if (saveDataParams.VFX != null) ES3.Save("VFX", saveDataParams.VFX);
        }

    } 
}

using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class RecordsManager : Base.Singleton<RecordsManager>
    {

        public float GetBestTime()
        {
            return (!PlayerPrefs.HasKey(Utilities.Constant.Player.TIME))? float.MaxValue :
                PlayerPrefs.GetFloat(Utilities.Constant.Player.TIME);
        }

        public void SaveBestTime(float time)
        {
            if(GetBestTime() <= time)
                return;
            PlayerPrefs.SetFloat(Utilities.Constant.Player.TIME, time);
        }
    }
}

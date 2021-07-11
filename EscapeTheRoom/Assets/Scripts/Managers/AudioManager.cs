using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class AudioManager : Base.Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _background;
        [SerializeField] private AudioSource _win;


        public void PlanWin()
        {
            _win.Play();
        }

        public void PlayBackground()
        {
            _background.Play();
        }

        public void StopBackground()
        {
            _background.Stop();
        }
    }

}

using WMPLib;
using GXA.Properties;

public partial class Audio
{
    internal WindowsMediaPlayer WMP; //Медиа плеер. Инициализируется в конструкторе формы

    public Audio ( )
    {
        WMP = new WindowsMediaPlayer();
        WMP.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(WMP_PlayStateChange);
    }

    void WMP_PlayStateChange (int NewState)
    {
        if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
        {
            //какие-то действия

        }
        else if (WMP.playState == WMPPlayState.wmppsStopped)
        {
            //какие-то действия
            WMP.settings.volume = 100; //громкость выставляем например
            WMP.URL = "37f61c1e3f386e.mp3"; //присвоить путь к музыке плееру. 
            //Например к радиостанции. Но также можно и к аудио файлу на твоём ПК.
            //Но для воспроизведения какого либо формата в системе должны быть соответствующие кодеки
            WMP.controls.play(); //Воспроизводим музыку
        }
    }
}
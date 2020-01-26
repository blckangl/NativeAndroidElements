using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeAndroidElements;

public class NativeUnityTest : MonoBehaviour
{
    public void ShowAlert()
    {
        Alert alertDiaolog = new Alert();
        alertDiaolog.onFail += () => { Toast.Show("adding function to delegate", Toast.LENGTH_LONG); };
        alertDiaolog.onFailLitener(() => { Toast.Show("adding function to delegate", Toast.LENGTH_LONG); });
        alertDiaolog.onSucessLitener(() => { Toast.Show("on sucess from event listener", Toast.LENGTH_LONG); });
        alertDiaolog.Show("title", "your message", "button 1");
    }

    public void ShowToast()
    {
        Toast.Show("test", Toast.LENGTH_LONG);
    }

    public void ShowIntent()
    {
        TextIntent intent = new TextIntent();
        intent.ShareText("share subject", "share message", "share your score");
    }

    public void ShowCalendar()
    {
        Calendar cal = new Calendar();
        cal.onSelectListener(() => { Toast.Show(cal.getSelectedDate().ToString(), Toast.LENGTH_LONG); });
        cal.Show();
    }
}
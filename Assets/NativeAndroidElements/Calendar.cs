using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NativeAndroidElements
{
    public class Calendar
    {
        private static DateTime selectedDate = DateTime.Now;

        public delegate void ResponseDelegate();

        public ResponseDelegate onSelect;

        public Calendar()
        {
        }

        public Calendar(DateTime initDate)
        {
            selectedDate = initDate;
        }

        public void onSelectListener(Action callback)
        {
            onSelect = new ResponseDelegate(callback);
        }

        public DateTime getSelectedDate()
        {
            return selectedDate;
        }

        class DateCallback : AndroidJavaProxy
        {
            private Calendar mDialog;

            public DateCallback(Calendar d) : base("android.app.DatePickerDialog$OnDateSetListener")
            {
                mDialog = d;
            }

            void onDateSet(AndroidJavaObject view, int year, int monthOfYear, int dayOfMonth)
            {
                selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                mDialog.onSelect.Invoke();
            }
        }

        public void Show()
        {
            AndroidJavaClass unityActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityActivity.GetStatic<AndroidJavaObject>
                ("currentActivity");
            activity.Call("runOnUiThread",
                new AndroidJavaRunnable(() =>
                {
                    new AndroidJavaObject("android.app.DatePickerDialog", activity, new DateCallback(this),
                        selectedDate.Year, selectedDate.Month - 1, selectedDate.Day).Call("show");
                }));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Android.Provider;

namespace Helios
{


    public class TimePickerFragment : DialogFragment,
                              TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "Y:" + typeof(TimePickerFragment).Name.ToUpper();

        Action<TimeSpan> _timeSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<TimeSpan> onTimeSet)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag._timeSelectedHandler = onTimeSet;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Calendar c = Calendar.Instance;
            int hour = c.Get(CalendarField.HourOfDay);
            int minute = c.Get(CalendarField.Minute);
            bool is24HourView = true;
            TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                           this,
                                                           hour,
                                                           minute,
                                                           is24HourView);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            TimeSpan selectedTime = new TimeSpan(hourOfDay, minute, 00);

            _timeSelectedHandler(selectedTime);
            Intent i = new Intent(AlarmClock.ActionSetAlarm);
            i.PutExtra(AlarmClock.ExtraHour, hourOfDay);
            i.PutExtra(AlarmClock.ExtraMinutes, minute);

            StartActivity(i);
        }

    }
}

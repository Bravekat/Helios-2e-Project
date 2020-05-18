using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Widget;
using Android.OS;
using Android.Provider;

namespace Helios
{
    [Activity(Label = "Helios", MainLauncher = true, Icon = "@drawable/xs")]
    public class MainActivity : FragmentActivity
    {
        private ViewPager mViewPager;
        private SlidingTabScrollView mScrollView;
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            Button button_alarmOn = FindViewById<Button>(Resource.Id.button_alarmOn);
            Button button_alarmOff = FindViewById<Button>(Resource.Id.button_alarmOff);

            mViewPager.Adapter = new SamplePagerAdapter(SupportFragmentManager);
            mScrollView.ViewPager = mViewPager;

            if (button_alarmOn != null)
            {
                button_alarmOn.Click += (sender, e) =>
                {

                    openDialog(button_alarmOn);

                };
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public void openDialog(Button button)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (System.TimeSpan time)
            {
                button.Text = time.ToString();
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);

        }
    }

    public class SamplePagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> mFragmentHolder;

        public SamplePagerAdapter(Android.Support.V4.App.FragmentManager fragManager) : base(fragManager)
        {
            mFragmentHolder = new List<Android.Support.V4.App.Fragment>();
            mFragmentHolder.Add(new Fragment1());
            mFragmentHolder.Add(new Fragment2());
            mFragmentHolder.Add(new Fragment3());
        }

        public override int Count
        {
            get { return mFragmentHolder.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }
    }

    public class Fragment1 : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Frag1Layout, container, false);

            LinearLayout viewPager = view.FindViewById<LinearLayout>(Resource.Id.viewPager);
            Button refreshButton = view.FindViewById<Button>(Resource.Id.refreshButton);
            TextView arduinoStatus = view.FindViewById<TextView>(Resource.Id.arduinoStatus);
            EditText editIP = view.FindViewById<EditText>(Resource.Id.editIP);
            EditText editPort = view.FindViewById<EditText>(Resource.Id.editPort);
            TextView sensorValueStatus = view.FindViewById<TextView>(Resource.Id.sensorValueStatus);

            refreshButton.Click += (s, arg) =>
            {
                Activity.Recreate();
            };

            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Home";
        }
    }

    public class Fragment2 : Android.Support.V4.App.Fragment
    {
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            var view = inflater.Inflate(Resource.Layout.Frag2Layout, container, false);

            var button_alarmOn = view.FindViewById<Button>(Resource.Id.button_alarmOn);
            var button_alarmOff = view.FindViewById<Button>(Resource.Id.button_alarmOff);

            if (button_alarmOn != null)
            {
                button_alarmOn.Click += (sender, e) =>
                {

                    ((MainActivity)Activity).openDialog(button_alarmOn);

                };
            }
            if (button_alarmOff != null)
            {
                button_alarmOff.Click += (sender, e) =>
                {

                    Intent m = new Intent(AlarmClock.ActionShowAlarms);
                    StartActivity(m);
                    button_alarmOn.Text = "Zet Alarm";


                };
            }
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Alarm";
        }
    }

    public class Fragment3 : Android.Support.V4.App.Fragment
    {
        private Button mKlikButton1;
        private Button mKlikButton2;
        private Button mKlikButton3;
        private Button mKlikButton4;
        private Button mKlikButton5;
        private Button mKlikButton6;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Frag3Layout, container, false);


            mKlikButton1 = view.FindViewById<Button>(Resource.Id.KlikButton1);
            mKlikButton2 = view.FindViewById<Button>(Resource.Id.KlikButton2);
            mKlikButton3 = view.FindViewById<Button>(Resource.Id.KlikButton3);
            mKlikButton4 = view.FindViewById<Button>(Resource.Id.KlikButton4);
            mKlikButton5 = view.FindViewById<Button>(Resource.Id.KlikButton5);
            mKlikButton6 = view.FindViewById<Button>(Resource.Id.KlikButton6);

            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Klik ON - Klik OFF";
        }
    }
}


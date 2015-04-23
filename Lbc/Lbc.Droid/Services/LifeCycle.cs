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
using Lbc.Services;
using Xamarin.Forms;
using Lbc.Droid.Services;


[assembly: Dependency(typeof(LifeCycle))]
namespace Lbc.Droid.Services {
    public class LifeCycle : ILifeCycle {
        public void Exit() {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
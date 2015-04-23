using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Lbc.Services;
using Xamarin.Forms;
using Lbc.iOS.Services;

[assembly:Dependency(typeof(LifeCycle))]
namespace Lbc.iOS.Services {
    public class LifeCycle : ILifeCycle {
        public void Exit() {
            //Do Nothing.
        }
    }
}
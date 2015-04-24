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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Lbc.Droid.Renders;
using Android.Graphics.Drawables;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Lbc.Droid.Renders {
    public class TabbedPageRender : TabbedRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TabbedPage> e) {
            base.OnElementChanged(e);

            var a = this.Context as Activity;
            var drb = new Android.Graphics.Drawables.ColorDrawable(Color.White.ToAndroid());
            a.ActionBar.SetStackedBackgroundDrawable(drb);
        }

    }
}
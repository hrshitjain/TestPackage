using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TestPackageApp.Database;
using System.Linq;

namespace TestPackageApp
{
    public class PacakageListFragment : ListFragment
    {
        string[] packages;
        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var list = await DatabaseService.Instance.GetAllItems<Package>();
            packages = list.Select(package => package.Barcode).ToArray();
            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, packages);
        }
    }
}
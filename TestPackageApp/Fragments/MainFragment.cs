using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace TestPackageApp
{
    public class MainFragment : Fragment
    {
        View view;
        Button btnShowPackage;
        Button btnAddPackage;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.content_main, container, false);
            btnAddPackage = view.FindViewById<Button>(Resource.Id.btnAddPackage);
            btnShowPackage = view.FindViewById<Button>(Resource.Id.btnShowPackages);

            btnShowPackage.Click += (sender, eventargs) => LoadFragment<PacakageListFragment>(new PacakageListFragment());
            btnAddPackage.Click += (sender, eventargs) => LoadFragment<AddPackageFragment>(new AddPackageFragment());
            return view;
        }

        private void LoadFragment<T>(T fragment) where T : Fragment
        {
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.frameLayout, fragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();
        }
    }
}
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using TestPackageApp.Database;

namespace TestPackageApp
{
    public class AddPackageFragment : Fragment
    {
        private View view;
        private Button btnReset, btnSave;
        private EditText txtBarcode, txtWidth, txtHeight, txtDepth;
        private int[] ids;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.add_package, container, false);
            btnReset = view.FindViewById<Button>(Resource.Id.btnReset);
            btnSave = view.FindViewById<Button>(Resource.Id.btnSave);
            txtBarcode = view.FindViewById<EditText>(Resource.Id.txtbarcode);
            txtWidth = view.FindViewById<EditText>(Resource.Id.txtwidth);
            txtHeight = view.FindViewById<EditText>(Resource.Id.txtheight);
            txtDepth = view.FindViewById<EditText>(Resource.Id.txtdepth);

            btnSave.Click += BtnSave_Click;
            btnReset.Click += BtnReset_Click;
            ids = new int[]
            {
                Resource.Id.txtbarcode,
                Resource.Id.txtwidth,
                Resource.Id.txtheight,
                Resource.Id.txtdepth,
            };
            return view;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtHeight.Text = string.Empty;
            txtDepth.Text = string.Empty;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsEmptyFields(ids))
            {
                string barcode = txtBarcode.Text;
                var width = Convert.ToDouble(txtWidth.Text);
                var height = Convert.ToDouble(txtHeight.Text);
                var depth = Convert.ToDouble(txtDepth.Text);
                Package package = new Package()
                {
                    Barcode = barcode,
                    Width = width,
                    Height = height,
                    Depth = depth
                };
                await DatabaseService.Instance.InsertItem<Package>(package);
                Toast.MakeText(Activity, $"Dimm({width} x {height} x {depth}) {barcode} saved", ToastLength.Short).Show();
            }
        }

        public override void OnDestroyView()
        {
            btnSave.Click -= BtnSave_Click;
            btnReset.Click -= BtnReset_Click;
            base.OnDestroyView();
        }

        public bool IsEmptyFields(int[] ids)
        {
            bool isEmpty = false;

            foreach (int id in ids)
            {
                EditText et = (EditText)view.FindViewById(id);

                if (TextUtils.IsEmpty(et.Text.ToString()))
                {
                    et.Error = "Must enter Value";
                    isEmpty = true;
                }
            }
            return isEmpty;
        }

    }
}
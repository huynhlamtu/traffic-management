using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Data;
using App1.Models;
using App1.Services;
using App1.WebServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.UtilsPages.TraCuuLuatPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListLuat : ContentPage
    {
        public ListLuat()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //List<Note> notes = await App.GetDatabase.GetNotesAsync();
            //listView.ItemsSource = notes.OrderBy(d => d.Date).ToList();
            GetData();
        }

        async void GetData()
        {
            List<Luat> luats = await new LuatWebServices().GetLuatList();
            listViewLuat.ItemsSource = luats.OrderBy(d => d.ma_luat).ToList();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new LuatDetail
                {
                    BindingContext = e.SelectedItem as Luat
                });
            }


        }
    }
}
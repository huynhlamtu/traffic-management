﻿using App1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Data;


namespace App1.UtilsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        //public ObservableCollection<mainNavItems> MySource { get; set; }
        public Home()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //List<Note> notes = await App.GetDatabase.GetNotesAsync();
            //listView.ItemsSource = notes.OrderBy(d => d.Date).ToList();
            getDanById();
        }

        async void getDanById()
        {
            Dans dan = await new DansWebService().GetDanById(Int16.Parse(Application.Current.Properties["userId"].ToString()));
            userName.Text = "Welcome, " + dan.ho_ten;
        }

        void funcHandleNopPhat(object sender, EventArgs e)
        {
        }

        void funcHandleSignOut(object sender, EventArgs e)
        {
        }
    }
}
﻿using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.DataModels.Page;
using HotelsApp.Core.ViewModels.Items;
using System;

namespace HotelsApp.Core.ViewModels
{
    public class HotelEditPageViewModel : BasePageViewModel
    {
        string fullImagePath;
        HotelViewModel hotel;

        public HotelViewModel Hotel
        {
            get => hotel;
            set
            {
                if (hotel != value)
                {
                    hotel = value;
                    OnPropertyChanged(nameof(Hotel));
                }
            }
        }
        public ObservableCollection<FacilityViewModel> Facilities { get; }

        public ICommand SaveCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand ManageRoomsCommand { get; set; }
        public ICommand LoadReportsCommand { get; set; }
        
        public HotelEditPageViewModel()
        {
            Facilities = new ObservableCollection<FacilityViewModel>();
        }
        
        protected override void InitializeCommands()
        {
            SaveCommand = new RelayCommand(Save);
            ManageRoomsCommand = new RelayCommand(ManageRooms);
            LoadReportsCommand = new RelayCommand(LoadReports);
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void ManageRooms()
        {
            IoCContainer.Application.GoTo(ApplicationPage.RoomsManagerPage, Hotel);
        }
        public void Refresh()
        {
            if (hotel == null) return;
            Facilities.Clear();
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelFacilitiesFlags(Hotel.Id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            foreach (DataRow row in dataTable.Rows)
            {
                Facilities.Add(new FacilityViewModel(ItemsFactory.GetFacility(row)));
            }            
        }
        public void Save()
        {
            var query = SQLQuery.UpdateHotel(Hotel.GetInternalData());
            IoCContainer.Application.ExecuteTableQuery(query, out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            query = SQLQuery.UpdateFacilities(Hotel.Id, Facilities);
            if (query != null)
            {
                IoCContainer.Application.ExecuteTableQuery(query, out error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
            }
            if (fullImagePath != null && !IoCContainer.UI.SaveImage(fullImagePath))
            {
                IoCContainer.Application.ShowMessage("Can't upload file");
            }
            else IoCContainer.Application.ShowMessage("Changes committed successfully");
        } 
        public void LoadReports()
        {
            IoCContainer.Application.GoTo(ApplicationPage.ReportsPage, Hotel);
        }
        public void OpenFile()
        {
            var fileInfo = IoCContainer.UI.LoadFile("images|*.png;*.jpg;*.bmp;*.jpeg");
            if (fileInfo != null)
            {
                Hotel.Image = fileInfo.Name;
                fullImagePath = fileInfo.FullName;
            }
        }
        #endregion
    }
}
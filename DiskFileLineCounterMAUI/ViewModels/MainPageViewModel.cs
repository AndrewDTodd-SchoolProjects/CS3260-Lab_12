using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DiskFileLineCounterMAUI.Services;

namespace DiskFileLineCounterMAUI.ViewModels
{
    #region CONSTRUCTORS
    public partial class MainPageViewModel : BaseViewModel
    {
        #region MEMBERS
        [ObservableProperty]
        int activeLineCount;
        [ObservableProperty]
        int totalCount;
        [ObservableProperty]
        string textFileLocation = null;
        #endregion

        #region CONSTRUCTORS
        public MainPageViewModel()
        {
            Title = "Line Counter";
        }
        #endregion

        #region PRIVATE_METHODS_ASYNC
        [RelayCommand]
        async Task GetLineCountAsync()
        {
            if (IsBusy || string.IsNullOrEmpty(textFileLocation)) return;

            try
            {
                IsBusy = true;

                TotalCount = await LineCounterService.CountLinesAsync(TextFileLocation, new Progress<int>(count => ActiveLineCount = count));
                return;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error Reading Text!", $"An error occured while trying to count the text file's lines\n{ex.Message}", "OK");
                ActiveLineCount = 0;
                TotalCount = 0;
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task BrowseFiles(PickOptions options)
        {
            if (IsBusy) return;

            try 
            {
                IsBusy = true;

                var result = await FilePicker.Default.PickAsync(options);
                if(result != null)
                {
                    TextFileLocation = result.FullPath;
                }
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error With File Browser!", $"An error occured while trying to access or use the file browser\n{ex.Message}", "OK");
                return;
            }
            finally
            { 
                IsBusy = false; 
            }
        }
        #endregion
    }
    #endregion
}

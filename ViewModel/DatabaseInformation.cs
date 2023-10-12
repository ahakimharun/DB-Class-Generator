using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DB_Class_Generator.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DB_Class_Generator.ViewModel
{
    public partial class DatabaseInformation : ObservableObject
    {
        [ObservableProperty]
        private string databaseName = "Zivy";
        [ObservableProperty]
        private string serverURL = "localhost";
        [ObservableProperty]
        private uint? portNumber = 3306;
        [ObservableProperty]
        private string username = "root";
        // See here for implementation of the Password Box: https://stackoverflow.com/questions/1483892/how-to-bind-to-a-passwordbox-in-mvvm
        [ObservableProperty]
        private SecureString? securePassword;

        [ObservableProperty]
        private string selectedTable = "";

        // See here for binding cheatsheet
        // https://www.nbdtech.com/Free/WpfBinding.pdf

        // See here for this amazing implementation with CommunityToolkit.MVVM:
        // https://devblogs.microsoft.com/ifdef-windows/announcing-net-community-toolkit-v8-0-0-preview-3/#partial-property-changed-methods

        partial void OnSelectedTableChanged(string value)
        {

#pragma warning disable CS4014 // Asynchronously receiving the table columns does not affect performance of the user interface
            ShowTableColumns();
#pragma warning restore CS4014 
        }

        public ObservableCollection<string> DatabaseTables { get; private set; } = new(); 
        public ObservableCollection<TableFields> TableColumns { get; private set; } = new();
        
        [RelayCommand]
        private async Task ShowTables()
        {
            try 
            {
                // I know this is completely unsecured - Having a password in memory is a big no-no
                // I need to find a better way of transferring a password to MySql - But for now this will have to do...
                // Reference on this post from StackOverflow: https://stackoverflow.com/questions/818704/how-to-convert-securestring-to-system-string
                string plaintextpassword = new System.Net.NetworkCredential(string.Empty, SecurePassword).Password;
                MySqlDatabaseConnection connection = new MySqlDatabaseConnection(ServerURL, PortNumber, DatabaseName, Username, plaintextpassword);
                DatabaseTables = new ObservableCollection<string>(await connection.ListTables<string>());
                OnPropertyChanged(nameof(DatabaseTables));
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }

        public async Task ShowTableColumns()
        {
            try
            {
                string plaintextpassword = new System.Net.NetworkCredential(string.Empty, SecurePassword).Password;
                MySqlDatabaseConnection connection = new MySqlDatabaseConnection(ServerURL, PortNumber, DatabaseName, Username, plaintextpassword);
                TableColumns = new ObservableCollection<TableFields>(await connection.GetTableFields<TableFields>(SelectedTable, DatabaseName));
                OnPropertyChanged(nameof(TableColumns));
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }
    }
}

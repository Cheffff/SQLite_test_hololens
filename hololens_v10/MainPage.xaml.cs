using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLitePCL;
using Windows.Storage; //Files, Folders 


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace hololens_v10
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public SQLiteConnection dbConnection = new SQLiteConnection("virtual_deck.db");


        private void btnFill_List_Click(object sender, RoutedEventArgs e)
        {
            var items = new List<String>();

            string sSQL = @"SELECT [name],[description] FROM games";
            ISQLiteStatement dbState = dbConnection.Prepare(sSQL);
            while (dbState.Step() == SQLiteResult.ROW)
            {
                string sFoldername = dbState["name"] as string;
                string sPath = dbState["description"] as string;

                //< correction > 
                sFoldername = sFoldername.Replace("''", "'");
                sPath = sPath.Replace("''", "'");
                //</ correction > 

                items.Add(sFoldername + "; " + sPath);
            }
            ctlList.ItemsSource = items;
        }

        private async void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder local_CacheFolder = ApplicationData.Current.LocalCacheFolder;
            await Windows.System.Launcher.LaunchFolderAsync(local_CacheFolder);
            //</ Open App Cache Folder > 

            // Debug.WriteLine();

        }
    }
}

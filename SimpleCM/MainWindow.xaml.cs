﻿using SimpleCM.Data;
using SimpleCM.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SimpleCM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditWindow editWindow = new EditWindow
                {
                    Owner = this
                };
                editWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.E("", "show add contract window error:" + ex.Message);
            }
        }

        private void DeleteContract_Click(object sender, RoutedEventArgs e)
        {
            Contract contract = (Contract)contract_list_box.SelectedItem;
            Contracts.Instance.Remove(contract);
            ContractDB.Instance.DeleteItem(contract);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Addtion_list_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string v = (string)(((ListBox)sender).SelectedItem);
            if (File.Exists(v))
            {
                Process.Start(new ProcessStartInfo(v));
            }            
        }

        private void Modify_btn_Click(object sender, RoutedEventArgs e)
        {
            Contract contract = (Contract)contract_list_box.SelectedItem;
            if (contract != null)
            {
                EditWindow ew = new EditWindow();
                ew.SetData(contract);
                ew.ShowDialog();
            }

        }

        private void Search_btn_click(object sender, RoutedEventArgs e)
        {
            string year = year_search_text.Text;
            string type = type_search_text.Text;
            string key = key_search_text.Text;
            ContractDB.Instance.Search(year, type, key);
        }

        private void All_btn_click(object sender, RoutedEventArgs e)
        {
            ContractDB.Instance.LoadAll();
        }
    }
}

﻿using DbAccessDatabase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbAccessWpfApp5
{
    /// <summary>
    /// EditRecordWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class EditRecordWindow : Window
    {
        private PaymentRecord _Record = null;

        public Boolean Deleted { get; set; } = false;

        public EditRecordWindow()
        {
            InitializeComponent();
        }
        public EditRecordWindow(PaymentRecord record)
        {
            InitializeComponent();
            _Record = record;
            this.SetControlValue();
        }
        private void SetControlValue()
        {
            this.TitleTextbox.Text = _Record.Title;
            this.DateTextbox.Text = _Record.Date.ToString("yyyy/MM/dd");
            this.PriceTextbox.Text = _Record.Price.ToString();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //入力値の検証処理
            if (DateTime.TryParse(this.DateTextbox.Text, out var date) == false)
            {
                MessageBox.Show("日付の値が間違っています！日付に変換可能な文字を入力してください。(2020/08/03)");
                return;
            }
            if (Int32.TryParse(this.PriceTextbox.Text, out var price) == false)
            {
                MessageBox.Show("価格の値が間違っています！数字を入力してください。");
                return;
            }

            var db = new Database();
            db.ConnectionString = System.IO.File.ReadAllText("C:\\GitHub\\ConnectionString.txt");

            //入力値を元にPaymentRecordのインスタンスを作成
            if (_Record == null)
            {
                _Record = new PaymentRecord();
                _Record.Title = this.TitleTextbox.Text;
                _Record.Date = date;
                _Record.Price = price;
                db.Insert(_Record);
            }
            else
            {
                _Record.Title = this.TitleTextbox.Text;
                _Record.Date = date;
                _Record.Price = price;
                db.Update(_Record);
            }
            this.Close();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new Database();
            db.ConnectionString = System.IO.File.ReadAllText("C:\\GitHub\\ConnectionString.txt");
            db.Delete(_Record.PaymentCD);

            this.Deleted = true;

            this.Close();
        }
    }
}

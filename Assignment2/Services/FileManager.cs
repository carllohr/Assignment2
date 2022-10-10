using Assignment2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Services
{
    internal interface IFileManager
    {
        public void Read(ref ObservableCollection<Contact> list);
        public void Save(ObservableCollection<Contact> list);
        
    }
    internal class FileManager : IFileManager
    {
        private string _filePath;
        private ObservableCollection<Contact> _contacts;
        public FileManager(string filePath)
        {
            _filePath = filePath;
            _contacts = new ObservableCollection<Contact>();
        }

        public void Read(ref ObservableCollection<Contact> list) // reads data from json file
        {
            try
            {
                using var sr = new StreamReader(_filePath);
                list = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(sr.ReadToEnd());
            }
            catch
            {

            }
        }

        public void Save(ObservableCollection<Contact> list) // saves contacts to json file
        {
            try
            {
                using var sw = new StreamWriter(_filePath);
                sw.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));
            }
            catch
            {

            }
        }
       
        
    } 

}

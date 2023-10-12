using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Class_Generator.ViewModel
{
    public partial class TableFields : ObservableObject
    {
        [ObservableProperty]
        private string? _field;
        [ObservableProperty]
        public string _fieldtype;
        [ObservableProperty]
        public bool _isnull;
        [ObservableProperty]
        public string _key;
        [ObservableProperty]
        public string _default;
        [ObservableProperty]
        public string _extra;
    }
}

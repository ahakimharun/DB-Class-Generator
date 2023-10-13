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
        private string _type;
        [ObservableProperty]
        private string _null;
        [ObservableProperty]
        private string _key;
        [ObservableProperty]
        private string _default;
        [ObservableProperty]
        private string _extra;
    }
}

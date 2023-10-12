using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Class_Generator.ViewModel
{
    // Shows the table name and properties
    public partial class TableList : ObservableObject
    {
        public TableList()
        {
            _tablelist = new ObservableCollection<string>();
        }

        private ObservableCollection<string> _tablelist;
        public ObservableCollection<string> Tablelist
        {
            get { return _tablelist; }
            set { _tablelist = value; }
        }
    }
}

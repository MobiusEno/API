using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Cost_Management.Models;
using Cost_Management.Services;

namespace Cost_Management.ViewModel
{
    public class MasterView
    {

        //顯示資料陣列
        [DisplayName("搜尋")]
        public string Search { get; set; }
        public ForPaging Paging { get; set; }
        public List<expense_form> DataList { get; set; }
        public List<AspNetUsers> Data1List { get; set; }
        public double NTamount = 0;
    }
}
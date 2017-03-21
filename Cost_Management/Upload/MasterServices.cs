using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cost_Management.Models;
using System.Collections;

namespace Cost_Management.Services
{
    public class MasterServices
    {
        public static string status = "";
        int i = 0;
        //實作db模型
        public Cost_Management.Models.Cost_Management1Entities db = new Models.Cost_Management1Entities();
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<expense_form> GetDataList(ForPaging Paging, string Search)
        {
            //宣告要接受全部搜尋資料的物件
            IQueryable<expense_form> SearchData;
            //判斷搜尋是否為空或Null，用於決定要呼叫取得搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }
            //先排序再根據分頁來回傳所需部分的資料陣列
            return SearchData.OrderByDescending(p => p.formID)
                .Skip((Paging.NowPage - 1) *
                    Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<expense_form> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<expense_form> Data = db.expense_form;
            //計算所需的總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }

        //包含搜尋值的搜尋資料方法
        public IQueryable<expense_form> GetAllDataList(ForPaging Paging, string Search)
        {
            //根據搜尋值來搜尋資料
            IQueryable<expense_form> Data = db.expense_form
                .Where(p => p.formID.Contains(Search) || p.userID.Contains(Search) || p.exp_date.Contains(Search));
            //計算所需的總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        public AspNetUsers GetAutrorityById(string Id)
        {
            //回傳根據標號所取得的資料
            return db.AspNetUsers.Find(Id);
        }
        //藉由標號取得單筆資料的方法
        public expense_form GetDataById(int ID)
        {
            //回傳根據標號所取得的資料
            return db.expense_form.Find(ID);
        }
        //修改表單
        public void Edit(expense_form UpdateData)
        {
            //取得要修改的資料
            expense_form OldData = db.expense_form.Find(UpdateData.ID);
            //修改資料庫裡的值
            OldData.formID = UpdateData.formID;
            OldData.formName = UpdateData.formName;
            OldData.formNumber = UpdateData.formNumber;
            OldData.circleID = UpdateData.circleID;
            OldData.description = UpdateData.description;
            OldData.nation = UpdateData.nation;
            OldData.exp_date = UpdateData.exp_date;
            OldData.location = UpdateData.location;
            OldData.userID = UpdateData.userID;
            OldData.userName = UpdateData.userName;
            OldData.exp_type = UpdateData.exp_type;
            OldData.exp_attr = UpdateData.exp_attr;
            OldData.currency = UpdateData.currency;
            OldData.QTY = UpdateData.QTY;
            OldData.price = UpdateData.price;
            OldData.amount = UpdateData.amount;
            OldData.tax = UpdateData.tax;
            OldData.invoice = UpdateData.invoice;
            OldData.projectCode = UpdateData.projectCode;
            OldData.signuser = UpdateData.signuser;
            OldData.closeuser = UpdateData.closeuser;
            OldData.signtime = UpdateData.signtime;
            OldData.closetime = UpdateData.closetime;
            OldData.SignStatus = UpdateData.SignStatus;
            OldData.note = UpdateData.note;
            OldData.DocumentsNumber = UpdateData.DocumentsNumber;
            OldData.picture = UpdateData.picture;


            //儲存資料庫變更
            db.SaveChanges();
        }
        public void AuthorityEdit(AspNetUsers UpdateData)
        {
            //取得要修改的資料
            AspNetUsers OldData = db.AspNetUsers.Find(UpdateData.Id);
            //修改資料庫裡的值
            OldData.Email = UpdateData.Email;
            OldData.PasswordHash = UpdateData.PasswordHash;
            OldData.SecurityStamp = UpdateData.SecurityStamp;
            OldData.LockoutEnabled = UpdateData.LockoutEnabled;
            OldData.AccessFailedCount = UpdateData.AccessFailedCount;
            OldData.UserName = UpdateData.UserName;
            
            //儲存資料庫變更
            db.SaveChanges();
        }
        //刪除表單
        public void Delete(int ID)
        {
            expense_form DeleteData = db.expense_form.Find(ID);
            db.expense_form.Remove(DeleteData);
            db.SaveChanges();
        }
        //新增表單
        public void DBCreate(string formID, string formName, float formNumber, float circleID, string description, string nation, string exp_date
                                              , string location, string userID, string userName, string exp_type, string exp_attr, string currency, float QTY
                                              , string price, float amount, string tax, string invoice, string projectCode, string lmuser, string note, string picturename0, string picturename1, string picturename2, string DocumentsNumber)
        {

            expense_form NewData = new expense_form();

            NewData.formID = formID;
            NewData.formName = formName;
            NewData.formNumber = formNumber;
            NewData.circleID = circleID;
            NewData.description = description;
            NewData.nation = nation;
            NewData.exp_date = exp_date;
            NewData.location = location;
            NewData.userID = userID;
            NewData.userName = userName;
            NewData.exp_type = exp_type;
            NewData.exp_attr = exp_attr;
            NewData.currency = currency;
            NewData.QTY = QTY;
            NewData.price = price;
            NewData.amount = amount;
            NewData.tax = tax;
            NewData.invoice = invoice;
            NewData.projectCode = projectCode;
            NewData.lmtime = DateTime.Now.ToString();
            NewData.lmuser = lmuser;
            NewData.signtime = DateTime.Now;
            NewData.signuser = "nosign@";
            NewData.closetime = DateTime.Now;
            NewData.closeuser = "nosign@";
            NewData.note = note;
            NewData.picture = picturename0;
            NewData.picture1 = picturename1;
            NewData.picture2 = picturename2;
            NewData.DocumentsNumber = DocumentsNumber;

            db.expense_form.Add(NewData);

            db.SaveChanges();
        }
        public void DBCreate_1(string form_id, string channel_id, string create_time, AppMetadata value)
        {
            expense_form NewData = new expense_form();
            if (value.FormID == "")
            {
                status += "\nFormID為必填";

            }
            NewData.formID = value.FormID;

            if (value.FormName == "")
            {
                status += "\nFormName為必填";

            }
            NewData.formName = value.FormName;

            if (value.FormNumber == "")
            {
                status += "\nFormNumber為必填";
            }
            NewData.formNumber = Int32.Parse(value.FormNumber);

            if (value.CircleID == "")
            {
                status += "\nCircleID為必填";
            }
            NewData.circleID = Int32.Parse(value.CircleID);

            if (value.FormDescription == "")
            {
                status += "\nFormDescription為必填";
            }
            NewData.description = value.FormDescription;


            Data_1 nation_1 = value.Data.Find(x => x.SubFormName == "國內/國外");
            SubData_1 nation_2 = nation_1.SubData.Find(x => x.Status == "Pressed");
            if (nation_2.Content.ToString() == "")
            {
                status += "\n國內/國外為必填";
            }
            NewData.nation = nation_2.Content.ToString();

            Data_1 exp_date_1 = value.Data.Find(x => x.SubFormName == "消費日期");
            SubData_1 exp_date_2 = exp_date_1.SubData.Find(x => x.Status == "Normal");
            if (exp_date_2.Content.ToString() == "")
            {
                status += "\n消費日期為必填";
            }
            NewData.exp_date = exp_date_2.Content.ToString();

            Data_1 location_1 = value.Data.Find(x => x.SubFormName == "消費城市");
            SubData_1 location_2 = location_1.SubData.Find(x => x.Status == "Normal");
            if (location_2.Content.ToString() == "")
            {
                status += "\n消費城市為必填";
            }
            NewData.location = location_2.Content.ToString();

            Data_1 userID_1 = value.Data.Find(x => x.SubFormName == "姓名");
            SubData_1 userID_2 = userID_1.SubData.Find(x => x.Status == "Normal");
            if (userID_2.Content.ToString() == "")
            {
                status += "\n姓名為必填";
            }
            NewData.userID = userID_2.Content.ToString();

            Data_1 userName_1 = value.Data.Find(x => x.SubFormName == "工號");
            SubData_1 userName_2 = userName_1.SubData.Find(x => x.Status == "Normal");
            if (userName_2.Content.ToString() == "")
            {
                status += "\n工號為必填";
            }
            NewData.userName = userName_2.Content.ToString();

            Data_1 exp_type_1 = value.Data.Find(x => x.SubFormName == "費用類別");
            SubData_1 exp_type_2 = exp_type_1.SubData.Find(x => x.Status == "Normal");
            if (exp_type_2.Content.ToString() == "")
            {
                status += "\n費用類別為必填";
            }
            NewData.exp_type = exp_type_2.Content.ToString();

            Data_1 exp_attr_1 = value.Data.Find(x => x.SubFormName == "費用屬性");
            SubData_1 exp_attr_2 = exp_attr_1.SubData.Find(x => x.Status == "Pressed");
            if (exp_attr_2.Content.ToString() == "")
            {
                status += "\n費用屬性為必填";
            }
            NewData.exp_attr = exp_attr_2.Content.ToString();

            Data_1 currency_1 = value.Data.Find(x => x.SubFormName == "幣別");
            SubData_1 currency_2 = currency_1.SubData.Find(x => x.Status == "Normal");
            if (currency_2.Content.ToString() == "")
            {
                status += "\n幣別為必填";
            }
            NewData.currency = currency_2.Content.ToString();

            Data_1 QTY_1 = value.Data.Find(x => x.SubFormName == "QTY");
            SubData_1 QTY_2 = QTY_1.SubData.Find(x => x.Status == "Normal");

            if (QTY_2.Content.ToString() == "")
            {
                status += "\nQTY為必填";
            }
            else if(int.TryParse(QTY_2.Content.ToString(),out i)==false)
            {
                status += "\nQTY必須為阿拉伯數字";
            }

            NewData.QTY = Int32.Parse(QTY_2.Content.ToString());

            Data_1 price_1 = value.Data.Find(x => x.SubFormName == "原幣總價");
            SubData_1 price_2 = price_1.SubData.Find(x => x.Status == "Normal");
            NewData.price = price_2.Content.ToString();

            Data_1 amount_1 = value.Data.Find(x => x.SubFormName == "台幣總價");
            SubData_1 amount_2 = amount_1.SubData.Find(x => x.Status == "Normal");
            if (amount_2.Content.ToString() == "")
            {
                status += "\n台幣總價為必填";
            }
            NewData.amount = Int32.Parse(amount_2.Content.ToString());

            Data_1 tax_1 = value.Data.Find(x => x.SubFormName == "稅額");
            SubData_1 tax_2 = tax_1.SubData.Find(x => x.Status == "Normal");
            NewData.tax = tax_2.Content.ToString();

            Data_1 invoice_1 = value.Data.Find(x => x.SubFormName == "發票號碼");
            SubData_1 invoice_2 = invoice_1.SubData.Find(x => x.Status == "Normal");
            NewData.invoice = invoice_2.Content.ToString();

            Data_1 projectCode_1 = value.Data.Find(x => x.SubFormName == "專案代號");
            SubData_1 projectCode_2 = projectCode_1.SubData.Find(x => x.Status == "Normal");
            NewData.projectCode = projectCode_2.Content.ToString();

            NewData.lmtime = value.TimeStamp;
            NewData.lmuser = value.UserName;
            DateTime date_1 = new DateTime(2000, 01, 01, 0, 0, 0);
            NewData.signtime = date_1;
            NewData.signuser = "nosign@";
            NewData.closetime = date_1;
            NewData.closeuser = "nosign@";

            Data_1 note_1 = value.Data.Find(x => x.SubFormName == "附註");
            SubData_1 note_2 = note_1.SubData.Find(x => x.Status == "Normal");
            NewData.note = note_2.Content.ToString();

            Data_1 picture_1 = value.Data.Find(x => x.SubFormName == "圖片");

            if (picture_1.SubData[0].Content.ToString() == "null")
            {
                NewData.picture = null;
            }
            else
            {
                NewData.picture = picture_1.SubData[0].Content.ToString();
            }

            if (picture_1.SubData[1].Content.ToString() == "null")
            {
                NewData.picture1 = null;
            }
            else
            {
                NewData.picture1 = picture_1.SubData[1].Content.ToString();
            }

            if (picture_1.SubData[2].Content.ToString() == "null")
            {
                NewData.picture2 = null;
            }
            else
            {
                NewData.picture2 = picture_1.SubData[2].Content.ToString();
            }
            //title傳值
            NewData.DocumentsNumber = "";
            NewData.form_id = form_id;
            NewData.channel_id = channel_id;
            NewData.create_time = create_time;

            db.expense_form.Add(NewData);
            db.SaveChanges();
        }
        public void DBCreate_2(string form_id, string channel_id, string create_time, AppMetadata value)
        {
            
            expense_form NewData = new expense_form();
            for (int i = 0; i < value.Data.Count; i++)
            {
                Data_1 note_1 = value.Data.Find(x => x.Type == "TEXT");
                SubData_1 formName_1 = value.Data[i].SubData.Find(x => x.Status == "Pressed");
                
                if (formName_1 != null)
                {
                    
                    NewData.exp_attr = formName_1.Content;
                    NewData.amount = Int32.Parse(value.Data[i].SubFormDescription.ToString());
                    NewData.note = note_1.SubData[0].Content;
                    NewData.formNumber = 0;//Int32.Parse(value.FormNumber.ToString());
                    NewData.circleID = 0;//Int32.Parse(value.CircleID.ToString()); 
                    NewData.formName = value.FormName;
                    NewData.formID = "0";//value.FormID;
                    NewData.nation = "0";
                    NewData.lmtime = value.TimeStamp;
                    NewData.exp_date = ((((new DateTime(1970, 1, 1)).AddSeconds(Convert.ToInt32(value.TimeStamp))).ToLocalTime())).ToString();
                    NewData.location = "0";
                    NewData.userID = value.UserName;
                    NewData.userName = "0";
                    NewData.description = value.FormDescription;
                    NewData.exp_type = "Bookmeal";
                    NewData.currency = "0";
                    NewData.QTY = 1;
                    NewData.lmuser = value.UserName;
                    NewData.signtime = DateTime.Now;
                    NewData.signuser = "nosign@";
                    NewData.closetime = DateTime.Now;
                    NewData.closeuser = "nosign@";
                    NewData.DocumentsNumber = "";
                    NewData.SignStatus = false;
                    //title傳值
                    NewData.form_id = form_id;
                    NewData.channel_id = channel_id;
                    NewData.create_time = create_time;
                    //儲存DB
                    db.expense_form.Add(NewData);
                    db.SaveChanges();
                }
            }
           

        }
        public List<AspNetUsers> Data1List()
        {
            return db.AspNetUsers.ToList();
        }
           
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DevExpress.Web.Data;
using DevExpress.Web;

public partial class _Default: System.Web.UI.Page {
    public class Person {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Position { get; set; }
    }

    public List<Person> PersonsList
    {
        get
        {
            List<Person> list = Session["source"] as List<Person>;
            if (list == null) {
                list = new List<Person>();
                list.Add(new Person() { ID = 1, FirstName = "Howard", LastName = "Snyder", Position = 1 });
                list.Add(new Person() { ID = 2, FirstName = "Yoshi", LastName = "Latimer", Position = 2 });
                list.Add(new Person() { ID = 3, FirstName = "Rene", LastName = "Phillips", Position = 10 });
                list.Add(new Person() { ID = 4, FirstName = "John", LastName = "Steel", Position = 3 });
                list.Add(new Person() { ID = 5, FirstName = "Jaime", LastName = "Yorres", Position = 7 });
                list.Add(new Person() { ID = 6, FirstName = "Paula", LastName = "Wilson", Position = 9 });
                Session["source"] = list;
            }
            return list;
        }
        set { Session["source"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e) {
        Grid.DataSource = PersonsList;
        Grid.DataBind();
    }
    protected void Grid_RowInserting(object sender, ASPxDataInsertingEventArgs e) {
        InsertNewItem(e.NewValues);
        CancelEditing(e);
    }
    protected void Grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e) {
        UpdateItem(e.Keys, e.NewValues);
        CancelEditing(e);
    }
    protected void Grid_RowDeleting(object sender, ASPxDataDeletingEventArgs e) {
        DeleteItem(e.Keys, e.Values);
        CancelEditing(e);
    }
    protected void Grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e) {
        foreach (var args in e.InsertValues)
            InsertNewItem(args.NewValues);
        foreach (var args in e.UpdateValues)
            UpdateItem(args.Keys, args.NewValues);
        foreach (var args in e.DeleteValues)
            DeleteItem(args.Keys, args.Values);
        e.Handled = true;
    }
    protected Person InsertNewItem(OrderedDictionary newValues) {
        var item = new Person() { ID = PersonsList.Max(t => t.ID)+1 };
        LoadNewValues(item, newValues);
        PersonsList.Add(item);
        return item;
    }
    protected Person UpdateItem(OrderedDictionary keys, OrderedDictionary newValues) {
        var id = Convert.ToInt32(keys["ID"]);
        var item = PersonsList.First(i => i.ID == id);
        LoadNewValues(item, newValues);
        return item;
    }
    protected Person DeleteItem(OrderedDictionary keys, OrderedDictionary values) {
        var id = Convert.ToInt32(keys["ID"]);
        var item = PersonsList.First(i => i.ID == id);
        PersonsList.Remove(item);
        return item;
    }
    
    protected void LoadNewValues(Person item, OrderedDictionary values) {
        item.FirstName = Convert.ToString(values["FirstName"]);
        item.LastName = Convert.ToString(values["LastName"]);
        item.Position = Convert.ToInt32(values["Position"]);
    }
    protected void CancelEditing(CancelEventArgs e) {
        e.Cancel = true;
        Grid.CancelEdit();
    }

}
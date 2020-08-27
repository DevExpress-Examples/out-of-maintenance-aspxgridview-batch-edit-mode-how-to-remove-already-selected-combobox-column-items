<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var deletedIndexArray = [];
        function Combo_OnInit(s, e) {
            for (var i = 0; i < gridView.GetVisibleRowsOnPage() ; i++) {
                var itemID = gridView.batchEditApi.GetCellValue(i, "Position", false);
                var item = s.FindItemByValue(itemID);
                if (item != null)
                    s.RemoveItem(item.index);
            }
        }

        function OnBatchEditStartEditing(s, e) {
            if (e.focusedColumn.fieldName == "Position") {
                var cellValue = e.rowValues[e.focusedColumn.index];
                if (cellValue.value != null) {
                    if (comboInstance.FindItemByValue(cellValue.value) == null)
                        comboInstance.AddItem(cellValue.text, cellValue.value)
                }
            }
        }
        function OnBatchEditEndEditing(s, e) {

            for (var i = 0; i < gridView.GetVisibleRowsOnPage() ; i++) {
                var itemID
                if (i != e.visibleIndex) {
                    if (deletedIndexArray.indexOf(i) == -1)
                        itemID = gridView.batchEditApi.GetCellValue(i, "Position", false);
                }
                else {
                    itemID = e.itemValues[s.GetColumnByField("Position").index].value;
                }
                var item = comboInstance.FindItemByValue(itemID);
                if (item != null)
                    comboInstance.RemoveItem(item.index);
            }
            if (e.visibleIndex <= -1) {
                itemID = e.itemValues[s.GetColumnByField("Position").index].value;
                var item = comboInstance.FindItemByValue(itemID);
                if (item != null)
                    comboInstance.RemoveItem(item.index);
            }
        }

        function OnBatchEditRowDeleting(s, e) {
            var cellValue = e.rowValues[s.GetColumnByField("Position").index];
            deletedIndexArray.push(e.visibleIndex);
            if (cellValue.value != null) {
                if (comboInstance.FindItemByValue(cellValue.value) == null)
                    comboInstance.AddItem(cellValue.text, cellValue.value)
            }
        }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
        <dx:ASPxGridView ID="Grid" runat="server" KeyFieldName="ID" OnBatchUpdate="Grid_BatchUpdate" ClientInstanceName="gridView"
            OnRowInserting="Grid_RowInserting" OnRowUpdating="Grid_RowUpdating" OnRowDeleting="Grid_RowDeleting">
            <ClientSideEvents BatchEditRowDeleting="OnBatchEditRowDeleting" BatchEditEndEditing="OnBatchEditEndEditing"
                BatchEditStartEditing="OnBatchEditStartEditing" />
            <Columns>
                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="true" />
                <dx:GridViewDataTextColumn FieldName="FirstName" />
                <dx:GridViewDataTextColumn FieldName="LastName" />
                <dx:GridViewDataComboBoxColumn FieldName="Position">
                    <PropertiesComboBox ValueType="System.Int32" ClientInstanceName="comboInstance">
                        <ClientSideEvents Init="Combo_OnInit" />
                        <Items>
                            <dx:ListEditItem Value="1" />
                            <dx:ListEditItem Value="2" />
                            <dx:ListEditItem Value="3" />
                            <dx:ListEditItem Value="4" />
                            <dx:ListEditItem Value="5" />
                            <dx:ListEditItem Value="6" />
                            <dx:ListEditItem Value="7" />
                            <dx:ListEditItem Value="8" />
                            <dx:ListEditItem Value="9" />
                            <dx:ListEditItem Value="10" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsEditing Mode="Batch" />
        </dx:ASPxGridView>
    </form>
</body>
</html>

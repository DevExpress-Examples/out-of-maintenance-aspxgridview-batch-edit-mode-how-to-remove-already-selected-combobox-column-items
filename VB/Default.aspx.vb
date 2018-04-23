Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Web.Data
Imports DevExpress.Web

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Public Class Person
        Public Property ID() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Position() As Integer
    End Class

    Public Property PersonsList() As List(Of Person)
        Get
            Dim list As List(Of Person) = TryCast(Session("source"), List(Of Person))
            If list Is Nothing Then
                list = New List(Of Person)()
                list.Add(New Person() With {.ID = 1, .FirstName = "Howard", .LastName = "Snyder", .Position = 1})
                list.Add(New Person() With {.ID = 2, .FirstName = "Yoshi", .LastName = "Latimer", .Position = 2})
                list.Add(New Person() With {.ID = 3, .FirstName = "Rene", .LastName = "Phillips", .Position = 10})
                list.Add(New Person() With {.ID = 4, .FirstName = "John", .LastName = "Steel", .Position = 3})
                list.Add(New Person() With {.ID = 5, .FirstName = "Jaime", .LastName = "Yorres", .Position = 7})
                list.Add(New Person() With {.ID = 6, .FirstName = "Paula", .LastName = "Wilson", .Position = 9})
                Session("source") = list
            End If
            Return list
        End Get
        Set(ByVal value As List(Of Person))
            Session("source") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DataSource = PersonsList
        Grid.DataBind()
    End Sub
    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
        InsertNewItem(e.NewValues)
        CancelEditing(e)
    End Sub
    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
        UpdateItem(e.Keys, e.NewValues)
        CancelEditing(e)
    End Sub
    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As ASPxDataDeletingEventArgs)
        DeleteItem(e.Keys, e.Values)
        CancelEditing(e)
    End Sub
    Protected Sub Grid_BatchUpdate(ByVal sender As Object, ByVal e As ASPxDataBatchUpdateEventArgs)
        For Each args In e.InsertValues
            InsertNewItem(args.NewValues)
        Next args
        For Each args In e.UpdateValues
            UpdateItem(args.Keys, args.NewValues)
        Next args
        For Each args In e.DeleteValues
            DeleteItem(args.Keys, args.Values)
        Next args
        e.Handled = True
    End Sub
    Protected Function InsertNewItem(ByVal newValues As OrderedDictionary) As Person
        Dim item = New Person() With {.ID = PersonsList.Max(Function(t) t.ID)+1}
        LoadNewValues(item, newValues)
        PersonsList.Add(item)
        Return item
    End Function
    Protected Function UpdateItem(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary) As Person

        Dim id_Renamed = Convert.ToInt32(keys("ID"))
        Dim item = PersonsList.First(Function(i) i.ID = id_Renamed)
        LoadNewValues(item, newValues)
        Return item
    End Function
    Protected Function DeleteItem(ByVal keys As OrderedDictionary, ByVal values As OrderedDictionary) As Person

        Dim id_Renamed = Convert.ToInt32(keys("ID"))
        Dim item = PersonsList.First(Function(i) i.ID = id_Renamed)
        PersonsList.Remove(item)
        Return item
    End Function

    Protected Sub LoadNewValues(ByVal item As Person, ByVal values As OrderedDictionary)
        item.FirstName = Convert.ToString(values("FirstName"))
        item.LastName = Convert.ToString(values("LastName"))
        item.Position = Convert.ToInt32(values("Position"))
    End Sub
    Protected Sub CancelEditing(ByVal e As CancelEventArgs)
        e.Cancel = True
        Grid.CancelEdit()
    End Sub

End Class
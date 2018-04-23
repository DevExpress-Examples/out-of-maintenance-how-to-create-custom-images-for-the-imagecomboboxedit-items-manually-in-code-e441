Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' create images for "Hatch Styles" ImageComboBoxEdit            
			CreateHatchStyleItems()


			' create images for "Custom Colors" ImageComboBoxEdit
			CreateCustomColorsItems()

			' in-place editing
			CreatedGridControlSource()

		End Sub

		Private Sub CreatedGridControlSource()
			Dim source As New BindingList(Of Test)()
			source.Add(New Test() With {.Style = HatchStyle.DarkDownwardDiagonal, .CustomColor = Color.Red})
			source.Add(New Test() With {.Style = HatchStyle.DarkVertical, .CustomColor = Color.Yellow})
			source.Add(New Test() With {.Style = HatchStyle.Percent10, .CustomColor = Color.Brown})
			source.Add(New Test() With {.Style = HatchStyle.Trellis, .CustomColor = Color.White})

			gridControl1.DataSource = source
			gridControl1.ForceInitialize()

			Dim reHS As RepositoryItemImageComboBox = TryCast(gridControl1.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)
			reHS.Assign(imageComboBoxEdit1.Properties)
			gridView1.Columns("Style").ColumnEdit = reHS


			Dim reColors As RepositoryItemImageComboBox = TryCast(gridControl1.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)
			reColors.Assign(imageComboBoxEdit2.Properties)
			gridView1.Columns("CustomColor").ColumnEdit = reColors
		End Sub

		Private Sub CreateCustomColorsItems()
			Dim customColors As New List(Of Color)()
			customColors.AddRange(New Color() { Color.Red, Color.Black, Color.Yellow, Color.Green, Color.Blue, Color.Brown, Color.White })
			For Each color As Color In customColors
				imageComboBoxEdit2.Properties.Items.Add(New ImageComboBoxItem(color.ToString(), color))
			Next color

			Dim imagesColors As New ImageList()
			imageComboBoxEdit2.Properties.SmallImages = imagesColors

			For Each item As ImageComboBoxItem In imageComboBoxEdit2.Properties.Items
				Dim iWidth As Integer = 16
				Dim iHeight As Integer = 16
				Dim bmp As New Bitmap(iWidth, iHeight)
				Using g As Graphics = Graphics.FromImage(bmp)
					g.DrawRectangle(New Pen(Color.Black, 2), 0, 0, iWidth, iHeight)
					g.FillRectangle(New SolidBrush(CType(item.Value, Color)), 1, 1, iWidth - 2, iHeight - 2)

				End Using
				imagesColors.Images.Add(bmp)
				item.ImageIndex = imagesColors.Images.Count - 1
			Next item
		End Sub

		Private Sub CreateHatchStyleItems()
			imageComboBoxEdit1.Properties.Items.AddEnum(GetType(System.Drawing.Drawing2D.HatchStyle))

			Dim imagesHS As New ImageList()
			imageComboBoxEdit1.Properties.SmallImages = imagesHS

			For Each item As ImageComboBoxItem In imageComboBoxEdit1.Properties.Items
				Dim iWidth As Integer = 16
				Dim iHeight As Integer = 16
				Dim bmp As New Bitmap(iWidth, iHeight)
				Using g As Graphics = Graphics.FromImage(bmp)
					g.DrawRectangle(New Pen(Color.Black, 2), 0, 0, iWidth, iHeight)
					Dim b As New HatchBrush(CType(item.Value, HatchStyle), Color.Black, Color.White)
					g.FillRectangle(b, 0, 0, iWidth, iHeight)

				End Using
				imagesHS.Images.Add(bmp)
				item.ImageIndex = imagesHS.Images.Count - 1
			Next item
		End Sub
	End Class

	Public Class Test
		Private privateStyle As HatchStyle
		Public Property Style() As HatchStyle
			Get
				Return privateStyle
			End Get
			Set(ByVal value As HatchStyle)
				privateStyle = value
			End Set
		End Property
		Private privateCustomColor As Color
		Public Property CustomColor() As Color
			Get
				Return privateCustomColor
			End Get
			Set(ByVal value As Color)
				privateCustomColor = value
			End Set
		End Property
	End Class
End Namespace

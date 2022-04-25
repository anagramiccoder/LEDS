<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Order
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Order))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.oc = New System.Windows.Forms.TextBox()
        Me.user = New System.Windows.Forms.TextBox()
        Me.cn = New System.Windows.Forms.TextBox()
        Me.od = New System.Windows.Forms.TextBox()
        Me.add = New System.Windows.Forms.TextBox()
        Me.tn = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.stat = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.sf = New System.Windows.Forms.TextBox()
        Me.cost = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Order Code:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Contact No.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Order Details:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 15)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Address:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 252)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Status:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 277)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Tracking No.:"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Location = New System.Drawing.Point(197, 327)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 35)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Return"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'oc
        '
        Me.oc.BackColor = System.Drawing.Color.White
        Me.oc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.oc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.oc.Enabled = False
        Me.oc.ForeColor = System.Drawing.Color.Black
        Me.oc.Location = New System.Drawing.Point(83, 7)
        Me.oc.Name = "oc"
        Me.oc.Size = New System.Drawing.Size(189, 22)
        Me.oc.TabIndex = 4
        Me.oc.Text = "test"
        '
        'user
        '
        Me.user.BackColor = System.Drawing.Color.White
        Me.user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.user.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.user.Enabled = False
        Me.user.ForeColor = System.Drawing.Color.Black
        Me.user.Location = New System.Drawing.Point(83, 34)
        Me.user.Name = "user"
        Me.user.Size = New System.Drawing.Size(189, 22)
        Me.user.TabIndex = 5
        Me.user.Text = "test"
        '
        'cn
        '
        Me.cn.BackColor = System.Drawing.Color.White
        Me.cn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.cn.Enabled = False
        Me.cn.ForeColor = System.Drawing.Color.Black
        Me.cn.Location = New System.Drawing.Point(83, 60)
        Me.cn.MaxLength = 11
        Me.cn.Name = "cn"
        Me.cn.Size = New System.Drawing.Size(189, 22)
        Me.cn.TabIndex = 6
        Me.cn.Text = "test"
        '
        'od
        '
        Me.od.BackColor = System.Drawing.Color.White
        Me.od.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.od.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.od.Enabled = False
        Me.od.ForeColor = System.Drawing.Color.Black
        Me.od.Location = New System.Drawing.Point(9, 104)
        Me.od.Multiline = True
        Me.od.Name = "od"
        Me.od.Size = New System.Drawing.Size(263, 45)
        Me.od.TabIndex = 7
        Me.od.Text = "test"
        '
        'add
        '
        Me.add.BackColor = System.Drawing.Color.White
        Me.add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.add.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.add.Enabled = False
        Me.add.ForeColor = System.Drawing.Color.Black
        Me.add.Location = New System.Drawing.Point(9, 171)
        Me.add.Multiline = True
        Me.add.Name = "add"
        Me.add.Size = New System.Drawing.Size(263, 49)
        Me.add.TabIndex = 8
        Me.add.Text = "test"
        '
        'tn
        '
        Me.tn.BackColor = System.Drawing.Color.White
        Me.tn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tn.Enabled = False
        Me.tn.ForeColor = System.Drawing.Color.Black
        Me.tn.Location = New System.Drawing.Point(83, 272)
        Me.tn.Name = "tn"
        Me.tn.Size = New System.Drawing.Size(189, 22)
        Me.tn.TabIndex = 11
        Me.tn.Text = "test"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Location = New System.Drawing.Point(9, 327)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 35)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Update"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'stat
        '
        Me.stat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.stat.Enabled = False
        Me.stat.FormattingEnabled = True
        Me.stat.Items.AddRange(New Object() {"Processing", "Ready to Ship", "Shipped", "Delivered", "Cancelled", "Returned"})
        Me.stat.Location = New System.Drawing.Point(83, 245)
        Me.stat.Name = "stat"
        Me.stat.Size = New System.Drawing.Size(189, 23)
        Me.stat.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 298)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Total Cost:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 324)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 15)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Ship. Fee:"
        Me.Label9.Visible = False
        '
        'sf
        '
        Me.sf.Location = New System.Drawing.Point(83, 322)
        Me.sf.Name = "sf"
        Me.sf.Size = New System.Drawing.Size(108, 22)
        Me.sf.TabIndex = 13
        Me.sf.Visible = False
        '
        'cost
        '
        Me.cost.Enabled = False
        Me.cost.Location = New System.Drawing.Point(83, 297)
        Me.cost.Name = "cost"
        Me.cost.Size = New System.Drawing.Size(189, 22)
        Me.cost.TabIndex = 12
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(83, 221)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(189, 22)
        Me.TextBox1.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 226)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 15)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Remarks:"
        '
        'Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SandyBrown
        Me.ClientSize = New System.Drawing.Size(284, 368)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cost)
        Me.Controls.Add(Me.sf)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.stat)
        Me.Controls.Add(Me.tn)
        Me.Controls.Add(Me.add)
        Me.Controls.Add(Me.od)
        Me.Controls.Add(Me.cn)
        Me.Controls.Add(Me.user)
        Me.Controls.Add(Me.oc)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("HoloLens MDL2 Assets", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Order"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents oc As System.Windows.Forms.TextBox
    Friend WithEvents user As System.Windows.Forms.TextBox
    Friend WithEvents cn As System.Windows.Forms.TextBox
    Friend WithEvents od As System.Windows.Forms.TextBox
    Friend WithEvents add As System.Windows.Forms.TextBox
    Friend WithEvents tn As System.Windows.Forms.TextBox
    Friend WithEvents stat As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents sf As System.Windows.Forms.TextBox
    Friend WithEvents cost As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label10 As Label
End Class

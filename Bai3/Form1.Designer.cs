namespace Bai3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblLop = new System.Windows.Forms.Label();
            this.lstLop = new System.Windows.Forms.ListBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.colMSSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpThongTinSV = new System.Windows.Forms.GroupBox();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.radNam = new System.Windows.Forms.RadioButton();
            this.radNu = new System.Windows.Forms.RadioButton();
            this.lblMaLop = new System.Windows.Forms.Label();
            this.cboLop = new System.Windows.Forms.ComboBox();
            this.btnThemSV = new System.Windows.Forms.Button();
            this.btnSuaSV = new System.Windows.Forms.Button();
            this.btnXoaSV = new System.Windows.Forms.Button();
            this.grpDiemSV = new System.Windows.Forms.GroupBox();
            this.dgvDiemMon = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.lblSoTC = new System.Windows.Forms.Label();
            this.nudSoTC = new System.Windows.Forms.NumericUpDown();
            this.lblDiem = new System.Windows.Forms.Label();
            this.nudDiem = new System.Windows.Forms.NumericUpDown();
            this.btnThemMon = new System.Windows.Forms.Button();
            this.btnSuaDiem = new System.Windows.Forms.Button();
            this.btnXoaMon = new System.Windows.Forms.Button();
            this.lblTongTC = new System.Windows.Forms.Label();
            this.lblDiemTB = new System.Windows.Forms.Label();
            this.lblXepLoai = new System.Windows.Forms.Label();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnCaiDatTrang = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.grpThongTinSV.SuspendLayout();
            this.grpDiemSV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Location = new System.Drawing.Point(15, 12);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(30, 19);
            this.lblLop.TabIndex = 0;
            this.lblLop.Text = "Lớp";
            // 
            // lstLop
            // 
            this.lstLop.FormattingEnabled = true;
            this.lstLop.ItemHeight = 19;
            this.lstLop.Location = new System.Drawing.Point(15, 35);
            this.lstLop.Name = "lstLop";
            this.lstLop.Size = new System.Drawing.Size(220, 232);
            this.lstLop.TabIndex = 1;
            this.lstLop.SelectedIndexChanged += new System.EventHandler(this.lstLop_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(245, 35);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm MSSV / họ tên";
            this.txtTimKiem.Size = new System.Drawing.Size(460, 26);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.AllowUserToAddRows = false;
            this.dgvSinhVien.AllowUserToDeleteRows = false;
            this.dgvSinhVien.AllowUserToResizeRows = false;
            this.dgvSinhVien.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMSSV,
            this.colHoTen,
            this.colNgaySinh,
            this.colGioiTinh,
            this.colLop});
            this.dgvSinhVien.Location = new System.Drawing.Point(245, 68);
            this.dgvSinhVien.MultiSelect = false;
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.ReadOnly = true;
            this.dgvSinhVien.RowHeadersWidth = 25;
            this.dgvSinhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSinhVien.Size = new System.Drawing.Size(460, 199);
            this.dgvSinhVien.TabIndex = 3;
            this.dgvSinhVien.SelectionChanged += new System.EventHandler(this.dgvSinhVien_SelectionChanged);
            // 
            // colMSSV
            // 
            this.colMSSV.DataPropertyName = "MaSv";
            this.colMSSV.HeaderText = "MSSV";
            this.colMSSV.Width = 75;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.Width = 135;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.DataPropertyName = "NgaySinh";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.colNgaySinh.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.Width = 90;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.DataPropertyName = "GioiTinh";
            this.colGioiTinh.HeaderText = "Giới tính";
            this.colGioiTinh.Width = 70;
            // 
            // colLop
            // 
            this.colLop.DataPropertyName = "MaLop";
            this.colLop.HeaderText = "Lớp";
            this.colLop.Width = 65;
            // 
            // grpThongTinSV
            // 
            this.grpThongTinSV.Controls.Add(this.lblMaSV);
            this.grpThongTinSV.Controls.Add(this.txtMaSV);
            this.grpThongTinSV.Controls.Add(this.lblHoTen);
            this.grpThongTinSV.Controls.Add(this.txtHoTen);
            this.grpThongTinSV.Controls.Add(this.dtpNgaySinh);
            this.grpThongTinSV.Controls.Add(this.radNam);
            this.grpThongTinSV.Controls.Add(this.radNu);
            this.grpThongTinSV.Controls.Add(this.lblMaLop);
            this.grpThongTinSV.Controls.Add(this.cboLop);
            this.grpThongTinSV.Controls.Add(this.btnThemSV);
            this.grpThongTinSV.Controls.Add(this.btnSuaSV);
            this.grpThongTinSV.Controls.Add(this.btnXoaSV);
            this.grpThongTinSV.Location = new System.Drawing.Point(715, 15);
            this.grpThongTinSV.Name = "grpThongTinSV";
            this.grpThongTinSV.Size = new System.Drawing.Size(305, 252);
            this.grpThongTinSV.TabIndex = 4;
            this.grpThongTinSV.TabStop = false;
            this.grpThongTinSV.Text = "Thông tin sinh viên";
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Location = new System.Drawing.Point(12, 28);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(46, 19);
            this.lblMaSV.TabIndex = 0;
            this.lblMaSV.Text = "MSSV";
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(75, 25);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(215, 26);
            this.txtMaSV.TabIndex = 1;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(12, 63);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(51, 19);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(75, 60);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(215, 26);
            this.txtHoTen.TabIndex = 3;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(75, 95);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(215, 26);
            this.dtpNgaySinh.TabIndex = 4;
            // 
            // radNam
            // 
            this.radNam.AutoSize = true;
            this.radNam.Checked = true;
            this.radNam.Location = new System.Drawing.Point(75, 130);
            this.radNam.Name = "radNam";
            this.radNam.Size = new System.Drawing.Size(56, 23);
            this.radNam.TabIndex = 5;
            this.radNam.TabStop = true;
            this.radNam.Text = "Nam";
            this.radNam.UseVisualStyleBackColor = true;
            // 
            // radNu
            // 
            this.radNu.AutoSize = true;
            this.radNu.Location = new System.Drawing.Point(145, 130);
            this.radNu.Name = "radNu";
            this.radNu.Size = new System.Drawing.Size(46, 23);
            this.radNu.TabIndex = 6;
            this.radNu.Text = "Nữ";
            this.radNu.UseVisualStyleBackColor = true;
            // 
            // lblMaLop
            // 
            this.lblMaLop.AutoSize = true;
            this.lblMaLop.Location = new System.Drawing.Point(12, 165);
            this.lblMaLop.Name = "lblMaLop";
            this.lblMaLop.Size = new System.Drawing.Size(30, 19);
            this.lblMaLop.TabIndex = 7;
            this.lblMaLop.Text = "Lớp";
            // 
            // cboLop
            // 
            this.cboLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLop.FormattingEnabled = true;
            this.cboLop.Location = new System.Drawing.Point(75, 162);
            this.cboLop.Name = "cboLop";
            this.cboLop.Size = new System.Drawing.Size(215, 27);
            this.cboLop.TabIndex = 8;
            // 
            // btnThemSV
            // 
            this.btnThemSV.Location = new System.Drawing.Point(15, 203);
            this.btnThemSV.Name = "btnThemSV";
            this.btnThemSV.Size = new System.Drawing.Size(80, 32);
            this.btnThemSV.TabIndex = 9;
            this.btnThemSV.Text = "Thêm";
            this.btnThemSV.UseVisualStyleBackColor = true;
            this.btnThemSV.Click += new System.EventHandler(this.btnThemSV_Click);
            // 
            // btnSuaSV
            // 
            this.btnSuaSV.Location = new System.Drawing.Point(110, 203);
            this.btnSuaSV.Name = "btnSuaSV";
            this.btnSuaSV.Size = new System.Drawing.Size(80, 32);
            this.btnSuaSV.TabIndex = 10;
            this.btnSuaSV.Text = "Sửa";
            this.btnSuaSV.UseVisualStyleBackColor = true;
            this.btnSuaSV.Click += new System.EventHandler(this.btnSuaSV_Click);
            // 
            // btnXoaSV
            // 
            this.btnXoaSV.Location = new System.Drawing.Point(205, 203);
            this.btnXoaSV.Name = "btnXoaSV";
            this.btnXoaSV.Size = new System.Drawing.Size(80, 32);
            this.btnXoaSV.TabIndex = 11;
            this.btnXoaSV.Text = "Xóa";
            this.btnXoaSV.UseVisualStyleBackColor = true;
            this.btnXoaSV.Click += new System.EventHandler(this.btnXoaSV_Click);
            // 
            // grpDiemSV
            // 
            this.grpDiemSV.Controls.Add(this.dgvDiemMon);
            this.grpDiemSV.Controls.Add(this.lblTenMon);
            this.grpDiemSV.Controls.Add(this.txtTenMon);
            this.grpDiemSV.Controls.Add(this.lblSoTC);
            this.grpDiemSV.Controls.Add(this.nudSoTC);
            this.grpDiemSV.Controls.Add(this.lblDiem);
            this.grpDiemSV.Controls.Add(this.nudDiem);
            this.grpDiemSV.Controls.Add(this.btnThemMon);
            this.grpDiemSV.Controls.Add(this.btnSuaDiem);
            this.grpDiemSV.Controls.Add(this.btnXoaMon);
            this.grpDiemSV.Controls.Add(this.lblTongTC);
            this.grpDiemSV.Controls.Add(this.lblDiemTB);
            this.grpDiemSV.Controls.Add(this.lblXepLoai);
            this.grpDiemSV.Location = new System.Drawing.Point(15, 275);
            this.grpDiemSV.Name = "grpDiemSV";
            this.grpDiemSV.Size = new System.Drawing.Size(1005, 275);
            this.grpDiemSV.TabIndex = 5;
            this.grpDiemSV.TabStop = false;
            this.grpDiemSV.Text = "Điểm của sinh viên";
            // 
            // dgvDiemMon
            // 
            this.dgvDiemMon.AllowUserToAddRows = false;
            this.dgvDiemMon.AllowUserToDeleteRows = false;
            this.dgvDiemMon.AllowUserToResizeRows = false;
            this.dgvDiemMon.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDiemMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiemMon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colTenMon,
            this.colSoTC,
            this.colDiem});
            this.dgvDiemMon.Location = new System.Drawing.Point(15, 28);
            this.dgvDiemMon.MultiSelect = false;
            this.dgvDiemMon.Name = "dgvDiemMon";
            this.dgvDiemMon.ReadOnly = true;
            this.dgvDiemMon.RowHeadersWidth = 25;
            this.dgvDiemMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiemMon.Size = new System.Drawing.Size(460, 230);
            this.dgvDiemMon.TabIndex = 0;
            this.dgvDiemMon.SelectionChanged += new System.EventHandler(this.dgvDiemMon_SelectionChanged);
            // 
            // colSTT
            // 
            this.colSTT.DataPropertyName = "STT";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSTT.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Width = 45;
            // 
            // colTenMon
            // 
            this.colTenMon.DataPropertyName = "TenMon";
            this.colTenMon.HeaderText = "Môn học";
            this.colTenMon.Width = 220;
            // 
            // colSoTC
            // 
            this.colSoTC.DataPropertyName = "SoTinChi";
            this.colSoTC.HeaderText = "Số TC";
            this.colSoTC.Width = 70;
            // 
            // colDiem
            // 
            this.colDiem.DataPropertyName = "DiemDisplay";
            this.colDiem.HeaderText = "Điểm";
            this.colDiem.Width = 75;
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.Location = new System.Drawing.Point(500, 28);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(61, 19);
            this.lblTenMon.TabIndex = 1;
            this.lblTenMon.Text = "Tên môn";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Location = new System.Drawing.Point(500, 52);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(250, 26);
            this.txtTenMon.TabIndex = 2;
            // 
            // lblSoTC
            // 
            this.lblSoTC.AutoSize = true;
            this.lblSoTC.Location = new System.Drawing.Point(765, 28);
            this.lblSoTC.Name = "lblSoTC";
            this.lblSoTC.Size = new System.Drawing.Size(43, 19);
            this.lblSoTC.TabIndex = 3;
            this.lblSoTC.Text = "Số TC";
            // 
            // nudSoTC
            // 
            this.nudSoTC.Location = new System.Drawing.Point(765, 52);
            this.nudSoTC.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudSoTC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoTC.Name = "nudSoTC";
            this.nudSoTC.Size = new System.Drawing.Size(65, 26);
            this.nudSoTC.TabIndex = 4;
            this.nudSoTC.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblDiem
            // 
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new System.Drawing.Point(845, 28);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(41, 19);
            this.lblDiem.TabIndex = 5;
            this.lblDiem.Text = "Điểm";
            // 
            // nudDiem
            // 
            this.nudDiem.DecimalPlaces = 1;
            this.nudDiem.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudDiem.Location = new System.Drawing.Point(845, 52);
            this.nudDiem.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDiem.Name = "nudDiem";
            this.nudDiem.Size = new System.Drawing.Size(75, 26);
            this.nudDiem.TabIndex = 6;
            this.nudDiem.Value = new decimal(new int[] {
            90,
            0,
            0,
            65536});
            // 
            // btnThemMon
            // 
            this.btnThemMon.Location = new System.Drawing.Point(500, 95);
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Size = new System.Drawing.Size(95, 32);
            this.btnThemMon.TabIndex = 7;
            this.btnThemMon.Text = "Thêm môn";
            this.btnThemMon.UseVisualStyleBackColor = true;
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            // 
            // btnSuaDiem
            // 
            this.btnSuaDiem.Location = new System.Drawing.Point(610, 95);
            this.btnSuaDiem.Name = "btnSuaDiem";
            this.btnSuaDiem.Size = new System.Drawing.Size(95, 32);
            this.btnSuaDiem.TabIndex = 8;
            this.btnSuaDiem.Text = "Sửa điểm";
            this.btnSuaDiem.UseVisualStyleBackColor = true;
            this.btnSuaDiem.Click += new System.EventHandler(this.btnSuaDiem_Click);
            // 
            // btnXoaMon
            // 
            this.btnXoaMon.Location = new System.Drawing.Point(720, 95);
            this.btnXoaMon.Name = "btnXoaMon";
            this.btnXoaMon.Size = new System.Drawing.Size(95, 32);
            this.btnXoaMon.TabIndex = 9;
            this.btnXoaMon.Text = "Xóa môn";
            this.btnXoaMon.UseVisualStyleBackColor = true;
            this.btnXoaMon.Click += new System.EventHandler(this.btnXoaMon_Click);
            // 
            // lblTongTC
            // 
            this.lblTongTC.AutoSize = true;
            this.lblTongTC.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTongTC.Location = new System.Drawing.Point(500, 160);
            this.lblTongTC.Name = "lblTongTC";
            this.lblTongTC.Size = new System.Drawing.Size(127, 20);
            this.lblTongTC.TabIndex = 10;
            this.lblTongTC.Text = "Tổng tín chỉ: 11";
            // 
            // lblDiemTB
            // 
            this.lblDiemTB.AutoSize = true;
            this.lblDiemTB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDiemTB.Location = new System.Drawing.Point(660, 160);
            this.lblDiemTB.Name = "lblDiemTB";
            this.lblDiemTB.Size = new System.Drawing.Size(107, 20);
            this.lblDiemTB.TabIndex = 11;
            this.lblDiemTB.Text = "Điểm TB: 7,77";
            // 
            // lblXepLoai
            // 
            this.lblXepLoai.AutoSize = true;
            this.lblXepLoai.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblXepLoai.Location = new System.Drawing.Point(805, 160);
            this.lblXepLoai.Name = "lblXepLoai";
            this.lblXepLoai.Size = new System.Drawing.Size(111, 20);
            this.lblXepLoai.TabIndex = 12;
            this.lblXepLoai.Text = "Xếp loại: Khá";
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.Location = new System.Drawing.Point(15, 560);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(125, 32);
            this.btnXemTruoc.TabIndex = 6;
            this.btnXemTruoc.Text = "Xem trước phiếu";
            this.btnXemTruoc.UseVisualStyleBackColor = true;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(150, 560);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(90, 32);
            this.btnIn.TabIndex = 7;
            this.btnIn.Text = "In...";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnCaiDatTrang
            // 
            this.btnCaiDatTrang.Location = new System.Drawing.Point(250, 560);
            this.btnCaiDatTrang.Name = "btnCaiDatTrang";
            this.btnCaiDatTrang.Size = new System.Drawing.Size(120, 32);
            this.btnCaiDatTrang.TabIndex = 8;
            this.btnCaiDatTrang.Text = "Cài đặt trang...";
            this.btnCaiDatTrang.UseVisualStyleBackColor = true;
            this.btnCaiDatTrang.Click += new System.EventHandler(this.btnCaiDatTrang_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(380, 560);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(160, 32);
            this.btnXuatExcel.TabIndex = 9;
            this.btnXuatExcel.Text = "Xuất Excel điểm lớp";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 606);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnCaiDatTrang);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnXemTruoc);
            this.Controls.Add(this.grpDiemSV);
            this.Controls.Add(this.grpThongTinSV);
            this.Controls.Add(this.dgvSinhVien);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lstLop);
            this.Controls.Add(this.lblLop);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sinh viên và điểm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.grpThongTinSV.ResumeLayout(false);
            this.grpThongTinSV.PerformLayout();
            this.grpDiemSV.ResumeLayout(false);
            this.grpDiemSV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ListBox lstLop;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMSSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLop;
        private System.Windows.Forms.GroupBox grpThongTinSV;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.RadioButton radNam;
        private System.Windows.Forms.RadioButton radNu;
        private System.Windows.Forms.Label lblMaLop;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.Button btnThemSV;
        private System.Windows.Forms.Button btnSuaSV;
        private System.Windows.Forms.Button btnXoaSV;
        private System.Windows.Forms.GroupBox grpDiemSV;
        private System.Windows.Forms.DataGridView dgvDiemMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiem;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label lblSoTC;
        private System.Windows.Forms.NumericUpDown nudSoTC;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.NumericUpDown nudDiem;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnSuaDiem;
        private System.Windows.Forms.Button btnXoaMon;
        private System.Windows.Forms.Label lblTongTC;
        private System.Windows.Forms.Label lblDiemTB;
        private System.Windows.Forms.Label lblXepLoai;
        private System.Windows.Forms.Button btnXemTruoc;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnCaiDatTrang;
        private System.Windows.Forms.Button btnXuatExcel;
    }
}

namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleGiaBan = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleNgayNhap = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleTon = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleMa = new System.Windows.Forms.DataGridViewCellStyle();

            grpThongTin = new GroupBox();
            chkConKinhDoanh = new CheckBox();
            dtpNgayNhap = new DateTimePicker();
            lblNgayNhap = new Label();
            numSoLuongTon = new NumericUpDown();
            lblSoLuongTon = new Label();
            numGiaBan = new NumericUpDown();
            lblGiaBan = new Label();
            cboDonVi = new ComboBox();
            lblDonVi = new Label();
            cboLoaiHang = new ComboBox();
            lblLoaiHang = new Label();
            txtTenHang = new TextBox();
            lblTenHang = new Label();

            grpTimKiem = new GroupBox();
            chkFilterConKinhDoanh = new CheckBox();
            cboFilterLoai = new ComboBox();
            txtSearch = new TextBox();

            dgvHangHoa = new DataGridView();
            colMaHang = new DataGridViewTextBoxColumn();
            colTenHang = new DataGridViewTextBoxColumn();
            colLoaiHang = new DataGridViewTextBoxColumn();
            colDonVi = new DataGridViewTextBoxColumn();
            colGiaBan = new DataGridViewTextBoxColumn();
            colSoLuongTon = new DataGridViewTextBoxColumn();
            colNgayNhap = new DataGridViewTextBoxColumn();
            colConKinhDoanh = new DataGridViewCheckBoxColumn();

            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnXemBangGia = new Button();
            btnXuatPdf = new Button();
            btnXuatExcelLuoi = new Button();
            btnXuatExcelTonKho = new Button();
            lblStatus = new Label();
            timerSearch = new System.Windows.Forms.Timer(components);

            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuongTon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGiaBan).BeginInit();
            grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
            SuspendLayout();

            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(chkConKinhDoanh);
            grpThongTin.Controls.Add(dtpNgayNhap);
            grpThongTin.Controls.Add(lblNgayNhap);
            grpThongTin.Controls.Add(numSoLuongTon);
            grpThongTin.Controls.Add(lblSoLuongTon);
            grpThongTin.Controls.Add(numGiaBan);
            grpThongTin.Controls.Add(lblGiaBan);
            grpThongTin.Controls.Add(cboDonVi);
            grpThongTin.Controls.Add(lblDonVi);
            grpThongTin.Controls.Add(cboLoaiHang);
            grpThongTin.Controls.Add(lblLoaiHang);
            grpThongTin.Controls.Add(txtTenHang);
            grpThongTin.Controls.Add(lblTenHang);
            grpThongTin.Location = new Point(12, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(575, 160);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin hàng hóa";

            // 
            // lblTenHang
            // 
            lblTenHang.AutoSize = true;
            lblTenHang.Location = new Point(15, 27);
            lblTenHang.Name = "lblTenHang";
            lblTenHang.Size = new Size(61, 15);
            lblTenHang.TabIndex = 0;
            lblTenHang.Text = "Tên hàng:";

            // 
            // txtTenHang
            // 
            txtTenHang.Location = new Point(95, 24);
            txtTenHang.Name = "txtTenHang";
            txtTenHang.Size = new Size(465, 23);
            txtTenHang.TabIndex = 1;

            // 
            // lblLoaiHang
            // 
            lblLoaiHang.AutoSize = true;
            lblLoaiHang.Location = new Point(15, 60);
            lblLoaiHang.Name = "lblLoaiHang";
            lblLoaiHang.Size = new Size(62, 15);
            lblLoaiHang.TabIndex = 2;
            lblLoaiHang.Text = "Loại hàng:";

            // 
            // cboLoaiHang
            // 
            cboLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiHang.FormattingEnabled = true;
            cboLoaiHang.Location = new Point(95, 57);
            cboLoaiHang.Name = "cboLoaiHang";
            cboLoaiHang.Size = new Size(180, 23);
            cboLoaiHang.TabIndex = 3;

            // 
            // lblSoLuongTon
            // 
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(295, 60);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Size = new Size(79, 15);
            lblSoLuongTon.TabIndex = 4;
            lblSoLuongTon.Text = "Số lượng tồn:";

            // 
            // numSoLuongTon
            // 
            numSoLuongTon.Location = new Point(380, 58);
            numSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numSoLuongTon.Name = "numSoLuongTon";
            numSoLuongTon.Size = new Size(180, 23);
            numSoLuongTon.TabIndex = 5;

            // 
            // lblDonVi
            // 
            lblDonVi.AutoSize = true;
            lblDonVi.Location = new Point(15, 93);
            lblDonVi.Name = "lblDonVi";
            lblDonVi.Size = new Size(44, 15);
            lblDonVi.TabIndex = 6;
            lblDonVi.Text = "Đơn vị:";

            // 
            // cboDonVi
            // 
            cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDonVi.FormattingEnabled = true;
            cboDonVi.Location = new Point(95, 90);
            cboDonVi.Name = "cboDonVi";
            cboDonVi.Size = new Size(180, 23);
            cboDonVi.TabIndex = 7;

            // 
            // lblNgayNhap
            // 
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(295, 93);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new Size(68, 15);
            lblNgayNhap.TabIndex = 8;
            lblNgayNhap.Text = "Ngày nhập:";

            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.Location = new Point(380, 90);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(180, 23);
            dtpNgayNhap.TabIndex = 9;

            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(15, 126);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(50, 15);
            lblGiaBan.TabIndex = 10;
            lblGiaBan.Text = "Giá bán:";

            // 
            // numGiaBan
            // 
            numGiaBan.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            numGiaBan.Location = new Point(95, 123);
            numGiaBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numGiaBan.Name = "numGiaBan";
            numGiaBan.Size = new Size(180, 23);
            numGiaBan.TabIndex = 11;
            numGiaBan.ThousandsSeparator = true;

            // 
            // chkConKinhDoanh
            // 
            chkConKinhDoanh.AutoSize = true;
            chkConKinhDoanh.Checked = true;
            chkConKinhDoanh.CheckState = CheckState.Checked;
            chkConKinhDoanh.Location = new Point(380, 125);
            chkConKinhDoanh.Name = "chkConKinhDoanh";
            chkConKinhDoanh.Size = new Size(110, 19);
            chkConKinhDoanh.TabIndex = 12;
            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.UseVisualStyleBackColor = true;

            // 
            // grpTimKiem
            // 
            grpTimKiem.Controls.Add(chkFilterConKinhDoanh);
            grpTimKiem.Controls.Add(cboFilterLoai);
            grpTimKiem.Controls.Add(txtSearch);
            grpTimKiem.Location = new Point(600, 12);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Size = new Size(356, 160);
            grpTimKiem.TabIndex = 1;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = "Tìm kiếm";

            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(15, 24);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên hàng để tìm...";
            txtSearch.Size = new Size(325, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // 
            // cboFilterLoai
            // 
            cboFilterLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterLoai.FormattingEnabled = true;
            cboFilterLoai.Location = new Point(15, 57);
            cboFilterLoai.Name = "cboFilterLoai";
            cboFilterLoai.Size = new Size(325, 23);
            cboFilterLoai.TabIndex = 1;
            cboFilterLoai.SelectedIndexChanged += cboFilterLoai_SelectedIndexChanged;

            // 
            // chkFilterConKinhDoanh
            // 
            chkFilterConKinhDoanh.AutoSize = true;
            chkFilterConKinhDoanh.Location = new Point(15, 93);
            chkFilterConKinhDoanh.Name = "chkFilterConKinhDoanh";
            chkFilterConKinhDoanh.Size = new Size(185, 19);
            chkFilterConKinhDoanh.TabIndex = 2;
            chkFilterConKinhDoanh.Text = "Chỉ hiện hàng còn kinh doanh";
            chkFilterConKinhDoanh.UseVisualStyleBackColor = true;
            chkFilterConKinhDoanh.CheckedChanged += chkFilterConKinhDoanh_CheckedChanged;

            // 
            // dgvHangHoa
            // 
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.AllowUserToDeleteRows = false;
            dgvHangHoa.BackgroundColor = SystemColors.ControlDark;
            dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] {
                colMaHang,
                colTenHang,
                colLoaiHang,
                colDonVi,
                colGiaBan,
                colSoLuongTon,
                colNgayNhap,
                colConKinhDoanh
            });
            dgvHangHoa.Location = new Point(12, 185);
            dgvHangHoa.MultiSelect = false;
            dgvHangHoa.Name = "dgvHangHoa";
            dgvHangHoa.ReadOnly = true;
            dgvHangHoa.RowHeadersWidth = 35;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.Size = new Size(944, 255);
            dgvHangHoa.TabIndex = 2;
            dgvHangHoa.CellClick += dgvHangHoa_CellClick;
            dgvHangHoa.SelectionChanged += dgvHangHoa_SelectionChanged;

            // 
            // colMaHang
            // 
            colMaHang.DataPropertyName = "MaHang";
            dgvCellStyleMa.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHang.DefaultCellStyle = dgvCellStyleMa;
            colMaHang.HeaderText = "Mã";
            colMaHang.Name = "colMaHang";
            colMaHang.ReadOnly = true;
            colMaHang.Width = 60;

            // 
            // colTenHang
            // 
            colTenHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenHang.DataPropertyName = "TenHang";
            colTenHang.HeaderText = "Tên hàng";
            colTenHang.MinimumWidth = 180;
            colTenHang.Name = "colTenHang";
            colTenHang.ReadOnly = true;

            // 
            // colLoaiHang
            // 
            colLoaiHang.DataPropertyName = "LoaiHang";
            colLoaiHang.HeaderText = "Loại hàng";
            colLoaiHang.Name = "colLoaiHang";
            colLoaiHang.ReadOnly = true;
            colLoaiHang.Width = 110;

            // 
            // colDonVi
            // 
            colDonVi.DataPropertyName = "DonVi";
            colDonVi.HeaderText = "Đơn vị";
            colDonVi.Name = "colDonVi";
            colDonVi.ReadOnly = true;
            colDonVi.Width = 80;

            // 
            // colGiaBan
            // 
            colGiaBan.DataPropertyName = "GiaBan";
            dgvCellStyleGiaBan.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCellStyleGiaBan.Format = "N0";
            colGiaBan.DefaultCellStyle = dgvCellStyleGiaBan;
            colGiaBan.HeaderText = "Giá bán";
            colGiaBan.Name = "colGiaBan";
            colGiaBan.ReadOnly = true;
            colGiaBan.Width = 110;

            // 
            // colSoLuongTon
            // 
            colSoLuongTon.DataPropertyName = "SoLuongTon";
            dgvCellStyleTon.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCellStyleTon.Format = "N0";
            colSoLuongTon.DefaultCellStyle = dgvCellStyleTon;
            colSoLuongTon.HeaderText = "Tồn";
            colSoLuongTon.Name = "colSoLuongTon";
            colSoLuongTon.ReadOnly = true;
            colSoLuongTon.Width = 70;

            // 
            // colNgayNhap
            // 
            colNgayNhap.DataPropertyName = "NgayNhap";
            dgvCellStyleNgayNhap.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCellStyleNgayNhap.Format = "dd/MM/yyyy";
            colNgayNhap.DefaultCellStyle = dgvCellStyleNgayNhap;
            colNgayNhap.HeaderText = "Ngày nhập";
            colNgayNhap.Name = "colNgayNhap";
            colNgayNhap.ReadOnly = true;
            colNgayNhap.Width = 105;

            // 
            // colConKinhDoanh
            // 
            colConKinhDoanh.DataPropertyName = "ConKinhDoanh";
            colConKinhDoanh.HeaderText = "Kinh doanh";
            colConKinhDoanh.Name = "colConKinhDoanh";
            colConKinhDoanh.ReadOnly = true;
            colConKinhDoanh.Resizable = DataGridViewTriState.True;
            colConKinhDoanh.SortMode = DataGridViewColumnSortMode.Automatic;
            colConKinhDoanh.Width = 90;

            // 
            // btnThem
            // 
            btnThem.Location = new Point(12, 452);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(88, 32);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;

            // 
            // btnSua
            // 
            btnSua.Location = new Point(106, 452);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(88, 32);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;

            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(200, 452);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(88, 32);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;

            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(294, 452);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(88, 32);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;

            // 
            // btnXemBangGia
            // 
            btnXemBangGia.Location = new Point(445, 452);
            btnXemBangGia.Name = "btnXemBangGia";
            btnXemBangGia.Size = new Size(110, 32);
            btnXemBangGia.TabIndex = 7;
            btnXemBangGia.Text = "Xem bảng giá";
            btnXemBangGia.UseVisualStyleBackColor = true;
            btnXemBangGia.Click += btnXemBangGia_Click;

            // 
            // btnXuatPdf
            // 
            btnXuatPdf.Location = new Point(561, 452);
            btnXuatPdf.Name = "btnXuatPdf";
            btnXuatPdf.Size = new Size(95, 32);
            btnXuatPdf.TabIndex = 8;
            btnXuatPdf.Text = "Xuất PDF";
            btnXuatPdf.UseVisualStyleBackColor = true;
            btnXuatPdf.Click += btnXuatPdf_Click;

            // 
            // btnXuatExcelLuoi
            // 
            btnXuatExcelLuoi.Location = new Point(662, 452);
            btnXuatExcelLuoi.Name = "btnXuatExcelLuoi";
            btnXuatExcelLuoi.Size = new Size(130, 32);
            btnXuatExcelLuoi.TabIndex = 9;
            btnXuatExcelLuoi.Text = "Xuất Excel (lưới)";
            btnXuatExcelLuoi.UseVisualStyleBackColor = true;
            btnXuatExcelLuoi.Click += btnXuatExcelLuoi_Click;

            // 
            // btnXuatExcelTonKho
            // 
            btnXuatExcelTonKho.Location = new Point(798, 452);
            btnXuatExcelTonKho.Name = "btnXuatExcelTonKho";
            btnXuatExcelTonKho.Size = new Size(158, 32);
            btnXuatExcelTonKho.TabIndex = 10;
            btnXuatExcelTonKho.Text = "Xuất Excel tồn kho";
            btnXuatExcelTonKho.UseVisualStyleBackColor = true;
            btnXuatExcelTonKho.Click += btnXuatExcelTonKho_Click;

            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9.5F);
            lblStatus.Location = new Point(12, 495);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(136, 17);
            lblStatus.TabIndex = 11;
            lblStatus.Text = "Tìm thấy 0 mặt hàng";

            // 
            // timerSearch
            // 
            timerSearch.Interval = 350;
            timerSearch.Tick += timerSearch_Tick;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 524);
            Controls.Add(lblStatus);
            Controls.Add(btnXuatExcelTonKho);
            Controls.Add(btnXuatExcelLuoi);
            Controls.Add(btnXuatPdf);
            Controls.Add(btnXemBangGia);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvHangHoa);
            Controls.Add(grpTimKiem);
            Controls.Add(grpThongTin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hàng hóa - Tạp hóa UTC2";
            Load += Form1_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoLuongTon).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGiaBan).EndInit();
            grpTimKiem.ResumeLayout(false);
            grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenHang;
        private TextBox txtTenHang;
        private Label lblLoaiHang;
        private ComboBox cboLoaiHang;
        private Label lblSoLuongTon;
        private NumericUpDown numSoLuongTon;
        private Label lblDonVi;
        private ComboBox cboDonVi;
        private Label lblNgayNhap;
        private DateTimePicker dtpNgayNhap;
        private Label lblGiaBan;
        private NumericUpDown numGiaBan;
        private CheckBox chkConKinhDoanh;

        private GroupBox grpTimKiem;
        private TextBox txtSearch;
        private ComboBox cboFilterLoai;
        private CheckBox chkFilterConKinhDoanh;

        private DataGridView dgvHangHoa;
        private DataGridViewTextBoxColumn colMaHang;
        private DataGridViewTextBoxColumn colTenHang;
        private DataGridViewTextBoxColumn colLoaiHang;
        private DataGridViewTextBoxColumn colDonVi;
        private DataGridViewTextBoxColumn colGiaBan;
        private DataGridViewTextBoxColumn colSoLuongTon;
        private DataGridViewTextBoxColumn colNgayNhap;
        private DataGridViewCheckBoxColumn colConKinhDoanh;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnXemBangGia;
        private Button btnXuatPdf;
        private Button btnXuatExcelLuoi;
        private Button btnXuatExcelTonKho;

        private Label lblStatus;
        private System.Windows.Forms.Timer timerSearch;
    }
}
